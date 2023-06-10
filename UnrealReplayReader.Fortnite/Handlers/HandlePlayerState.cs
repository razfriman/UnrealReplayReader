using System.Text;
using UnrealReplayReader.Fortnite.Models.Enums;
using UnrealReplayReader.Fortnite.Models.Exports;
using UnrealReplayReader.Fortnite.Models.Replay;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Handlers;

public static class HandlePlayerState
{
    public static void OnProperties(PlayerStateExport model, Actor actor, ExternalData? externalData,
        MatchData match, StateData states)
    {
        if (model.BOnlySpectator == true)
        {
            states.ActorOnlySpectatingPlayers[actor.ActorId] = true;
            return;
        }

        if (states.ActorOnlySpectatingPlayers.GetValueOrDefault(actor.ActorId, false))
        {
            return;
        }

        var isNewPlayer = false;
        if (!states.Players.ContainsKey(actor.ActorId))
        {
            var newPlayerData = new PlayerState
            {
                ActorId = actor.ActorId
            };
            states.Players[actor.ActorId] = newPlayerData;
            match.Players.Add(newPlayerData);
            isNewPlayer = true;
        }

        var playerData = states.Players[actor.ActorId];
        playerData.IsPlayersReplay |= model.Ping.HasValue;
        playerData.FinisherOrDowner = model.FinisherOrDowner ?? playerData.FinisherOrDowner;
        playerData.Distance = model.Distance ?? playerData.Distance;
        playerData.RebootCounter = model.RebootCounter ?? playerData.RebootCounter;
        playerData.UniqueId = model.UniqueId?.ToLower() ?? playerData.UniqueId;
        playerData.BotUniqueId = model.BotUniqueId?.ToLower() ?? playerData.BotUniqueId;
        playerData.PlatformUniqueNetId = model.PlatformUniqueNetId?.ToLower() ?? playerData.PlatformUniqueNetId;
        playerData.PlayerNamePrivate = model.PlayerNamePrivate ?? playerData.PlayerNamePrivate;
        playerData.Platform = model.Platform ?? playerData.Platform;
        playerData.PartyOwnerUniqueId = model.PartyOwnerUniqueId?.ToLower() ?? playerData.PartyOwnerUniqueId;
        playerData.BOnlySpectator = model.BOnlySpectator ?? playerData.BOnlySpectator;
        playerData.BDbno = model.BDbno ?? playerData.BDbno;
        playerData.BIsABot = model.BIsABot ?? playerData.BIsABot;
        playerData.BIsGameSessionOwner = model.BIsGameSessionOwner ?? playerData.BIsGameSessionOwner;
        playerData.BHasFinishedLoading = model.BHasFinishedLoading ?? playerData.BHasFinishedLoading;
        playerData.BHasStartedPlaying = model.BHasStartedPlaying ?? playerData.BHasStartedPlaying;
        playerData.BUsingStreamerMode = model.BUsingStreamerMode ?? playerData.BUsingStreamerMode;
        playerData.BThankedBusDriver = model.BThankedBusDriver ?? playerData.BThankedBusDriver;
        playerData.BHasEverSkydivedFromBus = model.BHasEverSkydivedFromBus ?? playerData.BHasEverSkydivedFromBus;
        playerData.BIsDisconnected = model.BIsDisconnected ?? playerData.BIsDisconnected;
        playerData.BUsingAnonymousMode = model.BUsingAnonymousMode ?? playerData.BUsingAnonymousMode;
        playerData.BHasEverSkydivedFromBusAndLanded = model.BHasEverSkydivedFromBusAndLanded ??
                                                      playerData.BHasEverSkydivedFromBusAndLanded;
        playerData.Level = model.Level ?? playerData.Level;
        playerData.Place = model.Place ?? playerData.Place;
        playerData.KillScore = model.KillScore ?? playerData.KillScore;
        playerData.TeamKillScore = model.TeamKillScore ?? playerData.TeamKillScore;
        playerData.TeamIndex = model.TeamIndex ?? playerData.TeamIndex;
        playerData.DeathCause = model.DeathCause ?? playerData.DeathCause;
        playerData.DeathTags = model.DeathTags ?? playerData.DeathTags;
        playerData.CharacterBodyType = model.CharacterBodyType ?? playerData.CharacterBodyType;
        playerData.CharacterGender = model.CharacterGender ?? playerData.CharacterGender;
        playerData.HeroType = model.HeroType ?? playerData.HeroType;
        playerData.Parts = model.Parts ?? playerData.Parts;
        playerData.VariantRequiredCharacterParts =
            model.VariantRequiredCharacterParts ?? playerData.VariantRequiredCharacterParts;
        playerData.WorldPlayerId = model.WorldPlayerId ?? playerData.WorldPlayerId;
        playerData.KeepPlayingTogetherMatchmakingRegion = model.KeepPlayingTogetherMatchmakingRegion?.ToLower() ??
                                                          playerData.KeepPlayingTogetherMatchmakingRegion;
        playerData.Ping = model.Ping ?? playerData.Ping;
        if (model.PlayerNamePrivate != null)
        {
            if (externalData?.IsEncrypted ?? false)
            {
                var rawName = model.PlayerNamePrivate;
                var builder = new StringBuilder();
                for (var i = 0; i < model.PlayerNamePrivate.Length; i++)
                {
                    var shift = (rawName.Length % 4 * 3 % 8 + 1 + i) * 3 % 8;
                    var characterValue = rawName[i] + shift;
                    builder.Append((char)characterValue);
                }

                playerData.DisplayName = builder.ToString();
            }
            else
            {
                playerData.DisplayName = model.PlayerNamePrivate;
            }
        }

        if (playerData.IsPlayersReplay)
        {
            match.ReplayPlayer = playerData;
        }

        if (model.DeathCause != EDeathCause.EDeathCauseMax)
        {
            playerData.LastDeathOrKnockTime = match.GameState.CurrentWorldTime;
        }

        if (model.TeamIndex != null)
        {
            if (!states.Teams.TryGetValue(model.TeamIndex.Value, out var team))
            {
                team = new Team();
                states.Teams[model.TeamIndex.Value] = team;
            }

            team.Players.Add(playerData);
        }

        if (model.BDbno != null || model.FinisherOrDowner != null || model.RebootCounter != null)
        {
            UpdateKillFeed(playerData, model, match, states);
        }

        if (model.BIsDisconnected == true &&
            playerData.StatusChanges.LastOrDefault()?.CurrentPlayerState != EPlayerState.Killed)
        {
            var entry = new KillFeedEntry
            {
                FinisherOrDowner = playerData,
                CurrentPlayerState = EPlayerState.Killed,
                Player = playerData,
                DeltaGameTimeSeconds = match.GameState.DeltaGameTime
            };

            playerData.StatusChanges.Add(entry);
            match.KillFeed.Add(entry);
        }

        //Internal info
        playerData.WorldPlayerId = model.WorldPlayerId ?? playerData.WorldPlayerId;
    }

