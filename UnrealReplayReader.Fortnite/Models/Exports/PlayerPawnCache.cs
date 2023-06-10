using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record PlayerPawnCache : ExportModel
{
    public record Configuration : GroupConfiguration<PlayerPawnCache>
    {
        public Configuration()
        {
            AddPath("PlayerPawn_Athena_C_ClassNetCache");
            AddPath("BP_PlayerPawn_Athena_Phoebe_C_ClassNetCache");
            AddPath("BP_AIPawn_Labrador_C_ClassNetCache");
            IsClassNetCache = true;
            AddPropertyFunction("NetMulticast_Athena_BatchedDamageCues",
                "/Script/FortniteGame.FortPawn:NetMulticast_Athena_BatchedDamageCues");
            AddNetDeltaProperty("ClientObservedStats", "/Script/FortniteGame.FortClientObservedStat", false);
            
        }
    }
}