using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Models;

public record Channel
{
    public uint ChIndex { get; set; }
    public EChannelTypes ChannelType { get; set; }
    public Actor? Actor { get; set; }
}