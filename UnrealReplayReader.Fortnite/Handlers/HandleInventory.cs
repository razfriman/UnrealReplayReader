using UnrealReplayReader.Fortnite.Models.Exports;
using UnrealReplayReader.Fortnite.Models.Replay;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Handlers;

public static class HandleInventory
{
    public static void OnProperties(InventoryExport model, Actor actor, MatchData match, StateData states)
    {
        if (!states.Inventories.ContainsKey(actor.ActorId))
        {
            states.Inventories[actor.ActorId] = new Inventory
            {
                Id = actor.ActorId,
                ReplayPawn = model.ReplayPawn,
            };
            return;
        }

        if (model.ReplayPawn != null)
        {
            // Console.WriteLine(actor.ActorId + " to " + model.ReplayPawn);
            states.Inventories[actor.ActorId].ReplayPawn = model.ReplayPawn;
        }
    }

    public static void OnNetDelta(InventoryExport? model, int elementIndex,
        bool isDeleted, Bunch bunch, MatchData match, StateData states)
    {
        if (!states.Inventories.ContainsKey(bunch.ActorId))
        {
            return;
        }

        var inventory = states.Inventories[bunch.ActorId];
        if (isDeleted)
        {
            var item = inventory.Items[elementIndex];
            // Console.WriteLine($"DROPPED {item.Item?.Name}:{item.Count}");
            inventory.Items.Remove(elementIndex);
            return;
        }


        if (inventory.PlayerId == null && inventory.ReplayPawn != null)
        {
            if (states.PlayerPawns.ContainsKey(inventory.ReplayPawn.Value))
            {
                var pawn = states.PlayerPawns[inventory.ReplayPawn.Value];
                var playerState = pawn.PlayerState;
                if (states.Players.ContainsKey(playerState.Value))
                {
                    var player = states.Players[playerState.Value];
                    player.Inventory = inventory;
                    inventory.PlayerId = playerState.Value;
                }
            }
        }

        if (model.ItemDefinition == null)
        {
            return;
        }

        if (inventory.Items.ContainsKey(elementIndex))
        {
            var item = inventory.Items[elementIndex];
            item.Count = model.Count ?? item.Count;
            item.Ammo = model.LoadedAmmo ?? item.Ammo;
            return;
        }

        inventory.Items[elementIndex] = new InventoryItem
        {
            Count = model.Count,
            Ammo = model.LoadedAmmo,
            Item = model.ItemDefinition,
        };
    }
}