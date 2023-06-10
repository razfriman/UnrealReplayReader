using Microsoft.Extensions.Logging;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    public (NetFieldExportGroup group, ExportModel? instance, List<string> changedProperties)? ReceiveProperties(
        NetBitReader reader,
        NetFieldExportGroup group,
        Bunch bunch,
        string staticActorId,
        bool enableProperyChecksum = true,
        bool exportDirectly = true
    )
    {
        if (enableProperyChecksum)
        {
            reader.SkipBits(1);
        }

        var instance = group?.Configuration?.CreateInstance();
        var changedProperties = new List<string>(10);
        while (true)
        {
            var handle = reader.ReadIntPacked();
            if (handle == 0)
            {
                break;
            }

            handle--;
            var field = group?.NetFieldExports.GetValueOrDefault(handle, null);
            var numbits = reader.ReadIntPacked();

            if (field == null)
            {
                reader.SkipBits((int)numbits);
                continue;
            }

            if (Settings.IsDebug)
            {
                Replay.ExportGroupDict[group.PathName]?[field.Name]?.Add(numbits);
            }

            changedProperties.Add(field.Name);

            if (numbits == 0)
            {
                continue;
            }

            if (field.Configuration == null)
            {
                reader.SkipBits((int)numbits);
                continue;
            }

            try
            {
                reader.SetTempEnd((int)numbits, FBitArchiveEndIndex.ReadArrayField);
                var key = "";

                if (group.Configuration.StoreAsHandle || field.Configuration.StoreAsHandle)
                {
                    key = field.Handle.ToString();
                }
                else
                {
                    key = field.Name;
                }

                var preReadIsError = reader.IsError;
                changedProperties.Add(key);
                field.Configuration?.ReadGeneric(Replay, instance, reader, group, field);
                if (!preReadIsError && reader.IsError)
                {
                    Logger?.LogWarning("Failed to Read Property Group: {Group} Field: {Field}", group?.Name,
                        field?.Name ?? field.Handle.ToString());
                }
            }
            catch (Exception e)
            {
                Logger?.LogError(e, "Failed to ReceiveProperties Group: {Group} Field: {Field}", group?.Name,
                    field?.Name ?? field.Handle.ToString());
            }
            finally
            {
                reader.RestoreTempEnd(FBitArchiveEndIndex.ReadArrayField);
            }
        }

        ExternalData? externalData = null;
        NetFieldExport? externalDataField = null;
        if (exportDirectly)
        {
            var actor = bunch.Actor;

            if (ExternalDatas.ContainsKey(actor.ActorNetGuid.Value))
            {
                externalData = ExternalDatas[actor.ActorNetGuid.Value];
                ExternalDatas.Remove(actor.ActorNetGuid.Value);
                externalDataField = group?.NetFieldExports.GetValueOrDefault(externalData.Value.Handle, null);

                if (externalDataField != null)
                {
                    var bitSize = externalData.Value.Payload.Length * 8;
                    var payloadReader = new NetBitReader(externalData?.Payload, bitSize,
                        GuidCache, reader);
                    if (Settings.IsDebug)
                    {
                        Replay.ExportGroupDict[group.PathName][externalDataField.Name].Add((uint)bitSize);
                    }

                    externalDataField.Configuration.ReadGeneric(Replay, instance, payloadReader, group,
                        externalDataField);
                }
            }

            try
            {
                if (instance != null)
                {
                    EmitProperties(instance, group, bunch, changedProperties, externalData, externalDataField,
                        staticActorId);
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e,
                    $"Error while exporting propertyExport '{group.Configuration.ModelName}': {e.StackTrace}");
            }
        }

        return (group, instance, changedProperties);
    }
}