using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private Packet ReadPacket(ByteReader reader, float timeSeconds)
    {
        var packet = new Packet
        {
            TimeSeconds = timeSeconds,
            PacketIndex = PacketIndex++
        };
        if (reader.HasLevelStreamingFixes)
        {
            packet.SeenLevelIndex = reader.ReadIntPacked();
        }

        packet.Size = reader.ReadInt32();

        packet.State = packet.Size switch
        {
            0 => 1,
            > 2048 or < 0 => 2,
            _ => 0
        };
        packet.Data = reader.ReadBytesAsMemory(packet.Size);

        return packet;
    }
}