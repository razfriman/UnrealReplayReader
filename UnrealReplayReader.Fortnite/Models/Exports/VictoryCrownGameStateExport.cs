using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record VictoryCrownGameStateExport : ExportModel
{
    public ActorGuid[]? CrownBearerPlayerStates { get; set; }

    public record Configuration : GroupConfiguration<VictoryCrownGameStateExport>
    {
        public Configuration()
        {
            AddPath("/VictoryCrownsGameplay/Items/VictoryCrownGameStateComponent.VictoryCrownGameStateComponent_C");
            AddProperty("CrownBearerPlayerStates",
                (model, reader) => model.CrownBearerPlayerStates =
                    reader.ReadDynamicArray(reader.ReadCustomClass<ActorGuid>));
        }
    }
}