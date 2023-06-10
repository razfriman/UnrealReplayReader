using UnrealReplayReader.IO;

namespace UnrealReplayReader.Models.Classes;

public record FName : CustomClass
{
    public string Name { get; set; }

    public override void Serialize(BitReader reader)
    {
        Name = reader.ReadFName();
    }
}