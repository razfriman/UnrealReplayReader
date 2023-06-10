namespace UnrealReplayReader.Models;

public class NetFieldExport
{
    public string Name { get; set; }
    public uint Handle { get; set; }
    public uint CompatibleChecksum { get; set; }
    public string OrigType { get; set; }

    public FieldConfiguration? Configuration { get; set; }
    // public string GroupType { get; set; }
}