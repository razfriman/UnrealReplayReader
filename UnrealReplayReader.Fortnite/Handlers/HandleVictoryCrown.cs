using UnrealReplayReader.Fortnite.Models.Exports;
using UnrealReplayReader.Fortnite.Models.Replay;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Handlers;

public class HandleVictoryCrown
{
    public static void OnProperties(VictoryCrownGameStateExport model, Actor actor, MatchData match, StateData states)
    {
        if (model.CrownBearerPlayerStates != null)
        {
            foreach (var key in states.VictoryCrownPlayerStateDict.Keys.ToList())
            {
                if (key >= model.CrownBearerPlayerStates.Length)
                {
                    states.VictoryCrownPlayerStateDict.Remove(key);
                }
            } 
            for (var index = 0; index < model.CrownBearerPlayerStates.Length; index++)
            {
                var playerState = model.CrownBearerPlayerStates[index];
                if (playerState == null)
                {
                    continue;
                }
                
                states.VictoryCrownPlayerStateDict[index] = playerState.Value;
            }
        }
    }
}