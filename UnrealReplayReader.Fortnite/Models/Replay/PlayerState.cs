using System.Text.Json.Serialization;
using UnrealReplayReader.Fortnite.Models.Classes;
using UnrealReplayReader.Fortnite.Models.Enums;
using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader.Fortnite.Models.Replay;

public record PlayerState
{
    public string? PlayerId => BIsABot ? BotUniqueId ?? DisplayName : UniqueId;
    public PlayerPawn? PlayerPawn { get; set; }
    public string? DisplayName { get; set; }
    public bool IsPlayersReplay { get; set; }
    public uint? ActorId { get; set; }
    public uint? ClientInfoId { get; set; }
    public RemoteClientInfo? RemoteClientInfo { get; set; }
    public FortSet? Health { get; set; }
    public FortSet? Shield { get; set; }
    public FortSet? OverShield { get; set; }
    public Inventory? Inventory { get; set; }
    public double LastDeathOrKnockTime { get; internal set; }
    [JsonIgnore] public List<KillFeedEntry> StatusChanges { get; private set; } = new();
    [JsonIgnore] public KillFeedEntry? LastKnockedEntry { get; set; }
    public ActorGuid? FinisherOrDowner { get; set; }
    public float Distance { get; set; }
    public uint RebootCounter { get; set; }
    public string? UniqueId { get; set; }
    public string? BotUniqueId { get; set; }
    public string? PlatformUniqueNetId { get; set; }
    public string? PlayerNamePrivate { get; set; }
    public string? Platform { get; set; }
    public string? PartyOwnerUniqueId { get; set; }
    public bool BOnlySpectator { get; set; }
    public bool BDbno { get; set; }
    public bool BIsABot { get; set; }
    public bool BIsGameSessionOwner { get; set; }
    public bool BHasFinishedLoading { get; set; }
    public bool BHasStartedPlaying { get; set; }
    public bool BUsingStreamerMode { get; set; }
    public bool BThankedBusDriver { get; set; }
    public bool BHasEverSkydivedFromBus { get; set; }
    public bool BIsDisconnected { get; set; }
    public bool BUsingAnonymousMode { get; set; }
    public bool BHasEverSkydivedFromBusAndLanded { get; set; }
    public int Level { get; set; }
    public int Place { get; set; }
    public int? KillScore { get; set; }
    public int? TeamKillScore { get; set; }
    public int TeamIndex { get; set; }
    public EDeathCause? DeathCause { get; set; }
    public FGameplayTagContainer? DeathTags { get; set; }
    public EFortCustomBodyType? CharacterBodyType { get; set; }
    public EFortCustomGender? CharacterGender { get; set; }
    public ItemDefinition? HeroType { get; set; }
    public ItemDefinition? Parts { get; set; }
    public ItemDefinition[]? VariantRequiredCharacterParts { get; set; }
    public short WorldPlayerId { get; set; }
    public string? KeepPlayingTogetherMatchmakingRegion { get; set; }
    public byte? Ping { get; set; }
    public bool BLoadedWithVictoryCrown { get; set; }
    public int BuildingEdits { get; set; }
}