using UnrealReplayReader.IO;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    private void ReadEvents(ByteReader reader)
    {
        var orderedEvents = Replay.Info.Events.OrderBy(x => x.Time1).ToList();
        foreach (var chunk in orderedEvents)
        {
            reader.Seek(chunk.EventDataOffset);
            var newReader = DecryptReader(reader, chunk.SizeInBytes);
            EmitEvent(chunk, newReader);
        }
    }
}