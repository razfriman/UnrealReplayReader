using UnrealReplayReader.Fortnite;
using UnrealReplayReader.Fortnite.Models.Exports;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Tests;

public class FortniteReplayReaderTest
{
    [Fact]
    public void TestReadReplay()
    {
        var settings = new ReplayReaderSettings
        {
            IsDebug = true,
            Logger = null,
            UseCheckpoints = false,
            ExportConfiguration = ReplayExportConfiguration.FromAssembly(typeof(GameStateExport))
        };
        var replay = FortniteReplayReader.FromFile("Replays/Chapter4_Season5.replay", settings);
        
        Assert.Equal("++Fortnite+Release-25.00", replay.Header.EngineVersion.Branch);
        Assert.Equal(ENetworkVersionHistory.HistoryUseCustomVersion, replay.Header.Version);
        Assert.Equal("WindowsClient", replay.Header.Platform);
        Assert.Equal(330, replay.ExportGroupDict.Count);
        var match = replay.MatchData;
        var state = replay.StateData;
        var events = replay.Events;
        Assert.Equal(116, match.Players.Count);
        Assert.Equal(116, state.Players.Count);
        Assert.Equal(36u, events.MatchStats?.WeaponDamage);
    }
}