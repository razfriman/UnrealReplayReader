using UnrealReplayReader.IO;
using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader.Fortnite.Models.Classes;

public record FAthenaPawnReplayData : CustomClass
{
    public byte[] EncryptedPlayerData { get; set; }

    public override void Serialize(BitReader reader)
    {
        var length = reader.ReadUInt32();
        EncryptedPlayerData = reader.ReadBytes((int)length);
    }
}