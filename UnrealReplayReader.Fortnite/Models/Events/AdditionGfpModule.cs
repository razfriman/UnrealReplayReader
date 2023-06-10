namespace UnrealReplayReader.Fortnite.Models.Events;

public record struct AdditionGfpModule
{
    public string Id { get; set; }
    public int Version { get; set; }
    public string ArtifactId { get; set; }
}