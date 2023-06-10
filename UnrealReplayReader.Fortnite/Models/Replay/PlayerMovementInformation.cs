using UnrealReplayReader.Fortnite.Models.Enums;

namespace UnrealReplayReader.Fortnite.Models.Replay;

public record PlayerMovementInformation
{
    public bool InBus { get; set; }
    public bool Crouched { get; set; }
    public bool IsSlopeSliding { get; set; }
    public bool GliderOpen { get; set; }
    public bool Skydiving { get; set; }
    public bool IsInteracting { get; set; }
    public bool IsEmoting { get; set; }
    public bool IsTargeting { get; set; }
    public bool JumpedForceApplied { get; set; }
    public bool Sprinting { get; set; }
    public bool IsInWater { get; set; }
    public EFortMovementStyle MovementStyle { get; set; } = EFortMovementStyle.EFortMovementStyleMax;

    public PlayerMovementInformation Copy()
    {
        return new PlayerMovementInformation
        {
            InBus = InBus,
            Crouched = Crouched,
            GliderOpen = GliderOpen,
            IsEmoting = IsEmoting,
            IsInteracting = IsInteracting,
            IsSlopeSliding = IsSlopeSliding,
            IsTargeting = IsTargeting,
            Sprinting = Sprinting,
            Skydiving = Skydiving,
            JumpedForceApplied = JumpedForceApplied,
            IsInWater = IsInWater,
            MovementStyle = MovementStyle
        };
    }
}