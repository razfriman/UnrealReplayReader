namespace UnrealReplayReader.Models;

public abstract record GroupConfiguration
{
    public HashSet<GroupConfiguration> Children { get; set; } = new();
    public string ConfigurationName => GetType().Name;
    public string ModelName => Model.GetType().Name;
    public ExportModel Model { get; set; }
    public bool IsClassNetCache { get; set; }
    public bool StoreAsHandle { get; set; }
    public uint? StoreAsHandleMaxDepth { get; set; }
    public List<string> Paths { get; set; } = new();
    public List<string> PartialPaths { get; set; } = new();
    public List<string> PlayerControllers { get; set; } = new();
    public virtual Dictionary<string, FieldConfiguration> Fields { get; set; } = new();

    public abstract ExportModel CreateInstance();

    public ExportModel Read()
    {
        return new ExportModel();
    }

    public List<string> StaticActorIds { get; set; } = new();
    public bool ParseUnknownHandles { get; set; }
    public List<string> Redirects { get; set; } = new();
}