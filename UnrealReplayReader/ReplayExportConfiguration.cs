using System.Reflection;
using UnrealReplayReader.Models;

namespace UnrealReplayReader;

public class ReplayExportConfiguration
{
    public HashSet<GroupConfiguration> GroupConfigurations { get; set; } = new();

    public static ReplayExportConfiguration FromTypes(List<Type> types)
    {
        var filteredTypes = types.Where(t => t.IsSubclassOf(typeof(GroupConfiguration)))
            .ToList();

        var configuration = new ReplayExportConfiguration();

        foreach (var type in filteredTypes)
        {
            var instance = (GroupConfiguration)Activator.CreateInstance(type);
            configuration.GroupConfigurations.Add(instance);
        }

        return configuration;
    }

    public static ReplayExportConfiguration FromExecutingAssembly() =>
        FromAssembly(Assembly.GetExecutingAssembly());

    public static ReplayExportConfiguration FromAssembly(Assembly assembly) =>
        FromTypes(assembly
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(GroupConfiguration)))
            .ToList());

    public static ReplayExportConfiguration FromAssembly(Type typeMarker) =>
        FromAssembly(Assembly.GetAssembly(typeMarker));
}