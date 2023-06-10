namespace UnrealReplayReader.Fortnite.Models.Events;

public record CharacterSampleEvent
{
    public List<CharacterSample> Samples { get; set; } = new();
}