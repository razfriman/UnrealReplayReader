using Microsoft.Extensions.Logging;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private const int MaxPacketInBits = 1024 * 2 * 8;

    public Dictionary<uint, Channel> Channels { get; set; } = new();
    public Dictionary<uint, bool> IgnoredChannels { get; set; } = new();
    public Dictionary<uint, string> ActorToPath { get; set; } = new();
    public Dictionary<uint, uint> ActorToChannel { get; set; } = new();
    public Dictionary<uint, uint> ChannelToActor { get; set; } = new();
    public Dictionary<uint, bool> DormantActors { get; set; } = new();

    private void ReceivePacket(Packet packet, NetBitReader reader)
    {
        InPacketId++;
        while (!reader.AtEnd)
        {
            HandlePacket(packet, reader);
        }
    }

    private void HandlePacket(Packet packet, NetBitReader reader)
    {
        if (reader.EngineNetworkVersion < EEngineNetworkVersionHistory.HistoryAcksIncludedInHeader)
        {
            // Ack bit
            reader.SkipBits(1);
        }

        var bControl = reader.ReadBit();
        var bOpen = bControl && reader.ReadBit();
        var bClose = bControl && reader.ReadBit();
        var bDormant = false;
        var closeReason = 0u;
        if (reader.EngineNetworkVersion < EEngineNetworkVersionHistory.HistoryChannelCloseReason)
        {
            bDormant = bClose && reader.ReadBit();
            closeReason = bDormant ? 1u : 0u;
        }
        else
        {
            closeReason = bClose ? reader.ReadSerializedInt(15) : 0;
            bDormant = closeReason == 1;
        }


        var bIsReplicationPaused = reader.ReadBit();
        var bReliable = reader.ReadBit();
        var chIndex = 0u;

        if (reader.EngineNetworkVersion < EEngineNetworkVersionHistory.HistoryMaxActorChannelsCustomization)
        {
            chIndex = reader.ReadSerializedInt(10240);
        }
        else
        {
            chIndex = reader.ReadIntPacked();
        }

        var bHasPackageExportMaps = reader.ReadBit();
        var bHasMustBeMappedGuids = reader.ReadBit();
        var bPartial = reader.ReadBit();

        var chSequence = 0;
        if (bReliable)
        {
            chSequence = InReliable + 1;
        }
        else if (bPartial)
        {
            chSequence = InPacketId;
        }

        var bPartialInital = bPartial && reader.ReadBit();
        var bPartialFinal = bPartial && reader.ReadBit();
        var chType = EChannelTypes.None;
        if (reader.EngineNetworkVersion < EEngineNetworkVersionHistory.HistoryChannelNames)
        {
            chType = (EChannelTypes)((bReliable || bOpen) ? reader.ReadSerializedInt(8) : 0);
        }
        else if (bReliable || bOpen)
        {
            var chName = reader.ReadFName();
            chType = chName switch
            {
                "Control" => EChannelTypes.Control,
                "Actor" => EChannelTypes.Actor,
                "File" => EChannelTypes.File,
                "Voice" => EChannelTypes.Voice,
                _ => chType
            };
        }


        var channel = Channels.GetValueOrDefault(chIndex, null);
        var bunchDataBits = reader.ReadSerializedInt(MaxPacketInBits);
        var ignoreChannel = IgnoredChannels.GetValueOrDefault(chIndex, false);

        NetBitReader? bunchReader = null;
        if (ignoreChannel)
        {
            reader.SetTempEnd((int)bunchDataBits, FBitArchiveEndIndex.Bunch);
            bunchReader = reader;
            reader.SkipBits((int)bunchDataBits);
        }
        else
        {
            if (bPartial)
            {
                var bits = reader.ReadBitsAsMemory((int)bunchDataBits);
                bunchReader = new NetBitReader(bits, (int)bunchDataBits, GuidCache, reader);
            }
            else
            {
                reader.SetTempEnd((int)bunchDataBits, FBitArchiveEndIndex.Bunch);
                bunchReader = reader;
                // var bits = reader.ReadBitsAsMemory((int)bunchDataBits);
                // bunchReader = new BitReader(bits, (int)bunchDataBits, reader);
            }
        }


        var bunch = new Bunch
        {
            TimeSeconds = packet.TimeSeconds,
            PacketId = InPacketId,
            BControl = bControl,
            BOpen = bOpen,
            BClose = bClose,
            BDormant = bDormant,
            CloseReason = closeReason,
            BIsReplicationPaused = bIsReplicationPaused,
            BReliable = bReliable,
            ChIndex = chIndex,
            BHasPackageExportMaps = bHasPackageExportMaps,
            BHasMustBeMappedGuids = bHasMustBeMappedGuids,
            BPartial = bPartial,
            ChSequence = chSequence,
            BPartialInital = bPartialInital,
            BPartialFinal = bPartialFinal,
            ChType = chType,
            BunchDataBits = bunchDataBits,
            Reader = bunchReader,
        };

        if (bHasPackageExportMaps)
        {
            ReceiveNetGuidBunch(bunchReader);
        }

        if (bReliable && chSequence <= InReliable)
        {
            bunchReader.RestoreTempEnd(FBitArchiveEndIndex.Bunch);
            return;
        }

        if (channel == null && !bReliable && !(bOpen && (bClose || bPartial)))
        {
            bunchReader.RestoreTempEnd(FBitArchiveEndIndex.Bunch);
            return;
        }

        if (channel == null)
        {
            var newChannel = new Channel
            {
                ChIndex = chIndex,
                ChannelType = chType,
                Actor = null,
            };
            Channels[chIndex] = newChannel;
        }

        try
        {
            if (!ignoreChannel)
            {
                ReceiveNextBunch(bunch);
            }
            else if (bClose)
            {
                OnChannelClosed(bunch);
            }
        }
        catch (Exception e)
        {
            Logger?.LogError(e, "Error while reading Bunch: {ActorId} {PacketId}", bunch?.ActorId, bunch?.PacketId);
        }
        finally
        {
            bunchReader.RestoreTempEnd(FBitArchiveEndIndex.Bunch);
        }
    }
}