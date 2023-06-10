namespace UnrealReplayReader.Models.Enums;

public enum ELocalFileVersionHistory
{
    HistoryInitial = 0,
    HistoryFixedsizeFriendlyName = 1,
    HistoryCompression = 2,
    HistoryRecordedTimestamp = 3,
    HistoryStreamChunkTimes = 4,
    HistoryFriendlyNameEncoding = 5,
    HistoryEncryption = 6,
    HistoryCustomVersions = 7,

    // -----<new versions can be added before this line>-------------------------------------------------
    HistoryPlusOne,
    HistoryLatest = HistoryPlusOne - 1
}