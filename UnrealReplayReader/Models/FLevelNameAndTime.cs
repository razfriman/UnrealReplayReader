namespace UnrealReplayReader.Models;

public struct FLevelNameAndTime
{
    public string LevelName { get; set; }
    public uint LevelChangeTimeInMs { get; set; }
}