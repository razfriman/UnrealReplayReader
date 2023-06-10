using UnrealReplayReader.Fortnite.Models.Exports;
using UnrealReplayReader.Fortnite.Models.Replay;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Handlers;

public class HandleLlama
{
    public static void OnProperties(LlamaExport model, Actor actor, MatchData match, StateData states)
    {
        if (!states.Llamas.ContainsKey(actor.ActorId))
        {
            var newLlama = new Llama();
            states.Llamas[actor.ActorId] = newLlama;
            match.Llamas.Add(newLlama);
        }

        var llama = states.Llamas[actor.ActorId];
        llama.Location = actor?.Location ?? llama.Location;
    }
}