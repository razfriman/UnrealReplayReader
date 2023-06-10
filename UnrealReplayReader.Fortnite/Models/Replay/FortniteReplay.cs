using UnrealReplayReader.Fortnite.Models.Events;

namespace UnrealReplayReader.Fortnite.Models.Replay;

public record FortniteReplay : UnrealReplay
{
    public readonly StateData StateData = new();
    public FortniteEvents Events { get; set; } = new();
    public MatchData MatchData { get; set; } = new();
}