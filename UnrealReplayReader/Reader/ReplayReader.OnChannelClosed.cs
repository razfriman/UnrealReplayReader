using Microsoft.Extensions.Logging;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    public void OnChannelClosed(Bunch bunch)
    {
        var channel = Channels[bunch.ChIndex];
        var actor = channel.Actor;

        if (!bunch.BDormant)
        {
            NetFieldExportGroup? group = null;
            string? staticActorId = null;

            if (channel.Actor?.ActorNetGuid.IsDynamic ?? false)
            {
                group = GuidCache.GetNetFieldExportGroup(channel.Actor.Archetype.Value);
            }
            else if (channel.Actor != null)
            {
                var result = GuidCache.GetStaticActorExportGroup(channel.Actor.ActorNetGuid.Value);
                group = result.group;
                staticActorId = result.staticActorId;
            }

            if (group != null)
            {
                try
                {
                    EmitActorDespawn(group.PathName, actor, staticActorId);
                }
                catch (Exception e)
                {
                    Logger?.LogError(e, "Error while emitting ActorDespawn: {ActorId}", actor?.ActorNetGuid?.Value);
                }
            }
        }
        else
        {
            DormantActors[actor.ActorNetGuid.Value] = true;
        }

        try
        {
            EmitChannelClosed(channel, actor);
        }
        catch (Exception e)
        {
            Logger?.LogError(e, "Error while emitting ChannelClosed: {ChannelId}", channel?.ChIndex);
        }

        IgnoredChannels.Remove(bunch.ChIndex);
        Channels.Remove(bunch.ChIndex);

        if (actor != null)
        {
            ActorToChannel.Remove(actor.ActorNetGuid.Value);
            ChannelToActor.Remove(bunch.ChIndex);
        }
    }
}