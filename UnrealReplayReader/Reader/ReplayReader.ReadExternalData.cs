using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    public Dictionary<uint, ExternalData> ExternalDatas { get; set; } = new();

    private void ReadExternalData(ByteReader reader)
    {
        while (true)
        {
            var externalDataNumBits = reader.ReadIntPacked();

            if (externalDataNumBits == 0)
            {
                return;
            }

            var externalData = new ExternalData
            {
                NetGuid = reader.ReadIntPacked(),
                ExternalDataNumBytes = (int)(externalDataNumBits + 7) >> 3,
                Handle = reader.ReadByte(),
                Something = reader.ReadByte(),
                IsEncrypted = reader.ReadByte() != 0
            };
            externalData.Payload = reader.ReadBytes(externalData.ExternalDataNumBytes - 3);
            ExternalDatas[externalData.NetGuid] = externalData;
        }
    }
}