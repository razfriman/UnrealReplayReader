using Microsoft.Extensions.Logging;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    public void ReadClassNetCache(
        NetBitReader reader,
        Bunch bunch,
        string staticActorId,
        NetFieldExportGroup group)
    {
        while (true)
        {
            var result = ReadFieldHeader(reader, group);

            if (result == null)
            {
                break;
            }

            var field = result.Value.OutField;
            var numPayloadBits = result.Value.NumPayloadBits;

            if (field == null)
            {
                reader.SkipBits((int)numPayloadBits);
                continue;
            }

            if (Settings.IsDebug)
            {
                Replay.ExportGroupDict[group.PathName][field.Name].Add(numPayloadBits);
            }

            if (field.Configuration == null)
            {
                reader.SkipBits((int)numPayloadBits);
                continue;
            }

            reader.SetTempEnd((int)numPayloadBits, FBitArchiveEndIndex.FieldHeaderPayload);

            switch (field.Configuration.ParseType)
            {
                case ParseTypes.Function:
                    ReadFunction(reader, group, field, bunch, staticActorId);
                    break;
                case ParseTypes.Class:
                    ReceiveCustomProperty(reader, group, field, bunch, staticActorId);
                    break;
                case ParseTypes.NetDeltaSerialize:
                    ReadNetDeltaSerialize(reader, bunch, staticActorId, group, field);
                    break;
            }

            reader.RestoreTempEnd(FBitArchiveEndIndex.FieldHeaderPayload);
        }
    }

    private void ReadNetDeltaSerialize(NetBitReader reader, Bunch bunch, string staticActorId,
        NetFieldExportGroup group,
        NetFieldExport field)
    {
        var exportGroup = GuidCache.getNFEReference(field.Configuration.Type);

        if (exportGroup == null)
        {
            reader.RestoreTempEnd(FBitArchiveEndIndex.FieldHeaderPayload);
            Logger?.LogWarning(
                $"class net cache {field.Name} from {group.PathName} has been declared but has no export group");
            return;
        }

        if (!NetFieldParser.WillReadType(exportGroup.PathName))
        {
            reader.RestoreTempEnd(FBitArchiveEndIndex.FieldHeaderPayload);
            return;
        }

        NetDeltaSerialize(
            reader,
            exportGroup,
            bunch,
            field.Configuration.EnablePropertyChecksum ?? false,
            staticActorId);
    }
}