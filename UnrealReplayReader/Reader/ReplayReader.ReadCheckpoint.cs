using UnrealReplayReader.Exceptions;
using UnrealReplayReader.Extensions;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Classes;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private void ReadCheckpoint(FLocalFileEventInfo checkpoint, ByteReader encryptedReader)
    {
        ResetForCheckpoint();
        encryptedReader.Seek(checkpoint.EventDataOffset);
        var decrypted = DecryptReader(encryptedReader, checkpoint.SizeInBytes);
        var reader = DecompressReader(decrypted, Replay.Info.IsCompressed);
        if (reader.HasDeltaCheckpoints)
        {
            var checkpointSize = reader.ReadInt32();
        }

        if (reader.HasLevelStreamingFixes)
        {
            var packetOffset = reader.ReadInt64();
        }

        if (reader.NetworkVersion >= ENetworkVersionHistory.HistoryMultipleLevels)
        {
            var checkpointLevel = reader.ReadInt32();
        }

        if (reader.NetworkVersion >= ENetworkVersionHistory.HistoryDeletedStartupActors)
        {
            if (reader.HasDeltaCheckpoints)
            {
                throw new ReplayException("Delta checkpoints not implemented");
            }

            var deletedActors = reader.ReadArray(x => x.ReadFString());
        }

        var cacheGuids = new List<NetworkGuid>();
        var guidCount = reader.ReadInt32();
        for (var i = 0; i < guidCount; i++)
        {
            var guid = reader.ReadIntPacked();
            var outerId = reader.ReadIntPacked();
            var cacheObject = new NetworkGuid
            {
                Value = guid
            };
            var outerGuid = new NetworkGuid
            {
                Value = outerId
            };
            if (outerId != 0)
            {
                outerGuid = cacheGuids.FirstOrDefault(x => x.Value == outerId) ?? outerGuid;
                cacheObject.Outer = outerGuid;
            }

            if (reader.NetworkVersion < ENetworkVersionHistory.HistoryGuidNametable)
            {
                cacheObject.Path = StringExtensions.RemovePathPrefix(reader.ReadFString());
            }
            else
            {
                var isExported = reader.ReadByte() != 0;
                if (isExported)
                {
                    cacheObject.Path = StringExtensions.RemovePathPrefix(reader.ReadFString());
                    cacheGuids.Add(cacheObject);
                }
                else
                {
                    var pathNameIndex = reader.ReadIntPacked();
                    if (pathNameIndex < cacheGuids.Count)
                    {
                        cacheObject.Path = cacheGuids[(int)pathNameIndex].Path;
                    }
                }
            }

            if (reader.NetworkVersion < ENetworkVersionHistory.HistoryGuidcacheChecksums)
            {
                cacheObject.Checksum = reader.ReadUInt32();
            }

            cacheObject.Flags = reader.ReadByte();
            GuidCache.NetGuids[guid] = cacheObject;

            if (Settings.IsDebug)
            {
                DebugNetGuidToPathName.Add(cacheObject);
            }
        }

        var groups = new List<NetFieldExportGroup>();

        if (reader.HasDeltaCheckpoints)
        {
            throw new ReplayException("Delta checkpoints not implemented");
        }
        else
        {
            var netFieldExportCount = reader.ReadInt32();
            for (var i = 0; i < netFieldExportCount; i++)
            {
                groups.Add(ReadCheckpointNetFieldExportGroup(reader));
            }
        }

        ReadPlaybackPackets(reader);
    }
}