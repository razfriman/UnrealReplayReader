using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader.Models;

public record Actor
{
    public NetworkGuid ActorNetGuid { get; set; }
    public uint ActorId => ActorNetGuid?.Value ?? 0;
    public NetworkGuid? Archetype { get; set; }
    public NetworkGuid? Level { get; set; }
    public FVector? Location { get; set; }
    public FRotator? Rotation { get; set; }
    public FVector? Scale { get; set; }
    public FVector? Velocity { get; set; }
}