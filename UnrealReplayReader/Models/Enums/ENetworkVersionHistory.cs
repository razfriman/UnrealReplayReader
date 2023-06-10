namespace UnrealReplayReader.Models.Enums;

public enum ENetworkVersionHistory
{
    HistoryReplayInitial = 1,
    HistorySaveAbsTimeMs = 2, // We now save the abs demo time in ms for each frame (solves accumulation errors)
    HistoryIncreaseBuffer = 3, // Increased buffer size of packets, which invalidates old replays
    HistorySaveEngineVersion = 4, // Now saving engine net version + InternalProtocolVersion
    HistoryExtraVersion = 5, // We now save engine/game protocol version, checksum, and changelist
    HistoryMultipleLevels = 6, // Replays support seamless travel between levels
    HistoryMultipleLevelsTimeChanges = 7, // Save out the time that level changes happen
    HistoryDeletedStartupActors = 8, // Save DeletedNetStartupActors inside checkpoints
    HistoryHeaderFlags = 9, // Save out enum flags with demo header
    HistoryLevelStreamingFixes = 10, // Optional level streaming fixes.
    HistorySaveFullEngineVersion = 11, // Now saving the entire FEngineVersion including branch name
    HistoryHeaderGuid = 12, // Save guid to demo header
    HistoryCharacterMovement = 13, // Change to using replicated movement and not interpolation
    HistoryCharacterMovementNointerp = 14, // No longer recording interpolated movement samples
    HistoryGuidNametable = 15, // Added a string table for exported guids
    HistoryGuidcacheChecksums = 16, // Removing guid export checksums from saved data, they are ignored during playback

    HistorySavePackageVersionUe =
        17, // Save engine and licensee package version as well, in case serialization functions need them for compatibility
    HistoryRecordingMetadata = 18, // Adding additional record-time information to the header
    HistoryUseCustomVersion = 19, // Serializing replay and network versions as custom verions going forward

    // -----<new versions can be added before this line>-------------------------------------------------
    HistoryPlusOne,
    HistoryLatest = HistoryPlusOne - 1
}