using UnrealReplayReader.Exceptions;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private void ReadPlaybackPackets(ByteReader reader)
    {
        if (reader.NetworkVersion >= ENetworkVersionHistory.HistoryMultipleLevels)
        {
            var levelIndex = reader.ReadInt32();
        }

        var timeSeconds = reader.ReadSingle();

        if (reader.NetworkVersion >= ENetworkVersionHistory.HistoryLevelStreamingFixes)
        {
            ReadNetFieldExports(reader);
            ReadNetExportGuids(reader);
        }

        if (reader.HasLevelStreamingFixes)
        {
            var numStreamingLevels = reader.ReadIntPacked();

            for (var i = 0; i < numStreamingLevels; i++)
            {
                var level = reader.ReadFString();
            }
        }
        else
        {
            throw new ReplayException("FTransform deserialize not implemented");
        }

        if (reader.HasLevelStreamingFixes)
        {
            reader.SkipBytes(8);
        }

        ReadExternalData(reader);

        if (reader.HasGameSpecificFrameData)
        {
            var externalOffsetSize = reader.ReadUInt64();

            if (externalOffsetSize > 0)
            {
                reader.SkipBytes((int)externalOffsetSize);
            }
        }

        while (true)
        {
            var packet = ReadPacket(reader, timeSeconds);
            if (packet.State == 0)
            {
                ReceiveRawPacket(packet, reader);
            }
            else
            {
                break;
            }
        }
    }
}