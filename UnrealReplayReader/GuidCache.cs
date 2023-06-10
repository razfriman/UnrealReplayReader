using UnrealReplayReader.Extensions;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader;

public class GuidCache
{
    public Dictionary<string, NetFieldExportGroup> NetFieldExportGroupMap { get; set; } = new();

    public Dictionary<uint, string> NetFieldExportGroupIndexToGroup { get; set; } = new();
    public Dictionary<uint, NetworkGuid> NetGuids { get; set; } = new();

    /** used as a cache for the get export group functions */
    public Dictionary<string, NetFieldExportGroup> ArchTypeToExportGroup { get; set; } = new();

    public Dictionary<uint, string> FailedPaths { get; set; } = new();
    public Dictionary<uint, string> FailedStaticPaths { get; set; } = new();
    public Dictionary<string, string> CleanedClassNetCache { get; set; } = new();

    /** Contains some hardcoded strings */
    public NetFieldExportGroup? NetworkGameplayTagNodeIndex { get; set; }

    /** Contains all actors */
    public Dictionary<uint, Actor> ActorIdToActorMap { get; } = new();

    public Dictionary<string, NetFieldExportGroup> StaticActorIdMap { get; set; } = new();

    /** Contains groups that dont have a netFieldExport config but are still required to be parsed in debug mode */
    public Dictionary<string, NetFieldExportGroup> NotReadGroups { get; set; } = new();

    public ReplayReaderSettings Settings { get; set; }
    public NetFieldParser NetFieldParser { get; set; }

    public NetFieldExportGroup? AddToExportGroupMap(string pathName, NetFieldExportGroup group)
    {
        if (pathName.EndsWith("ClassNetCache"))
        {
            group.PathName = group.PathName.RemovePathPrefix();
        }

        var groupConfiguration = NetFieldParser.GetGroupConfiguration(group.PathName);

        if (pathName == "NetworkGameplayTagNodeIndex")
        {
            var baseGroup = new NetFieldExportGroup
            {
                PathName = group.PathName,
                PathNameIndex = group.PathNameIndex,
                NetFieldExportsLength = group.NetFieldExportsLength,
            };

            NetworkGameplayTagNodeIndex = baseGroup;
            NetFieldExportGroupMap[pathName] = baseGroup;
            NetFieldExportGroupIndexToGroup[group.PathNameIndex] = pathName;
            return baseGroup;
        }

        if (groupConfiguration == null)
        {
            if (Settings.IsDebug)
            {
                var debugGroup = new NetFieldExportGroup
                {
                    PathName = group.PathName,
                    PathNameIndex = group.PathNameIndex,
                    NetFieldExportsLength = group.NetFieldExportsLength,
                };
                NotReadGroups[debugGroup.PathNameIndex.ToString()] = debugGroup;
                NetFieldExportGroupMap[pathName] = debugGroup;
                NetFieldExportGroupIndexToGroup[debugGroup.PathNameIndex] = pathName;
                return debugGroup;
            }

            return null;
        }

        if (groupConfiguration.IsClassNetCache)
        {
            var baseName = pathName.Split("_ClassNetCache")[0];

            if (!NetFieldExportGroupMap.ContainsKey(baseName))
            {
                var baseGroup = new NetFieldExportGroup
                {
                    PathName = baseName,
                    PathNameIndex = 0,
                    NetFieldExportsLength = 0,
                    Configuration = groupConfiguration,
                };

                NetFieldExportGroupMap[baseName] = baseGroup;

                var lookupValue = baseName.Split(".")[1];
                FailedPaths.Where(x => x.Value == lookupValue)
                    .Select(x => x.Key)
                    .ToList()
                    .ForEach(x => FailedPaths.Remove(x));
            }
        }

        var theGroup = new NetFieldExportGroup
        {
            PathName = group.PathName,
            PathNameIndex = group.PathNameIndex,
            NetFieldExportsLength = group.NetFieldExportsLength,
            Configuration = groupConfiguration
        };

        NetFieldExportGroupMap[pathName] = theGroup;
        NetFieldExportGroupIndexToGroup[group.PathNameIndex] = pathName;
        // Console.WriteLine("DEBUG WHY THIS IS NOT CALLED FOR STATIC");
        if (groupConfiguration.StaticActorIds.Count > 0)
        {
            foreach (var staticActorId in groupConfiguration.StaticActorIds)
            {
                AddStaticActorId(staticActorId, theGroup);
            }
        }

        return theGroup;
    }

    public string TryGetPathName(uint? netGuid) =>
        netGuid.HasValue ? NetGuids.GetValueOrDefault(netGuid.Value)?.Path : null;

