namespace UnrealReplayReader.Fortnite.Models.Replay;

public record CreativeIsland
{
    public string CreatorAccountId { get; set; }
    public string CreatorName { get; set; }
    public string CreatorSacCode { get; set; }
    public string IslandCode { get; set; }
    public int? Version { get; set; }
    public string SelfAccountId { get; set; }
    public string[] IslandIntroduction { get; set; }
    public string[] DescriptionTags { get; set; }
    public string[] LinkTitle { get; set; }
    public string? TextLiteral { get; set; }
    public string AltTitle { get; set; }
    public string[] LinkTagline { get; set; }
    public string LinkType { get; set; }
    public bool? BAllowJoinInProgress { get; set; }
    public int? MinimumNumberOfPlayers { get; set; }
    public int? MaximumNumberOfPlayers { get; set; }
    public int? NumberOfTeams { get; set; }
    public int? PlayersPerTeam { get; set; }
}