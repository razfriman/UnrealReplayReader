using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Replay;

public record SafeZone
{
    public float Radius { get; set; }
    public float ShrinkStartTime { get; set; }
    public float ShringEndTime { get; set; }
    public float CurrentRadius { get; set; }

    public float NextRadius { get; set; }
    public float NextNextRadius { get; set; }
    public FVector LastCenter { get; set; }
    public FVector NextCenter { get; set; }
    public FVector NextNextCenter { get; set; }
    public int PlayersAlive { get; set; }
}