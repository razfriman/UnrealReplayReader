namespace UnrealReplayReader.Models.Enums;

public enum ELocalFileChunkType : uint
{
    Header,
    ReplayData,
    Checkpoint,
    Event,
    Unknown = 0xFFFFFFFF
}