namespace UnrealReplayReader.Fortnite.Models.Enums;

public enum EPlayEventType : byte
{
    None = 0,
    TeamFlight = 1,
    Zone = 2,
    Elimination = 3,
    PlayerMoves = 4,
    SinglePlayerMove = 5,
    ActorsPosition = 6,
    GameHighlights = 7,
    Timecode = 8,
    EPlayEventTypeMax = 9
};