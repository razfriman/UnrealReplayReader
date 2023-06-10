namespace UnrealReplayReader.Models;

public record struct FRotator
{
    public double Pitch { get; set; }
    public double Yaw { get; set; }
    public double Roll { get; set; }
}