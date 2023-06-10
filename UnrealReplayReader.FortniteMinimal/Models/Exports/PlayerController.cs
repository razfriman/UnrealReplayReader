using UnrealReplayReader.Models;

namespace UnrealReplayReader.FortniteMinimal.Models.Exports;

public record PlayerController : ExportModel
{
    public record Configuration : GroupConfiguration<PlayerController>
    {
        public Configuration()
        {
            AddPath("/Game/Spectating/BP_ReplayPC_Athena.BP_ReplayPC_Athena_C");
            AddPlayerController("BP_ReplayPC_Athena_C");
        }
    }
}