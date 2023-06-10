using UnrealReplayReader.Fortnite.Models.Enums;

namespace UnrealReplayReader.Fortnite.Models.Events;

/// <summary>
/// FReplayEliminationEventInfo
/// </summary>
public record PlayerElimEvent
{
    public PlayerElimPlayerInfo? EliminatedInfo { get; set; }
    public PlayerElimPlayerInfo? EliminatorInfo { get; set; }
    public EFortReplayEventType EventType { get; set; }
    public EDeathCause GunType { get; set; }
    public bool IsKnocked { get; set; }
    public uint Timestamp { get; set; }
}