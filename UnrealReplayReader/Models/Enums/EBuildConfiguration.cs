namespace UnrealReplayReader.Models.Enums;

public enum EBuildConfiguration : byte
{
    /** Unknown build configuration. */
    Unknown,

    /** Debug build. */
    Debug,

    /** DebugGame build. */
    DebugGame,

    /** Development build. */
    Development,

    /** Shipping build. */
    Shipping,

    /** Test build. */
    Test
}