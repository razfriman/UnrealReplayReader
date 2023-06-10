using Microsoft.Extensions.Logging;

namespace UnrealReplayReader.Models;

public record ReplayReaderSettings
{
    public required ReplayExportConfiguration ExportConfiguration { get; init; }
    public bool UseCheckpoints { get; set; }
    public bool EnableActorToPath { get; set; }
    public ILogger? Logger { get; set; }
    public bool IsDebug { get; set; }
}