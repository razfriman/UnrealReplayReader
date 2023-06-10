namespace UnrealReplayReader.FortniteMinimal.Models;

public record Match
{
    public DateTime? MatchTime { get; set; }
    public Player? Player { get; set; }
    public string? MatchId { get; set; }
    public string? Playlist { get; set; }
}