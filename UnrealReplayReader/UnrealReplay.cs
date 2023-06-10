using UnrealReplayReader.Models;

namespace UnrealReplayReader;

public abstract record UnrealReplay
{
    public FLocalFileReplayInfo Info { get; set; }
    public FNetworkDemoHeader Header { get; set; }
    public long ParseTime { get; set; }
    public Dictionary<string, Dictionary<string, HashSet<uint>>> ExportGroupDict { get; set; } = new();

    public ReplayReaderSettings Settings { get; set; }
}