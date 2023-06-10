using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Replay;

public record WeaponShot
{
    public PlayerPawn? ShotByPlayerPawn;
    public PlayerPawn? HitPlayerPawn;
    public double DeltaGameTimeSeconds { get; set; }
    public FVector? StartLocation { get; set; }
    public FVector? Location { get; set; }
    public FVector? Normal { get; set; }
    public float Damage { get; set; }
    public bool HitPlayer => HitPlayerPawn != null;
}