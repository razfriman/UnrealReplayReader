using UnrealReplayReader.IO;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Models;

public class Bunch
{
    public int PacketId { get; set; }
    public uint ChIndex { get; set; }
    public EChannelTypes ChType { get; set; }
    public int ChSequence { get; set; }
    public bool BOpen { get; set; }
    public bool BClose { get; set; }
    public bool BDormant { get; set; }
    public bool BIsReplicationPaused { get; set; }
    public bool BControl { get; set; }
    public bool BReliable { get; set; }
    public bool BPartial { get; set; }
    public bool BPartialInital { get; set; }
    public bool BPartialFinal { get; set; }
    public bool BHasPackageExportMaps { get; set; }
    public bool BHasMustBeMappedGuids { get; set; }
    public uint CloseReason { get; set; }
    public float TimeSeconds { get; set; }
    public uint BunchDataBits { get; set; }
    public NetBitReader? Reader { get; set; }
    public Actor? Actor { get; set; }
    public uint ActorId => Actor?.ActorNetGuid.Value ?? 0;
}