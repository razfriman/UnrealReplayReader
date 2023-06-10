using UnrealReplayReader.Models;

namespace UnrealReplayReader;

public class NetFieldParser
{
    public HashSet<GroupConfiguration> GroupConfigurations { get; set; } = new();
    public Dictionary<string, GroupConfiguration> PathToGroupConfigurations { get; set; } = new();
    public Dictionary<string, GroupConfiguration> PartialPathToGroupConfigurations { get; set; } = new();
    public Dictionary<string, string> Redirects { get; set; } = new();
    public Dictionary<string, GroupConfiguration> ClassPathCache { get; set; } = new();
    public HashSet<string> PlayerControllers { get; set; } = new();
    public GuidCache GuidCache { get; set; }

    public void Init(ReplayExportConfiguration replayConfiguration)
    {
        GroupConfigurations = replayConfiguration.GroupConfigurations;
        foreach (var config in replayConfiguration.GroupConfigurations)
        {
            foreach (var path in config.Paths)
            {
                PathToGroupConfigurations[path] = config;

                foreach (var redirect in config.Redirects)
                {
                    if (!config.IsClassNetCache)
                    {
                        Redirects[redirect] = path;
                    }
                }
            }

            foreach (var path in config.PartialPaths)
            {
                PartialPathToGroupConfigurations[path] = config;
            }

            foreach (var playerController in config.PlayerControllers)
            {
                PlayerControllers.Add(playerController);
            }
        }
    }

    public GroupConfiguration? GetGroupConfiguration(string group)
    {
        if (ClassPathCache.ContainsKey(group))
        {
            return ClassPathCache[group];
        }

        var groupConfiguration = FindMatchingGroup(group);
        if (groupConfiguration == null)
        {
            return null;
        }

        ClassPathCache[group] = groupConfiguration;
        return groupConfiguration;
    }

    public GroupConfiguration? FindMatchingGroup(string group)
    {
        if (PathToGroupConfigurations.ContainsKey(group))
        {
            return PathToGroupConfigurations[group];
        }

        foreach (var key in PartialPathToGroupConfigurations.Keys)
        {
            if (group.Contains(key))
            {
                var configuration = PartialPathToGroupConfigurations[key];
                PathToGroupConfigurations[group] = configuration;
                return configuration;
            }
        }

        return null;
    }

    public string? GetRedirect(string netGuidPath) => Redirects.GetValueOrDefault(netGuidPath, null);

    public void ResetForCheckpoint() => ClassPathCache.Clear();

    public bool WillReadType(string group) => GetGroupConfiguration(group) != null;
}