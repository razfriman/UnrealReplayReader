using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Events;

public record ActorPositionEvent
{
    public List<FVector> ChestPositions { get; set; } = new();
}