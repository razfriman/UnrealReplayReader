namespace UnrealReplayReader.Models;

public record struct FTransform
{
    public FQuat Rotation { get; set; }
    public FVector Translation { get; set; }
    public FVector Scale3D { get; set; }
}