using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Models;

public record FLocalFileReplayInfo
{
    public uint LengthInMs { get; set; }
    public ENetworkVersionHistory NetworkVersion { get; set; }
    public uint Changelist { get; set; }
    public string FriendlyName { get; set; }
    public DateTime Timestamp { get; set; }
    public long TotalDataSizeInBytes { get; set; }
    public bool IsLive { get; set; }
    public bool IsCompressed { get; set; }
    public bool Encrypted { get; set; }
    public byte[] EncryptionKey { get; set; }
    public ELocalFileVersionHistory FileVersion { get; set; }

    public int HeaderChunkIndex { get; set; }
    public List<FLocalFileChunkInfo> Chunks { get; set; } = new();
    public List<FLocalFileEventInfo> Checkpoints { get; set; } = new();
    public List<FLocalFileEventInfo> Events { get; set; } = new();
    public List<FLocalFileReplayDataInfo> DataChunks { get; set; } = new();
}