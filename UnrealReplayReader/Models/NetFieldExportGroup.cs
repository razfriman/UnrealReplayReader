namespace UnrealReplayReader.Models;

public class NetFieldExportGroup
{
    public string PathName { get; set; }
    public string SimpleName => Path.GetFileName(PathName);
    public string Name => Configuration?.ModelName ?? PathName;
    public uint PathNameIndex { get; set; }
    public uint NetFieldExportsLength { get; set; }
    public Dictionary<uint, NetFieldExport> NetFieldExports { get; set; } = new();

    public GroupConfiguration? Configuration { get; set; }
}