using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Replay;

public record SupplyDrop
{
    public FVector? Location { get; set; }
    public FRotator? Rotation { get; set; }
    public double SpawnTime { get; set; }
}