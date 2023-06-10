using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record VictoryCrownGameStateComponent_C_ClassNetCache : ExportModel
{
    public record
        VictoryCrownGameStateComponent_C_ClassNetCache_Configuration : GroupConfiguration<
            VictoryCrownGameStateComponent_C_ClassNetCache>
    {
        public VictoryCrownGameStateComponent_C_ClassNetCache_Configuration()
        {
            AddPath("VictoryCrownGameStateComponent_C_ClassNetCache");
            IsClassNetCache = true;
            AddPropertyFunction("ClientKillFeedMessage",
                "/Script/VictoryCrownsRuntime.FortGameStateComponent_VictoryCrowns:ClientKillFeedMessage");
        }
    }
}