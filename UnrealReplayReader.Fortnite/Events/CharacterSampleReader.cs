using Microsoft.Extensions.Logging;
using UnrealReplayReader.Fortnite.Models.Enums;
using UnrealReplayReader.Fortnite.Models.Events;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Events;

public static class CharacterSampleReader
{
    private const int CharacterSampleVersion = 9;

    public static CharacterSampleEvent? ReadCharacterSample(FLocalFileEventInfo chunk, ByteReader reader,
        ILogger? logger = null)
    {
        var version = reader.ReadInt32();

        if (version != CharacterSampleVersion)
        {
            logger?.LogWarning(
                $"Unknown event version. Group: {chunk.Group} Metadata: {chunk.Metadata} Version: {version}");
            return null;
        }

        var result = new CharacterSampleEvent();

        var count = reader.ReadInt32();
        for (var i = 0; i < count; i++)
        {
            result.Samples.Add(new CharacterSample
            {
                EpicId = reader.ReadFString(),
                MovementEvents = reader.ReadArray(x =>
                    new MovementEvent
                    {
                        Position = x.ReadFVector(),
                        MovementStyle = x.ReadByteAsEnum<EFortMovementStyle>(),
                        DeltaGameTime = x.ReadUInt16()
                    }).ToList()
            });
        }

        return result;
    }
}