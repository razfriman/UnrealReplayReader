using System.Buffers;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Text;
using UnrealReplayReader.Exceptions;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.IO;

public abstract class FReader
{
    public EEngineNetworkVersionHistory EngineNetworkVersion { get; set; }
    public EReplayHeaderFlags ReplayHeaderFlags { get; set; }
    public ENetworkVersionHistory NetworkVersion { get; set; }
    public ELocalFileVersionHistory FileVersion { get; set; }
    public FEngineVersion NetworkReplayVersion { get; set; }
    public int Position;
    public int EndPosition;
    public int Available => EndPosition - Position;
    public bool AtEnd => Position >= EndPosition;
    public bool IsError;

    public bool HasDeltaCheckpoints => ReplayHeaderFlags.HasFlag(EReplayHeaderFlags.DeltaCheckpoints);
    public bool HasLevelStreamingFixes => ReplayHeaderFlags.HasFlag(EReplayHeaderFlags.HasStreamingFixes);
    public bool HasGameSpecificFrameData => ReplayHeaderFlags.HasFlag(EReplayHeaderFlags.GameSpecificFrameData);

    public ReadOnlyMemory<byte> Buffer;

    public FReader(ReadOnlyMemory<byte> buffer, FReader? existingReader = null)
    {
        Buffer = buffer;
        Position = 0;
        EndPosition = buffer.Length;
        if (existingReader != null)
        {
            FileVersion = existingReader.FileVersion;
            NetworkVersion = existingReader.NetworkVersion;
            EngineNetworkVersion = existingReader.EngineNetworkVersion;
            NetworkReplayVersion = existingReader.NetworkReplayVersion;
            ReplayHeaderFlags = existingReader.ReplayHeaderFlags;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract bool CanReadBytes(int count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void ReadBytes(Span<byte> data, int? dataCount = null);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract sbyte ReadSByte();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract byte ReadByte();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual short ReadInt16()
    {
        Span<byte> value = stackalloc byte[2];
        ReadBytes(value);
        return BinaryPrimitives.ReadInt16LittleEndian(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual ushort ReadUInt16()
    {
        Span<byte> value = stackalloc byte[2];
        ReadBytes(value);
        return BinaryPrimitives.ReadUInt16LittleEndian(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual int ReadInt32()
    {
        Span<byte> value = stackalloc byte[4];
        ReadBytes(value);
        return BinaryPrimitives.ReadInt32LittleEndian(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual uint ReadUInt32()
    {
        Span<byte> value = stackalloc byte[4];
        ReadBytes(value);
        return BinaryPrimitives.ReadUInt32LittleEndian(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual long ReadInt64()
    {
        Span<byte> value = stackalloc byte[8];
        ReadBytes(value);
        return BinaryPrimitives.ReadInt64LittleEndian(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual ulong ReadUInt64()
    {
        Span<byte> value = stackalloc byte[8];
        ReadBytes(value);
        return BinaryPrimitives.ReadUInt64LittleEndian(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual float ReadSingle()
    {
        Span<byte> value = stackalloc byte[4];
        ReadBytes(value);
        return BinaryPrimitives.ReadSingleLittleEndian(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual double ReadDouble()
    {
        Span<byte> value = stackalloc byte[8];
        ReadBytes(value);
        return BinaryPrimitives.ReadDoubleLittleEndian(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual bool ReadIsHardcoded() => ReadByte() != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual string ReadFName()
    {
        var isHardcoded = ReadIsHardcoded();

        if (isHardcoded)
        {
            var nameIndex = EngineNetworkVersion < EEngineNetworkVersionHistory.HistoryChannelNames
                ? ReadUInt32()
                : ReadIntPacked();
            return UnrealNameConstants.Names[nameIndex];
        }

        var inString = ReadFString();
        var inNumber = ReadInt32();
        return inString;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T ReadByteAsEnum<T>() where T : struct, IConvertible => (T)Enum.ToObject(typeof(T), ReadByte());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T ReadUInt32AsEnum<T>() where T : struct, IConvertible => (T)Enum.ToObject(typeof(T), ReadUInt32());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ReadUInt32AsBoolean() => ReadUInt32() == 1u;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DateTime ReadDate() => DateTime.FromBinary(ReadInt64()).ToUniversalTime();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadGuid(byte bytes = 16) => ReadBytesToString(bytes).ToLower();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadBytesToString(int count)
    {
        if (count < 1024)
        {
            Span<byte> buffer = stackalloc byte[count];
            ReadBytes(buffer);
            return Convert.ToHexString(buffer);
        }
        else
        {
            var buffer = ArrayPool<byte>.Shared.Rent(count);
            try
            {
                ReadBytes(buffer);
                return Convert.ToHexString(buffer);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadFString()
    {
        var length = ReadInt32();

        if (length == 0)
        {
            return string.Empty;
        }

        var isUnicode = length < 0;
        var encoding = isUnicode ? Encoding.Unicode : Encoding.ASCII;
        if (isUnicode)
        {
            length *= -2;
        }

        if (length < 0 || length > 1024)
        {
            throw new ReplayException("Bad String Length");
        }

        Span<byte> buffer = stackalloc byte[length];
        ReadBytes(buffer);
        return encoding.GetString(buffer).Trim(' ', '\0');
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte[] ReadBytes(int count)
    {
        if (count == 0)
        {
            return Array.Empty<byte>();
        }

        var result = new byte[count];
        ReadBytes(result);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Seek(long offset, SeekOrigin origin = SeekOrigin.Begin)
    {
        switch (origin)
        {
            case SeekOrigin.Begin:
                Position = (int)offset;
                break;
            case SeekOrigin.Current:
                Position += (int)offset;
                break;
            case SeekOrigin.End:
                Position = EndPosition - (int)offset;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(origin), origin, null);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public List<T> ReadList<T>(Func<FReader, T> func)
    {
        var count = ReadInt32();
        var result = new List<T>(count);
        for (var i = 0; i < count; i++)
        {
            result.Add(func(this));
        }

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] ReadArray<T>(Func<FReader, T> func)
    {
        var count = ReadInt32();
        var result = new T[count];
        for (var i = 0; i < count; i++)
        {
            result[i] = func(this);
        }

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FVector ReadFVector()
    {
        if (EngineNetworkVersion < EEngineNetworkVersionHistory.HistoryPackedVectorLwcSupport)
        {
            return new FVector
            {
                X = ReadSingle(),
                Y = ReadSingle(),
                Z = ReadSingle(),
            };
        }

        return new FVector
        {
            X = ReadDouble(),
            Y = ReadDouble(),
            Z = ReadDouble(),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FVector4D ReadFVector4d()
    {
        if (EngineNetworkVersion < EEngineNetworkVersionHistory.HistoryPackedVectorLwcSupport)
        {
            return new FVector4D
            {
                X = ReadSingle(),
                Y = ReadSingle(),
                Z = ReadSingle(),
                W = ReadSingle(),
            };
        }

        return new FVector4D
        {
            X = ReadDouble(),
            Y = ReadDouble(),
            Z = ReadDouble(),
            W = ReadDouble(),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FTransform ReadFTransform()
    {
        return new FTransform
        {
            Rotation = ReadFQuat(),
            Translation = ReadFVector(),
            Scale3D = ReadFVector(),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FQuat ReadFQuat()
    {
        var fVector4d = ReadFVector4d();
        return new FQuat
        {
            X = fVector4d.X,
            Y = fVector4d.Y,
            Z = fVector4d.Z,
            W = fVector4d.W,
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract void SkipBytes(int count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual uint ReadIntPacked()
    {
        uint value = 0;
        byte count = 0;
        var remaining = true;

        while (remaining)
        {
            var nextByte = ReadByte();
            remaining = (nextByte & 1) == 1; // Check 1 bit to see if theres more after this
            nextByte >>= 1; // Shift to get actual 7 bit value
            value += (uint)nextByte << (7 * count++); // Add to total value
        }

        return value;
    }
}