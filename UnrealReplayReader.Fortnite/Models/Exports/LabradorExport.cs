using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record LabradorExport : ExportModel
{
    public int? PawnUniqueId { get; set; }

    public record Configuration : GroupConfiguration<LabradorExport>
    {
        public Configuration()
        {
            AddPath("/Labrador/Pawn/BP_AIPawn_Labrador.BP_AIPawn_Labrador_C");
            AddProperty("PawnUniqueID", (model, reader) => model.PawnUniqueId = reader.ReadInt32());
        }
    }
}