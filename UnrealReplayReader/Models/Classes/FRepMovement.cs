using UnrealReplayReader.IO;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Models.Classes;

public record FRepMovement : CustomClass
{
    public bool BSimulatedPhysicSleep { get; set; }
    public bool BRepPhysics { get; set; }
    public FVector Location { get; set; }
    public FRotator? Rotation { get; set; }
    public FVector LinearVelocity { get; set; }
    public FVector AngularVelocity { get; set; }
    public bool BRepServerFrame { get; set; }
    public bool BRepServerHandle { get; set; }
    public uint ServerFrame { get; set; }
    public uint ServerHandle { get; set; }

    public override void Serialize(BitReader reader) => Serialize(reader, 2, 0, 0);

    public void Serialize(BitReader reader, int locationQuatLevel, int rotationQuatLevel, int velocityQuatLevel)
    {
        BSimulatedPhysicSleep = reader.ReadBit();
        BRepPhysics = reader.ReadBit();

        if (reader.EngineNetworkVersion >= EEngineNetworkVersionHistory.HistoryRepmoveServerframeAndHandle &&
            reader.EngineNetworkVersion != EEngineNetworkVersionHistory.History21AndViewpitchOnlyDoNotUse)
        {
            BRepServerFrame = reader.ReadBit();
            BRepServerHandle = reader.ReadBit();
        }

        Location = reader.ReadPackedVector(locationQuatLevel);
        Rotation = rotationQuatLevel != 0 ? reader.ReadRotationShort() : reader.ReadRotation();
        // LinearVelocity = reader.ReadPackedVector(velocityQuatLevel);
        // if (BRepPhysics)
        // {
        //     AngularVelocity = reader.ReadPackedVector(velocityQuatLevel);
        // }
        //
        // if (BRepServerFrame)
        // {
        //     ServerFrame = reader.ReadIntPacked();
        // }
        //
        // if (BRepServerHandle)
        // {
        //     ServerHandle = reader.ReadIntPacked();
        // }
    }
}