namespace UnrealReplayReader.Fortnite.Models.Replay;

public record Inventory
{
    public Dictionary<int, InventoryItem> Items { get; set; } = new();
    public uint? Id { get; set; }
    public uint? PlayerId { get; set; }
    public uint? ReplayPawn { get; set; }
}