namespace UnrealReplayReader.Fortnite.Models.Enums;

public enum EAthenaRoundsMutatorPhase : byte
{
    GamePhaseSetup = 0,
    GamePhaseWarmup = 1,
    FadeOutToNextRound = 2,
    RoundSetup = 3,
    RoundPlay = 4,
    RoundEnd = 5,
    RoundEndUi = 6,
    MatchEndUi = 7,
    MatchEndedPrematurely = 8,
    EAthenaRoundsMutatorPhaseMax = 9
};