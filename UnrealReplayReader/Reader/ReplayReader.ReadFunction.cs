using Microsoft.Extensions.Logging;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    public bool ReadFunction(NetBitReader reader, NetFieldExportGroup group, NetFieldExport field, Bunch bunch,
        string staticActorId)
    {
        if (reader.Available > 0)
        {
            if (field?.Configuration?.Type == null)
            {
                Logger.LogError("No type set for function with data. Name: {} Data: {}", field.Name, reader.Available);
                return false;
            }

            var exportGroup = GuidCache.getNFEReference(field.Configuration.Type);

            if (exportGroup == null)
            {
                reader.RestoreTempEnd(FBitArchiveEndIndex.FieldHeaderPayload);
                return true;
            }

            var properties = ReceiveProperties(reader, exportGroup, bunch, staticActorId, true, false);
            if (properties.HasValue)
            {
                EmitFunction(properties.Value.instance, properties.Value.group, field, bunch,
                    properties.Value.changedProperties, staticActorId);
            }

            if (!reader.AtEnd)
            {
                reader.RestoreTempEnd(FBitArchiveEndIndex.FieldHeaderPayload);
                return true;
            }
        }
        else
        {
            EmitFunction(null, group, field, bunch, new List<string>(), staticActorId);
            if (!reader.AtEnd)
            {
                reader.RestoreTempEnd(FBitArchiveEndIndex.FieldHeaderPayload);
                return true;
            }
        }

        return false;
    }
}