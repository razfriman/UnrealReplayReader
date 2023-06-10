using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record HealthSetExport : ExportModel
{
    public Dictionary<uint, float> BaseValue { get; set; } = new();
    public Dictionary<uint, float> CurrentValue { get; set; } = new();
    public Dictionary<uint, float> Maximum { get; set; } = new();
    public Dictionary<uint, float> UnclampedBaseValue { get; set; } = new();
    public Dictionary<uint, float> UnclampedCurrentValue { get; set; } = new();

    public record Configuration : GroupConfiguration<HealthSetExport>
    {
        public Configuration()
        {
            AddPath("/Script/FortniteGame.FortRegenHealthSet");
            AddRedirect("HealthSet");
            StoreAsHandle = true;
            AddProperty("BaseValue",
                (_, model, reader, _, field) => model.BaseValue[field.Handle] = reader.ReadSingle());
            AddProperty("CurrentValue",
                (_, model, reader, _, field) => model.CurrentValue[field.Handle] = reader.ReadSingle());
            AddProperty("Maximum",
                (_, model, reader, _, field) => model.Maximum[field.Handle] = reader.ReadSingle());
            AddProperty("UnclampedBaseValue",
                (_, model, reader, _, field) => model.UnclampedBaseValue[field.Handle] = reader.ReadSingle());
            AddProperty("UnclampedCurrentValue",
                (_, model, reader, _, field) => model.UnclampedCurrentValue[field.Handle] = reader.ReadSingle());
        }
    }
}