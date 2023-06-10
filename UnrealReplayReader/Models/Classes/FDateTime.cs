using UnrealReplayReader.IO;

namespace UnrealReplayReader.Models.Classes;

public record FDateTime : CustomClass
{
    public DateTime Time { get; set; }

    public override void Serialize(BitReader reader)
    {
        Time = reader.ReadDate();
    }
}