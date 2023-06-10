using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Models;

public record struct FLocalFileChunkInfo
{
    public ELocalFileChunkType ChunkType { get; set; }
    public int SizeInBytes { get; set; }
    public long TypeOffset { get; set; }
    public long DataOffset { get; set; }
}