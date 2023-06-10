using UnrealReplayReader.Fortnite.Models.Enums;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Events;

public record MovementEvent
{
    public FVector Position { get; set; }
    public EFortMovementStyle MovementStyle { get; set; }
    public ushort DeltaGameTime { get; set; }
}