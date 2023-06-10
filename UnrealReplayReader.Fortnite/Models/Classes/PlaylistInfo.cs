using UnrealReplayReader.IO;
using UnrealReplayReader.Models.Classes;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Fortnite.Models.Classes;

public record PlaylistInfo : CustomClass
{
    public uint Id { get; set; }
    public string Name { get; set; }

    public override void Serialize(BitReader reader)
    {
        if (reader.EngineNetworkVersion >= EEngineNetworkVersionHistory.HistoryFastArrayDeltaStruct)
        {
            reader.ReadBit();
        }

        reader.ReadBit();
        Id = reader.ReadIntPacked();
        reader.SkipBits(31);
    }

    public override void Resolve(GuidCache cache)
    {
        Name = cache.TryGetPathName(Id);
    }
}