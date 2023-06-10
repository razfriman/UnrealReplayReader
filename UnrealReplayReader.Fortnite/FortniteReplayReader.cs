using System.Text.Json;
using UnrealReplayReader.Fortnite.Events;
using UnrealReplayReader.Fortnite.Handlers;
using UnrealReplayReader.Fortnite.Models.Exports;
using UnrealReplayReader.Fortnite.Models.Replay;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;
using UnrealReplayReader.Reader;

namespace UnrealReplayReader.Fortnite;

public class FortniteReplayReader : ReplayReader<FortniteReplay>
{

    public static FortniteReplay FromFile(string file, ReplayReaderSettings replaySettings)
    {
        var reader = new FortniteReplayReader();
        var replay = reader.ReadFile(file, replaySettings);
        return replay;
    }

    public override void EmitEvent(FLocalFileEventInfo chunk, ByteReader reader) =>
        FortniteEventReader.ReadEvent(chunk, reader, Replay, Logger);

    public override void EmitActorSpawn(NetFieldExportGroup exportGroup, Actor actor, string? staticActorId)
    {
        if (exportGroup?.Configuration?.Model != null)
        {
            switch (exportGroup.Configuration.Model)
            {
                case PlayerPawnExport playerPawn:
                    HandlePlayerPawn.OnActorSpawn(playerPawn, actor, Replay.MatchData, Replay.StateData);
                    break;
            }
        }
    }

    public override void EmitFunction(ExportModel instance, NetFieldExportGroup group, NetFieldExport? field,
        Bunch bunch, List<string> changedProperties,
        string? staticActorId = null)
    {
        var actor = bunch.Actor;
        switch (instance)
        {
            case null:
                switch (field.Name)
                {
                    default:
                        Console.WriteLine($"Unhandled Simple Function: {group.Name}::{field.Name}");
                        break;
                }
                break;
            case BatchedDamageCuesExport batchedDamageCues:
                HandleBatchedDamageCues.OnProperties(batchedDamageCues, actor, Replay.MatchData, Replay.StateData);
                break;
            default:
                Console.WriteLine("Unhandled Typed Function: " + group.PathName);
                break;
        }
    }

    public override void EmitNetDelta(ExportModel? instance, NetFieldExportGroup exportGroup, int elementIndex,
        bool isDeleted, Bunch bunch)
    {
        switch (instance)
        {
            case InventoryExport inventoryExport:
                HandleInventory.OnNetDelta(inventoryExport, elementIndex, isDeleted, bunch, Replay.MatchData,
                    Replay.StateData);
                break;
            case FortClientObservedStatExport fortClientObservedStatExport:
                HandleFortClientObservedStat.OnNetDelta(fortClientObservedStatExport, elementIndex, isDeleted, bunch,
                    Replay.MatchData,
                    Replay.StateData);
                break;
            default:
                Console.WriteLine("Unhandled NetExport: " + exportGroup.PathName);
                break;
        }
    }

    private static void DebugModel(ExportModel model, List<string> changedProperties)
    {
        if (changedProperties != null && changedProperties.Count > 0)
        {
            Console.WriteLine($"Changed Properties: {string.Join(" ", changedProperties)}");
        }

        Console.WriteLine(model.GetType().Name);
        Console.WriteLine(JsonSerializer.Serialize<object>(model, new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            WriteIndented = true
        }));
        Console.WriteLine();
    }

    public override void EmitProperties(ExportModel model, NetFieldExportGroup group, Bunch bunch,
        List<string> changedProperties,
        ExternalData? externalData = null,
        NetFieldExport? field = null, string? staticActorId = null)
    {
        var actor = bunch.Actor;
        switch (model)
        {
            case VictoryCrownGameStateExport victoryCrownGameStateExport:
                HandleVictoryCrown.OnProperties(victoryCrownGameStateExport, actor, Replay.MatchData, Replay.StateData);
                break;
            case GameStateCache gameStateCache:
                HandleGameState.OnProperties(gameStateCache, actor, Replay.MatchData, Replay.StateData);
                break;
            case LlamaExport llama:
                HandleLlama.OnProperties(llama, actor, Replay.MatchData, Replay.StateData);
                break;
            case LabradorExport labrador:
                HandleLabrador.OnProperties(labrador, actor, Replay.MatchData, Replay.StateData);
                break;
            case SupplyDropExport supplyDrop:
                HandleSupplyDrop.OnProperties(supplyDrop, actor, Replay.MatchData, Replay.StateData);
                break;
            case GameStateExport gameState:
                HandleGameState.OnProperties(gameState, actor, Replay.MatchData, Replay.StateData);
                break;
            case PlayerStateExport playerState:
                HandlePlayerState.OnProperties(playerState, actor, externalData, Replay.MatchData, Replay.StateData);
                break;
            case PlayerPawnExport playerPawn:
                HandlePlayerPawn.OnProperties(playerPawn, actor, Replay.MatchData, Replay.StateData);
                break;
            case SafeZoneIndicatorExport safeZoneIndicator:
                HandleSafeZone.OnProperties(safeZoneIndicator, actor, Replay.MatchData, Replay.StateData);
                break;
            case InventoryExport inventoryExport:
                HandleInventory.OnProperties(inventoryExport, actor, Replay.MatchData, Replay.StateData);
                break;
            case PlayerControllerExport playerControllerExport:
                break;
            case HealthSetExport healthSetExport:
                HandleHealthSet.OnProperties(healthSetExport, actor, Replay.MatchData, Replay.StateData);
                break;
            default:
                Console.WriteLine($"Unhandled export: {model.GetType().Name}");
                break;
        }
    }

    public override void EmitReadReplayFinished()
    {
        if (Settings.IsDebug)
        {
            File.WriteAllText("NetFieldExports.json", JsonSerializer.Serialize(Replay.ExportGroupDict,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
            File.WriteAllText("PartiallyHydratedNetFieldExports.json", JsonSerializer.Serialize(Replay.ExportGroupDict
                    .Where(x =>
                        x.Value.Any(x => x.Value.Count == 0) &&
                        x.Value.Any(x => x.Value.Count > 0)
                    ),
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
            File.WriteAllText("FailedPaths.json", JsonSerializer.Serialize(
                GuidCache.FailedPaths.Values.ToHashSet().OrderBy(x => x),
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }));

            File.WriteAllText("FailedStaticPaths.json", JsonSerializer.Serialize(
                GuidCache.FailedStaticPaths.Values
                    .OrderBy(x => x)
                    .GroupBy(x => x)
                    .OrderByDescending(x => x.Count())
                    .ToDictionary(x => x.Key, x => x.Count()),
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
            File.WriteAllText("NetGuids.json", JsonSerializer.Serialize(
                GuidCache.NetGuids.Values
                    .Select(x => x.Path)
                    .OrderBy(x => x)
                    .GroupBy(x => x)
                    .ToDictionary(x => x.Key, x => x.Count())
                    .ToList(),
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
        }
    }
}