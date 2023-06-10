using UnrealReplayReader.IO;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private NetFieldExport? ReadNetFieldExport(FReader reader)
    {
        var isExported = reader.ReadByte();

        if (isExported != 0)
        {
            var field = new NetFieldExport
            {
                Name = "",
                Handle = reader.ReadIntPacked(),
                CompatibleChecksum = reader.ReadUInt32(),
            };
            if (reader.EngineNetworkVersion < EEngineNetworkVersionHistory.HistoryNetexportSerialization)
            {
                field.Name = reader.ReadFString();
                field.OrigType = reader.ReadFString();
            }
            else if (reader.EngineNetworkVersion < EEngineNetworkVersionHistory.HistoryNetexportSerializeFix)
            {
                field.Name = reader.ReadFString();
            }
            else
            {
                field.Name = reader.ReadFName();
            }

            return field;
        }

        return null;
    }
}