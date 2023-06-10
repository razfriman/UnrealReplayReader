namespace UnrealReplayReader.Fortnite.Models.Events;

public record AdditionGfpEvent
{
    public List<AdditionGfpModule> Modules { get; set; } = new();
}