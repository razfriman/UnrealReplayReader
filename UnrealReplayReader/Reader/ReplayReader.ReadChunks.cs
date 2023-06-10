using UnrealReplayReader.IO;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private void ReadChunks(ByteReader reader, uint startingTime = 0)
    {
        var time = startingTime;
        if (Settings.UseCheckpoints && Replay.Info.Checkpoints.Count > 0)
        {
            var checkpoint = Replay.Info.Checkpoints.Last();
            Replay.Info.Checkpoints.Remove(checkpoint);
            ReadCheckpoint(checkpoint, reader);
            time = checkpoint.Time2;
        }

        for (var i = 0; i < Replay.Info.DataChunks.Count; i++)
        {
            var dataChunk = Replay.Info.DataChunks[i];
            if (time <= dataChunk.Time1)
            {
                ReadDataChunk(dataChunk, reader);
                time = dataChunk.Time2;
            }
        }
    }
}