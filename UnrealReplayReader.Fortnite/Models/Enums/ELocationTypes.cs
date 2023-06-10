namespace UnrealReplayReader.Fortnite.Models.Enums;

public enum ELocationTypes
{
    /// <summary>
    /// Grab all possible locations from any player.
    /// </summary>
    All,

    /// <summary>
    /// Grab locations from replay owner and all teammates.
    /// </summary>
    Team,

    /// <summary>
    /// Only grab locations from replay owner.
    /// </summary>
    User,

    /// <summary>
    /// Grab no locations.
    /// </summary>
    None
};