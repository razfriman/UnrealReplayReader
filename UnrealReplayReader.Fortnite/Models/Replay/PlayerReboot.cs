namespace UnrealReplayReader.Fortnite.Models.Replay;

public record PlayerReboot
{
    public PlayerState Player { get; set; }
    public float WorldTime { get; set; }
}