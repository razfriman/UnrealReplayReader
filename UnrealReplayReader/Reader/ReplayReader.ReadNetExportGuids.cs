using UnrealReplayReader.IO;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private void ReadNetExportGuids(ByteReader reader)
    {
        var numGuids = reader.ReadIntPacked();

        for (var i = 0; i < numGuids; i++)
        {
            var size = reader.ReadInt32();
            var newReader = new ByteReader(reader.ReadBytesAsMemory(size));
            ReadNetGuid(newReader, true);
        }
    }
}