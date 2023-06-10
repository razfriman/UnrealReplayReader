using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Replay;

public record Llama
{
    public FVector? Location { get; set; }
    public FRotator? Rotation { get; set; }
}