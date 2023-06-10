using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record PlayerControllerExport : ExportModel
{
    public record Configuration : GroupConfiguration<PlayerControllerExport>
    {
        public Configuration()
        {
            AddPath("/Game/Spectating/BP_ReplayPC_Athena.BP_ReplayPC_Athena_C");
            AddPlayerController("BP_ReplayPC_Athena_C");
        }
    }
}