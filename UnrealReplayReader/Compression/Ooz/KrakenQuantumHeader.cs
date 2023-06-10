namespace UnrealReplayReader.Compression.Ooz;

public class KrakenQuantumHeader
{
    internal uint CompressedSize { get; set; }
    internal uint Checksum { get; set; }
    internal byte Flag1 { get; set; }
    internal byte Flag2 { get; set; }

    // Whether the whole block matched a previous block
    internal uint WholeMatchDistance { get; set; }
}