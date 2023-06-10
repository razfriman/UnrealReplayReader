using UnrealReplayReader.Fortnite.Models.Exports;
using UnrealReplayReader.Fortnite.Models.Replay;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Handlers;

public static class HandleLabrador
{
    public static void OnProperties(LabradorExport model, Actor actor, MatchData match, StateData states)
    {
        if (!model.PawnUniqueId.HasValue)
        {
            return;
        }

        if (!states.Labradors.ContainsKey(model.PawnUniqueId.Value))
        {
            var newLabrador = new LabradorPawn();
            states.Labradors[model.PawnUniqueId.Value] = newLabrador;
            match.Labradors.Add(newLabrador);
        }

        var llama = states.Labradors[model.PawnUniqueId.Value];
        llama.Location = actor?.Location ?? llama.Location;
        llama.Rotation = actor?.Rotation ?? llama.Rotation;
    }
}