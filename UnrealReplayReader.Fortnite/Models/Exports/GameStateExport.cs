using UnrealReplayReader.Fortnite.Models.Enums;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record GameStateExport : ExportModel
{
    public int? WinningTeam { get; set; }
    public int? PlayerBotsLeft { get; set; }
    public int? PlayersLeft { get; set; }
    public EFortGameplayState GameplayState { get; set; }
    public EAthenaGamePhase? GamePhase { get; set; }
    public EServerStability ServerStability { get; set; }
    public int? ServerChangelistNumber { get; set; }
    public string? GameSessionId { get; set; }
    public int? TotalPlayerStructures { get; set; }
    public int? ElapsedTime { get; set; }
    public int? TeamCount { get; set; }
    public int? TeamSize { get; set; }
    public int? TeamsLeft { get; set; }
    public bool? BIsLargeTeamGame { get; set; }
    public FDateTime? UtcTimeStartedMatch { get; set; }
    public ActorGuid? RecorderPlayerState { get; set; }
    public double? ReplicatedWorldTimeSeconds { get; set; }
    public GameStateExport[]? TeamFlightPaths { get; set; }
    public FVector? FlightStartLocation { get; set; }
    public FRotator? FlightStartRotation { get; set; }
    public float? AircraftStartTime { get; set; }
    public float? SafeZonesStartTime { get; set; }
    public float? EndGameStartTime { get; set; }
    public EEventTournamentRound? EventTournamentRound { get; set; }
    public byte? SafeZonePhase { get; set; }
    public byte[]? ActiveTeamNums { get; set; }
    public string MatchState { get; set; }

    public record Configuration : GroupConfiguration<GameStateExport>
    {
        public Configuration()
        {
            AddPath("/Game/Athena/Athena_GameState.Athena_GameState_C");
            AddProperty("MatchState", (model, reader) => model.MatchState = reader.ReadFName());


            AddProperty("AICharacterPartsPreloadData", (replay, model, reader, group, field) =>
                reader.ReadDynamicArray<GameStateExport>(replay, group, this));
            AddProperty("AIPawnCustomizationPreloadData", (replay, model, reader, group, field) =>
                reader.ReadDynamicArray<GameStateExport>(replay, group, this));

            AddProperty("VariantUsageByCosmetic", (replay, model, reader, group, field) =>
                reader.ReadDynamicArray<GameStateExport>(replay, group, this));
            AddProperty("VariantUsage", (replay, model, reader, group, field) =>
                reader.ReadDynamicArray<GameStateExport>(replay, group, this));
            AddProperty("WinningPlayerList", (replay, model, reader, group, field) =>
                reader.ReadDynamicArray<GameStateExport>(replay, group, this));


            // AddDependency("AICharacterPartsPreloadData", typeof(FAICharacterPartsPreloadData));

            // AddProperty("AICharacterPartsPreloadData", (replay, model, reader, group, field) =>
            //     reader.ReadDynamicArray<FAICharacterPartsPreloadData.Configuration>(replay, group)
            //     );

            // AddProperty("AICharacterPartsPreloadData", (a, b, c, d) =>
            // {
            //     b.ReadDynamicArray<GameStateExport>(a, d);
            //     //Priority
            //     //CharacterPart
            //     // var x = reader.ReadFName();
            //     // Console.WriteLine(x);
            // });
            //

            AddProperty("TeamFlightPaths",
                (replay, model, reader, group, field) =>
                    model.TeamFlightPaths = reader.ReadDynamicArray<GameStateExport>(replay, group, this));
            AddProperty("FlightStartLocation",
                (model, reader) => model.FlightStartLocation = reader.ReadPackedVector(1));
            AddProperty("FlightStartRotation", (model, reader) => model.FlightStartRotation = reader.ReadRotationShort());


            AddProperty("GameplayState",
                (model, reader) => model.GameplayState = reader.ReadBitsToUInt32AsEnum<EFortGameplayState>());
            AddProperty("GamePhase",
                (model, reader) => model.GamePhase = reader.ReadBitsToUInt32AsEnum<EAthenaGamePhase>());
            AddProperty("ServerStability",
                (model, reader) => model.ServerStability = reader.ReadBitsToUInt32AsEnum<EServerStability>(3));
            AddProperty("GameSessionId", (model, reader) => model.GameSessionId = reader.ReadFString());
            AddProperty("ServerChangelistNumber", (model, reader) => model.ServerChangelistNumber = reader.ReadInt32());
            AddProperty("TotalPlayerStructures", (model, reader) => model.TotalPlayerStructures = reader.ReadInt32());
            AddProperty("TeamCount", (model, reader) => model.TeamCount = reader.ReadInt32());
            AddProperty("UtcTimeStartedMatch",
                (model, reader) => model.UtcTimeStartedMatch = reader.ReadCustomClass<FDateTime>());
            AddProperty("RecorderPlayerState",
                (model, reader) => model.RecorderPlayerState = reader.ReadCustomClass<ActorGuid>());
            AddProperty("ReplicatedWorldTimeSeconds",
                (model, reader) => model.ReplicatedWorldTimeSeconds = reader.ReadSingle());
            AddProperty("ReplicatedWorldTimeSecondsDouble",
                (model, reader) => model.ReplicatedWorldTimeSeconds = reader.ReadDouble());
            AddProperty("AircraftStartTime", (model, reader) => model.AircraftStartTime = reader.ReadSingle());
            AddProperty("SafeZonesStartTime", (model, reader) => model.SafeZonesStartTime = reader.ReadSingle());
            AddProperty("EndGameStartTime", (model, reader) => model.EndGameStartTime = reader.ReadSingle());
            AddProperty("WinningTeam", (model, reader) => model.WinningTeam = reader.ReadInt32());
            AddProperty("PlayerBotsLeft", (model, reader) => model.PlayerBotsLeft = reader.ReadInt32());
            AddProperty("PlayersLeft", (model, reader) => model.PlayersLeft = reader.ReadInt32());
            AddProperty("ElapsedTime", (model, reader) => model.ElapsedTime = reader.ReadInt32());
            AddProperty("TeamSize", (model, reader) => model.TeamSize = reader.ReadInt32());
            AddProperty("TeamsLeft", (model, reader) => model.TeamsLeft = reader.ReadInt32());
            AddProperty("SafeZonePhase", (model, reader) => model.SafeZonePhase = reader.ReadByte());
            AddProperty("EventTournamentRound",
                (model, reader) =>
                    model.EventTournamentRound = reader.ReadBitsToUInt32AsEnum<EEventTournamentRound>(3));
            AddProperty("bIsLargeTeamGame", (model, reader) => model.BIsLargeTeamGame = reader.ReadBit());
            AddProperty("ActiveTeamNums",
                (model, reader) => model.ActiveTeamNums = reader.ReadDynamicArray(() => reader.ReadByte()));

            AddProperty("AdditionalPlaylistLevelsStreamed", (replay, model, reader, group, field) =>
                reader.ReadDynamicArray<GameStateExport>(replay, group, this));
            AddProperty("LevelName", (model, reader) =>
                reader.ReadFName());
        }

        private void AddDependency(string aicharacterpartspreloaddata, Type type,
            NetFieldExportGroup parentGroup,
            NetFieldExportGroup childGroup
        )
        {
            throw new NotImplementedException();
        }
    }

    public record FAICharacterPartsPreloadData : ExportModel
    {
        public float? Priority { get; set; }
        public ActorGuid? CharacterPart { get; set; }

        public record Configuration : GroupConfiguration<FAICharacterPartsPreloadData>
        {
            public Configuration()
            {
                // AddParent(GameStateExport);
                AddPath("/Script/FortniteGame.AICharacterPartsPreloadData");
                AddProperty("Priority",
                    (model, reader) =>
                        model.Priority = reader.ReadSingle()
                );
                AddProperty("CharacterPart",
                    (model, reader) =>
                        model.CharacterPart = reader.ReadCustomClass<ActorGuid>()
                );
            }
        }
    }
}