using Microsoft.Extensions.Logging;
using UnrealReplayReader.Fortnite.Models.Replay;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Events;

public static class FortniteEventReader
{
    public static void ReadEvent(FLocalFileEventInfo chunk, ByteReader reader, FortniteReplay replay,
        ILogger? logger = null)
    {
        switch (chunk.Group)
        {
            case "AdditionGFPEventGroup" when chunk.Metadata == "AdditionGFPEvent":
                replay.Events.AdditionGfp = AdditionGfpReader.ReadAdditionGfp(chunk, reader);
                break;
            case "playerElim" when chunk.Metadata == "versionedEvent":
                var playerElim = PlayerElimReader.ReadPlayerElim(chunk, reader);
                if (playerElim != null)
                {
                    replay.Events.Eliminations.Add(playerElim);
                }

                break;
            case "PlayerStateEncryptionKeyEvent" when chunk.Metadata == "PlayerStateEncryptionKeyEvent":
            {
                var playerEncryptionKey = PlayerStateEncryptionKeyReader.ReadPlayerStateEncryptionKey(chunk, reader);
                break;
            }
            case "PlayerStateEncryptionKey" when chunk.Metadata == "PlayerStateEncryptionKey":
            {
                var playerEncryptionKey = PlayerStateEncryptionKeyReader.ReadPlayerStateEncryptionKey(chunk, reader);
                break;
            }
            case "AthenaReplayBrowserEvents" when chunk.Metadata == "AthenaMatchStats":
                var matchStats = MatchStatsReader.ReadMatchStats(chunk, reader);
                if (matchStats.HasValue)
                {
                    replay.Events.MatchStats = matchStats.Value;
                }

                break;
            case "AthenaReplayBrowserEvents" when chunk.Metadata == "AthenaMatchTeamStats":
                var teamStats = TeamStatsReader.ReadTeamStats(chunk, reader);
                if (teamStats != null)
                {
                    replay.Events.TeamStats = teamStats;
                }

                break;
            case "Timecode" when chunk.Metadata == "TimecodeVersionedMeta":
            {
                var timecode = TimecodeReader.ReadTimecode(chunk, reader);
                if (timecode != null)
                {
                    replay.Events.Timecode = timecode;
                }

                break;
            }
            case "ActorsPosition" when chunk.Metadata == "ActorsVersionedMeta":
            {
                var actorPosition = ActorPositionsReader.ReadActorPositions(chunk, reader);
                replay.Events.ActorPositions.Add(actorPosition);
                break;
            }
            case "CharacterSample" when chunk.Metadata == "CharacterSampleMeta":
            {
                var characterSample = CharacterSampleReader.ReadCharacterSample(chunk, reader);
                if (characterSample != null)
                {
                    replay.Events.CharacterSamples.Add(characterSample);
                }

                break;
            }
            case "ZoneUpdate" when chunk.Metadata == "ZoneVersionedMeta":
            {
                var zoneUpdate = ZoneUpdateReader.ReadZoneUpdate(chunk, reader);
                if (zoneUpdate != null)
                {
                    replay.Events.ZoneUpdates.Add(zoneUpdate);
                }

                break;
            }
            case "sessionG0" when chunk.Metadata == "sessionM":
            {
                // Unknown - encrypted?
                break;
            }
            default:
            {
                logger?.LogWarning(
                    $"Unknown event {chunk.Group} ({chunk.Metadata}) of size {chunk.SizeInBytes}");
                break;
            }
        }

        if (reader.Available != 0)
        {
            logger?.LogWarning(
                $"Event not fully handled, event {chunk.Group} ({chunk.Metadata}) with remaining bytes {reader.Available}");
        }
    }
}