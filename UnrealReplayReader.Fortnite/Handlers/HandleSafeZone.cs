using UnrealReplayReader.Fortnite.Models.Exports;
using UnrealReplayReader.Fortnite.Models.Replay;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Handlers;

public class HandleSafeZone
{
    public static void OnProperties(SafeZoneIndicatorExport model, Actor actor, MatchData match, StateData states)
    {
        var newSafeZone = states.SafeZones.LastOrDefault();
        if (model.SafeZoneFinishShrinkTime != null)
        {
            if (newSafeZone != null)
            {
                newSafeZone.PlayersAlive = states.GameState.RemainingBots + states.GameState.RemainingPlayers;
            }

            newSafeZone = new SafeZone();

            states.SafeZones.Add(newSafeZone);
            match.SafeZones.Add(newSafeZone);

            newSafeZone.Radius = model.Radius ?? newSafeZone.Radius;
            newSafeZone.NextNextRadius = model.NextNextRadius ?? newSafeZone.NextNextRadius;
            newSafeZone.NextRadius = model.NextRadius ?? newSafeZone.NextRadius;
            newSafeZone.ShringEndTime = model.SafeZoneFinishShrinkTime ?? newSafeZone.ShringEndTime;
            newSafeZone.ShrinkStartTime = model.SafeZoneStartShrinkTime ?? newSafeZone.ShrinkStartTime;
            newSafeZone.LastCenter = model.LastCenter ?? newSafeZone.LastCenter;
            newSafeZone.NextCenter = model.NextCenter ?? newSafeZone.NextCenter;
            newSafeZone.NextNextCenter = model.NextNextCenter ?? newSafeZone.NextNextCenter;
        }

        newSafeZone.CurrentRadius = model.Radius ?? newSafeZone.CurrentRadius;
    }
}