using UnrealReplayReader.Fortnite.Models.Enums;
using UnrealReplayReader.Fortnite.Models.Exports;
using UnrealReplayReader.Fortnite.Models.Replay;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Handlers;

public class HandleGameState
{
    public static void OnProperties(GameStateExport model, Actor actor, MatchData match, StateData states)
    {
        if (states.GameState == null)
        {
            var newGameState = new GameState();
            states.GameState = newGameState;
            match.GameState = newGameState;
        }

        states.GameState.GamePhase = model.GamePhase ?? states.GameState.GamePhase;
        states.GameState.AirCraftStartTime = model.AircraftStartTime ?? states.GameState.AirCraftStartTime;
        states.GameState.InitialSafeZoneStartTime =
            model.SafeZonesStartTime ?? states.GameState.InitialSafeZoneStartTime;
        states.GameState.SessionId = model.GameSessionId?.ToLower() ?? states.GameState.SessionId;
        states.GameState.MatchTime = model.UtcTimeStartedMatch?.Time ?? states.GameState.MatchTime;
        states.GameState.EEventTournamentRound =
            model.EventTournamentRound ?? states.GameState.EEventTournamentRound;
        states.GameState.LargeTeamGame = model.BIsLargeTeamGame ?? states.GameState.LargeTeamGame;
        states.GameState.MaxPlayers = model.TeamCount ?? states.GameState.MaxPlayers;
        states.GameState.MatchEndTime = model.EndGameStartTime ?? states.GameState.MatchEndTime;
        states.GameState.TotalTeams = model.ActiveTeamNums?.Length ?? states.GameState.TotalTeams;
        states.GameState.TotalBots = model.PlayerBotsLeft > states.GameState.TotalBots
            ? model.PlayerBotsLeft.Value
            : states.GameState.TotalBots;

        //Internal information to keep track of current state of the game
        states.GameState.CurrentWorldTime =
            model.ReplicatedWorldTimeSeconds ?? states.GameState.CurrentWorldTime;
        states.GameState.RemainingPlayers = model.PlayersLeft ?? states.GameState.RemainingPlayers;
        states.GameState.CurrentTeams = model.TeamsLeft ?? states.GameState.CurrentTeams;
        states.GameState.SafeZonePhase = model.SafeZonePhase ?? states.GameState.SafeZonePhase;
        states.GameState.RemainingBots = model.PlayerBotsLeft ?? states.GameState.RemainingBots;
        states.GameState.ElapsedTime = model.ElapsedTime ?? states.GameState.ElapsedTime;
        states.GameState.OldTeamSize = model.TeamSize ?? states.GameState.OldTeamSize;
        states.GameState.TotalPlayerStructures =
            model.TotalPlayerStructures ?? states.GameState.TotalPlayerStructures;
        states.GameState.RecorderActor = model.RecorderPlayerState?.Value ?? states.GameState.RecorderActor;


        if (states.GameState.GameWorldStartTime == 0)
        {
            states.GameState.GameWorldStartTime =
                model.ReplicatedWorldTimeSeconds ?? states.GameState.AirCraftStartTime;
        }

        if (model.TeamFlightPaths != null)
        {
            foreach (var flightPath in model.TeamFlightPaths)
            {
                states.GameState.BusPaths.Add(new Aircraft
                {
                    FlightRotation = flightPath.FlightStartRotation ?? new FRotator(),
                    FlightStartLocation = flightPath.FlightStartLocation ?? new FVector()
                });
            }
        }

        if (model.WinningTeam.HasValue)
        {
            states.GameState.WinningTeam = model.WinningTeam.Value;

            if (states.Teams.TryGetValue(states.GameState.WinningTeam, out var team))
            {
                foreach (var player in team.Players)
                {
                    if (player?.Place == 0)
                    {
                        player.Place = 1;
                    }
                }
            }
        }

        if (model.GamePhase == EAthenaGamePhase.SafeZones)
        {
            foreach (var playerState in states.VictoryCrownPlayerStateDict.Values)
            {
                var player = states.Players.GetValueOrDefault(playerState.Value, null);
                if (player != null)
                {
                    player.BLoadedWithVictoryCrown = true;
                }
            }
        }
    }

    public static void OnProperties(GameStateCache model, Actor actor, MatchData match, StateData states)
    {
        if (states.GameState == null)
        {
            var newGameState = new GameState();
            states.GameState = newGameState;
            match.GameState = newGameState;
        }

        if (model.CurrentPlaylistInfo != null)
        {
            states.GameState.PlaylistId ??= model.CurrentPlaylistInfo.Name;
        }
    }
}