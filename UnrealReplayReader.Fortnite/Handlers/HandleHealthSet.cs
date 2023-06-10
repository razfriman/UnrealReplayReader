using UnrealReplayReader.Fortnite.Models.Classes;
using UnrealReplayReader.Fortnite.Models.Exports;
using UnrealReplayReader.Fortnite.Models.Replay;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Handlers;

public class HandleHealthSet
{
    public static void OnProperties(HealthSetExport model, Actor actor, MatchData match, StateData states)
    {
        if (!states.Players.ContainsKey(actor.ActorId))
        {
            return;
        }

        var player = states.Players[actor.ActorId];
        HandleNewSets(player, model);
        var hasNewData = false;
        hasNewData |= HandleSet(player.Health, model);
        hasNewData |= HandleSet(player.Shield, model);
        hasNewData |= HandleSet(player.OverShield, model);
    }

    private static void HandleNewSets(PlayerState player, HealthSetExport model)
    {
        var maximums = model.Maximum.ToList();
        for (var i = 0; i < maximums.Count; i++)
        {
            var fortSet = new FortSet
            {
                StartingHandle = maximums[i].Key - 3,
            };
            switch (i)
            {
                case 0:
                    player.Health = fortSet;
                    break;
                case 1:
                    player.Shield = fortSet;
                    break;
                case 2:
                    player.OverShield = fortSet;
                    break;
            }
        }
    }

    private static bool HandleSet(FortSet? set, HealthSetExport model)
    {
        if (set == null)
        {
            return false;
        }

        var hasNewData = false;
        if (model.BaseValue.ContainsKey(set.StartingHandle))
        {
            set.BaseValue = model.BaseValue[set.StartingHandle];
            hasNewData = true;
        }

        if (model.CurrentValue.ContainsKey(set.StartingHandle + 1))
        {
            set.CurrentValue = model.CurrentValue[set.StartingHandle + 1];
            hasNewData = true;
        }

        if (model.Maximum.ContainsKey(set.StartingHandle + 3))
        {
            set.Maximum = model.Maximum[set.StartingHandle + 3];
            hasNewData = true;
        }

        return hasNewData;
    }
}