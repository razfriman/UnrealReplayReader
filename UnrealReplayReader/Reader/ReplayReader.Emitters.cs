using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    public virtual void EmitProperties(ExportModel model, NetFieldExportGroup group, Bunch bunch,
        List<string> changedProperties, ExternalData? externalData = null,
        NetFieldExport? field = null, string? staticActorId = null)
    {
    }

    public virtual void EmitActorSpawn(NetFieldExportGroup exportGroup, Actor actor, string? staticActorId)
    {
    }

    public virtual void EmitActorDespawn(string exportName, Actor actor, string? staticActorId)
    {
    }

    public virtual void EmitChannelOpened(Channel channel, Actor actor)
    {
    }

    public virtual void EmitChannelClosed(Channel channel, Actor actor)
    {
    }

    public virtual void EmitFunction(ExportModel model, NetFieldExportGroup group, NetFieldExport field, Bunch bunch,
        List<string> changedProperties,
        string? staticActorId = null)
    {
    }

    public virtual void EmitNetDelta(ExportModel valueInstance, NetFieldExportGroup valueExportGroup, int elementIndex,
        bool isDeleted, Bunch bunch)
    {
    }

    public virtual void EmitReadReplayFinished()
    {
    }

    public virtual void EmitEvent(FLocalFileEventInfo chunk, ByteReader reader)
    {
    }
}