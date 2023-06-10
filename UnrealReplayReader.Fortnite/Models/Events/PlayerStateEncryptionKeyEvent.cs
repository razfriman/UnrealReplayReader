namespace UnrealReplayReader.Fortnite.Models.Events;

public record struct PlayerStateEncryptionKeyEvent
{
    public byte[] Key { get; set; }
}