    public static string? GetFullGuidPath(NetworkGuid? guid)
    {
        if (guid == null)
        {
            return null;
        }

        if (guid.Outer != null)
        {
            return $"{GetFullGuidPath(guid.Outer)}.{guid.Path}";
        }

        return guid.Path;
    }

    public string? TryGetFullPathName(uint netGuid)
    {
        return GetFullGuidPath(NetGuids[netGuid]);
    }

    public NetFieldExportGroup GetNetFieldExportGroupString(string path) =>
        NetFieldExportGroupMap.GetValueOrDefault(path, null);

    public NetFieldExportGroup? GetNetFieldExportGroup(uint netguid)
    {
        var group = ArchTypeToExportGroup.GetValueOrDefault(netguid.ToString(), null);

        if (group != null)
        {
            return group;
        }

        var netGuid = NetGuids.GetValueOrDefault(netguid, null);

        if (netGuid == null)
        {
            ArchTypeToExportGroup.Remove(netguid.ToString());
            return null;
        }

        var redirect = NetFieldParser.GetRedirect(netGuid.Path);

        if (redirect != null)
        {
            var theGroup = NetFieldExportGroupMap.GetValueOrDefault(redirect, null);

            if (theGroup != null)
            {
                ArchTypeToExportGroup[netguid.ToString()] = theGroup;

                return theGroup;
            }
        }

        var fullPath = GetFullGuidPath(netGuid);

        var returnValue = NetFieldExportGroupMap.GetValueOrDefault(fullPath, null);

        if (returnValue != null)
        {
            ArchTypeToExportGroup[netguid.ToString()] = returnValue;

            return returnValue;
        }

        FailedPaths[netguid] = netGuid.Path;
        ArchTypeToExportGroup.Remove(netguid.ToString());

        return null;
    }

    public NetFieldExportGroup? GetNetFieldExportGroupFromIndex(uint index)
    {
        var group = NetFieldExportGroupIndexToGroup.GetValueOrDefault(index, null);

        if (group == null)
        {
            if (NotReadGroups.GetValueOrDefault(index.ToString(), null) != null)
            {
                return NotReadGroups[index.ToString()];
            }

            return null;
        }

        return NetFieldExportGroupMap[group];
    }

    public string? TryGetTagName(uint tagIndex) =>
        NetworkGameplayTagNodeIndex?.NetFieldExports?.GetValueOrDefault(tagIndex, null)?.Name;

    public NetFieldExportGroup? TryGetClassNetCache(string? group, bool useFullName)
    {
        if (group == null)
        {
            return null;
        }

        var classNetCachePath = CleanedClassNetCache.GetValueOrDefault(group, null);

        if (classNetCachePath == null)
        {
            classNetCachePath =
                useFullName ? $"{group}_ClassNetCache" : $"{group.RemovePathPrefix()}_ClassNetCache";

            CleanedClassNetCache[group] = classNetCachePath;
        }

        return NetFieldExportGroupMap.GetValueOrDefault(classNetCachePath, null);
    }

    public void AddActor(Actor inActor) => ActorIdToActorMap[inActor.ActorNetGuid.Value] = inActor;

    public Actor? TryGetActorById(uint netGuid) => ActorIdToActorMap.GetValueOrDefault(netGuid, null);


    public (string? staticActorId, NetFieldExportGroup? group) GetStaticActorExportGroup(uint netGuid)
    {
        var staticActorId = NetGuids.GetValueOrDefault(netGuid, null);
        if (staticActorId == null)
        {
            return (null, null);
        }

        var index = staticActorId.Path.IndexOf("_UAID");
        var cleanedPath = index == -1
            ? staticActorId.Path.CleanStaticIdSuffix()
            : staticActorId.Path[..index];
        var fullStaticActorId = staticActorId.GetFullGuidPath();
        var exportGroup = StaticActorIdMap.GetValueOrDefault(cleanedPath, null);
        if (exportGroup == null)
        {
            FailedStaticPaths[netGuid] = cleanedPath;
        }

        return (fullStaticActorId, exportGroup);
    }

    public void AddStaticActorId(string path, NetFieldExportGroup exportGroup) =>
        StaticActorIdMap[path] = exportGroup;

    public void CleanForCheckpoint()
    {
        NetFieldExportGroupMap.Clear();
        NetFieldExportGroupIndexToGroup.Clear();
        ArchTypeToExportGroup.Clear();
        FailedPaths.Clear();
        CleanedClassNetCache.Clear();
        NetworkGameplayTagNodeIndex = null;
        ActorIdToActorMap.Clear();
        StaticActorIdMap.Clear();
    }

    public NetFieldExportGroup? getNFEReference(string pathName) =>
        NetFieldExportGroupMap.GetValueOrDefault(pathName, null);
}