namespace UnrealReplayReader.Fortnite.Models.Events;

public record CharacterSample
{
    public string EpicId { get; set; }
    public List<MovementEvent> MovementEvents { get; set; } = new();
}