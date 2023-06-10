using Microsoft.Extensions.Logging;
using UnrealReplayReader.Exceptions;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private const uint FileMagic = 0x1CA2E27F;

    /// <summary>
    /// https://github.com/EpicGames/UnrealEngine/blob/ue5-main/Engine/Source/Runtime/NetworkReplayStreaming/LocalFileNetworkReplayStreaming/Private/LocalFileNetworkReplayStreaming.cpp
    /// </summary>
    private FLocalFileReplayInfo ReadReplayInfo(ByteReader archive)
    {
        var magicNumber = archive.ReadUInt32();
        if (magicNumber != FileMagic)
        {
            Logger?.LogError("Invalid replay file");
            throw new ReplayException("Invalid replay file");
        }

        var info = new FLocalFileReplayInfo();
        info.FileVersion = archive.ReadUInt32AsEnum<ELocalFileVersionHistory>();

        if (info.FileVersion >= ELocalFileVersionHistory.HistoryCustomVersions)
        {
            var customVersionCount = archive.ReadInt32();
            for (var i = 0; i < customVersionCount; i++)
            {
                var guid = archive.ReadGuid();
                var version = archive.ReadInt32();
            }
        }

        info.LengthInMs = archive.ReadUInt32();
        info.NetworkVersion = archive.ReadUInt32AsEnum<ENetworkVersionHistory>();
        info.Changelist = archive.ReadUInt32();
        info.FriendlyName = archive.ReadFString();
        info.IsLive = archive.ReadUInt32AsBoolean();
        archive.NetworkVersion = info.NetworkVersion;
        archive.FileVersion = info.FileVersion;

        if (info.FileVersion >= ELocalFileVersionHistory.HistoryRecordedTimestamp)
        {
            info.Timestamp = archive.ReadDate();
        }

        if (info.FileVersion >= ELocalFileVersionHistory.HistoryCompression)
        {
            info.IsCompressed = archive.ReadUInt32AsBoolean();
        }

        if (info.FileVersion >= ELocalFileVersionHistory.HistoryEncryption)
        {
            info.Encrypted = archive.ReadUInt32AsBoolean();
            info.EncryptionKey = archive.ReadBytes(archive.ReadInt32());
        }

        if (!info.IsLive && info.Encrypted && info.EncryptionKey.Length == 0)
        {
            Logger?.LogError("ReadReplayInfo: Completed replay is marked encrypted but has no key!");
            throw new ReplayException("Completed replay is marked encrypted but has no key!");
        }

        if (info.IsLive && info.Encrypted)
        {
            Logger?.LogError("ReadReplayInfo: Replay is marked encrypted and but not yet marked as completed!");
            throw new ReplayException("Replay is marked encrypted and but not yet marked as completed!");
        }


        while (!archive.AtEnd)
        {
            var chunkIndex = info.Chunks.Count;
            var chunk = new FLocalFileChunkInfo
            {
                TypeOffset = archive.Position,
                ChunkType = archive.ReadUInt32AsEnum<ELocalFileChunkType>(),
                SizeInBytes = archive.ReadInt32(),
                DataOffset = archive.Position
            };
            info.Chunks.Add(chunk);
            switch (chunk.ChunkType)
            {
                case ELocalFileChunkType.Header:
                    info.HeaderChunkIndex = chunkIndex;
                    break;
                case ELocalFileChunkType.ReplayData:
                    var dataChunk = new FLocalFileReplayDataInfo();
                    dataChunk.ChunkIndex = info.DataChunks.Count;
                    dataChunk.StreamOffset = info.TotalDataSizeInBytes;
                    if (archive.FileVersion >= ELocalFileVersionHistory.HistoryStreamChunkTimes)
                    {
                        dataChunk.Time1 = archive.ReadUInt32();
                        dataChunk.Time2 = archive.ReadUInt32();
                        dataChunk.SizeInBytes = archive.ReadInt32();
                    }
                    else
                    {
                        dataChunk.SizeInBytes = chunk.SizeInBytes;
                    }

                    if (archive.FileVersion < ELocalFileVersionHistory.HistoryEncryption)
                    {
                        dataChunk.ReplayDataOffset = archive.Position;
                        if (info.IsCompressed)
                        {
                            // Deprecated
                            dataChunk.MemorySizeInBytes = 0;
                        }
                        else
                        {
                            dataChunk.MemorySizeInBytes = dataChunk.SizeInBytes;
                        }
                    }
                    else
                    {
                        dataChunk.MemorySizeInBytes = archive.ReadInt32();
                        dataChunk.ReplayDataOffset = archive.Position;
                    }

                    info.TotalDataSizeInBytes += dataChunk.MemorySizeInBytes;
                    info.DataChunks.Add(dataChunk);
                    break;
                case ELocalFileChunkType.Checkpoint:
                    var checkpoint = new FLocalFileEventInfo();
                    checkpoint.ChunkIndex = info.Checkpoints.Count;
                    checkpoint.Id = archive.ReadFString();
                    checkpoint.Group = archive.ReadFString();
                    checkpoint.Metadata = archive.ReadFString();
                    checkpoint.Time1 = archive.ReadUInt32();
                    checkpoint.Time2 = archive.ReadUInt32();
                    checkpoint.SizeInBytes = archive.ReadInt32();
                    checkpoint.EventDataOffset = archive.Position;
                    info.Checkpoints.Add(checkpoint);
                    break;
                case ELocalFileChunkType.Event:
                    var eventInfo = new FLocalFileEventInfo();
                    eventInfo.ChunkIndex = info.Events.Count;
                    eventInfo.Id = archive.ReadFString();
                    eventInfo.Group = archive.ReadFString();
                    eventInfo.Metadata = archive.ReadFString();
                    eventInfo.Time1 = archive.ReadUInt32();
                    eventInfo.Time2 = archive.ReadUInt32();
                    eventInfo.SizeInBytes = archive.ReadInt32();
                    eventInfo.EventDataOffset = archive.Position;
                    info.Events.Add(eventInfo);
                    break;
                case ELocalFileChunkType.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            archive.Seek(chunk.DataOffset + chunk.SizeInBytes);
        }

        return info;
    }
}