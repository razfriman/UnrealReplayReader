using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record GameplayCueExport : ExportModel
{
    public FGameplayTag GameplayCueTag { get; set; }

    public record Configuration : GroupConfiguration<GameplayCueExport>
    {
        public Configuration()
        {
            // AddPath("/Script/GameplayAbilities.AbilitySystemComponent:NetMulticast_InvokeGameplayCueExecuted_WithParams");
            AddPath("/Script/FortniteGame.FortPawn:NetMulticast_InvokeGameplayCueAdded_WithParams");
            AddPath("/Script/FortniteGame.FortPawn:NetMulticast_InvokeGameplayCueExecuted_WithParams");
            AddPath("/Script/FortniteGame.FortPawn:NetMulticast_InvokeGameplayCueExecuted_FromSpec");
            AddProperty(x => x.GameplayCueTag,
                (model, reader) => model.GameplayCueTag = reader.ReadCustomClass<FGameplayTag>());
        }
    }
}