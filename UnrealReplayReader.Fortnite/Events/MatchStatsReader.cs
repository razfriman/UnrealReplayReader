using Microsoft.Extensions.Logging;
using UnrealReplayReader.Fortnite.Models.Events;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Events;

public static class MatchStatsReader
{
    private const int MatchStatsVersion = 0;

    public static MatchStatsEvent? ReadMatchStats(FLocalFileEventInfo chunk, ByteReader reader, ILogger? logger = null)
    {
        var version = reader.ReadUInt32();
        if (version != MatchStatsVersion)
        {
            logger?.LogWarning(
                $"Unknown event version. Group: {chunk.Group} Metadata: {chunk.Metadata} Version: {version}");
            return null;
        }

        return new MatchStatsEvent
        {
            Accuracy = reader.ReadSingle(),
            Assists = reader.ReadUInt32(),
            Eliminations = reader.ReadUInt32(),
            WeaponDamage = reader.ReadUInt32(),
            OtherDamage = reader.ReadUInt32(),
            Revives = reader.ReadUInt32(),
            DamageTaken = reader.ReadUInt32(),
            DamageToStructures = reader.ReadUInt32(),
            MaterialsGathered = reader.ReadUInt32(),
            MaterialsUsed = reader.ReadUInt32(),
            TotalTraveled = reader.ReadUInt32()
        };
    }
}