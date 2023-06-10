using Microsoft.Extensions.Logging;
using UnrealReplayReader.Fortnite.Models.Events;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Events;

public static class AdditionGfpReader
{
    public static AdditionGfpEvent ReadAdditionGfp(FLocalFileEventInfo chunk, ByteReader reader,
        ILogger? logger = null)
    {
        var result = new AdditionGfpEvent();
        var version = reader.ReadInt32();
        var count = reader.ReadInt32();
        for (var i = 0; i < count; i++)
        {
            var module = new AdditionGfpModule();
            module.Id = reader.ReadFString();
            if (version < 2)
            {
                module.Version = reader.ReadInt32();
            }
            else
            {
                module.ArtifactId = reader.ReadFString();
            }

            result.Modules.Add(module);
        }

        return result;
    }
}