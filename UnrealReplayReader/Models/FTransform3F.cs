namespace UnrealReplayReader.Models;

public record struct FTransform3F
{
    public FVector4F Rotation { get; set; }
    public FVector4F Translation { get; set; }
    public FVector4F Scale3D { get; set; }
};