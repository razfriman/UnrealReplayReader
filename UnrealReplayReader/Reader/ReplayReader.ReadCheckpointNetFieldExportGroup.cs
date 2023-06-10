using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    public NetFieldExportGroup ReadCheckpointNetFieldExportGroup(ByteReader reader)
    {
        var theGroup = new NetFieldExportGroup
        {
            PathName = reader.ReadFString(),
            PathNameIndex = reader.ReadIntPacked(),
            NetFieldExportsLength = reader.ReadIntPacked()
        };

        var group = GuidCache.AddToExportGroupMap(theGroup.PathName, theGroup) ?? theGroup;

        for (var i = 0; i < group.NetFieldExportsLength; i++)
        {
            var field = ReadNetFieldExport(reader);

            if (field == null || group == null)
            {
                continue;
            }

            var groupConfiguration = NetFieldParser.GetGroupConfiguration(group.PathName);

            if (groupConfiguration == null)
            {
                if (group.PathName == "NetworkGameplayTagNodeIndex")
                {
                    group.NetFieldExports[field.Handle] = new NetFieldExport
                    {
                        Handle = field.Handle,
                        Name = field.Name,
                        Configuration = field.Configuration,
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

            var fieldConfiguration = groupConfiguration.Fields.GetValueOrDefault(field.Name, null);

            if (fieldConfiguration == null)
            {
                if (group.PathName == "NetworkGameplayTagNodeIndex")
                {
                    group.NetFieldExports[field.Handle] = new NetFieldExport
                    {
                        Handle = field.Handle,
                        Name = field.Name,
                        Configuration = null,
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

            group.NetFieldExports[field.Handle] = new NetFieldExport
            {
                Handle = field.Handle,
                Name = field.Name,
                CompatibleChecksum = field.CompatibleChecksum,
                Configuration = fieldConfiguration,
            };
            AddDebugExport(group.PathName, field.Name);
        }

        return group;
    }
}