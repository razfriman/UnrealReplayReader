using Microsoft.Extensions.Logging;
using UnrealReplayReader.Exceptions;
using UnrealReplayReader.Fortnite.Models.Enums;
using UnrealReplayReader.Fortnite.Models.Events;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Events;

public static class PlayerElimReader
{
    private const int PlayerElimVersion = 9;

    public static PlayerElimEvent? ReadPlayerElim(FLocalFileEventInfo chunk, ByteReader reader,
        ILogger? logger = null)
    {
        try
        {
            var playerElimEvent = new PlayerElimEvent();
            playerElimEvent.GunType = EDeathCause.Unspecified;
            var eventVersion = reader.ReadInt32();
            var eventType = reader.ReadByte();

            if (eventVersion == PlayerElimVersion && eventType == 4)
            {
                playerElimEvent.EliminatedInfo = new PlayerElimPlayerInfo
                {
                    Location = reader.ReadFTransform()
                };
                playerElimEvent.EliminatorInfo = new PlayerElimPlayerInfo
                {
                    Location = reader.ReadFTransform()
                };
                ReadPlayer(reader, playerElimEvent.EliminatedInfo, logger);
                ReadPlayer(reader, playerElimEvent.EliminatorInfo, logger);
                playerElimEvent.GunType = reader.ReadByteAsEnum<EDeathCause>();
                playerElimEvent.IsKnocked = reader.ReadUInt32AsBoolean();
            }
            else
            {
                logger?.LogWarning($"Unknown Elimination Event Version: {eventVersion} Type: {eventType}");
            }

            return playerElimEvent;
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "Error while parsing PlayerEliminationEvent at timestamp {TimeStamp}", chunk.Time1);
            throw new ReplayException(
                $"Error while parsing PlayerEliminationEvent at timestamp {chunk.Time1}", ex);
        }
    }

    public static void ReadPlayer(ByteReader archive, PlayerElimPlayerInfo info, ILogger? logger = null)
    {
        info.EPlayerElimPlayerType = archive.ReadByteAsEnum<EPlayerElimPlayerType>();

        switch (info.EPlayerElimPlayerType)
        {
            case EPlayerElimPlayerType.Bot:
                break;
            case EPlayerElimPlayerType.NamedBot:
                info.Id = archive.ReadFString();
                break;
            case EPlayerElimPlayerType.Player:
                info.Id = archive.ReadGuid(archive.ReadByte())?.ToLower();
                break;
            default:
                logger?.LogWarning($"Unknown player type: {info.EPlayerElimPlayerType}");
                break;
        }
    }
}