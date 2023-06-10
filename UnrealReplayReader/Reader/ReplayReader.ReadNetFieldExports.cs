using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private void ReadNetFieldExports(FReader reader)
    {
        var isBitReader = reader is BitReader;
        var numLayoutCmdExports = isBitReader ? reader.ReadUInt32() : reader.ReadIntPacked();
        for (var i = 0; i < numLayoutCmdExports; i++)
        {
            var pathNameIndex = reader.ReadIntPacked();
            var isExported = reader is BitReader bitReader
                ? bitReader.ReadBit()
                : reader.ReadIntPacked() == 1;
            NetFieldExportGroup? group;
            if (isExported)
            {
                var pathname = reader.ReadFString();
                var numExports = isBitReader
                    ? reader.ReadUInt32()
                    : reader.ReadIntPacked();
                group = GuidCache.NetFieldExportGroupMap.GetValueOrDefault(pathname, null);

                if (group == null)
                {
                    var theGroup = new NetFieldExportGroup
                    {
                        PathName = pathname,
                        PathNameIndex = pathNameIndex,
                        NetFieldExportsLength = numExports,
                    };
                    group = GuidCache.AddToExportGroupMap(pathname, theGroup);
                }
                else if (group.NetFieldExportsLength == 0)
                {
                    group.NetFieldExportsLength = numExports;
                    group.PathNameIndex = pathNameIndex;
                    GuidCache.NetFieldExportGroupIndexToGroup[pathNameIndex] = pathname;
                }
            }
            else
            {
                group = GuidCache.GetNetFieldExportGroupFromIndex(pathNameIndex);
            }

            var field = ReadNetFieldExport(reader);

            if (field == null || group == null)
            {
                continue;
            }

            AddDebugExport(group.PathName, field.Name);


            var groupConfiguration = NetFieldParser.GetGroupConfiguration(group.PathName);
            if (groupConfiguration == null)
            {
                if (group.PathName == "NetworkGameplayTagNodeIndex")
                {
                    group.NetFieldExports[field.Handle] = new NetFieldExport
                    {
                        Handle = field.Handle,
                        Name = field.Name,
                        CompatibleChecksum = field.CompatibleChecksum,
                    };
                    AddDebugExport(group.PathName, field.Name);
                    continue;
                }

                if (Settings.IsDebug)
                {
                    group.NetFieldExports[field.Handle] = new NetFieldExport
                    {
                        Handle = field.Handle,
                        Name = field.Name,
                        CompatibleChecksum = field.CompatibleChecksum,
                    };
                }

                AddDebugExport(group.PathName, field.Name);
                continue;
            }

            var fieldConfiguration = groupConfiguration.Fields.GetValueOrDefault(field.Name, null);

            if (fieldConfiguration == null)
            {
                if (group.PathName == "NetworkGameplayTagNodeIndex")
                {
                    group.NetFieldExports[field.Handle] = new NetFieldExport
                    {
                        Handle = field.Handle,
                        Name = field.Name,
                        CompatibleChecksum = field.CompatibleChecksum,
                    };
                    continue;
                }

                if (Settings.IsDebug)
                {
                    group.NetFieldExports[field.Handle] = new NetFieldExport
                    {
                        Handle = field.Handle,
                        Name = field.Name,
                        CompatibleChecksum = field.CompatibleChecksum,
                    };
                }

                AddDebugExport(group.PathName, field.Name);
                continue;
            }

            group.NetFieldExports[field.Handle] = new NetFieldExport
            {
                Handle = field.Handle,
                Name = field.Name,
                CompatibleChecksum = field.CompatibleChecksum,
                Configuration = fieldConfiguration,
            };
            AddDebugExport(group.PathName, field.Name);
        }
    }

    private void AddDebugExport(string groupName, string fieldName)
    {
        if (!Settings.IsDebug)
        {
            return;
        }

        if (!Replay.ExportGroupDict.ContainsKey(groupName))
        {
            Replay.ExportGroupDict[groupName] = new Dictionary<string, HashSet<uint>>();
        }

        if (!Replay.ExportGroupDict[groupName].ContainsKey(fieldName))
        {
            Replay.ExportGroupDict[groupName][fieldName] = new HashSet<uint>();
        }
    }
}