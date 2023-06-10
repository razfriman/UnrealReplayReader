using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record SafeZoneIndicatorExport : ExportModel
{
    public float? NextRadius { get; set; }
    public float? NextNextRadius { get; set; }
    public FVector? LastCenter { get; set; }
    public FVector? NextCenter { get; set; }
    public FVector? NextNextCenter { get; set; }
    public float? SafeZoneStartShrinkTime { get; set; }
    public float? SafeZoneFinishShrinkTime { get; set; }
    public float? Radius { get; set; }

    public record Configuration : GroupConfiguration<SafeZoneIndicatorExport>
    {
        public Configuration()
        {
            AddPath("/Game/Athena/SafeZone/SafeZoneIndicator.SafeZoneIndicator_C");
            AddProperty(x => x.NextRadius, (model, reader) => model.NextRadius = reader.ReadSingle());
            AddProperty(x => x.NextNextRadius, (model, reader) => model.NextNextRadius = reader.ReadSingle());
            AddProperty(x => x.LastCenter, (model, reader) => model.LastCenter = reader.ReadPackedVector(100, 30));
            AddProperty(x => x.NextCenter, (model, reader) => model.NextCenter = reader.ReadPackedVector(100, 30));
            AddProperty(x => x.NextNextCenter,
                (model, reader) => model.NextNextCenter = reader.ReadPackedVector(100, 30));
            AddProperty(x => x.SafeZoneStartShrinkTime,
                (model, reader) => model.SafeZoneStartShrinkTime = reader.ReadSingle());
            AddProperty(x => x.SafeZoneFinishShrinkTime,
                (model, reader) => model.SafeZoneFinishShrinkTime = reader.ReadSingle());
            AddProperty(x => x.Radius, (model, reader) => model.Radius = reader.ReadSingle());
        }
    }
}