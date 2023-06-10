using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader.Fortnite.Models.Replay;

public record GameplayCue
{
    public FVector? Location { get; set; }
    public FGameplayTag? GameplayCueTag { get; set; }
    public int? TimeSeconds { get; set; }
}