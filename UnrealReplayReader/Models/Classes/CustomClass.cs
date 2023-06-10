using UnrealReplayReader.IO;

namespace UnrealReplayReader.Models.Classes;

public record CustomClass
{
    public virtual void Serialize(BitReader reader)
    {
    }

    public virtual void Resolve(GuidCache netGuidCache)
    {
    }
}