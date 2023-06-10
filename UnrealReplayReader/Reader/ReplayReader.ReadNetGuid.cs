using UnrealReplayReader.Extensions;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    public static int A = 0;

    public NetworkGuid ReadNetGuid(FReader reader, bool isExportingNetGuidBunch,
        int internalLoadObjectRecursionCount = 0)
    {
        if (internalLoadObjectRecursionCount > 16)
        {
            return new NetworkGuid();
        }

        var netGuid = new NetworkGuid();

        netGuid.Value = reader.ReadIntPacked();

        if (!netGuid.IsValid)
        {
            return netGuid;
        }

        if (netGuid.IsDefault || isExportingNetGuidBunch)
        {
            var flags = reader.ReadByte();

            if ((flags & 1) == 1)
            {
                netGuid.Outer = ReadNetGuid(reader, true, internalLoadObjectRecursionCount + 1);

                var pathName = reader.ReadFString();

                if ((flags & 4) == 4)
                {
                    netGuid.Checksum = reader.ReadUInt32();
                }

                if (isExportingNetGuidBunch)
                {
                    var cleanedPath = pathName.RemovePathPrefix();
                    netGuid.Path = cleanedPath;
                    netGuid.Outer = GuidCache.NetGuids.GetValueOrDefault(netGuid.Outer.Value, null);
                    GuidCache.NetGuids[netGuid.Value] = netGuid;

                    if (Settings.IsDebug)
                    {
                        DebugNetGuidToPathName.Add(netGuid);
                    }
                }

                return netGuid;
            }
        }

        return netGuid;
    }
}