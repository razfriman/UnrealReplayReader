using UnrealReplayReader.FortniteMinimal.Models;

namespace UnrealReplayReader.FortniteMinimal;

public record FortniteMinimalReplay : UnrealReplay
{
    public Match Match { get; set; } = new();
}