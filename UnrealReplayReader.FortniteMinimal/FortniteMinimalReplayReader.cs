using System.Text;
using UnrealReplayReader.FortniteMinimal.Models;
using UnrealReplayReader.FortniteMinimal.Models.Exports;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Classes;
using UnrealReplayReader.Reader;

namespace UnrealReplayReader.FortniteMinimal;

public class FortniteMinimalReplayReader : ReplayReader<FortniteMinimalReplay>
{
    public static readonly ReplayExportConfiguration ExportConfiguration =
        ReplayExportConfiguration.FromAssembly(typeof(FortniteMinimalReplayReader));

    private ActorGuid? _recorderPlayerState;
    private readonly Dictionary<uint, Player> _players = new();

    public static FortniteMinimalReplay FromFile(string file, ReplayReaderSettings settings)
    {
        var reader = new FortniteMinimalReplayReader();
        var replay = reader.ReadFile(file, settings);
        return replay;
    }

    public override void EmitProperties(ExportModel model, NetFieldExportGroup group, Bunch bunch, List<string> changedProperties, ExternalData? externalData = null,
        NetFieldExport? field = null, string? staticActorId = null)
    {
        switch (model)
        {
            case GameState gameState:
                HandleGameState(gameState);
                break;
            case GameStateCache gameStateCache:
                HandleGameStateCache(gameStateCache);
                break;
            case PlayerState playerState:
                HandlePlayerState(bunch, externalData, playerState);
                break;
        }
    }

    private void HandlePlayerState(Bunch bunch, ExternalData? externalData, PlayerState playerState)
    {
        var isNewPlayer = !_players.ContainsKey(bunch.ActorId);
        if (isNewPlayer)
        {
            var newPlayer = new Player();
            _players[bunch.ActorId] = newPlayer;
            if (_recorderPlayerState?.Value == bunch.ActorId)
            {
                Replay.Match.Player = newPlayer;
            }
        }

        var player = _players[bunch.ActorId];
        player.Id ??= playerState.UniqueId?.ToLower();

        if (playerState.Ping.HasValue)
        {
            Replay.Match.Player ??= player;
        }

        if (playerState.PlayerNamePrivate != null)
        {
            if (externalData?.IsEncrypted ?? false)
            {
                player.DisplayName = DecryptPlayerName(playerState);
            }
            else
            {
                player.DisplayName = playerState.PlayerNamePrivate;
            }
        }
    }

    private static string DecryptPlayerName(PlayerState playerState)
    {
        var rawName = playerState.PlayerNamePrivate;
        var builder = new StringBuilder();
        for (var i = 0; i < playerState.PlayerNamePrivate.Length; i++)
        {
            var shift = (rawName.Length % 4 * 3 % 8 + 1 + i) * 3 % 8;
            var characterValue = rawName[i] + shift;
            builder.Append((char)characterValue);
        }

        return builder.ToString();
    }

    private void HandleGameStateCache(GameStateCache gameStateCache)
    {
        Replay.Match.Playlist ??= gameStateCache.CurrentPlaylistInfo?.Name;
    }

    private void HandleGameState(GameState gameState)
    {
        Replay.Match.MatchId ??= gameState.GameSessionId;
        Replay.Match.MatchTime ??= gameState.UtcTimeStartedMatch?.Time;
        _recorderPlayerState ??= gameState.RecorderPlayerState;
    }
}