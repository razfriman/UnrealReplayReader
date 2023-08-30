using UnrealReplayReader.Fortnite.Models.Enums;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record PlayerStateExport : ExportModel
{
    public ActorGuid? FinisherOrDowner { get; set; }
    public float? Distance { get; set; }
    public uint? RebootCounter { get; set; }
    public string? UniqueID { get; set; }
    public string? BotUniqueId { get; set; }
    public string? PlatformUniqueNetId { get; set; }
    public string? PlayerNamePrivate { get; set; }
    public string? Platform { get; set; }
    public string? PartyOwnerUniqueId { get; set; }
    public bool? BOnlySpectator { get; set; }
    public bool? BDbno { get; set; }
    public bool? BIsABot { get; set; }
    public bool? BIsGameSessionOwner { get; set; }
    public bool? BHasFinishedLoading { get; set; }
    public bool? BHasStartedPlaying { get; set; }
    public bool? BUsingStreamerMode { get; set; }
    public bool? BThankedBusDriver { get; set; }
    public bool? BHasEverSkydivedFromBus { get; set; }
    public bool? BIsDisconnected { get; set; }
    public bool? BUsingAnonymousMode { get; set; }
    public bool? BHasEverSkydivedFromBusAndLanded { get; set; }
    public int? Level { get; set; }
    public int? Place { get; set; }
    public int? KillScore { get; set; }
    public int? TeamKillScore { get; set; }
    public int? TeamIndex { get; set; }
    public string? IconId { get; set; }
    public string? ColorId { get; set; }
    public FText? PlayerNameCustomOverride { get; set; }
    public EDeathCause? DeathCause { get; set; }
    public FGameplayTagContainer? DeathTags { get; set; }
    public EFortCustomBodyType? CharacterBodyType { get; set; }
    public EFortCustomGender? CharacterGender { get; set; }
    public ItemDefinition? HeroType { get; set; }
    public ItemDefinition? Parts { get; set; }
    public ItemDefinition[]? VariantRequiredCharacterParts { get; set; }
    public short? WorldPlayerId { get; set; }
    public string? KeepPlayingTogetherMatchmakingRegion { get; set; }
    public byte? Ping { get; set; }

    public record Configuration : GroupConfiguration<PlayerStateExport>
    {
        public Configuration()
        {
            AddPath("/Script/FortniteGame.FortPlayerStateAthena");
            AddProperty("WorldPlayerId", (model, reader) => model.WorldPlayerId = reader.ReadInt16());
            AddProperty("TeamIndex", (model, reader) => model.TeamIndex = (int)reader.ReadBitsToUInt32(7));
            AddProperty("VariantRequiredCharacterParts",
                (model, reader) => model.VariantRequiredCharacterParts =
                    reader.ReadDynamicArray<ItemDefinition>(() => reader.ReadCustomClass<ItemDefinition>()));
            AddProperty("Parts", (model, reader) => model.Parts = reader.ReadCustomClass<ItemDefinition>());
            AddProperty("CharacterBodyType",
                (model, reader) => model.CharacterBodyType = reader.ReadBitsToUInt32AsEnum<EFortCustomBodyType>());
            AddProperty("CharacterGender",
                (model, reader) => model.CharacterGender = reader.ReadBitsToUInt32AsEnum<EFortCustomGender>());
            AddProperty("KillScore", (model, reader) => model.KillScore = reader.ReadInt32());
            AddProperty("TeamKillScore", (model, reader) => model.TeamKillScore = reader.ReadInt32());
            AddProperty("Place", (model, reader) => model.Place = reader.ReadInt32());
            AddProperty("bUsingStreamerMode", (model, reader) => model.BUsingStreamerMode = reader.ReadBit());
            AddProperty("bThankedBusDriver", (model, reader) => model.BThankedBusDriver = reader.ReadBit());
            AddProperty("bHasEverSkydivedFromBus", (model, reader) => model.BHasEverSkydivedFromBus = reader.ReadBit());
            AddProperty("bHasFinishedLoading", (model, reader) => model.BHasFinishedLoading = reader.ReadBit());
            AddProperty("bHasStartedPlaying", (model, reader) => model.BHasStartedPlaying = reader.ReadBit());
            AddProperty("bIsDisconnected", (model, reader) => model.BIsDisconnected = reader.ReadBit());
            AddProperty("bUsingAnonymousMode", (model, reader) => model.BUsingAnonymousMode = reader.ReadBit());
            AddProperty("bHasEverSkydivedFromBusAndLanded",
                (model, reader) => model.BHasEverSkydivedFromBusAndLanded = reader.ReadBit());
            AddProperty("bOnlySpectator", (model, reader) => model.BOnlySpectator = reader.ReadBit());
            AddProperty("bDBNO", (model, reader) => model.BDbno = reader.ReadBit());
            AddProperty("bIsABot", (model, reader) => model.BIsABot = reader.ReadBit());
            AddProperty("bIsGameSessionOwner", (model, reader) => model.BIsGameSessionOwner = reader.ReadBit());
            AddProperty(x => x.Level, (model, reader) => model.Level = reader.ReadInt32());
            AddProperty(x => x.FinisherOrDowner,
                (model, reader) => model.FinisherOrDowner = reader.ReadCustomClass<ActorGuid>());
            AddProperty(x => x.RebootCounter, (model, reader) => model.RebootCounter = reader.ReadUInt32());
            AddProperty("Ping", (model, reader) => model.Ping = reader.ReadByte());
            AddProperty("CompressedPing", (model, reader) => model.Ping = reader.ReadByte());
            AddProperty(x => x.Distance, (model, reader) => model.Distance = reader.ReadSingle());
            AddProperty(x => x.Platform, (model, reader) => model.Platform = reader.ReadFString());
            AddProperty(x => x.PartyOwnerUniqueId, (model, reader) => model.PartyOwnerUniqueId = reader.ReadNetId());
            AddProperty(x => x.UniqueID, (model, reader) => model.UniqueID = reader.ReadNetId());
            AddProperty(x => x.PlatformUniqueNetId, (model, reader) => model.PlatformUniqueNetId = reader.ReadNetId());
            AddProperty(x => x.BotUniqueId, (model, reader) => model.BotUniqueId = reader.ReadNetId());
            AddProperty(x => x.PlayerNamePrivate, (model, reader) => model.PlayerNamePrivate = reader.ReadFString());
            AddProperty("KeepPlayingTogetherMatchmakingRegion",
                (model, reader) => model.KeepPlayingTogetherMatchmakingRegion = reader.ReadFString()
            );
            AddProperty(x => x.DeathCause,
                (model, reader) => model.DeathCause = reader.ReadBitsToUInt32AsEnum<EDeathCause>(6));
            AddProperty(x => x.DeathTags,
                (model, reader) => model.DeathTags = reader.ReadCustomClass<FGameplayTagContainer>());
            
            AddProperty(x => x.IconId,
                (model, reader) => model.IconId = reader.ReadFString());
            AddProperty(x => x.ColorId,
                (model, reader) => model.ColorId = reader.ReadFString());
            AddProperty("PlayerNameCustomOverride",
                (model, reader) => model.PlayerNameCustomOverride = reader.ReadCustomClass<FText>());
            
            // PlayerTeamPrivate
            // VariantRequiredCharacterParts
                // PlayerNameCustomOverride
            // Parts
        }
    }
}
