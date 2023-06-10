using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Replay;

public record CapturePoint
{
    public FVector Location { get; set; }
    public int? TeamId { get; set; }
    public List<PlayerState> Players { get; set; } = new();
}