namespace UnrealReplayReader.Models.Enums;

public enum EChannelTypes
{
    None = 0, // Invalid type.
    Control, // Connection control.
    Actor, // Actor-update channel.
    File, // Binary file transfer.
    Voice, // Voice channel
    Max,
}