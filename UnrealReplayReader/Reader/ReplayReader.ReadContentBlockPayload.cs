using UnrealReplayReader.Models;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    public (uint? repObject, bool bOutHasRepLayout, bool bObjectDeleted, uint numPayloadBits, bool bIsActor )
        ReadContentBlockPayload(Bunch bunch)
    {
        var header = ReadContentBlockHeader(bunch);

        if (header.bObjectDeleted || !header.repObject.HasValue)
        {
            return (
                header.repObject,
                header.bOutHasRepLayout,
                header.bObjectDeleted,
                0,
                false
            );
        }

        var numPayloadBits = bunch.Reader.ReadIntPacked();

        return (
            header.repObject,
            header.bOutHasRepLayout,
            header.bObjectDeleted,
            numPayloadBits,
            header.bIsActor
        );
    }
}