namespace UnrealReplayReader.Fortnite.Models.Events;

public record TeamStatsEvent
{
    public object Position { get; set; }
    public object TotalPlayers { get; set; }
}