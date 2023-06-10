namespace UnrealReplayReader.Models;

public record Packet
{
    public int PacketIndex { get; set; }
    public uint SeenLevelIndex { get; set; }
    public int State { get; set; }
    public int Size { get; set; }
    public float TimeSeconds { get; set; }
    public ReadOnlyMemory<byte> Data { get; set; }
}