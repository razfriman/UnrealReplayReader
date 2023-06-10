using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record ClientRemotePlayerDamagedResourceBuildingExport : ExportModel
{
    public record Configuration : GroupConfiguration<ClientRemotePlayerDamagedResourceBuildingExport>
    {
        public Configuration()
        {
            AddPath("/Script/FortniteGame.FortBroadcastRemoteClientInfo:ClientRemotePlayerDamagedResourceBuilding");
        }
    }
}