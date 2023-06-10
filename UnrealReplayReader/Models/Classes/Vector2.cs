using UnrealReplayReader.IO;

namespace UnrealReplayReader.Models.Classes;

public record Vector2 : CustomClass
{
    public float X { get; set; }
    public float Y { get; set; }


    public override void Serialize(BitReader reader)
    {
        X = reader.ReadSingle();
        Y = reader.ReadSingle();
    }
}