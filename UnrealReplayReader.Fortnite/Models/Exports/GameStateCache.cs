using UnrealReplayReader.Fortnite.Models.Classes;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record GameStateCache : ExportModel
{
    public PlaylistInfo? CurrentPlaylistInfo { get; set; }

    public record Configuration : GroupConfiguration<GameStateCache>
    {
        public Configuration()
        {
            AddPath("Athena_GameState_C_ClassNetCache");
            IsClassNetCache = true;
            AddProperty(x => x.CurrentPlaylistInfo,
                (model, reader) => model.CurrentPlaylistInfo = reader.ReadCustomClass<PlaylistInfo>(),
                ParseTypes.Class);
            AddNetDeltaProperty("GameMemberInfoArray", "/Script/FortniteGame.GameMemberInfo", false);
        }
    }
}