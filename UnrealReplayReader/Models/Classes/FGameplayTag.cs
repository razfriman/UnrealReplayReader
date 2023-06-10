using UnrealReplayReader.IO;

namespace UnrealReplayReader.Models.Classes;

public record FGameplayTag : CustomClass
{
    public uint TagIndex { get; set; }
    public string? TagName { get; set; }

    public override void Serialize(BitReader reader)
    {
        TagIndex = reader.ReadIntPacked();
    }

    public override void Resolve(GuidCache cache)
    {
        TagName = cache.TryGetTagName(TagIndex);
    }
}