using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Classes;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.IO;

public class NetBitReader : BitReader
{
    public GuidCache GuidCache { get; set; }

    public NetBitReader(ReadOnlyMemory<byte> buffer, int bitSize, GuidCache guidCache,
        FReader? existingReader = null) : base(buffer,
        bitSize, existingReader)
    {
        GuidCache = guidCache;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T ReadCustomClass<T>() where T : CustomClass, new()
    {
        var result = new T();
        result.Serialize(this);
        result.Resolve(GuidCache);
        if (IsError)
        {
            Console.WriteLine("ReadCustomClass: ERRRRROR");
        }

        return result;
    }

    private static int errorCount = 0;

    public FRepMovement ReadFRepMovement(int locationQuatLevel, int rotationQuatLevel, int velocityQuatLevel)
    {
        var result = new FRepMovement();
        result.Serialize(this, locationQuatLevel, rotationQuatLevel, velocityQuatLevel);
        result.Resolve(GuidCache);
        if (IsError)
        {
            Console.WriteLine("ReadFRepMovement: ERRRRROR " + errorCount++);
        }

        return result;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] ReadDynamicArray<T>(Func<T> itemFunc)
    {
        var count = ReadIntPacked();
        var arr = new T[count];
        while (true)
        {
            var index = ReadIntPacked();

            if (index == 0)
            {
                break;
            }

            index--;

            if (index > count)
            {
                return arr;
            }

            while (true)
            {
                var handle = ReadIntPacked();
                if (handle == 0)
                {
                    break;
                }

                handle--;
                var numBits = ReadIntPacked();
                if (numBits == 0)
                {
                    continue;
                }

                var arrItem = itemFunc();
                arr[index] = arrItem;
            }
        }

        return arr;
    }

    public T[] ReadDynamicArray<T>(UnrealReplay replay, NetFieldExportGroup group,
        GroupConfiguration childConfig
    ) where T : ExportModel
    {
        var count = ReadIntPacked();
        var arr = new T[count];
        while (true)
        {
            var index = ReadIntPacked();

            if (index == 0)
            {
                break;
            }

            index--;

            if (index > count)
            {
                return arr;
            }

            var itemInstance = Activator.CreateInstance<T>();

            while (true)
            {
                var handle = ReadIntPacked();
                if (handle == 0)
                {
                    break;
                }

                handle--;
                var field = group?.NetFieldExports.GetValueOrDefault(handle, null);
                var numbits = ReadIntPacked();

                if (field == null)
                {
                    SkipBits((int)numbits);
                    continue;
                }

                if (replay.Settings.IsDebug)
                {
                    replay.ExportGroupDict[group.PathName][field.Name].Add(numbits);
                }

                if (numbits == 0)
                {
                    continue;
                }

                var configuration = childConfig != null
                    ? childConfig.Fields.GetValueOrDefault(field.Name)
                    : field.Configuration;

                if (configuration == null)
                {
                    SkipBits((int)numbits);
                    continue;
                }

                try
                {
                    SetTempEnd((int)numbits, FBitArchiveEndIndex.ReadDynamicArray);
                    var preReadIsError = IsError;
                    configuration?.ReadGeneric(replay, itemInstance, this, group, field);
                    if (!preReadIsError && IsError)
                    {
                        replay.Settings.Logger?.LogWarning("Failed to Read Property Group: {Group} Field: {Field}",
                            group?.Name,
                            field?.Name ?? field.Handle.ToString());
                    }
                }
                catch (Exception e)
                {
                    replay.Settings.Logger?.LogError(e, "Failed to ReceiveProperties Group: {Group} Field: {Field}",
                        group?.Name,
                        field?.Name ?? field.Handle.ToString());
                }
                finally
                {
                    RestoreTempEnd(FBitArchiveEndIndex.ReadDynamicArray);
                }
            }

            arr[index] = itemInstance;
        }

        return arr;
    }
}