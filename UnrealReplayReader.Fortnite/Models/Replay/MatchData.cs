using System.Text.Json.Serialization;

namespace UnrealReplayReader.Fortnite.Models.Replay;

public record MatchData
{
    public List<PlayerState> Players { get; set; } = new();
    [JsonIgnore] public List<KillFeedEntry> KillFeed { get; set; } = new();
    public PlayerState? ReplayPlayer { get; set; } = new();
    public List<Container> Containers { get; set; } = new();
    public List<PlayerBuild> PlayerBuilds { get; set; } = new();
    public List<Pickup> Pickups { get; set; } = new();
    public List<Llama> Llamas { get; set; } = new();
    public List<LabradorPawn> Labradors { get; set; } = new();
    public List<SupplyDrop> SupplyDrops { get; set; } = new();
    public List<CapturePoint> CapturePoints { get; set; } = new();
    public List<Vehicle> Vehicles { get; set; } = new();
    public List<PlayerMarker> PlayerMarkers { get; set; } = new();
    public List<CreativeIsland> CreativeIslands { get; set; } = new();
    public List<string> ActiveGameplayModifiers { get; set; } = new();
    public List<SafeZone> SafeZones { get; set; } = new();
    public GameState GameState { get; set; } = new();
}