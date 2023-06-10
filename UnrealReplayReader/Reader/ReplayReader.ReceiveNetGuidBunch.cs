using UnrealReplayReader.IO;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private const int MaxGuidCount = 2048;

    public void ReceiveNetGuidBunch(BitReader reader)
    {
        var bHasRepLayoutExport = reader.ReadBit();

        if (bHasRepLayoutExport)
        {
            ReadNetFieldExports(reader);
            return;
        }

        var numGuiDsInBunch = reader.ReadInt32();

        if (numGuiDsInBunch > MaxGuidCount)
        {
            return;
        }

        for (var i = 0; i < numGuiDsInBunch; i += 1)
        {
            ReadNetGuid(reader, true);
        }
    }
}