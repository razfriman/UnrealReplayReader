using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record SupplyDropExport : ExportModel
{
    public FVector? LandingLocation { get; set; }

    public record Configuration : GroupConfiguration<SupplyDropExport>
    {
        public Configuration()
        {
            AddPath("/Game/Athena/SupplyDrops/AthenaSupplyDrop.AthenaSupplyDrop_C");
            AddProperty("LandingLocation", (model, reader) => model.LandingLocation = reader.ReadFVector());
        }
    }
}