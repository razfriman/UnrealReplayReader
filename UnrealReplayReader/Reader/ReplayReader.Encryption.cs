using System.Security.Cryptography;
using UnrealReplayReader.IO;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private ByteReader DecryptReader(ByteReader existingReader, int size)
    {
        var slice = existingReader.ReadBytesAsMemory(size);
        if (!Replay.Info.Encrypted)
        {
            return new ByteReader(slice, existingReader);
        }

        var key = Replay.Info.EncryptionKey;

        using var rDel = new RijndaelManaged
        {
            KeySize = (key.Length * 8),
            Key = key,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };
        using var cTransform = rDel.CreateDecryptor();
        var decryptedArray = cTransform.TransformFinalBlock(slice.ToArray(), 0, slice.Length);

        return new ByteReader(decryptedArray, existingReader);
    }
}