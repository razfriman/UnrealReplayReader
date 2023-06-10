using UnrealReplayReader.Exceptions;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private const uint NetworkMagic = 0x2CF5A13D;

    private FNetworkDemoHeader ReadHeader(ByteReader reader, FLocalFileReplayInfo replayInfo)
    {
        var header = new FNetworkDemoHeader();
        var chunk = replayInfo.Chunks[replayInfo.HeaderChunkIndex];
        reader.Seek(chunk.DataOffset);
        header.Magic = reader.ReadUInt32();
        if (header.Magic != NetworkMagic)
        {
            throw new ReplayException("Header.Magic != NETWORK_DEMO_MAGIC");
        }

        header.Version = reader.ReadUInt32AsEnum<ENetworkVersionHistory>();
        reader.NetworkVersion = header.Version;

        if (reader.NetworkVersion >= ENetworkVersionHistory.HistoryUseCustomVersion)
        {
            var customVersionCount = reader.ReadInt32();
            for (var i = 0; i < customVersionCount; i++)
            {
                var guid = reader.ReadGuid();
                var version = reader.ReadInt32();
            }
        }

        header.NetworkChecksum = reader.ReadUInt32();
        header.EngineNetworkProtocolVersion = reader.ReadUInt32AsEnum<EEngineNetworkVersionHistory>();
        reader.EngineNetworkVersion = header.EngineNetworkProtocolVersion;

        header.GameNetworkProtocolVersion = reader.ReadUInt32();

        if (header.Version >= ENetworkVersionHistory.HistoryHeaderGuid)
        {
            header.Guid = reader.ReadGuid();
        }

        header.EngineVersion = new FEngineVersion
        {
            Major = reader.ReadUInt16(),
            Minor = reader.ReadUInt16(),
            Patch = reader.ReadUInt16(),
            Changelist = reader.ReadUInt32(),
            Branch = reader.ReadFString(),
        };
        reader.NetworkReplayVersion = header.EngineVersion;

        if (header.Version >= ENetworkVersionHistory.HistoryRecordingMetadata)
        {
            header.PackageVersionUe = new FPackageFileVersion
            {
                FileVersionUe4 = reader.ReadUInt32AsEnum<EUnrealEngineObjectUe4Version>(),
                FileVersionUe5 = reader.ReadUInt32AsEnum<EUnrealEngineObjectUe5Version>()
            };
            header.PackageVersionLicenseeUe = reader.ReadInt32();
        }

        header.LevelNamesAndTimes = reader.ReadList(x => new FLevelNameAndTime
        {
            LevelName = x.ReadFString(),
            LevelChangeTimeInMs = x.ReadUInt32()
        });
        if (header.Version >= ENetworkVersionHistory.HistoryHeaderFlags)
        {
            header.HeaderFlags = reader.ReadUInt32AsEnum<EReplayHeaderFlags>();
            reader.ReplayHeaderFlags = header.HeaderFlags;
        }

        header.GameSpecificData = reader.ReadList(x => x.ReadFString());

        if (header.Version >= ENetworkVersionHistory.HistorySavePackageVersionUe)
        {
            header.MinRecordHz = reader.ReadSingle();
            header.MaxRecordHz = reader.ReadSingle();
            header.FrameLimitInMs = reader.ReadSingle();
            header.CheckpointLimitInMs = reader.ReadSingle();
            header.Platform = reader.ReadFString();
            header.BuildConfig = reader.ReadByteAsEnum<EBuildConfiguration>();
            header.BuildTarget = reader.ReadByteAsEnum<EBuildTargetType>();
        }

        return header;
    }
}