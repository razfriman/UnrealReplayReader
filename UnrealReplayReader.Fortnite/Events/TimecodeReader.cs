using Microsoft.Extensions.Logging;
using UnrealReplayReader.Fortnite.Models.Events;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Events;

public static class TimecodeReader
{
    private const int TimecodeVersion = 9;

    public static TimecodeEvent? ReadTimecode(FLocalFileEventInfo chunk, ByteReader reader, ILogger? logger = null)
    {
        var version = reader.ReadInt32();
        if (version != TimecodeVersion)
        {
            logger?.LogWarning(
                $"Unknown event version. Group: {chunk.Group} Metadata: {chunk.Metadata} Version: {version}");
            return null;
        }

        return new TimecodeEvent
        {
            Date = reader.ReadDate()
        };
    }
}