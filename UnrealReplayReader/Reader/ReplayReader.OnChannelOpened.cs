using Microsoft.Extensions.Logging;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    public void OnChannelOpened(Channel channel, Actor? actor, Bunch bunch)
    {
        var chIndex = bunch.ChIndex;
        try
        {
            EmitChannelOpened(channel, actor);
        }
        catch (Exception e)
        {
            Logger?.LogError(e, "Error while emitting ChannelOpened: {Channel}", channel?.ChIndex);
        }

        if (actor == null)
        {
            return;
        }

        ActorToChannel[actor.ActorNetGuid.Value] = chIndex;
        ChannelToActor[chIndex] = actor.ActorNetGuid.Value;

        NetFieldExportGroup? group = null;
        string? staticActorId = null;

        if (DormantActors.GetValueOrDefault(actor.ActorNetGuid.Value, false))
        {
            DormantActors[actor.ActorNetGuid.Value] = false;
            return;
        }

        var repObject = 0u;

        if (actor?.ActorNetGuid.IsDynamic ?? false)
        {
            group = GuidCache.GetNetFieldExportGroup(actor.Archetype.Value);
            repObject = actor.Archetype.Value;
        }
        else if (actor != null)
        {
            var result = GuidCache.GetStaticActorExportGroup(actor.ActorNetGuid.Value);
            group = result.group;
            staticActorId = result.staticActorId;
            repObject = actor.ActorNetGuid.Value;
        }

        if (group == null)
        {
            return;
        }

        if (Settings.EnableActorToPath)
        {
            var path = GuidCache.TryGetFullPathName(repObject);
            ActorToPath[actor.ActorNetGuid.Value] = path;
        }

        try
        {
            EmitActorSpawn(group, actor, staticActorId);
        }
        catch (Exception e)
        {
            Logger?.LogError(e, "Error while emitting ActorSpawn: {Actor}", actor?.ActorNetGuid?.Value);
        }
    }
}