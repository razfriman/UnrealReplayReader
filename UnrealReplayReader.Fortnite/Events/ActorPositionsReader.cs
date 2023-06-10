using Microsoft.Extensions.Logging;
using UnrealReplayReader.Fortnite.Models.Events;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Events;

public static class ActorPositionsReader
{
    private const int ActorPositionsVersion = 9;

    public static ActorPositionEvent ReadActorPositions(FLocalFileEventInfo chunk, ByteReader reader,
        ILogger? logger = null)
    {
        var version = reader.ReadInt32();
        if (version != ActorPositionsVersion)
        {
            logger?.LogWarning(
                $"Unknown event version. Group: {chunk.Group} Metadata: {chunk.Metadata} Version: {version}");
            return null;
        }

        var chestPositions = reader.ReadArray(x => x.ReadFVector()).ToList();
        return new ActorPositionEvent
        {
            ChestPositions = chestPositions
        };
    }
}