namespace UnrealReplayReader.Models;

public abstract class FieldConfiguration
{
    public string Name { get; set; }
    public string? Type { get; set; }
    public ParseTypes ParseType { get; set; }
    public bool StoreAsHandle { get; set; }
    public uint? StoreAsHandleMaxDepth { get; set; }
    public bool? EnablePropertyChecksum { get; set; }
    public abstract ReadFieldAction ReadGeneric { get; }
    public abstract ExportModel CreateInstance();
}