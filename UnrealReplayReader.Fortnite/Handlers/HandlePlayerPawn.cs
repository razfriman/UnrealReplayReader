using UnrealReplayReader.Fortnite.Models.Exports;
using UnrealReplayReader.Fortnite.Models.Replay;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Handlers;

public class HandlePlayerPawn
{
    public static void OnActorSpawn(PlayerPawnExport model, Actor actor, MatchData match,
        StateData states)
    {
        var pawn = new PlayerPawn();
        states.Pawns[actor.ActorNetGuid.Value] = pawn;
        states.PlayerPawns[actor.ActorNetGuid.Value] = pawn;
    }

    public static void OnProperties(PlayerPawnExport model, Actor actor, MatchData match,
        StateData states)
    {
        if (!states.PlayerPawns.ContainsKey(actor.ActorId))
        {
            return;
        }

        var pawn = states.PlayerPawns[actor.ActorId];
        pawn.Vehicle = model.Vehicle ?? pawn.Vehicle;
        pawn.ReplayLastTransformUpdateTimeStamp =
            model.ReplayLastTransformUpdateTimeStamp ?? pawn.ReplayLastTransformUpdateTimeStamp;
        pawn.BIsSkydiving = model.BIsSkydiving ?? pawn.BIsSkydiving;
        pawn.BIsParachuteOpen = model.BIsParachuteOpen ?? pawn.BIsParachuteOpen;
        pawn.PlayerState = model.PlayerState ?? pawn.PlayerState;
        pawn.Character = model.Character ?? pawn.Character;
        pawn.SkyDiveContrail = model.SkyDiveContrail ?? pawn.SkyDiveContrail;
        pawn.Glider = model.Glider ?? pawn.Glider;
        pawn.Pickaxe = model.Pickaxe ?? pawn.Pickaxe;
        pawn.MusicPack = model.MusicPack ?? pawn.MusicPack;
        pawn.LoadingScreen = model.LoadingScreen ?? pawn.LoadingScreen;
        pawn.Backpack = model.Backpack ?? pawn.Backpack;
        pawn.PetSkin = model.PetSkin ?? pawn.PetSkin;
        pawn.ItemWraps = model.ItemWraps ?? pawn.ItemWraps;
        pawn.Dances = model.Dances ?? pawn.Dances;
        pawn.BannerIconId = model.BannerIconId ?? pawn.BannerIconId;
        pawn.BannerColorId = model.BannerColorId ?? pawn.BannerColorId;
        pawn.bIsDefaultCharacter = model.bIsDefaultCharacter ?? pawn.bIsDefaultCharacter;
        pawn.ReplicatedMovement = model.ReplicatedMovement ?? pawn.ReplicatedMovement;

        if (pawn.PlayerState != null && !pawn.ResolvedPlayer && states.Players.ContainsKey(pawn.PlayerState.Value))
        {
            var playerState = states.Players[pawn.PlayerState.Value];
            playerState.PlayerPawn = pawn;
            pawn.ResolvedPlayer = true;
        }

        if (pawn.PlayerState == null)
        {
            return;
        }

        var player = states.Players.GetValueOrDefault(pawn.PlayerState.Value, null);
        if (player == null)
        {
            return;
        }
    }
}