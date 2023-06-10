namespace UnrealReplayReader.Models;

public record struct ExternalData
{
    public uint NetGuid { get; set; }
    public int ExternalDataNumBytes { get; set; }
    public byte Handle { get; set; }
    public byte Something { get; set; }
    public bool IsEncrypted { get; set; }
    public byte[] Payload { get; set; }
}