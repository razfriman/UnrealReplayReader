using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Classes;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    public (bool bObjectDeleted, bool bOutHasRepLayout, uint? repObject, bool bIsActor)
        ReadContentBlockHeader(Bunch bunch)
    {
        var bObjectDeleted = false;
        var bOutHasRepLayout = bunch.Reader.ReadBit();
        var bIsActor = bunch.Reader.ReadBit();
        var actor = Channels[bunch.ChIndex].Actor;

        if (bIsActor)
        {
            return (
                bObjectDeleted,
                bOutHasRepLayout,
                actor.Archetype?.Value ?? actor.ActorNetGuid.Value,
                bIsActor
            );
        }

        var netGuid = ReadNetGuid(bunch.Reader, false);
        var bStablyNamed = bunch.Reader.ReadBit();

        if (bStablyNamed)
        {
            return (
                bObjectDeleted,
                bOutHasRepLayout,
                netGuid.Value,
                false
            );
        }


        // OLD
        // var classNetGuid = ReadNetGuid(bunch.Reader, false);
        // if (!classNetGuid.IsValid)
        // {
        //     bObjectDeleted = true;
        //     return (
        //         bObjectDeleted,
        //         bOutHasRepLayout,
        //         netGuid.Value,
        //         false
        //     );
        // }


        // NEW
        var bDeleteSubObject = false;
        var bSerializeClass = true;

        if (bunch.Reader.EngineNetworkVersion >= EEngineNetworkVersionHistory.HistorySubobjectDestroyFlag)
        {
            var bIsDestroyMessage = bunch.Reader.ReadBit();
            if (bIsDestroyMessage)
            {
                bDeleteSubObject = true;
                bSerializeClass = false;
                var destroyFlags = bunch.Reader.ReadByte(); // Destroy flags to be defined later
            }
        }
        else
        {
            bSerializeClass = true;
        }

        NetworkGuid? classNetGuid = null;

        if (bSerializeClass)
        {
            // Serialize the class in case we have to spawn it.
            // Manually serialize the object so that we can get the NetGUID (in order to assign it if we spawn the object here)
            classNetGuid = ReadNetGuid(bunch.Reader, false);
            // When ClassNetGUID is empty it means the sender requested to delete the subobject
            bDeleteSubObject = !classNetGuid.IsValid;
        }

        if (bDeleteSubObject)
        {
            bObjectDeleted = true;
            return (
                bObjectDeleted,
                bOutHasRepLayout,
                netGuid.Value,
                false
            );
        }

        if (bunch.Reader.EngineNetworkVersion >= EEngineNetworkVersionHistory.HistorySubobjectOuterChain)
        {
            var bActorIsOuter = bunch.Reader.ReadBit();

            if (!bActorIsOuter)
            {
                ReadNetGuid(bunch.Reader, false);
            }
        }

        return (
            bObjectDeleted,
            bOutHasRepLayout,
            classNetGuid?.Value,
            false
        );
    }
}