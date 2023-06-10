namespace UnrealReplayReader.Models;

public class NetDeltaExportData<T>
{
    public bool? Deleted { get; set; }
    public int ElementIndex { get; set; }
    public string Path { get; set; }
    public T Export { get; set; }
    public List<string> ChangedProperties { get; set; } = new();
}