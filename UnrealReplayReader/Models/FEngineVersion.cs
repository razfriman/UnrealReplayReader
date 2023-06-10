namespace UnrealReplayReader.Models;

public record struct FEngineVersion
{
    public string Branch { get; set; }
    public ushort Major { get; set; }
    public ushort Minor { get; set; }
    public ushort Patch { get; set; }
    public uint Changelist { get; set; }
}