using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    public void ProcessBunch(Bunch bunch)
    {
        var channel = Channels.GetValueOrDefault(bunch.ChIndex, null);

        if (bunch.BHasMustBeMappedGuids)
        {
            var numMustBeMappedGuiDs = bunch.Reader.ReadUInt16();
            for (var i = 0; i < numMustBeMappedGuiDs; i++)
            {
                bunch.Reader.ReadIntPacked();
            }
        }

        if (channel != null && channel.Actor == null)
        {
            if (!bunch.BOpen)
            {
                return;
            }

            var inActor = new Actor
            {
                ActorNetGuid = ReadNetGuid(bunch.Reader, false),
            };

            GuidCache.AddActor(inActor);

            if (inActor.ActorNetGuid.IsDynamic)
            {
                if (bunch.Reader.AtEnd)
                {
                    return;
                }

                inActor.Archetype = ReadNetGuid(bunch.Reader, false);

                if (bunch.Reader.EngineNetworkVersion >= EEngineNetworkVersionHistory.HistoryNewActorOverrideLevel)
                {
                    inActor.Level = ReadNetGuid(bunch.Reader, false);
                }

                inActor.Location = bunch.Reader.SerializeQuantizedVector(new FVector());

                if (bunch.Reader.ReadBit())
                {
                    inActor.Rotation = bunch.Reader.ReadRotationShort();
                }
                else
                {
                    inActor.Rotation = new FRotator();
                }

                inActor.Scale = bunch.Reader.SerializeQuantizedVector(new FVector
                {
                    X = 1,
                    Y = 1,
                    Z = 1
                });
                inActor.Velocity = bunch.Reader.SerializeQuantizedVector(new FVector());
            }

            channel.Actor = inActor;

            OnChannelOpened(channel, inActor, bunch);

            var path = GuidCache.TryGetPathName(channel.Actor?.Archetype?.Value ?? 0);
            if (path != null)
            {
                if (NetFieldParser.PlayerControllers.Contains(path))
                {
                    bunch.Reader.ReadByte();
                }
            }
        }

        bunch.Actor = channel.Actor;

        while (!bunch.Reader.AtEnd && !bunch.Reader.IsError)
        {
            var payload = ReadContentBlockPayload(bunch);
            if (payload.numPayloadBits > 0)
            {
                bunch.Reader.SetTempEnd((int)payload.numPayloadBits, FBitArchiveEndIndex.ContentBlockPayload);
            }

            if (payload.bObjectDeleted)
            {
                if (payload.numPayloadBits > 0)
                {
                    bunch.Reader.RestoreTempEnd(FBitArchiveEndIndex.ContentBlockPayload);
                }

                continue;
            }

            if (bunch.Reader.IsError)
            {
                if (payload.numPayloadBits > 0)
                {
                    bunch.Reader.RestoreTempEnd(FBitArchiveEndIndex.ContentBlockPayload);
                }

                break;
            }

            if (payload.repObject is null or 0 || payload.numPayloadBits == 0)
            {
                if (payload.numPayloadBits > 0)
                {
                    bunch.Reader.RestoreTempEnd(FBitArchiveEndIndex.ContentBlockPayload);
                }

                continue;
            }

            ReceivedReplicatorBunch(bunch, bunch.Reader, payload.repObject.Value, payload.bOutHasRepLayout,
                payload.bIsActor);

            if (payload.numPayloadBits > 0)
            {
                bunch.Reader.RestoreTempEnd(FBitArchiveEndIndex.ContentBlockPayload);
            }
        }

        if (bunch.BClose)
        {
            OnChannelClosed(bunch);
        }
    }
}