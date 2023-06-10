using Microsoft.Extensions.Logging;
using UnrealReplayReader.Exceptions;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private void ReceiveRawPacket(Packet packet, ByteReader parentReader)
    {
        int lastByte = packet.Data.Span[packet.Data.Length - 1];

        if (lastByte == 0)
        {
            Logger?.LogError("Malformed packet: Received packet with 0's in last byte of packet");
            throw new ReplayException("Malformed packet: Received packet with 0's in last byte of packet");
        }

        var bitSize = (packet.Data.Length * 8) - 1;
        while (!((lastByte & 0x80) >= 1))
        {
            lastByte *= 2;
            bitSize--;
        }

        var reader = new NetBitReader(packet.Data, bitSize, GuidCache, parentReader);

        try
        {
            if (reader.Available > 0)
            {
                ReceivePacket(packet, reader);
            }
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, $"failed ReceivedPacket, index: {packet.PacketIndex}");
        }
    }
}