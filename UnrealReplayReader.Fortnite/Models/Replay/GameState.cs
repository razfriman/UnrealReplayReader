using UnrealReplayReader.Fortnite.Models.Enums;

namespace UnrealReplayReader.Fortnite.Models.Replay;

public record GameState
{
    public string SessionId { get; set; }
    public double GameWorldStartTime { get; set; }
    public float MatchEndTime { get; set; }
    public float InitialSafeZoneStartTime { get; set; }
    public DateTimeOffset MatchTime { get; set; }
    public int TotalBots { get; set; }
    public string? PlaylistId { get; set; }
    public List<string> ActiveGameplayModifiers { get; set; } = new();
    public int TotalTeams { get; set; }
    public int MaxPlayers { get; set; }
    public int WinningTeam { get; set; }

    public double AirCraftStartTime { get; set; }

    public List<Aircraft> BusPaths { get; set; } = new();

    public bool LargeTeamGame { get; set; }

    public EEventTournamentRound EEventTournamentRound { get; set; } =
        EEventTournamentRound.EEventTournamentRoundMax;

    public double CurrentWorldTime { get; set; }
    internal EAthenaGamePhase? GamePhase { get; set; }
    internal int RemainingPlayers { get; set; }
    internal int CurrentTeams { get; set; }
    internal byte SafeZonePhase { get; set; }
    internal int RemainingBots { get; set; }
    internal int TotalPlayerStructures { get; set; }
    internal float ElapsedTime { get; set; } //Time since last update?
    internal int OldTeamSize { get; set; } //Used in older replays
    public double DeltaGameTime => CurrentWorldTime - GameWorldStartTime;
    public uint RecorderActor { get; set; }
}