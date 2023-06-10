namespace UnrealReplayReader.Fortnite.Models.Replay;

public record StateData
{
    public GameState? GameState { get; set; }
    public List<SafeZone> SafeZones { get; set; } = new();
    public Dictionary<uint, Vehicle> Vehicles { get; set; } = new();
    public Dictionary<uint, PlayerMarker> PlayerMarker { get; set; } = new();
    public Dictionary<uint, SupplyDrop> SupplyDrops { get; set; } = new();
    public Dictionary<uint, CapturePoint> CapturePoints { get; set; } = new();
    public Dictionary<uint, RemoteClientInfo> RemoteClientInfos { get; set; } = new();
    public Dictionary<uint, SpectatorInfoPlayerInfo> SpectatorInfoPlayerInfos { get; set; } = new();
    public Dictionary<uint, Inventory> Inventories { get; set; } = new();
    public Dictionary<int, LabradorPawn> Labradors { get; set; } = new();
    public Dictionary<uint, Llama> Llamas { get; set; } = new();
    public Dictionary<uint, Pickup> Pickups { get; set; } = new();
    public Dictionary<uint, PlayerBuild> PlayerBuilds { get; set; } = new();
    public Dictionary<uint, PlayerPawn> PlayerPawns { get; set; } = new();
    public Dictionary<uint, Pawn> Pawns { get; set; } = new();
    public Dictionary<uint, PlayerState> Players { get; set; } = new();
    public Dictionary<uint, Container> Containers { get; set; } = new();
    public Dictionary<int, Team> Teams { get; set; } = new();
    public Dictionary<uint, CreativeIsland> CreativeIslands { get; set; } = new();
    public Dictionary<uint, bool> ActorOnlySpectatingPlayers { get; } = new();
    public Dictionary<int, uint?> VictoryCrownPlayerStateDict { get; set; } = new();
}