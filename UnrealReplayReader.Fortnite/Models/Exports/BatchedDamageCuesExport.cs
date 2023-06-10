using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record BatchedDamageCuesExport : ExportModel
{
    public FVector? Location { get; set; } //Type:  Bits: 1
    public FVector? Normal { get; set; } //Type:  Bits: 1
    public float Magnitude { get; set; } //Type:  Bits: 1
    public uint HitActor { get; set; }
    public uint NonPlayerHitActor { get; set; }

    public record Configuration : GroupConfiguration<BatchedDamageCuesExport>
    {
        public Configuration()
        {
            AddPath("/Script/FortniteGame.FortPawn:NetMulticast_Athena_BatchedDamageCues");
            AddPath("/Script/FortniteGame.FortPlayerPawnAthena:NetMulticast_Athena_BatchedDamageCues");
            AddProperty(x => x.Location, (model, reader) => model.Location = reader.ReadPackedVector(10, 27));
            AddProperty(x => x.Normal, (model, reader) => model.Normal = reader.ReadFVectorNormal());
            AddProperty(x => x.Magnitude, (model, reader) => model.Magnitude = reader.ReadSingle());
            AddProperty(x => x.HitActor, (model, reader) => model.HitActor = reader.ReadIntPacked());
            AddProperty(x => x.NonPlayerHitActor, (model, reader) => model.NonPlayerHitActor = reader.ReadIntPacked());
        }
    }
}