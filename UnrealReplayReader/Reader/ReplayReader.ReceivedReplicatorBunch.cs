using UnrealReplayReader.IO;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    public bool ReceivedReplicatorBunch(Bunch bunch, NetBitReader reader, uint repObject, bool bHasRepLayout,
        bool bIsActor)
    {
        //AGID_KeysAndLocks_Key
        NetFieldExportGroup? group;
        string? staticActorId = null;

        if (bunch.Actor.ActorNetGuid.IsDynamic || !bIsActor)
        {
            group = GuidCache.GetNetFieldExportGroup(repObject);
        }
        else
        {
            var result = GuidCache.GetStaticActorExportGroup(repObject);
            group = result.group;
            staticActorId = result.staticActorId;
        }

        if (group == null)
        {
            if (bIsActor && !Settings.IsDebug)
            {
                IgnoredChannels[bunch.ChIndex] = true;
            }

            return true;
        }

        if (bHasRepLayout)
        {
            var propertiesResult = ReceiveProperties(
                reader,
                group,
                bunch,
                staticActorId,
                true,
                true);
            if (propertiesResult == null)
            {
                return false;
            }
        }

        if (reader.AtEnd)
        {
            return true;
        }

        var classNetCache = GuidCache.TryGetClassNetCache(
            group.PathName,
            bunch.Reader.EngineNetworkVersion >= EEngineNetworkVersionHistory.HistoryClassnetcacheFullname);

        if (classNetCache == null)
        {
            return false;
        }

        ReadClassNetCache(reader, bunch, staticActorId, classNetCache);
        return false;
    }
}