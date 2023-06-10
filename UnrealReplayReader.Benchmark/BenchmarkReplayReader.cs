using BenchmarkDotNet.Attributes;
using UnrealReplayReader.Fortnite;
using UnrealReplayReader.Fortnite.Models.Exports;
using UnrealReplayReader.FortniteMinimal;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Benchmark;

[MemoryDiagnoser]
[SimpleJob(1, 10, 10)]
public class BenchmarkReplayReader
{
    public const string ReplayFile = "Replays/Chapter4_Season5.replay";

    public static readonly ReplayReaderSettings Settings = new()
    {
        IsDebug = false,
        Logger = null,
        UseCheckpoints = false,
        ExportConfiguration = ReplayExportConfiguration.FromAssembly(typeof(GameStateExport))
    };
    
    
    private static readonly ReplayReaderSettings SettingsMinimal = new()
    {
        IsDebug = false,
        Logger = null,
        UseCheckpoints = false,
        ExportConfiguration = FortniteMinimalReplayReader.ExportConfiguration
    };
    
    [Benchmark]
    public void ParseReplayForniteReplayReader()
    {
        var replay = FortniteReplayReader.FromFile(ReplayFile, Settings);
    }
    
    [Benchmark]
    public void ParseReplayForniteMinimalReplayReader()
    {
        var replay = FortniteMinimalReplayReader.FromFile(ReplayFile, SettingsMinimal);
    }
}