namespace UnrealReplayReader.Fortnite.Models.Events;

public record struct MatchStatsEvent
{
    public float Accuracy { get; set; }
    public uint Assists { get; set; }
    public uint Eliminations { get; set; }
    public uint WeaponDamage { get; set; }
    public uint OtherDamage { get; set; }
    public uint Revives { get; set; }
    public uint DamageTaken { get; set; }
    public uint DamageToStructures { get; set; }
    public uint DamageToPlayers => WeaponDamage + OtherDamage;
    public uint MaterialsGathered { get; set; }
    public uint MaterialsUsed { get; set; }
    public uint TotalTraveled { get; set; }
}