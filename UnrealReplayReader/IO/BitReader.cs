using System.Runtime.CompilerServices;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.IO;

public class BitReader : FReader
{
    private readonly Dictionary<FBitArchiveEndIndex, int> _tempLastBit = new();

    protected BitReader(ReadOnlyMemory<byte> buffer, int bitSize, FReader? existingReader = null) : base(buffer,
        existingReader)
    {
        EndPosition = bitSize;
    }


    public int PositionByte => Position >> 3;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool CanReadBits(int count) => count >= 0 && Available >= count;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool CanReadBytes(int count) => CanReadBits(count * 8);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool PeekBit() => (Buffer.Span[PositionByte] & (1 << (Position & 7))) > 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ReadBit()
    {
        if (AtEnd || IsError)
        {
            IsError = true;
            return false;
        }

        var result = (Buffer.Span[PositionByte] & (1 << (Position & 7))) > 0;
        Position++;
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void ReadBytes(Span<byte> data, int? dataCount = null)
    {
        var byteCount = dataCount ?? data.Length;
        if (!CanReadBytes(byteCount))
        {
            IsError = true;
            return;
        }

        var bitCountUsedInByte = Position & 7;
        var bitCountLeftInByte = 8 - (Position & 7);
        if (bitCountUsedInByte == 0)
        {
            Buffer.Slice(PositionByte, byteCount).Span.CopyTo(data);
        }
        else
        {
            var bufferSpan = Buffer.Span;
            var positionByte = PositionByte;
            for (var i = 0; i < byteCount; i++)
            {
                data[i] = (byte)((bufferSpan[positionByte + i] >> bitCountUsedInByte) |
                                 ((bufferSpan[positionByte + 1 + i] & ((1 << bitCountUsedInByte) - 1)) <<
                                  bitCountLeftInByte));
            }
        }

        Position += byteCount * 8;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sbyte ReadSByte() => (sbyte)ReadByte();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override byte ReadByte()
    {
        if (!CanReadBytes(1))
        {
            IsError = true;
            return 0;
        }

        var bitCountUsedInByte = Position & 7;
        var bitCountLeftInByte = 8 - (Position & 7);

        var result = (bitCountUsedInByte == 0)
            ? Buffer.Span[PositionByte]
            : (byte)((Buffer.Span[PositionByte] >> bitCountUsedInByte) |
                     ((Buffer.Span[PositionByte + 1] & ((1 << bitCountUsedInByte) - 1)) << bitCountLeftInByte));

        Position += 8;
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ReadIsHardcoded() => ReadBit();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void SkipBytes(int count) => SkipBits(count * 8);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SkipBits(int count)
    {
        if (!CanReadBits(count))
        {
            IsError = true;
            return;
        }

        Position += count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint ReadSerializedInt(int maxValue)
    {
        uint value = 0;
        for (uint mask = 1; (value + mask) < maxValue; mask *= 2)
        {
            if (ReadBit())
            {
                value |= mask;
            }
        }

        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadNetId()
    {
        if (Available == 32)
        {
            SkipBits(32);
            return string.Empty;
        }

        // Use highest value for type for other (out of engine) oss type 
        const byte typeHashOther = 31;

        var encodingFlags = ReadByteAsEnum<UniqueIdEncodingFlags>();
        var encoded = false;
        if ((encodingFlags & UniqueIdEncodingFlags.IsEncoded) == UniqueIdEncodingFlags.IsEncoded)
        {
            encoded = true;
            if ((encodingFlags & UniqueIdEncodingFlags.IsEmpty) == UniqueIdEncodingFlags.IsEmpty)
            {
                // empty cleared out unique id
                return string.Empty;
            }
        }

        // Non empty and hex encoded
        var typeHash = (int)(encodingFlags & UniqueIdEncodingFlags.TypeMask) >> 3;
        if (typeHash == 0)
        {
            // If no type was encoded, assume default
            //TypeHash = UOnlineEngineInterface::Get()->GetReplicationHashForSubsystem(UOnlineEngineInterface::Get()->GetDefaultOnlineSubsystemName());
            return "NULL";
        }

        var bValidTypeHash = typeHash != 0;
        if (typeHash == typeHashOther)
        {
            var typeString = ReadFString();
            if (typeString == UnrealNameConstants.Names[(int)UnrealNames.None])
            {
                bValidTypeHash = false;
            }
        }

        if (bValidTypeHash)
        {
            if (encoded)
            {
                var encodedSize = ReadByte();
                return ReadBytesToString(encodedSize);
            }
            else
            {
                return ReadFString();
            }
        }

        return string.Empty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlyMemory<byte> ReadBitsAsMemory(int bitCount)
    {
        if (!CanReadBits(bitCount) || bitCount < 0)
        {
            IsError = true;
            return ReadOnlyMemory<byte>.Empty;
        }

        var bitCountUsedInByte = Position & 7;
        var byteCount = bitCount / 8;
        var extraBits = bitCount % 8;
        if (bitCountUsedInByte == 0 && extraBits == 0)
        {
            var byteResult = Buffer.Slice(PositionByte, byteCount);
            Position += bitCount;
            return byteResult;
        }


        var result = new byte[(bitCount + 7) / 8];

        var bitCountLeftInByte = 8 - (Position & 7);
        var currentByte = PositionByte;
        var span = Buffer.Span;
        var shiftDelta = (1 << bitCountUsedInByte) - 1;
        for (var i = 0; i < byteCount; i++)
        {
            result[i] = (byte)(
                (span[currentByte + i] >> bitCountUsedInByte) |
                ((span[currentByte + i + 1] & shiftDelta) << bitCountLeftInByte)
            );
        }

        Position += (byteCount * 8);

        bitCount %= 8;
        for (var i = 0; i < bitCount; i++)
        {
            var bit = (Buffer.Span[PositionByte] & (1 << (Position & 7))) > 0;
            Position++;
            if (bit)
            {
                result[^1] |= (byte)(1 << i);
            }
        }

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint ReadBitsToUInt32(int bitCount)
    {
        var result = new byte();
        for (var i = 0; i < bitCount; i++)
        {
            if (IsError)
            {
                return 0;
            }

            if (ReadBit())
            {
                result |= (byte)(1 << i);
            }
        }

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T ReadBitsToUInt32AsEnum<T>(int? bitCount = null) where T : struct, IConvertible =>
        (T)Enum.ToObject(typeof(T), ReadBitsToUInt32(bitCount ?? Available));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong ReadBitsToLong(int bitCount)
    {
        var result = new ulong();
        for (var i = 0; i < bitCount; i++)
        {
            if (ReadBit())
            {
                result |= (1UL << i);
            }
        }

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FVector ReadPackedVector(int quantizationLevel) =>
        quantizationLevel switch
        {
            1 => ReadPackedVector(100, 30),
            2 => ReadPackedVector(10, 27),
            _ => ReadPackedVector(1, 24)
        };


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FVector ReadPackedVector(int scaleFactor, int maxBits)
    {
        if (EngineNetworkVersion >= EEngineNetworkVersionHistory.HistoryPackedVectorLwcSupport)
        {
            return ReadQuantizedVector(scaleFactor);
        }

        return ReadPackedVectorLegacy(scaleFactor, maxBits);
    }

    public UInt32 ReadUInt32Max(Int32 maxValue)
    {
        var maxBits = Math.Floor(Math.Log10(maxValue) / Math.Log10(2)) + 1;

        UInt32 value = 0;
        for (var i = 0; i < maxBits && (value + (1 << i)) < maxValue; ++i)
        {
            value += (ReadBit() ? 1U : 0U) << i;
        }

        if (value > maxValue)
        {
            throw new Exception("ReadUInt32Max overflowed!");
        }

        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FVector ReadQuantizedVector(int scaleFactor)
    {
        var componentBitCountAndExtraInfo = ReadUInt32Max(1 << 7);
        var componentBitCount = (int)(componentBitCountAndExtraInfo & 63U);
        var extraInfo = componentBitCountAndExtraInfo >> 6;

        if (componentBitCount > 0U)
        {
            var x = ReadBitsToLong(componentBitCount);
            var y = ReadBitsToLong(componentBitCount);
            var z = ReadBitsToLong(componentBitCount);

            var signBit = 1UL << componentBitCount - 1;

            double fX = (long)(x ^ signBit) - (long)signBit;
            double fY = (long)(y ^ signBit) - (long)signBit;
            double fZ = (long)(z ^ signBit) - (long)signBit;

            if (extraInfo > 0)
            {
                fX /= scaleFactor;
                fY /= scaleFactor;
                fZ /= scaleFactor;
            }

            return new FVector
            {
                X = fX,
                Y = fY,
                Z = fZ
            };
        }
        else if (extraInfo == 0)
        {
            double x = ReadSingle();
            double y = ReadSingle();
            double z = ReadSingle();

            return new FVector
            {
                X = x,
                Y = y,
                Z = z
            };
        }
        else
        {
            var x = ReadDouble();
            var y = ReadDouble();
            var z = ReadDouble();

            return new FVector
            {
                X = x,
                Y = y,
                Z = z
            };
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FVector ReadPackedVectorLegacy(int scaleFactor, int maxBits)
    {
        var bits = ReadSerializedInt(maxBits);

        if (IsError)
        {
            return new FVector();
        }

        var bias = 1 << ((int)bits + 1);
        var max = 1 << ((int)bits + 2);

        var dx = ReadSerializedInt(max);
        var dy = ReadSerializedInt(max);
        var dz = ReadSerializedInt(max);

        if (IsError)
        {
            return new FVector();
        }

        var x = (dx - bias) / scaleFactor;
        var y = (dy - bias) / scaleFactor;
        var z = (dz - bias) / scaleFactor;

        return new FVector
        {
            X = x,
            Y = y,
            Z = z
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<byte> ReadBitsAsSpan(int bitCount) => ReadBitsAsMemory(bitCount).Span;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FRotator ReadRotation()
    {
        float pitch = 0;
        float yaw = 0;
        float roll = 0;

        if (ReadBit()) // Pitch
        {
            pitch = ReadByte() * 360 / 256;
        }

        if (ReadBit())
        {
            yaw = ReadByte() * 360 / 256;
        }

        if (ReadBit())
        {
            roll = ReadByte() * 360 / 256;
        }

        if (IsError)
        {
            return new FRotator();
        }

        return new FRotator
        {
            Pitch = pitch,
            Yaw = yaw,
            Roll = roll
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FRotator ReadRotationShort()
    {
        float pitch = 0;
        float yaw = 0;
        float roll = 0;

        if (ReadBit())
        {
            pitch = ReadUInt16() * 360 / 65536;
        }

        if (ReadBit())
        {
            yaw = ReadUInt16() * 360 / 65536;
        }

        if (ReadBit())
        {
            roll = ReadUInt16() * 360 / 65536;
        }

        if (IsError)
        {
            return new FRotator();
        }

        return new FRotator
        {
            Pitch = pitch,
            Yaw = yaw,
            Roll = roll
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendDataFromChecked(ReadOnlyMemory<byte> data, int bitCount)
    {
        Memory<byte> newBuffer = new byte[Buffer.Length + data.Length];
        Buffer.CopyTo(newBuffer);
        data.CopyTo(newBuffer.Slice(Buffer.Length));
        Buffer = newBuffer;
        EndPosition += bitCount;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetTempEnd(int size, FBitArchiveEndIndex index)
    {
        var setPosition = Position + size;
        if (setPosition > EndPosition)
        {
            IsError = true;
            return;
        }

        _tempLastBit[index] = EndPosition;
        EndPosition = setPosition;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RestoreTempEnd(FBitArchiveEndIndex index)
    {
        Position = EndPosition;
        if (_tempLastBit.ContainsKey(index))
        {
            EndPosition = _tempLastBit[index];
            _tempLastBit.Remove(index);
        }

        IsError = false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FVector SerializeQuantizedVector(FVector defaultValue)
    {
        var bWasSerialized = ReadBit();

        if (bWasSerialized)
        {
            var bShouldQuantize =
                EngineNetworkVersion < EEngineNetworkVersionHistory.HistoryOptionallyQuantizeSpawnInfo || ReadBit();
            if (bShouldQuantize)
            {
                return ReadPackedVector(10, 24);
            }

            return ReadFVector();
        }

        return defaultValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override uint ReadIntPacked()
    {
        var bitCountUsedInByte = Position & 7;
        var bitCountLeftInByte = 8 - (Position & 7);
        var srcMaskByte0 = (byte)((1U << bitCountLeftInByte) - 1U);
        var srcMaskByte1 = (byte)((1U << bitCountUsedInByte) - 1U);
        var srcIndex = PositionByte;
        var nextSrcIndex = bitCountUsedInByte != 0 ? srcIndex + 1 : srcIndex;

        uint value = 0;
        for (int It = 0, shiftCount = 0; It < 5; ++It, shiftCount += 7)
        {
            if (!CanReadBits(8))
            {
                IsError = true;
                break;
            }

            if (nextSrcIndex >= Buffer.Length)
            {
                nextSrcIndex = srcIndex;
            }

            Position += 8;

            var readByte = (byte)(((Buffer.Span[srcIndex] >> bitCountUsedInByte) & srcMaskByte0) |
                                  ((Buffer.Span[nextSrcIndex] & srcMaskByte1) << (bitCountLeftInByte & 7)));
            value = (uint)((readByte >> 1) << shiftCount) | value;
            srcIndex++;
            nextSrcIndex++;

            if ((readByte & 1) == 0)
            {
                break;
            }
        }

        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FVector ReadFVectorNormal() =>
        new()
        {
            X = ReadFixedCompressedFloat(1, 16),
            Y = ReadFixedCompressedFloat(1, 16),
            Z = ReadFixedCompressedFloat(1, 16)
        };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float ReadFixedCompressedFloat(int maxValue, int numBits)
    {
        var maxBitValue = (1 << (numBits - 1)) - 1;
        var bias = 1 << (numBits - 1);
        var serIntMax = 1 << (numBits - 0);

        var delta = ReadSerializedInt(serIntMax);
        float unscaledValue = unchecked((int)delta) - bias;

        if (maxValue > maxBitValue)
        {
            var invScale = maxValue / (float)maxBitValue;

            return unscaledValue * invScale;
        }
        else
        {
            var scale = maxBitValue / (float)maxValue;
            var invScale = 1f / scale;

            return unscaledValue * invScale;
        }
    }
}