using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record PlayerPawnExport : ExportModel
{
    public ActorGuid? Vehicle { get; set; }
    public float? ReplayLastTransformUpdateTimeStamp { get; set; }
    public bool? BIsSkydiving { get; set; }
    public bool? BIsParachuteOpen { get; set; }
    public ActorGuid? PlayerState { get; set; }
    public ItemDefinition? Character { get; set; }
    public ItemDefinition? SkyDiveContrail { get; set; }
    public ItemDefinition? Glider { get; set; }
    public ItemDefinition? Pickaxe { get; set; }
    public ItemDefinition? MusicPack { get; set; }
    public ItemDefinition? LoadingScreen { get; set; }
    public ItemDefinition? Backpack { get; set; }
    public ItemDefinition? PetSkin { get; set; }
    public ItemDefinition[]? ItemWraps { get; set; }
    public ItemDefinition[]? Dances { get; set; }
    public string? BannerIconId { get; set; }
    public string? BannerColorId { get; set; }
    public bool? bIsDefaultCharacter { get; set; }
    public FRepMovement? ReplicatedMovement { get; set; }


    public record Configuration : GroupConfiguration<PlayerPawnExport>
    {
        public Configuration()
        {
            AddPath("/Game/Athena/PlayerPawn_Athena.PlayerPawn_Athena_C");
            AddPath("/Game/Athena/AI/Phoebe/BP_PlayerPawn_Athena_Phoebe.BP_PlayerPawn_Athena_Phoebe_C");
            AddPath("/Game/Athena/AI/NPCs/Tandem/Base/Pawns/BP_PlayerPawn_Tandem.BP_PlayerPawn_Tandem_C");
            AddPath("/NPCLibrary/NPCs/Outter/Outter/Pawns/BP_PlayerPawn_Outter.BP_PlayerPawn_Outter_C");
            AddProperty("bIsDefaultCharacter", (model, reader) => model.bIsDefaultCharacter = reader.ReadBit());
            AddProperty("bIsSkydiving", (model, reader) => model.BIsSkydiving = reader.ReadBit());
            AddProperty("bIsParachuteOpen", (model, reader) => model.BIsParachuteOpen = reader.ReadBit());
            AddProperty(x => x.Vehicle, (model, reader) => model.Vehicle = reader.ReadCustomClass<ActorGuid>());
            AddProperty(x => x.PlayerState, (model, reader) => model.PlayerState = reader.ReadCustomClass<ActorGuid>());
            AddProperty(x => x.ReplicatedMovement,
                (model, reader) => model.ReplicatedMovement = reader.ReadFRepMovement(1,0,0));
            AddProperty(x => x.PetSkin, (model, reader) => model.PetSkin = reader.ReadCustomClass<ItemDefinition>());
            AddProperty(x => x.Character,
                (model, reader) => model.Character = reader.ReadCustomClass<ItemDefinition>());
            AddProperty(x => x.SkyDiveContrail,
                (model, reader) => model.SkyDiveContrail = reader.ReadCustomClass<ItemDefinition>());
            AddProperty(x => x.Glider, (model, reader) => model.Glider = reader.ReadCustomClass<ItemDefinition>());
            AddProperty(x => x.LoadingScreen,
                (model, reader) => model.LoadingScreen = reader.ReadCustomClass<ItemDefinition>());
            AddProperty(x => x.Pickaxe, (model, reader) => model.Pickaxe = reader.ReadCustomClass<ItemDefinition>());
            AddProperty(x => x.Backpack, (model, reader) => model.Backpack = reader.ReadCustomClass<ItemDefinition>());
            AddProperty(x => x.MusicPack,
                (model, reader) => model.MusicPack = reader.ReadCustomClass<ItemDefinition>());
            AddProperty(x => x.ItemWraps,
                (model, reader) => model.ItemWraps = reader.ReadDynamicArray(reader.ReadCustomClass<ItemDefinition>));
            AddProperty(x => x.Dances,
                (model, reader) => model.Dances = reader.ReadDynamicArray(reader.ReadCustomClass<ItemDefinition>));
            AddProperty(x => x.BannerIconId, (model, reader) => model.BannerIconId = reader.ReadFString());
            AddProperty(x => x.BannerColorId, (model, reader) => model.BannerColorId = reader.ReadFString());
            AddProperty(x => x.ReplayLastTransformUpdateTimeStamp,
                (model, reader) => model.ReplayLastTransformUpdateTimeStamp = reader.ReadSingle());
            AddProperty("CharacterVariantChannels", (replay, model, reader, group, field) =>
                reader.ReadDynamicArray<PlayerPawnExport>(replay, group, this));
            AddProperty("VocalChords", (replay, model, reader, group, field) =>
                reader.ReadDynamicArray<PlayerPawnExport>(replay, group, this));
            AddProperty("AppliedSwaps", (replay, model, reader, group, field) =>
                reader.ReadDynamicArray<PlayerPawnExport>(replay, group, this));
            AddProperty("SwapData", (replay, model, reader, group, field) =>
                reader.ReadDynamicArray<PlayerPawnExport>(replay, group, this));
        }
    }
}
