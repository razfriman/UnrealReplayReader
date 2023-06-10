using UnrealReplayReader.Fortnite.Models.Exports;
using UnrealReplayReader.Fortnite.Models.Replay;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Handlers;

public class HandleSupplyDrop
{
    public static void OnProperties(SupplyDropExport model, Actor actor, MatchData match, StateData states)
    {
        if (!states.SupplyDrops.ContainsKey(actor.ActorId))
        {
            var newSupplyDrop = new SupplyDrop
            {
                SpawnTime = states?.GameState?.DeltaGameTime ?? 0
            };
            states.SupplyDrops[actor.ActorId] = newSupplyDrop;
            match.SupplyDrops.Add(newSupplyDrop);
        }

        var supplyDrop = states.SupplyDrops[actor.ActorId];
        supplyDrop.Location = model.LandingLocation ?? supplyDrop.Location;
    }
}