    public static void UpdateKillFeed(PlayerState channelPlayer, PlayerStateExport playerState, MatchData data,
        StateData states)
    {
        var entry = new KillFeedEntry
        {
            DeltaGameTimeSeconds = data.GameState.DeltaGameTime,
            DeathTags = playerState.DeathTags?.Tags.Select(x => x.TagName).ToArray(),
            Distance = playerState.Distance ?? 0
        };

        if (playerState.RebootCounter != null)
        {
            entry.CurrentPlayerState = EPlayerState.Revived;
            entry.Player = channelPlayer;
            entry.FinisherOrDowner = channelPlayer; //Unknown, so figure this out if possible
            entry.ItemType = EItemType.RebootVan;
        }
        else
        {
            var eliminator = channelPlayer.LastKnockedEntry?.FinisherOrDowner;

            if (playerState.FinisherOrDowner != null)
            {
                if (playerState.FinisherOrDowner.Value == 0)
                {
                    //DBNO revives?
                    if (playerState.DeathCause != EDeathCause.EDeathCauseMax)
                    {
                        entry.Player = channelPlayer;
                        entry.CurrentPlayerState = EPlayerState.Alive;
                        channelPlayer.LastKnockedEntry = null;
                        channelPlayer.StatusChanges.Add(entry);
                        data.KillFeed.Add(entry);
                    }

                    return;
                }

                if (states.Players.ContainsKey(playerState.FinisherOrDowner.Value))
                {
                    eliminator = states.Players[playerState.FinisherOrDowner.Value];
                }
            }

            if (playerState.BDbno != null)
            {
                if (playerState.BDbno == true)
                {
                    channelPlayer.LastKnockedEntry = entry;
                }

                entry.Player = channelPlayer;
                entry.FinisherOrDowner = eliminator;
                entry.CurrentPlayerState = playerState.BDbno == true ? EPlayerState.Knocked : EPlayerState.Killed;
                entry.DeathCause = playerState.DeathCause;

                if (entry.DeathCause == EDeathCause.EDeathCauseMax)
                {
                    if (channelPlayer.LastKnockedEntry?.DeathCause != null)
                    {
                        entry.DeathCause = channelPlayer.LastKnockedEntry.DeathCause;
                    }
                }

                if (entry.DeathTags == null)
                {
                    entry.DeathTags = channelPlayer.LastKnockedEntry?.DeathTags;
                }
            }
            else //Full kill
            {
                entry.Player = channelPlayer;
                entry.FinisherOrDowner = eliminator;
                entry.CurrentPlayerState = EPlayerState.Killed;
                entry.DeathCause = playerState.DeathCause;
            }
        }

        channelPlayer.StatusChanges.Add(entry);
        data.KillFeed.Add(entry);
    }
}