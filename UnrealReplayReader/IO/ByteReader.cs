using System.Runtime.CompilerServices;

namespace UnrealReplayReader.IO;

public class ByteReader : FReader
{
    public ByteReader(ReadOnlyMemory<byte> buffer, FReader? existingReader = null) : base(buffer, existingReader)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool CanReadBytes(int count) => Available >= count;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void ReadBytes(Span<byte> data, int? dataCount = null)
    {
        var count = dataCount ?? data.Length;
        if (!CanReadBytes(count))
        {
            IsError = true;
            return;
        }

        Buffer.Slice(Position, count).Span.CopyTo(data);
        Position += count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override sbyte ReadSByte()
    {
        if (!CanReadBytes(1))
        {
            IsError = true;
            return 0;
        }

        var result = (sbyte)Buffer.Span[Position];
        Position++;
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override byte ReadByte()
    {
        if (!CanReadBytes(1))
        {
            IsError = true;
            return 0;
        }

        var result = Buffer.Span[Position];
        Position++;
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ReadIsHardcoded() => ReadByte() != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void SkipBytes(int count)
    {
        if (!CanReadBytes(count))
        {
            IsError = true;
            return;
        }

        Position += count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlyMemory<byte> ReadBytesAsMemory(int count)
    {
        if (count == 0)
        {
            return Array.Empty<byte>();
        }

        if (!CanReadBytes(count))
        {
            IsError = true;
            return Array.Empty<byte>();
        }

        var slice = Buffer.Slice(Position, count);
        Position += count;
        return slice;
    }

    public ReadOnlySpan<byte> ReadBytesAsSpan(int count) => ReadBytesAsMemory(count).Span;
}