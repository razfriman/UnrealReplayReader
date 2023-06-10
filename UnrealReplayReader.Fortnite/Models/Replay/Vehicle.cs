using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader.Fortnite.Models.Replay;

public record Vehicle
{
    public required string Type { get; init; }
    public FRepMovement? ReplicatedMovement { get; set; }
}