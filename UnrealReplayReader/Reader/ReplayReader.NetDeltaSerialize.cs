using Microsoft.Extensions.Logging;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private void NetDeltaSerialize(NetBitReader reader, NetFieldExportGroup group, Bunch bunch,
        bool enablePropertyChecksum, string staticActorId)
    {
        var exportName = group.PathName;

        if (reader.EngineNetworkVersion >= EEngineNetworkVersionHistory.HistoryFastArrayDeltaStruct &&
            !reader.ReadBit())
        {
            return;
        }

        var header = new
        {
            arrayReplicationKey = reader.ReadInt32(),
            baseReplicationKey = reader.ReadInt32(),
            numDeletes = reader.ReadInt32(),
            numChanged = reader.ReadInt32(),
        };

        if (reader.IsError)
        {
            return;
        }

        for (var i = 0; i < header.numDeletes; i++)
        {
            var elementIndex = reader.ReadInt32();
            try
            {
                EmitNetDelta(group.Configuration.CreateInstance(), group, elementIndex, true, bunch);
            }
            catch (Exception e)
            {
                Logger?.LogError(e, "Error while exporting netDelta {ExportName}", exportName);
            }
        }

        for (var i = 0; i < header.numChanged; i++)
        {
            var elementIndex = reader.ReadInt32();
            var properties = ReceiveProperties(
                reader,
                group,
                bunch,
                staticActorId,
                !enablePropertyChecksum,
                false);

            if (properties == null)
            {
                continue;
            }

            try
            {
                EmitNetDelta(properties.Value.instance, properties.Value.group, elementIndex, false, bunch);
            }
            catch (Exception e)
            {
                Logger?.LogError(e, "Error while exporting netDelta {ExportName}", exportName);
            }
        }
    }
}