using Microsoft.Extensions.Logging;
using UnrealReplayReader.Fortnite.Models.Events;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Events;

public static class ZoneUpdateReader
{
    private const int ZoneUpdateVersion = 9;

    public static ZoneUpdateEvent? ReadZoneUpdate(FLocalFileEventInfo chunk, ByteReader reader, ILogger? logger = null)
    {
        var version = reader.ReadInt32();

        if (version != ZoneUpdateVersion)
        {
            logger?.LogWarning(
                $"Unknown event version. Group: {chunk.Group} Metadata: {chunk.Metadata} Version: {version}");
            return null;
        }

        var position = reader.ReadFVector();
        var radius = reader.ReadSingle();
        return new ZoneUpdateEvent
        {
            Position = position,
            Radius = radius
        };
    }
}