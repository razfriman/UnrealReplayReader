namespace UnrealReplayReader.Models;

public enum ParseTypes
{
    Unknown,
    ReadClass,
    ReadDynamicArray,
    ReadEnum,
    Ignore,
    Function,
    Default,
    Class,
    NetDeltaSerialize
}