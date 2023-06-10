namespace UnrealReplayReader.Fortnite.Models.Replay;

public record Team
{
    public int TeamId { get; set; }
    public List<PlayerState> Players { get; set; } = new();
}