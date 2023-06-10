namespace UnrealReplayReader.Fortnite.Models.Classes;

public record FortSet
{
    public uint StartingHandle { get; set; }
    public float BaseValue { get; set; }
    public float CurrentValue { get; set; }
    public float Maximum { get; set; }
}