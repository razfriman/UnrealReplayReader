using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Reader;

public record struct FieldHeader
{
    public uint NumPayloadBits { get; set; }
    public NetFieldExport? OutField { get; set; }
}

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    public FieldHeader? ReadFieldHeader(BitReader archive, NetFieldExportGroup group)
    {
        if (archive.AtEnd)
        {
            return null;
        }

        var netFieldExportHandle = archive.ReadSerializedInt((int)Math.Max(group.NetFieldExportsLength, 2));
        var numPayloadBits = archive.ReadIntPacked();

        var outField = group.NetFieldExports.GetValueOrDefault(netFieldExportHandle, null);

        if (archive.IsError || !archive.CanReadBits((int)numPayloadBits))
        {
            return null;
        }

        return new FieldHeader
        {
            NumPayloadBits = numPayloadBits,
            OutField = outField,
        };
    }
}