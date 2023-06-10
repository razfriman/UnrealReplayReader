using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Models;

public record FNetworkDemoHeader
{
    public uint Magic { get; set; }
    public ENetworkVersionHistory Version { get; set; }
    public uint NetworkChecksum { get; set; }
    public EEngineNetworkVersionHistory EngineNetworkProtocolVersion { get; set; }
    public uint GameNetworkProtocolVersion { get; set; }
    public string Guid { get; set; }
    public float MinRecordHz { get; set; }
    public float MaxRecordHz { get; set; }
    public float FrameLimitInMs { get; set; }
    public float CheckpointLimitInMs { get; set; }
    public string Platform { get; set; }
    public EBuildConfiguration BuildConfig { get; set; }
    public EBuildTargetType BuildTarget { get; set; }
    public FEngineVersion EngineVersion { get; set; }
    public EReplayHeaderFlags HeaderFlags { get; set; }
    public List<FLevelNameAndTime> LevelNamesAndTimes { get; set; } = new();
    public List<string> GameSpecificData { get; set; } = new();
    public FPackageFileVersion PackageVersionUe { get; set; }
    public int PackageVersionLicenseeUe { get; set; }
}