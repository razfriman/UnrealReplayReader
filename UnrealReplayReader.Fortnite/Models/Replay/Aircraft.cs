using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Replay;

public record Aircraft
{
    public FVector FlightStartLocation { get; set; }
    public FRotator FlightRotation { get; set; }
}