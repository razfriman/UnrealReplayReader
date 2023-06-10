using UnrealReplayReader.Compression;
using UnrealReplayReader.IO;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private ByteReader DecompressReader(ByteReader existingReader, bool isCompressed)
    {
        if (!isCompressed)
        {
            return existingReader;
        }

        var decompressedSize = existingReader.ReadInt32();
        var compressedSize = existingReader.ReadInt32();
        var compressedData = existingReader.ReadBytes(compressedSize);
        var decompressedData = Oodle.DecompressReplayData(compressedData, decompressedSize);
        return new ByteReader(decompressedData, existingReader);
    }
}