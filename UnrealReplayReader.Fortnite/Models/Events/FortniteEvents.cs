namespace UnrealReplayReader.Fortnite.Models.Events;

public record FortniteEvents
{
    public MatchStatsEvent? MatchStats { get; set; }
    public TeamStatsEvent? TeamStats { get; set; }
    public List<PlayerElimEvent> Eliminations { get; set; } = new();
    public List<ZoneUpdateEvent> ZoneUpdates { get; set; } = new();
    public List<CharacterSampleEvent> CharacterSamples { get; set; } = new();
    public List<ActorPositionEvent> ActorPositions { get; set; } = new();
    public TimecodeEvent? Timecode { get; set; }
    public AdditionGfpEvent? AdditionGfp { get; set; }
}