using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader.Fortnite.Models.Replay;

public record PlayerPawn : Pawn
{
    public ActorGuid? Vehicle { get; set; }
    public float? ReplayLastTransformUpdateTimeStamp { get; set; }
    public bool? BIsSkydiving { get; set; }
    public bool? BIsParachuteOpen { get; set; }
    public ActorGuid? PlayerState { get; set; }
    public ItemDefinition? Character { get; set; }
    public ItemDefinition? SkyDiveContrail { get; set; }
    public ItemDefinition? Glider { get; set; }
    public ItemDefinition? Pickaxe { get; set; }
    public ItemDefinition? MusicPack { get; set; }
    public ItemDefinition? LoadingScreen { get; set; }
    public ItemDefinition? Backpack { get; set; }
    public ItemDefinition? PetSkin { get; set; }
    public ItemDefinition[]? ItemWraps { get; set; }
    public ItemDefinition[]? Dances { get; set; }
    public string? BannerIconId { get; set; }
    public string? BannerColorId { get; set; }
    public bool? bIsDefaultCharacter { get; set; }
    public FRepMovement? ReplicatedMovement { get; set; }
    public Dictionary<string, int> ClientStats { get; set; } = new();

    public List<WeaponShot> Shots { get; set; } = new();
    public List<WeaponShot> DamageTaken { get; set; } = new();
    public List<GameplayCue> GameplayCues { get; set; } = new();
    public bool ResolvedPlayer { get; set; }
    public Vehicle? CurrentVehicle { get; set; }
}