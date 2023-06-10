using UnrealReplayReader.Fortnite.Models.Exports;
using UnrealReplayReader.Fortnite.Models.Replay;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Handlers;

public class HandleFortClientObservedStat
{
    public static void OnNetDelta(FortClientObservedStatExport? model, int elementIndex,
        bool isDeleted, Bunch bunch, MatchData match, StateData states)
    {
        if (isDeleted)
        {
            
        }
        else
        {
            var pawn = states.PlayerPawns.GetValueOrDefault(bunch.ActorId, null);
            pawn.ClientStats[model.StatName] = model.StatValue ?? 0;    
        }
    }
}