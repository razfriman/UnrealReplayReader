using UnrealReplayReader.Compression.Ooz;

namespace UnrealReplayReader.Compression;

public class Oodle
{
    private static readonly Kracken Kraken = new Kracken();

    public static byte[] DecompressReplayData(byte[] buffer, int uncompressedSize)
    {
        return Kraken.Decompress(buffer, uncompressedSize);
    }
}