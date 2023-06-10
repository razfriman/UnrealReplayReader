namespace UnrealReplayReader.Models.Enums;

public enum FBitArchiveEndIndex
{
    Chunk = 1,
    Packet = 2,
    Bunch = 3,
    ContentBlockPayload = 4,
    FieldHeaderPayload = 5,
    ReadArrayField = 6,
    ReadDynamicArray = 7,
}