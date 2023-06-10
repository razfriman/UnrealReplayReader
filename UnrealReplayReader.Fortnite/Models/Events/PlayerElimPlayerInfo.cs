using UnrealReplayReader.Fortnite.Models.Enums;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Events;

public record PlayerElimPlayerInfo
{
    public FTransform Location { get; set; }
    public EPlayerElimPlayerType EPlayerElimPlayerType { get; set; }
    public string Id { get; set; }
    public bool IsBot => EPlayerElimPlayerType is EPlayerElimPlayerType.Bot or EPlayerElimPlayerType.NamedBot;
}