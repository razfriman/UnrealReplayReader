using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record FortClientObservedStatExport : ExportModel
{
    public string? StatName { get; set; }
    public int? StatValue { get; set; }

    public record Configuration : GroupConfiguration<FortClientObservedStatExport>
    {
        public Configuration()
        {
            AddPath("/Script/FortniteGame.FortClientObservedStat");
            AddProperty("StatName", (model, reader) => model.StatName = reader.ReadFName());
            AddProperty("StatValue", (model, reader) => model.StatValue = reader.ReadInt32());
        }
    }
}