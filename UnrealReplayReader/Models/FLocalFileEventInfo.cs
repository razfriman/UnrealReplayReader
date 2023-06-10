namespace UnrealReplayReader.Models;

public record struct FLocalFileEventInfo
{
    public int ChunkIndex { get; set; }
    public string Id { get; set; }
    public string Group { get; set; }
    public string Metadata { get; set; }
    public uint Time1 { get; set; }
    public uint Time2 { get; set; }
    public int SizeInBytes { get; set; }
    public long EventDataOffset { get; set; }
}