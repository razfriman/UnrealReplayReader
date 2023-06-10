namespace UnrealReplayReader.Models;

public record struct FLocalFileReplayDataInfo
{
    public int ChunkIndex { get; set; }
    public uint Time1 { get; set; }
    public uint Time2 { get; set; }
    public int SizeInBytes { get; set; }
    public int MemorySizeInBytes { get; set; }
    public long ReplayDataOffset { get; set; }
    public long StreamOffset { get; set; }
}