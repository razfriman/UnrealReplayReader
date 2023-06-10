using UnrealReplayReader.FortniteMinimal;
using UnrealReplayReader.FortniteMinimal.Models;
using UnrealReplayReader.FortniteMinimal.Models.Exports;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Tests;

public class FortniteMinimalReplayReaderTest
{
    [Fact]
    public void TestReadReplay()
    {
        var expectedMatch = new Match
        {
            Player = new Player
            {
                Id = "795f8ecd0b7b466e818cbe1c2b3e66cc",
                DisplayName = "phoenix1074"
            },
            Playlist = "Playlist_NoBuildBR_Solo",
            MatchId = "140c02856903406fbd7ae310af14ba67",
            MatchTime = new DateTime(638218702017500000)
        };
        var settings = new ReplayReaderSettings
        {
            IsDebug = true,
            Logger = null,
            UseCheckpoints = false,
            ExportConfiguration = ReplayExportConfiguration.FromAssembly(typeof(GameState))
        };
        var replay = FortniteMinimalReplayReader.FromFile("Replays/Chapter4_Season5.replay", settings);
        
        Assert.Equal("++Fortnite+Release-25.00", replay.Header.EngineVersion.Branch);
        Assert.Equal(ENetworkVersionHistory.HistoryUseCustomVersion, replay.Header.Version);
        Assert.Equal("WindowsClient", replay.Header.Platform);
        Assert.Equal(330, replay.ExportGroupDict.Count);
        Assert.Equal(expectedMatch, replay.Match);
    }
}