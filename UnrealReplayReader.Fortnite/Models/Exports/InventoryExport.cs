using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record InventoryExport : ExportModel
{
    public ItemDefinition? ItemDefinition { get; set; }
    public int? Count { get; set; }
    public int? LoadedAmmo { get; set; }
    public uint? ReplayPawn { get; set; }

    public record Configuration : GroupConfiguration<InventoryExport>
    {
        public Configuration()
        {
            AddPath("/Script/FortniteGame.FortInventory");
            AddPath("/Script/FortniteGame.FortItemEntry");

            AddProperty(x => x.ItemDefinition,
                (model, reader) => model.ItemDefinition = reader.ReadCustomClass<ItemDefinition>());
            AddProperty(x => x.Count,
                (model, reader) => model.Count = reader.ReadInt32());
            AddProperty(x => x.LoadedAmmo,
                (model, reader) => model.LoadedAmmo = reader.ReadInt32());
            AddProperty(x => x.ReplayPawn,
                (model, reader) => model.ReplayPawn = reader.ReadIntPacked());
        }
    }
}