using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record LlamaExport : ExportModel
{
    public record Configuration : GroupConfiguration<LlamaExport>
    {
        public Configuration()
        {
            AddPath("/Game/Athena/SupplyDrops/Llama/AthenaSupplyDrop_Llama.AthenaSupplyDrop_Llama_C");
        }
    }
}