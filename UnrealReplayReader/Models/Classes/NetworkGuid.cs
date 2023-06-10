using UnrealReplayReader.IO;

namespace UnrealReplayReader.Models.Classes;

public record NetworkGuid : CustomClass
{
    public uint Value { get; set; }
    public uint? Checksum { get; set; }
    public NetworkGuid? Outer { get; set; }
    public int? Flags { get; set; }
    public string? Path { get; set; }

    public bool IsValid => Value > 0;

    public bool IsDynamic => Value > 0 && (Value & 1) != 1;

    public bool IsDefault => Value == 1;

    public override void Serialize(BitReader reader) => Value = reader.ReadIntPacked();
}