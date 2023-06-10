using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Events;

public record ZoneUpdateEvent
{
    public FVector Position { get; set; }
    public float Radius { get; set; }
}