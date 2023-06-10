using UnrealReplayReader.IO;

namespace UnrealReplayReader.Models.Classes;

public record FGameplayTagContainer : CustomClass
{
    public List<FGameplayTag> Tags { get; set; } = new List<FGameplayTag>();

    public override void Serialize(BitReader reader)
    {
        if (reader.ReadBit())
        {
            return;
        }

        var numTags = reader.ReadBitsToUInt32(7);
        Tags = new List<FGameplayTag>((int)numTags);
        for (var i = 0; i < numTags; i++)
        {
            var tag = new FGameplayTag();
            tag.Serialize(reader);
            Tags.Add(tag);
        }
    }

    public override void Resolve(GuidCache cache)
    {
        if (Tags.Count == 0)
        {
            return;
        }

        foreach (var tag in Tags)
        {
            tag.Resolve(cache);
        }
    }
}