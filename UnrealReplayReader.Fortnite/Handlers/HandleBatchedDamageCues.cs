using UnrealReplayReader.Fortnite.Models.Enums;
using UnrealReplayReader.Fortnite.Models.Exports;
using UnrealReplayReader.Fortnite.Models.Replay;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Handlers;

public static class HandleBatchedDamageCues
{
    public static void OnProperties(BatchedDamageCuesExport model, Actor actor, MatchData match, StateData states)
    {
        if (states.GameState?.GamePhase is EAthenaGamePhase.Warmup or EAthenaGamePhase.Setup)
        {
            // Ignore damage before the game starts
            return;
        }
        var hitPlayerPawn = states.PlayerPawns.GetValueOrDefault(model.HitActor, null);
        var pawn = states.PlayerPawns.GetValueOrDefault(actor.ActorId, null);
        var shot = new WeaponShot
        {
            ShotByPlayerPawn = pawn,
            HitPlayerPawn = hitPlayerPawn,
            Location = model.Location,
            Damage = model.Magnitude,
            Normal = model.Normal,
            DeltaGameTimeSeconds = match.GameState.DeltaGameTime
        };
        hitPlayerPawn?.DamageTaken.Add(shot);
        pawn?.Shots.Add(shot);
    }
}