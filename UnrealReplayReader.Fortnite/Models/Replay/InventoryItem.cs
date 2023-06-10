using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader.Fortnite.Models.Replay;

public record InventoryItem
{
    public int? Count { get; set; }
    public int? Ammo { get; set; }
    public ItemDefinition? Item { get; set; }
}