using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private void ReadDataChunk(FLocalFileReplayDataInfo dataChunk, ByteReader reader)
    {
        reader.Seek(dataChunk.ReplayDataOffset);
        var decrypted = DecryptReader(reader, dataChunk.SizeInBytes);
        var decompressed = DecompressReader(decrypted, Replay.Info.IsCompressed);
        while (!decompressed.AtEnd)
        {
            ReadPlaybackPackets(decompressed);
        }
    }
}