using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader.FortniteMinimal.Models.Exports;

public record GameState : ExportModel
{
    public string? GameSessionId { get; set; }
    public FDateTime? UtcTimeStartedMatch { get; set; }
    public ActorGuid? RecorderPlayerState { get; set; }

    public record Configuration : GroupConfiguration<GameState>
    {
        public Configuration()
        {
            AddPath("/Game/Athena/Athena_GameState.Athena_GameState_C");
            AddProperty("GameSessionId", (model, reader) => model.GameSessionId = reader.ReadFString());
            AddProperty("UtcTimeStartedMatch",
                (model, reader) => model.UtcTimeStartedMatch = reader.ReadCustomClass<FDateTime>());
            AddProperty("RecorderPlayerState",
                (model, reader) => model.RecorderPlayerState = reader.ReadCustomClass<ActorGuid>());
        }
    }
}