using Microsoft.Extensions.Logging;
using UnrealReplayReader.Fortnite.Models.Events;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Events;

public class TeamStatsReader
{
    private const int TeamStatsVersion = 0;

    public static TeamStatsEvent? ReadTeamStats(FLocalFileEventInfo chunk, ByteReader reader, ILogger? logger = null)
    {
        var version = reader.ReadInt32();

        if (version != TeamStatsVersion)
        {
            logger?.LogWarning(
                $"Unknown event version. Group: {chunk.Group} Metadata: {chunk.Metadata} Version: {version}");
            return null;
        }

        return new TeamStatsEvent
        {
            Position = reader.ReadUInt32(),
            TotalPlayers = reader.ReadUInt32()
        };
    }
}