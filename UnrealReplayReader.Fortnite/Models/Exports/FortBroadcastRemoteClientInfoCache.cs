using UnrealReplayReader.Fortnite.Models.Classes;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record FortBroadcastRemoteClientInfoCache : ExportModel
{
    public record
        Configuration : GroupConfiguration<FortBroadcastRemoteClientInfoCache>
    {
        public Configuration()
        {
            AddPath("FortBroadcastRemoteClientInfo_ClassNetCache");
            IsClassNetCache = true;
            AddPropertyFunction("ClientRemotePlayerAddMapMarker",
                "/Script/FortniteGame.FortBroadcastRemoteClientInfo:ClientRemotePlayerAddMapMarker");
            AddPropertyFunction("ClientRemotePlayerRemoveMapMarker",
                "/Script/FortniteGame.FortBroadcastRemoteClientInfo:ClientRemotePlayerRemoveMapMarker");
            AddPropertyFunction("ClientRemotePlayerHitMarkers",
                "/Script/FortniteGame.FortBroadcastRemoteClientInfo:ClientRemotePlayerHitMarkers");
            AddPropertyFunction("ClientRemotePlayerDamagedResourceBuilding",
                "/Script/FortniteGame.FortBroadcastRemoteClientInfo:ClientRemotePlayerDamagedResourceBuilding");
        }
    }
}