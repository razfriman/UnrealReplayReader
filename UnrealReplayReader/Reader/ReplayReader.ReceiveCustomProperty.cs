using Microsoft.Extensions.Logging;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private void ReceiveCustomProperty(NetBitReader reader, NetFieldExportGroup group, NetFieldExport field,
        Bunch bunch,
        string staticActorId)
    {
        var instance = field.Configuration.CreateInstance();
        field.Configuration.ReadGeneric(Replay, instance, reader, group, field);
        var changedProperties = new List<string> { field.Name };
        try
        {
            EmitProperties(instance, group, bunch, changedProperties, null, field, staticActorId);
        }
        catch (Exception e)
        {
            Logger?.LogError(e, "Error while exporting Property: {Property}",
                field?.Name ?? field?.Handle.ToString());
        }
    }
}