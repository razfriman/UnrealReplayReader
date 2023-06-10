using Microsoft.Extensions.Logging;
using UnrealReplayReader.Fortnite.Models.Events;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Events;

public static class PlayerStateEncryptionKeyReader
{
    public static PlayerStateEncryptionKeyEvent ReadPlayerStateEncryptionKey(FLocalFileEventInfo chunk,
        ByteReader reader, ILogger? logger = null)
    {
        return new PlayerStateEncryptionKeyEvent
        {
            Key = reader.ReadBytes(32)
        };
    }
}