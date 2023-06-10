using UnrealReplayReader.Fortnite.Models.Enums;

namespace UnrealReplayReader.Fortnite.Models.Replay;

public record KillFeedEntry
{
    public PlayerState FinisherOrDowner { get; set; }
    public PlayerState Player { get; set; }
    public EPlayerState CurrentPlayerState { get; set; }
    public EFortRarity ItemRarity { get; set; }
    public EItemType ItemType { get; set; }
    public EDeathCause? DeathCause { get; set; } = EDeathCause.EDeathCauseMax;

    public double DeltaGameTimeSeconds { get; set; }
    public float Distance { get; set; }
    public bool DoNotDisplayInKillFeed { get; set; }
    public bool InsideSafeZone { get; set; }
    public string Biome { get; set; }
    public string PointOfInterest { get; set; }
    public bool IsTargeting { get; set; }
    public bool IsHeadshot { get; set; }

    public string[] DeathTags
    {
        get { return _deathTags; }
        set
        {
            _deathTags = value;
            UpdateWeaponTypes();
        }
    }

    private string[] _deathTags;

    public bool KilledSelf => FinisherOrDowner == Player;
    public bool HasError { get; set; }

    private void UpdateWeaponTypes()
    {
        if (_deathTags == null || _deathTags.Length == 0)
        {
            return;
        }

        foreach (var deathTag in DeathTags)
        {
            if (deathTag == null)
            {
                continue;
            }

            switch (deathTag.ToLower())
            {
                case "item.weapon.melee.sword.katana":
                    ItemType = EItemType.KineticBlade;
                    break;
                case "item.weapon.ranged.assault.radical":
                    ItemType = EItemType.HavocSuppressedAssaultRifle;
                    break;
                case "item.weapon.ranged.shotgun.radical":
                    ItemType = EItemType.HavocPumpShotgun;
                    break;
                case "item.weapon.ranged.assault.pastaripper":
                    ItemType = EItemType.OverclockedPulseRifle;
                    break;
                case "item.power.emeraldglass.push":
                    ItemType = EItemType.DekuSmash;
                    break;
                case "item.weapon.ranged.smg.pdw.x":
                    ItemType = EItemType.HeistedRunAndGunSmg;
                    break;
                case "item.weapon.ranged.shotgun.heavy.x":
                    ItemType = EItemType.HeistedBreacherShotgun;
                    break;
                case "item.weapon.ranged.shotgun.musterauto.x":
                    ItemType = EItemType.HeistedAccelerantShotgun;
                    break;
                case "item.weapon.ranged.smg.musterquickmag.x":
                    ItemType = EItemType.HeistedBlinkMagSmg;
                    break;
                case "item.weapon.ranged.assault.musterscoped.ex":
                    ItemType = EItemType.HeistedExplosiveAssaultRifle;
                    break;
                case "item.consumable.bigleafgrenade":
                    ItemType = EItemType.BigBushBomb;
                    break;
                case "item.weapon.shielder":
                    ItemType = EItemType.GuardianShield;
                    break;
                case "item.weapon.ranged.pistol.muster":
                    ItemType = EItemType.TacticalPistol;
                    break;
                case "item.weapon.ranged.shotgun.musterpump":
                    ItemType = EItemType.ThunderShotgun;
                    break;
                case "item.weapon.ranged.assault.musterscoped":
                    ItemType = EItemType.RedEyeAssaultRifle;
                    break;
                case "item.weapon.ranged.shotgun.musterauto":
                    ItemType = EItemType.MavenAutoShotgun;
                    break;
                case "item.weapon.ranged.smg.musterquickmag":
                    ItemType = EItemType.TwinMagSmg;
                    break;
                case "gameplay.damage.physical.blunt.shockwavemace":
                case "item.weapon.melee.shockwavemace":
                    ItemType = EItemType.ShockwaveHammer;
                    break;
                case "item.weapon.ranged.pistol.donut":
                    ItemType = EItemType.DeadPoolHandCannons;
                    break;
                case "item.weapon.ranged.dmr.excalibur":
                case "item.weapons.ranged.dmr.excalibur.boss":
                    ItemType = EItemType.ExCaliberRifle;
                    break;
                case "homebase.class.iscommando.headshot":
                    IsHeadshot = true;
                    break;
                case "gameplay.insidesafezone":
                    InsideSafeZone = true;
                    break;
                case "gameplay.status.istargeting":
                    IsTargeting = true;
                    break;
                case "pawn.athena.donotdisplayinkillfeed":
                    DoNotDisplayInKillFeed = true;
                    break;
                case "vehicle.biplane":
                    ItemType = EItemType.BiPlane;
                    break;
                case "vehicle.hoagie":
                case "athena.vehicle.hoagie":
                    ItemType = EItemType.Chopper;
                    break;
                case "vehicle.meatball":
                    ItemType = EItemType.Boat;
                    break;
                case "vehicle.valet.basicsedan":
                case "vehicle.valet.pickup":
                case "vehicle.valet.sport":
                case "athena.vehicle.valet":
                case "vehicle.foray":
                case "vehicle.valet.foray":
                case "athena.vehicle.foray":
                case "athena.vehicle.valet.foray":
                case "athena.vehicle.valet.basicsedan":
                case "athena.vehicle.valet.basicpickup":
                case "athena.vehicle.valet.semi":
                case "athena.vehicle.valet.sportscar":
                case "athena.vehicle.valet.taxi":
                case "athena.vehicle.valet.sedan":
                    ItemType = EItemType.Vehicle;
                    break;
                case "weapon.ranged.sniper.bolt":
                    ItemType = EItemType.BoltActionSniperRifle;
                    break;

                case "pawn.athena.npc.wildlife.predator.spicysake":
                case "pawn.athena.npc.wildlife.spicysake":
                    ItemType = EItemType.Shark;
                    break;
                case "pawn.athena.npc.wildlife.predator.robert":
                    ItemType = EItemType.Raptor;
                    break;
                case "pawn.athena.npc.wildlife.predator.grandma":
                    ItemType = EItemType.Wolf;
                    break;
                case "pawn.athena.npc.wildlife.prey.burt":
                    ItemType = EItemType.Boar;
                    break;
                case "pawn.athena.npc.wildlife.hardy":
                    ItemType = EItemType.Hardy; /////////////////////////////
                    break;
                case "gameplay.damage.elemental.fire":
                case "curie.state.elementinteraction.fire":
                case "curie.asset.state.elementalstatus.burning":
                    ItemType = EItemType.Fire;
                    break;
                case "gameplay.damage.physical.poison":
                    ItemType = EItemType.Poison;
                    break;
                case "event.damage.killed.kickedduetoinactivity":
                    ItemType = EItemType.KickedDueToInactivity;
                    break;
                case "gameplay.effect.instantdeath.environment.felloutofworld":
                    ItemType = EItemType.InstantEnvironmentFellOutOfWorld;
                    break;
                case "gameplay.effect.instantdeath.environment.underlandscape":
                    ItemType = EItemType.InstantEnvironmentUnderLandscape;
                    break;
                case "gameplay.effect.instantdeath.environment":
                    ItemType = EItemType.InstantEnvironment;
                    break;
                case "gameplay.effect.instantdeath":
                case "gameplay.effect.instantdeath.silent":
                    ItemType = EItemType.InstantDeath;
                    break;
                case "event.damage.died":
                    if (ItemType == EItemType.Unknown)
                    {
                        ItemType = EItemType.InstantDeath;
                    }

                    break;
                case "gameplay.damage.physical.explosive":
                    ItemType = EItemType.EnvironmentExplosion;
                    break;
                case "gameplay.damage.physical.explosive.gas":
                    ItemType = EItemType.EnvironmentExplosion;
                    break;
                case "envitem.reactiveprop.propanetank":
                    ItemType = EItemType.EnvironmentExplosion;
                    break;
                case "abilities.generic.m80":
                    ItemType = EItemType.Grenade;
                    break;
                case "phoebe.items.harpoon":
                    ItemType = EItemType.Harpoon;
                    break;
                case "weapon.ranged.heavy.c4":
                    ItemType = EItemType.C4;
                    ItemRarity = EFortRarity.Epic; //Doesn't have death tags for it
                    break;
                case "gameplay.damage.physical.energy":
                    ItemType = EItemType.Storm;
                    break;
                case "deathcause.loggedout":
                    ItemType = EItemType.Logout;
                    break;
                case "deathcause.removedfromgame":
                    ItemType = EItemType.Logout;
                    break;
                case "gameplay.effect.instantdeath.silent.switchingtospectate":
                    ItemType = EItemType.SwitchingToSpectate;
                    break;
                case "asset.creative.device":
                    ItemType = EItemType.CreativeDevice;
                    break;
                case "asset.athena.envitem.sentry.turret.damage":
                    ItemType = EItemType.SentryTurret;
                    break;
                case "gameplay.damage.environment":
                    ItemType = EItemType.Environment;
                    break;
                case "envitem.reactiveprop.gaspump":
                    ItemType = EItemType.Environment;
                    break;
                case "gameplay.damage.environment.falling":
                    ItemType = EItemType.Falling;
                    break;
                case "item.trap.damagetrap":
                    ItemType = EItemType.Trap;
                    break;
                case "ability.ostrich.secondaryfire":
                    // Ignore
                    break;
                case "abilityweapon.ranged":
                    // Ignore
                    break;
                case "analytics.item.weapon.ranged.slurpbazooka":
                    ItemType = EItemType.ChugCannon;
                    break;
                case "animation.characterparts.hidefx.hands":
                    // Ignore
                    break;
                case "asset.creative.weapon.ranged.flashlight":
                    ItemType = EItemType.Ignore;
                    break;
                case "athena.achievements.catchfish":
                    // Ignore
                    break;
                case "athena.crafting.forcecraftableicon":
                    // Ignore
                    break;
                case "athena.item.utility":
                    // Ignore
                    break;
                case "athena.itemaction.consume":
                    // Ignore
                    break;
                case "athena.itemaction.secondary.throw":
                    // Ignore
                    break;
                case "athena.itemaction.throw":
                    // Ignore
                    break;
                case "athena.ltm.ashton.miloweap.launcher":
                    ItemType = EItemType.ChitauriEnergyLauncher;
                    break;
                case "athena.ltm.ashton.miloweap.sniper":
                    ItemType = EItemType.ChitauriLaserRifle;
                    break;
                case "athena.ostrich.shotgun":
                    ItemType = EItemType.OstrichWeapon;
                    break;
                case "athena.quests.awakening.hightowertapas1h":
                    // Ignore
                    break;
                case "athena.quests.hightower.wasabipickaxe":
                    // Ignore
                    break;
                case "athena.weaponstatbounds.bow.bone":
                    // Ignore
                    break;
                case "athena.weaponstatbounds.bow.flame":
                    // Ignore
                    break;
                case "athena.weaponstatbounds.bow.stink":
                    // Ignore
                    break;
                case "athena.weaponstatbounds.pistol.bone":
                    // Ignore
                    break;
                case "bacchus.consume.health.healthpack":
                    // Ignore
                    break;
                case "bacchus.harvest.pickaxe":
                    ItemType = EItemType.PickAxe;
                    break;
                case "bacchus.shoot.ar.auto":
                    // Ignore
                    break;
                case "bacchus.shoot.ar.precision":
                    // Ignore
                    break;
                case "bacchus.shoot.ar.scoped":
                    // Ignore
                    break;
                case "bacchus.shoot.ar.semi":
                    // Ignore
                    break;
                case "bacchus.shoot.launcher.grenade":
                    // Ignore
                    break;
                case "bacchus.shoot.launcher.rocket":
                    // Ignore
                    break;
                case "bacchus.shoot.machinegun.minigun":
                    // Ignore
                    break;
                case "bacchus.shoot.pistol.revolver":
                    // Ignore
                    break;
                case "bacchus.shoot.pistol.semi":
                    // Ignore
                    break;
                case "bacchus.shoot.pistol.supressed":
                    // Ignore
                    break;
                case "bacchus.shoot.shotgun":
                    // Ignore
                    break;
                case "bacchus.shoot.shotgun.pumpaction":
                    // Ignore
                    break;
                case "bacchus.shoot.shotgun.tactical":
                    // Ignore
                    break;
                case "bacchus.shoot.smg.auto":
                    // Ignore
                    break;
                case "bacchus.shoot.smg.supressed":
                    // Ignore
                    break;
                case "bacchus.shoot.sniper.bolt":
                    // Ignore
                    break;
                case "bacchus.shoot.sniper.semi":
                    ItemType = EItemType.SemiAutoSniperRifle;
                    break;
                case "bacchus.throw.grenade":
                    ItemType = EItemType.Grenade;
                    break;
                case "bacchus.throw.grenade.explosive":
                    // Ignore
                    break;
                case "cosmetics.filter.season.13":
                    // Ignore
                    break;
                case "cosmetics.overridewrapbehavior.donotoverride":
                    // Ignore
                    break;
                case "cosmetics.overridewrapbehavior.nativeandbp":
                    // Ignore
                    break;
                case "cosmetics.pickaxe.backspin":
                    // Ignore
                    break;
                case "cosmetics.pickaxe.baseballbat":
                    // Ignore
                    break;
                case "cosmetics.pickaxe.candyapplesour":
                    // Ignore
                    break;
                case "cosmetics.pickaxe.donutplate":
                    // Ignore
                    break;
                case "cosmetics.pickaxe.dualwield":
                    // Ignore
                    break;
                case "cosmetics.pickaxe.embers":
                    // Ignore
                    break;
                case "cosmetics.pickaxe.galileoferry":
                    // Ignore
                    break;
                case "cosmetics.pickaxe.gladiator":
                    // Ignore
                    break;
                case "cosmetics.pickaxe.hightowertapas":
                    // Ignore
                    break;
                case "cosmetics.pickaxe.hightowerwasabi":
                    // Ignore
                    break;
                case "cosmetics.pickaxe.onehanded":
                    // Ignore
                    break;
                case "cosmetics.pickaxe.seasalt":
                    // Ignore
                    break;
                case "cosmetics.pickaxe.vertigo":
                    // Ignore
                    break;
                case "cosmetics.source.itemshop":
                    // Ignore
                    break;
                case "curie.element.water":
                    ItemType = EItemType.FireExtinguisher;
                    break;
                case "gameplay.damagesource.ability":
                    // Ignore
                    break;
                case "gameplay.damagesource.weapon":
                    // Ignore
                    break;
                case "item.consumable.coal":
                    ItemType = EItemType.Coal;
                    break;
                case "item.consumable.fish.cuddlefish":
                    ItemType = EItemType.CuddleFish;
                    break;
                case "item.consumable.flaregun":
                    ItemType = EItemType.FlareGun;
                    break;
                case "item.consumable.grapplinghoot":
                    ItemType = EItemType.SkyeGrappler;
                    break;
                case "item.consumable.hookgun":
                    ItemType = EItemType.Grappler;
                    break;
                case "item.consumable.hookgun.grapplinghoot":
                    ItemType = EItemType.JulesGliderGun;
                    break;
                case "item.consumable.meat":
                    ItemType = EItemType.Meat;
                    break;
                case "item.consumable.purplemouse":
                    ItemType = EItemType.ProximityMine;
                    break;
                case "item.consumable.trait.attachable":
                    // Ignore
                    break;
                case "item.consumable.whiff":
                    ItemType = EItemType.HunterCloak;
                    break;
                case "item.fuel.petrolpickup":
                    ItemType = EItemType.GasCan;
                    break;
                case "item.fuel.petrolpump":
                    ItemType = EItemType.PetrolPump;
                    break;
                case "item.gadget.jumpboots":
                    ItemType = EItemType.SpireJumpBoots;
                    break;
                case "item.ingredient.animalbones":
                    ItemType = EItemType.AnimalBone;
                    break;
                case "item.ingredient.mechanicalparts":
                    ItemType = EItemType.MechanicalPart;
                    break;
                case "item.ingredient.toxicsac":
                    ItemType = EItemType.StinkSac;
                    break;
                case "item.sortsright":
                    // Ignore
                    break;
                case "item.water.fireextinguisher":
                    ItemType = EItemType.FireExtinguisher;
                    break;
                case "item.weapon.ranged.assault.ht.starkar":
                    ItemType = EItemType.StarkIndustriesEnergyRifle;
                    break;
                case "item.weapon.ranged":
                    ItemType = EItemType.PropOMatic;
                    break;
                case "item.weapon.ranged.assault.auto.hightier":
                    ItemType = EItemType.AssaultRifle;
                    break;
                case "item.weapon.ranged.assault.auto.jas":
                    ItemType = EItemType.SkyeAssaultRifle;
                    break;
                case "item.weapon.ranged.assault.auto.lowtier":
                    ItemType = EItemType.AssaultRifle;
                    break;
                case "item.weapon.ranged.assault.bone":
                    ItemType = EItemType.PrimalAssaultRifle;
                    break;
                case "item.weapon.ranged.assault.bone.boss":
                    ItemType = EItemType.SpireGuardianPrimalAssaultRifle;
                    break;
                case "item.weapon.ranged.assault.burst.hightier":
                    ItemType = EItemType.BurstAssaultRifle;
                    break;
                case "item.weapon.ranged.assault.burst.lowtier":
                    ItemType = EItemType.BurstAssaultRifle;
                    break;
                case "item.weapon.ranged.assault.burst.neongirl":
                    ItemType = EItemType.OceanBurstAssaultRifle;
                    break;
                case "item.weapon.ranged.assault.drum":
                    ItemType = EItemType.DrumGun;
                    break;
                case "item.weapon.ranged.assault.drum.ghostmidas":
                    ItemType = EItemType.ShadowMidasDrumGun;
                    break;
                case "item.weapon.ranged.assault.drum.midas":
                    ItemType = EItemType.JulesDrumGun;
                    break;
                case "item.weapon.ranged.assault.heavy.hightier":
                    ItemType = EItemType.HeavyAssaultRifle;
                    break;
                case "item.weapon.ranged.assault.heavy.lowtier":
                    ItemType = EItemType.HeavyAssaultRifle;
                    break;
                case "item.weapon.ranged.assault.heavy.meowscles":
                    ItemType = EItemType.MeowsclesHeavyAssaultRifle;
                    break;
                case "item.weapon.ranged.assault.infantry.hightier":
                    ItemType = EItemType.InfantryRifle;
                    break;
                case "item.weapon.ranged.assault.infantry.lowtier":
                    ItemType = EItemType.InfantryRifle;
                    break;
                case "item.weapon.ranged.assault.lmg":
                    ItemType = EItemType.LightMachineGun;
                    break;
                case "item.weapon.ranged.assault.minigun":
                    ItemType = EItemType.Minigun;
                    break;
                case "item.weapon.ranged.assault.minigun.brutus":
                    ItemType = EItemType.BrutusMinigun;
                    break;
                case "item.weapon.ranged.assault.scoped":
                    if (ItemType == EItemType.Unknown)
                    {
                        ItemType = EItemType.ScopedAssaultRifle;
                    }

                    break;
                case "item.weapon.ranged.assault.scrap":
                    ItemType = EItemType.MakeshiftAssaultRifle;
                    break;
                case "item.weapon.ranged.assault.suppressed":
                    ItemType = EItemType.SuppressedAssaultRifle;
                    break;
                case "item.weapon.ranged.bow":
                    ItemType = EItemType.SplinterBow;
                    break;
                case "item.weapon.ranged.bow.bone":
                    ItemType = EItemType.PrimalBow;
                    break;
                case "item.weapon.ranged.bow.clusterbomb":
                    ItemType = EItemType.ClusterBombBow;
                    break;
                case "item.weapon.ranged.bow.clusterbomb.boss":
                    ItemType = EItemType.RazExplosiveBow;
                    break;
                case "item.weapon.ranged.bow.crossbow":
                    ItemType = EItemType.Crossbow;
                    break;
                case "item.weapon.ranged.bow.explosive":
                    ItemType = EItemType.ClusterBombBow;
                    break;
                case "item.weapon.ranged.bow.fiendhunter":
                    ItemType = EItemType.FiendHunterCrossbow;
                    break;
                case "item.weapon.ranged.bow.flame":
                    ItemType = EItemType.FlameBow;
                    break;
                case "item.weapon.ranged.bow.grappler":
                    ItemType = EItemType.GrapplerBow;
                    break;
                case "item.weapon.ranged.bow.metal":
                    ItemType = EItemType.MechanicalBow;
                    break;
                case "item.weapon.ranged.bow.scrap":
                    ItemType = EItemType.MakeshiftBow;
                    break;
                case "item.weapon.ranged.bow.shockwave":
                    ItemType = EItemType.ShockwaveBow;
                    break;
                case "item.weapon.ranged.bow.stink":
                    ItemType = EItemType.StinkBow;
                    break;
                case "item.weapon.ranged.bow.unstable":
                    ItemType = EItemType.UnstableBow;
                    break;
                case "item.weapon.ranged.bow.valentine":
                    ItemType = EItemType.CupidCrossbow;
                    break;
                case "item.weapon.ranged.unstableliquidgun":
                    ItemType = EItemType.GooGun;
                    break;
                case "item.weapon.ranged.launcher":
                case "item.weapon.ranged.launcher.snowball":
                    ItemType = EItemType.SnowballLauncher;
                    break;
                case "item.weapon.ranged.launcher.egg":
                    ItemType = EItemType.EggLauncher;
                    break;
                case "item.weapon.ranged.launcher.grenade":
                    ItemType = EItemType.GrenadeLauncher;
                    break;
                case "item.weapon.ranged.launcher.grenadeprox":
                    ItemType = EItemType.ProximityGrenadeLauncher;
                    break;
                case "item.weapon.ranged.launcher.junkgun":
                    ItemType = EItemType.Recycler;
                    break;
                case "item.weapon.ranged.launcher.junkgun.boss":
                    ItemType = EItemType.SpireAssassinRecycler;
                    break;
                case "item.weapon.ranged.launcher.paint":
                    ItemType = EItemType.PaintLauncher;
                    break;
                case "item.weapon.ranged.launcher.petrolpump":
                    ItemType = EItemType.PetrolPump;
                    break;
                case "item.weapon.ranged.launcher.quad":
                    ItemType = EItemType.QuadLauncher;
                    break;
                case "item.weapon.ranged.launcher.rocket":
                    ItemType = EItemType.RocketLauncher;
                    break;
                case "item.weapon.ranged.launcher.rocket.burst":
                    ItemType = EItemType.BurstQuadLauncher;
                    break;
                case "item.weapon.ranged.launcher.shockwave":
                    ItemType = EItemType.KitShockwaveLauncher;
                    break;
                case "item.weapon.ranged.pistol.bone":
                    ItemType = EItemType.PrimalPistol;
                    break;
                case "item.weapon.ranged.pistol.dualpistols":
                    if (ItemType == EItemType.Unknown)
                    {
                        ItemType = EItemType.DualPistols;
                    }

                    break;
                case "item.weapon.ranged.pistol.dualpistols.hoprock":
                    ItemType = EItemType.HopRockDualies;
                    break;
                case "item.weapon.ranged.pistol.dualsuppressedpistols":
                    ItemType = EItemType.DualSuppressedPistols;
                    break;
                case "item.weapon.ranged.pistol.flintlock":
                case "item.weapon.ranged.yeetknock":
                    ItemType = EItemType.FlintKnockPistol;
                    break;
                case "item.weapon.ranged.pistol.handcannon":
                    ItemType = EItemType.HandCannon;
                    break;
                case "item.weapon.ranged.pistol.marksmanrevolver":
                    ItemType = EItemType.MarksmanSixShooter;
                    break;
                case "item.weapon.ranged.pistol.revolver":
                    ItemType = EItemType.Revolver;
                    break;
                case "item.weapon.ranged.pistol.revolver.hightier":
                    ItemType = EItemType.Revolver;
                    break;
                case "item.weapon.ranged.pistol.revolver.scrap":
                    ItemType = EItemType.MakeshiftRevolver;
                    break;
                case "item.weapon.ranged.pistol.standard.hightier":
                    ItemType = EItemType.Pistol;
                    break;
                case "item.weapon.ranged.pistol.standard.lowtier":
                    ItemType = EItemType.Pistol;
                    break;
                case "item.weapon.ranged.pistol.thermal":
                    ItemType = EItemType.NightHawk;
                    break;
                case "item.weapon.ranged.pistol.tracker":
                    ItemType = EItemType.ShadowTracker;
                    break;
                case "item.weapon.ranged.shotgun.bone":
                    ItemType = EItemType.PrimalShotgun;
                    break;
                case "item.weapon.ranged.shotgun.bone.boss":
                    ItemType = EItemType.SpireAssassinPrimalShotgun;
                    break;
                case "item.weapon.ranged.shotgun.breakbarrel":
                    ItemType = EItemType.DoubleBarrelShotgun;
                    break;
                case "item.weapon.ranged.shotgun.charge.hightier":
                    ItemType = EItemType.ChargeShotgun;
                    break;
                case "item.weapon.ranged.shotgun.charge.lowtier":
                    ItemType = EItemType.ChargeShotgun;
                    break;
                case "item.weapon.ranged.shotgun.charge.robomeow":
                    ItemType = EItemType.KitChargeShotgun;
                    break;
                case "item.weapon.ranged.shotgun.combat":
                    ItemType = EItemType.CombatShotgun;
                    break;
                case "item.weapon.ranged.shotgun.drumfed":
                    if (ItemType == EItemType.Unknown)
                    {
                        ItemType = EItemType.DrumShotgun;
                    }

                    break;
                case "item.weapon.ranged.shotgun.dub":
                    ItemType = EItemType.TheDub;
                    break;
                case "item.weapon.ranged.shotgun.heavy":
                    ItemType = EItemType.HeavyShotgun;
                    break;
                case "item.weapon.ranged.shotgun.pump.hightier":
                    ItemType = EItemType.PumpShotgun;
                    break;
                case "item.weapon.ranged.shotgun.pump.lowtier":
                    ItemType = EItemType.PumpShotgun;
                    break;
                case "item.weapon.ranged.shotgun.scrap":
                    ItemType = EItemType.MakeshiftShotgun;
                    break;
                case "item.weapon.ranged.shotgun.tactical":
                    ItemType = EItemType.TacticalShotgun;
                    break;
                case "item.weapon.ranged.shotgun.tactical.hightier":
                    ItemType = EItemType.TacticalShotgun;
                    break;
                case "item.weapon.ranged.smg.bone":
                    ItemType = EItemType.PrimalSmg;
                    break;
                case "item.weapon.ranged.smg.burst":
                    ItemType = EItemType.BurstSmg;
                    break;
                case "item.weapon.ranged.smg.mp":
                    ItemType = EItemType.MachinePistol;
                    break;
                case "item.weapon.ranged.shotgun.swing.lowtier":
                    ItemType = EItemType.LeverShotgun;
                    break;
                case "item.weapon.ranged.shotgun.swing.hightier":
                    ItemType = EItemType.LeverShotgun;
                    break;
                case "item.weapon.ranged.smg.pdw":
                    if (ItemType == EItemType.Unknown)
                    {
                        ItemType = EItemType.SubmachineGun;
                    }

                    break;
                case "item.weapon.ranged.smg.rapidfire":
                    ItemType = EItemType.RapidFireSmg;
                    break;
                case "item.weapon.ranged.smg.scrap":
                    ItemType = EItemType.MakeshiftSubmachineGun;
                    break;
                case "item.weapon.ranged.smg.suppressed":
                    ItemType = EItemType.SuppressedSubmachineGun;
                    break;
                case "item.weapon.ranged.smg.tactical":
                    ItemType = EItemType.TacticalSubmachineGun;
                    break;
                case "item.weapon.ranged.sniper.boltaction":
                    if (ItemType == EItemType.Unknown)
                    {
                        ItemType = EItemType.BoltActionSniperRifle;
                    }

                    break;
                case "item.weapon.ranged.sniper.boom":
                    ItemType = EItemType.BoomSniperRifle;
                    break;
                case "item.weapon.ranged.sniper.fullauto":
                    ItemType = EItemType.AutomaticSniperRifle;
                    break;
                case "item.weapon.ranged.sniper.heavy":
                    ItemType = EItemType.HeavySniperRifle;
                    break;
                case "item.weapon.ranged.sniper.huntingrifle":
                    if (ItemType == EItemType.Unknown)
                    {
                        ItemType = EItemType.HuntingRifle;
                    }

                    break;
                case "item.weapon.ranged.sniper.semiauto":
                    ItemType = EItemType.SemiAutoSniperRifle;
                    break;
                case "item.weapon.ranged.sniper.weather":
                    ItemType = EItemType.StormScout;
                    break;
                case "item.weapon.vehicle.meatball":
                    ItemType = EItemType.Boat;
                    break;
                case "athena.vehicle.nevada":
                    ItemType = EItemType.Ufo;
                    break;
                case "leadalloy.item.weapon.ranged.bow.bone":
                    ItemType = EItemType.PrimalBow;
                    break;
                case "leadalloy.item.weapon.ranged.bow.clusterbomb":
                    ItemType = EItemType.ClusterBombBow;
                    break;
                case "leadalloy.item.weapon.ranged.bow.flame":
                    ItemType = EItemType.FlameBow;
                    break;
                case "leadalloy.item.weapon.ranged.bow.metal":
                    ItemType = EItemType.MechanicalBow;
                    break;
                case "leadalloy.item.weapon.ranged.bow.scrap":
                    ItemType = EItemType.MakeshiftBow;
                    break;
                case "leadalloy.item.weapon.ranged.bow.stink":
                    ItemType = EItemType.StinkBow;
                    break;
                case "leadalloy.item.weapon.ranged.dualpistols.metal":
                    ItemType = EItemType.DualPistols;
                    break;
                case "leadalloy.weapon":
                    // Ignore
                    break;
                case "phoebe.items.ammo":
                    // Ignore
                    break;
                case "phoebe.items.assault_rifle":
                    // Ignore
                    break;
                case "phoebe.items.bolt_action_sniper_rifle":
                    // Ignore
                    break;
                case "phoebe.items.boombow":
                    ItemType = EItemType.BoomBow;
                    break;
                case "phoebe.items.bow":
                    // Ignore
                    break;
                case "phoebe.items.bow.clusterbomb":
                    // Ignore
                    break;
                case "phoebe.items.bow.flame":
                    // Ignore
                    break;
                case "phoebe.items.bow.shockwave":
                    // Ignore
                    break;
                case "phoebe.items.bow.stinkbomb":
                    // Ignore
                    break;
                case "phoebe.items.burst_assault_rifle":
                    // Ignore
                    break;
                case "phoebe.items.burstsmg":
                    // Ignore
                    break;
                case "phoebe.items.combatshotgun":
                    // Ignore
                    break;
                case "phoebe.items.consumable.meat":
                    // Ignore
                    break;
                case "phoebe.items.crossbow":
                    // Ignore
                    break;
                case "phoebe.items.cshotgun":
                    // Ignore
                    break;
                case "phoebe.items.doublebarrelshotgun":
                    // Ignore
                    break;
                case "phoebe.items.drumgun":
                    // Ignore
                    break;
                case "item.gadget.jetpack.cosmos":
                    ItemType = EItemType.TheMandalorianJetpack;
                    break;
                case "phoebe.items.drumshotgun":
                    ItemType = EItemType.DrumShotgun;
                    break;
                case "item.consumable.badgerbangs":
                    ItemType = EItemType.ExplosiveBatarang;
                    break;
                case "phoebe.items.hightower.date":
                case "phoebe.weapon.ranged.hightower.date":
                    ItemType = EItemType.DoctorDoomArcaneGauntlets;
                    break;
                case "phoebe.items.dualpistols":
                    // Ignore
                    break;
                case "phoebe.items.flintknockpistol":
                    // Ignore
                    break;
                case "phoebe.items.grenade_launcher":
                    // Ignore
                    break;
                case "phoebe.items.grenade_launcher.egg":
                    // Ignore
                    break;
                case "phoebe.items.grenade_launcher.junkgun":
                    // Ignore
                    break;
                case "phoebe.items.handcannon":
                    // Ignore
                    break;
                case "phoebe.items.heavy_assault_rifle":
                    // Ignore
                    break;
                case "phoebe.items.heavysniper":
                    // Ignore
                    break;
                case "phoebe.items.huntingrifle":
                    // Ignore
                    break;
                case "phoebe.items.infantryrifle":
                    // Ignore
                    break;
                case "phoebe.items.ingredients.animalbones":
                    // Ignore
                    break;
                case "phoebe.items.ingredients.mechanicalparts":
                    // Ignore
                    break;
                case "phoebe.items.ingredients.toxicsac":
                    // Ignore
                    break;
                case "phoebe.items.lmg":
                    // Ignore
                    break;
                case "phoebe.items.minigun":
                    // Ignore
                    break;
                case "phoebe.items.pickaxe":
                    // Ignore
                    break;
                case "phoebe.items.pistol":
                    // Ignore
                    break;
                case "phoebe.items.pistol.marksman":
                    // Ignore
                    break;
                case "phoebe.items.pistolrapidfiresmg":
                    // Ignore
                    break;
                case "phoebe.items.proximitygrenadelauncher":
                    // Ignore
                    break;
                case "phoebe.items.pump_shotgun":
                    // Ignore
                    break;
                case "phoebe.items.pumpkinlauncher":
                    ItemType = EItemType.PumpkinLauncher;
                    break;
                case "phoebe.items.repeater":
                    ItemType = EItemType.LeverActionRifle;
                    break;
                case "phoebe.items.revolver":
                    // Ignore
                    break;
                case "phoebe.items.rocket_launcher":
                    // Ignore
                    break;
                case "phoebe.items.scopedar":
                    ItemType = EItemType.ScopedAssaultRifle;
                    break;
                case "phoebe.items.sixshooterpistol":
                    // Ignore
                    break;
                case "phoebe.items.smg":
                    // Ignore
                    break;
                case "phoebe.items.suppressedar":
                    // Ignore
                    break;
                case "phoebe.items.suppressedpistol":
                    // Ignore
                    break;
                case "phoebe.items.suppressedsmg":
                    // Ignore
                    break;
                case "phoebe.items.suppressedsniper":
                    // Ignore
                    break;
                case "phoebe.items.tactical_shotgun":
                    // Ignore
                    break;
                case "phoebe.items.tacticalar":
                    ItemType = EItemType.TacticalAssaultRifle;
                    break;
                case "phoebe.items.tacticalsmg":
                    // Ignore
                    break;
                case "phoebe.items.waffletruck.03":
                    ItemType = EItemType.BoomSniperRifle;
                    break;
                case "phoebe.items.waffletruck.04":
                    ItemType = EItemType.TheDub;
                    break;
                case "phoebe.items.waffletruck.05":
                    ItemType = EItemType.HopRockDualies;
                    break;
                case "phoebe.items.waffletruck.06":
                    ItemType = EItemType.BurstQuadLauncher;
                    break;
                case "phoebe.items.waffletruck.07":
                    ItemType = EItemType.NightHawk;
                    break;
                case "phoebe.items.waffletruck.08":
                    ItemType = EItemType.ShadowTracker;
                    break;
                case "phoebe.items.waffletruck.09":
                    ItemType = EItemType.FrozenSmg;
                    break;
                case "phoebe.items.waffletruck.11":
                    ItemType = EItemType.DragonsBreathSniper;
                    break;
                case "phoebe.items.waffletruck.12":
                    ItemType = EItemType.StormScout;
                    break;
                case "phoebe.weapon.ranged.bow":
                    // Ignore
                    break;
                case "phoebe.weapon.ranged.crossbow":
                    // Ignore
                    break;
                case "phoebe.weapon.ranged.grenade_launcher":
                    // Ignore
                    break;
                case "phoebe.weapon.ranged.grenade_launcher.junkgun":
                    ItemType = EItemType.JunkGun;
                    break;
                case "phoebe.weapon.ranged.lmg":
                    // Ignore
                    break;
                case "phoebe.weapon.ranged.minigun":
                    // Ignore
                    break;
                case "phoebe.weapon.ranged.pistol":
                    // Ignore
                    break;
                case "phoebe.weapon.ranged.rifle":
                    // Ignore
                    break;
                case "phoebe.weapon.ranged.rocket_launcher":
                    // Ignore
                    break;
                case "phoebe.weapon.ranged.shotgun":
                    // Ignore
                    break;
                case "phoebe.weapon.ranged.smg":
                    // Ignore
                    break;
                case "phoebe.weapon.ranged.sniper":
                    // Ignore
                    break;
                case "phoebe.weapon.trait.directhit":
                    // Ignore
                    break;
                case "phoebe.weapon.trait.fullauto":
                    // Ignore
                    break;
                case "phoebe.weapon.trait.infiniteammo":
                    // Ignore
                    break;
                case "phoebe.weapon.trait.scoped":
                    // Ignore
                    break;
                case "phoebe.weapon.trait.singleshot":
                    // Ignore
                    break;
                case "phoebe.weapon.trait.suppressed":
                    // Ignore
                    break;
                case "quest.metadata.ltm.wax":
                    // Ignore
                    break;
                case "rarity.common":
                    ItemRarity = EFortRarity.Common;
                    break;
                case "rarity.uncommon":
                    ItemRarity = EFortRarity.Uncommon;
                    break;
                case "rarity.rare":
                    ItemRarity = EFortRarity.Rare;
                    break;
                case "rarity.superrare":
                    ItemRarity = EFortRarity.Legendary;
                    break;
                case "rarity.transcendent":
                    ItemRarity = EFortRarity.Transcendent;
                    break;
                case "rarity.veryrare":
                    ItemRarity = EFortRarity.Epic;
                    break;
                case "rarity.mythic":
                    ItemRarity = EFortRarity.Mythic;
                    break;
                case "skill.sniper.headshotbuff":
                    // Ignore
                    break;
                case "specialevent.spring.lunar.weapons":
                    // Ignore
                    break;
                case "supplies.ammo.arrows":
                    // Ignore
                    break;
                case "supplies.ammo.explosive":
                    // Ignore
                    break;
                case "supplies.ammo.shell":
                    // Ignore
                    break;
                case "tech.spy.item.defaultpistol":
                    // Ignore
                    break;
                case "tech.spy.item.defaultpistol.suppressed":
                    // Ignore
                    break;
                case "token.rarity.rare":
                    // Ignore
                    break;
                case "trait.boss":
                    // Ignore
                    break;
                case "trait.camouflage":
                    // Ignore
                    break;
                case "trait.explosive":
                    // Ignore
                    break;
                case "trait.fish.big":
                    // Ignore
                    break;
                case "trait.grappler":
                    // Ignore
                    break;
                case "trait.meat":
                    // Ignore
                    break;
                case "trait.papaya":
                    // Ignore
                    break;
                case "trait.questvaulteditem":
                    // Ignore
                    break;
                case "trait.restoration.health":
                    // Ignore
                    break;
                case "trait.scoped":
                    // Ignore
                    break;
                case "trait.suppressed":
                    // Ignore
                    break;
                case "trait.vehicle":
                    // Ignore
                    break;
                case "vehicleweapon":
                    // Ignore
                    break;
                case "weapon":
                    // Ignore
                    break;
                case "weapon.gauntlet":
                    // Ignore
                    break;
                case "weapon.ignorestriggerhaptics":
                    // Ignore
                    break;
                case "weapon.melee.impact.pickaxe":
                    ItemType = EItemType.PickAxe;
                    break;
                case "weapon.meta.donotapplywraps":
                    // Ignore
                    break;
                case "weapon.meta.miscwrapped":
                    // Ignore
                    break;
                case "weapon.meta.scoped":
                    // Ignore
                    break;
                case "weapon.meta.suppressed":
                    // Ignore
                    break;
                case "weapon.ranged":
                    // Ignore
                    break;
                case "weapon.ranged.assault.burst":
                    // Ignore
                    break;
                case "weapon.ranged.assault.heavy":
                    // Ignore
                    break;
                case "weapon.ranged.assault.infantry":
                    // Ignore
                    break;
                case "weapon.ranged.assault.lmg":
                    // Ignore
                    break;
                case "weapon.ranged.assault.silenced":
                    // Ignore
                    break;
                case "weapon.ranged.assault.standard":
                    // Ignore
                    break;
                case "weapon.ranged.bow":
                    // Ignore
                    break;
                case "weapon.ranged.crossbow":
                    // Ignore
                    break;
                case "weapon.ranged.crossbow.explosive":
                    // Ignore
                    break;
                case "weapon.ranged.hamsterball":
                    ItemType = EItemType.Baller;
                    break;
                case "item.weapon.vehicle.ferret":
                case "weapon.ranged.biplane":
                    ItemType = EItemType.BiPlane;
                    break;
                case "weapon.ranged.heavy":
                    // Ignore
                    break;
                case "weapon.ranged.heavy.grenade_launcher":
                    // Ignore
                    break;
                case "weapon.ranged.heavy.junkgun":
                    // Ignore
                    break;
                case "weapon.ranged.heavy.proximitymine":
                    ItemType = EItemType.ProximityMine;
                    break;
                case "weapon.ranged.heavy.rocket_launcher":
                    ItemType = EItemType.RocketLauncher;
                    break;
                case "weapon.ranged.hippo":
                    ItemType = EItemType.HawkeyeBow;
                    break;
                case "weapon.ranged.indigo":
                case "phoebe.items.hightower.tomato":
                case "phoebe.weapon.ranged.hightower.tomato":
                    ItemType = EItemType.IronManRepulsors;
                    break;
                case "weapon.ranged.lmg":
                    // Ignore
                    break;
                case "weapon.ranged.milo.launcher":
                    ItemType = EItemType.ChitauriEnergyLauncher;
                    break;
                case "weapon.ranged.milo.rifle":
                    ItemType = EItemType.ChitauriLaserRifle;
                    break;
                case "weapon.ranged.minigun":
                    // Ignore
                    break;
                case "weapon.ranged.piratecannon":
                    ItemType = EItemType.MountedTurret;
                    break;
                case "weapon.ranged.pistol.flintlock":
                    // Ignore
                    break;
                case "weapon.ranged.pistol.handcannon":
                    // Ignore
                    break;
                case "weapon.ranged.pistol.sixshooter":
                    // Ignore
                    break;
                case "weapon.ranged.pistol.standard":
                    // Ignore
                    break;
                case "weapon.ranged.pistol.suppressed":
                    // Ignore
                    break;
                case "weapon.ranged.shotgun":
                    // Ignore
                    break;
                case "weapon.ranged.shotgun.break":
                    // Ignore
                    break;
                case "weapon.ranged.shotgun.heavy":
                    // Ignore
                    break;
                case "weapon.ranged.shotgun.pump":
                    // Ignore
                    break;
                case "weapon.ranged.shotgun.tactical":
                    // Ignore
                    break;
                case "weapon.ranged.smg":
                    // Ignore
                    break;
                case "weapon.ranged.sniper":
                    // Ignore
                    break;
                case "weapon.ranged.sniper.heavy":
                    // Ignore
                    break;
                case "weapon.ranged.sniper.hunting":
                    // Ignore
                    break;
                case "abilities.generic.proximitymine":
                    // Ignore
                    break;
                case "ability.athena.item.shadowbomb":
                    ItemType = EItemType.ShadowBomb;
                    break;
                case "athena.blade":
                    ItemType = EItemType.InfinityBlade;
                    break;
                case "athena.carmine":
                    ItemType = EItemType.AirStrike;
                    break;
                case "athena.creative":
                    // Ignore
                    break;
                case "athena.itemaction.place":
                    // Ignore
                    break;
                case "athena.ltm.ashton.chicago.equipped":
                    // Ignore
                    break;
                case "bacchus.consume.boost.chugjug":
                    // Ignore
                    break;
                case "bacchus.consume.boost.slurp":
                    // Ignore
                    break;
                case "bacchus.consume.health.bandage":
                    // Ignore
                    break;
                case "bacchus.consume.shield.shieldsm":
                    // Ignore
                    break;
                case "bacchus.consume.shield.sieldlg":
                    // Ignore
                    break;
                case "bacchus.place.trap":
                    // Ignore
                    break;
                case "bacchus.place.trap.campfire":
                    // Ignore
                    break;
                case "bacchus.place.trap.damage":
                    // Ignore
                    break;
                case "bacchus.place.trap.launchpad":
                    // Ignore
                    break;
                case "bacchus.throw.grenade.dance":
                    // Ignore
                    break;
                case "bacchus.throw.grenade.smoke":
                    // Ignore
                    break;
                case "bacchus.use.wear.bush":
                    // Ignore
                    break;
                case "bacchus.use.wear.snowman":
                    // Ignore
                    break;
                case "building.type.itemcontainer.wumba":
                    // Ignore
                    break;
                case "creative.beta.deprecated":
                    // Ignore
                    break;
                case "creative.beta.earlyaccess":
                    // Ignore
                    break;
                case "fish.gas.default":
                    // Ignore
                    break;
                case "gameplay.action.player.medium":
                    // Ignore
                    break;
                case "granted.athena.item.lotus.mustache":
                    // Ignore
                    break;
                case "granted.athena.item.waffletruck.slurpbazooka":
                    // Ignore
                    break;
                case "item.consumable":
                    // Ignore
                    break;
                case "item.consumable.apple":
                    ItemType = EItemType.Apple;
                    break;
                case "item.consumable.applesauce":
                    ItemType = EItemType.AppleSauce;
                    break;
                case "item.consumable.applesun":
                    ItemType = EItemType.CrashPad;
                    break;
                case "item.consumable.balloons":
                    ItemType = EItemType.Balloons;
                    break;
                case "item.consumable.banana":
                    ItemType = EItemType.Banana;
                    break;
                case "item.consumable.bandage":
                    ItemType = EItemType.Bandage;
                    break;
                case "item.consumable.boogiebomb":
                    ItemType = EItemType.BoogieBomb;
                    break;
                case "item.consumable.boombox":
                    ItemType = EItemType.BoomBox;
                    break;
                case "item.consumable.bottomlesschugjug":
                    ItemType = EItemType.BottomlessChugJug;
                    break;
                case "item.consumable.bucket.nice":
                    ItemType = EItemType.MythicGoldfish;
                    break;
                case "item.consumable.bucket.old":
                    ItemType = EItemType.RustyCan;
                    break;
                case "item.consumable.bush":
                    ItemType = EItemType.Bush;
                    break;
                case "item.consumable.cabbage":
                    ItemType = EItemType.Cabbage;
                    break;
                case "item.consumable.candycorn":
                    ItemType = EItemType.CandyCorn;
                    break;
                case "item.consumable.chillbronco":
                    ItemType = EItemType.ChugSplash;
                    break;
                case "item.consumable.chugjug":
                    ItemType = EItemType.ChugJug;
                    break;
                case "item.consumable.clinger":
                    ItemType = EItemType.Clinger;
                    break;
                case "item.consumable.coconut":
                    ItemType = EItemType.Coconut;
                    break;
                case "item.consumable.coolcarpet":
                    ItemType = EItemType.CreepinCardboard;
                    break;
                case "item.consumable.corn":
                    ItemType = EItemType.Corn;
                    break;
                case "item.consumable.decoy":
                    ItemType = EItemType.Decoy;
                    break;
                case "item.consumable.dogsweater":
                    ItemType = EItemType.StormFlip;
                    break;
                case "item.consumable.fireworksmortar":
                    ItemType = EItemType.BottleRockets;
                    break;
                case "item.consumable.fish.flopper":
                    ItemType = EItemType.Flopper;
                    break;
                case "item.consumable.fish.flopperbattle":
                    ItemType = EItemType.VendettaFlopper;
                    break;
                case "item.consumable.fish.floppereffective":
                    ItemType = EItemType.Slurpfish;
                    break;
                case "item.consumable.fish.flopperfire":
                    ItemType = EItemType.SpicyFish;
                    break;
                case "item.consumable.fish.floppergas":
                    ItemType = EItemType.StinkFish;
                    break;
                case "item.consumable.fish.flopperhoprock":
                    ItemType = EItemType.HopFlopper;
                    break;
                case "item.consumable.fish.flopperjellyfish":
                    ItemType = EItemType.Jellyfish;
                    break;
                case "item.consumable.fish.floppermechanic":
                    ItemType = EItemType.MidasFlopper;
                    break;
                case "item.consumable.fish.flopperrift":
                    ItemType = EItemType.RiftFish;
                    break;
                case "item.consumable.fish.floppershield":
                    ItemType = EItemType.ShieldFish;
                    break;
                case "item.consumable.fish.floppersmall":
                    ItemType = EItemType.SmallFry;
                    break;
                case "item.consumable.fish.floppersnowy":
                    ItemType = EItemType.SnowyFlopper;
                    break;
                case "item.consumable.fish.flopperthermal":
                    ItemType = EItemType.ThermalFish;
                    break;
                case "item.consumable.fish.flopperzero":
                    ItemType = EItemType.ZeroPointFish;
                    break;
                case "item.consumable.floppingrabbit":
                    ItemType = EItemType.FishingRod;
                    break;
                case "item.consumable.floppingrabbit.hightier":
                    ItemType = EItemType.ProFishingRod;
                    break;
                case "item.consumable.giftbox":
                    ItemType = EItemType.Presents;
                    break;
                case "item.consumable.glider":
                    ItemType = EItemType.Glider;
                    break;
                case "item.consumable.goldentruffle":
                    ItemType = EItemType.GoldenTruffle;
                    break;
                case "item.consumable.grenade":
                    ItemType = EItemType.Grenade;
                    break;
                case "item.consumable.happyghost":
                    ItemType = EItemType.Harpoon;
                    break;
                case "item.consumable.hopdrop":
                    ItemType = EItemType.HopDrop;
                    break;
                case "item.consumable.icegrenade":
                    ItemType = EItemType.ChillerGrenade;
                    break;
                case "item.consumable.impulsegrenade":
                    ItemType = EItemType.ImpulseGrenade;
                    break;
                case "item.consumable.jellybean":
                    ItemType = EItemType.JellyBean;
                    break;
                case "item.weapon.unclebrolly":
                    ItemType = EItemType.Kingsman;
                    break;
                case "item.consumable.jollyrascal":
                case "asset.athena.item.jollyrascal.damage":
                case "asset.athena.item.jollyrascal.damage.anch":
                case "asset.athena.item.jollyrascal.damage.car":
                case "asset.athena.item.jollyrascal.damage.dino":
                case "asset.athena.item.jollyrascal.damage.porta":
                case "item.consumable.jollyrascal.galileo":
                    ItemType = EItemType.JunkRift;
                    break;
                case "item.consumable.medkit":
                    ItemType = EItemType.MedKit;
                    break;
                case "item.consumable.molotov":
                    ItemType = EItemType.FireflyJar;
                    break;
                case "item.consumable.pepper":
                    ItemType = EItemType.Pepper;
                    break;
                case "item.consumable.peppermint":
                    ItemType = EItemType.PepperMint;
                    break;
                case "item.consumable.portafort":
                    ItemType = EItemType.PortAFort;
                    break;
                case "item.consumable.remoteexplosives":
                    ItemType = EItemType.C4;
                    break;
                case "item.consumable.rift":
                    ItemType = EItemType.Rift;
                    break;
                case "item.consumable.shieldbubble":
                    ItemType = EItemType.ShieldBubble;
                    break;
                case "item.consumable.shieldhops":
                    ItemType = EItemType.BouncyEgg;
                    break;
                case "item.consumable.shieldmushroom":
                    ItemType = EItemType.ShieldMushroom;
                    break;
                case "item.consumable.shieldpotion":
                    ItemType = EItemType.ShieldPotion;
                    break;
                case "item.consumable.shockgrenade":
                    ItemType = EItemType.ShockwaveGrenade;
                    break;
                case "item.consumable.slurpjuice":
                    ItemType = EItemType.SlurpJuice;
                    break;
                case "item.consumable.slurpmushroom":
                    ItemType = EItemType.SlurpMushroom;
                    break;
                case "item.consumable.smallshieldpotion":
                    ItemType = EItemType.SmallShieldPotion;
                    break;
                case "item.consumable.smokegrenade":
                    ItemType = EItemType.ShadowBomb;
                    break;
                case "item.consumable.sneakysnowman":
                    ItemType = EItemType.SneakySnowman;
                    break;
                case "item.consumable.stinkbomb":
                    ItemType = EItemType.StinkBomb;
                    break;
                case "item.consumable.thermaltaffy":
                    ItemType = EItemType.ThermalTaffy;
                    break;
                case "item.consumable.tnt":
                    ItemType = EItemType.Dynamite;
                    break;
                case "item.consumable.upgradebench":
                    ItemType = EItemType.UpgradeBench;
                    break;
                case "item.consumable.zaptrap":
                    ItemType = EItemType.ZapperTrap;
                    break;
                case "item.foraged.cake":
                    // Ignore
                    break;
                case "item.foraged.mushroom":
                    // Ignore
                    break;
                case "item.persistwhenemptyforanyinstance":
                    // Ignore
                    break;
                case "item.power.witchbroom":
                    ItemType = EItemType.WitchBroom;
                    break;
                case "item.trap.cozycampfire":
                    ItemType = EItemType.CozyCampfire;
                    break;
                case "item.trap.fire":
                    ItemType = EItemType.FireTrap;
                    break;
                case "item.trap.ice":
                    ItemType = EItemType.Chiller;
                    break;
                case "item.trap.launchpad":
                    if (ItemType == EItemType.Unknown)
                    {
                        ItemType = EItemType.Bouncer;
                    }

                    break;
                case "item.trap.mountedturret":
                    ItemType = EItemType.MountedTurret;
                    break;
                case "item.utility":
                    // Ignore
                    break;
                case "item.weapon.ranged.launcher.gravygoblin":
                case "item.weapon.ranged.gravygoblin":
                    ItemType = EItemType.Grabitron;
                    break;
                case "item.weapon.ranged.mustache":
                    ItemType = EItemType.BandageBazooka;
                    break;
                case "item.weapon.trap.mountedturret":
                    ItemType = EItemType.MountedTurret;
                    break;
                case "loot.type.gadget.bling":
                    // Ignore
                    break;
                case "phoebe.items.consumable":
                    // Ignore
                    break;
                case "phoebe.items.consumable.bandage":
                    // Ignore
                    break;
                case "phoebe.items.consumable.battleflopper":
                    // Ignore
                    break;
                case "phoebe.items.consumable.flopperfish":
                    // Ignore
                    break;
                case "phoebe.items.consumable.floppershield":
                    // Ignore
                    break;
                case "phoebe.items.consumable.jellyfish":
                    // Ignore
                    break;
                case "phoebe.items.consumable.mechanicflopper":
                    // Ignore
                    break;
                case "phoebe.items.consumable.medkit":
                    // Ignore
                    break;
                case "phoebe.items.consumable.shield_potion":
                    // Ignore
                    break;
                case "phoebe.items.consumable.slurpfish":
                    // Ignore
                    break;
                case "phoebe.items.consumable.small_shield_potion":
                    // Ignore
                    break;
                case "phoebe.items.consumable.smallfryfish":
                    // Ignore
                    break;
                case "phoebe.items.grenade.boogiebomb":
                    // Ignore
                    break;
                case "phoebe.items.grenade.grenade":
                    // Ignore
                    break;
                case "phoebe.items.grenade.grenadem":
                    // Ignore
                    break;
                case "phoebe.items.grenade.stink":
                    // Ignore
                    break;
                case "phoebe.items.key.vaultkeycard":
                    // Ignore
                    break;
                case "phoebe.weapon.ranged.harpoon":
                    // Ignore
                    break;
                case "phoebe.weapon.throwable.grenade":
                    // Ignore
                    break;
                case "phoebe.items.arsenic.claws":
                    ItemType = EItemType.GhostClaws;
                    break;
                case "trait.candy":
                    // Ignore
                    break;
                case "trait.fish.small":
                    // Ignore
                    break;
                case "trait.foraged":
                    // Ignore
                    break;
                case "trait.fruit":
                    // Ignore
                    break;
                case "trait.fungus":
                    // Ignore
                    break;
                case "trait.restoration.shields":
                    // Ignore
                    break;
                case "trait.slurp":
                    // Ignore
                    break;
                case "trait.spicy":
                    // Ignore
                    break;
                case "trait.veggie":
                    // Ignore
                    break;
                case "trap.beneficial":
                    // Ignore
                    break;
                case "trap.building.validity.ignore":
                    // Ignore
                    break;
                case "trap.campfire":
                    // Ignore
                    break;
                case "trap.ceiling":
                    ItemType = EItemType.Trap;
                    break;
                case "trap.extrapiece.cost.ignore":
                    // Ignore
                    break;
                case "trap.floor":
                    ItemType = EItemType.Trap;
                    break;
                case "trap.hostile":
                    ItemType = EItemType.Trap;
                    break;
                case "trap.ignoreinteractbuildcheck":
                    // Ignore
                    break;
                case "trap.jumppad":
                    ItemType = EItemType.LaunchPad;
                    break;
                case "trap.mobility":
                    // Ignore
                    break;
                case "trap.wall":
                    ItemType = EItemType.Trap;
                    break;
                case "trap.wall.fence":
                    ItemType = EItemType.Trap;
                    break;
                case "weapon.backpack.jetpack":
                    ItemType = EItemType.Jetpack;
                    break;
                case "weapon.gadget.cooldowntype.attribute":
                    // Ignore
                    break;
                case "weapon.melee.edged.sword":
                case "weapon.melee.infinityblade":
                    ItemType = EItemType.InfinityBlade;
                    break;
                case "creative.eliminationmanager.enemytype.fiend":
                    ItemType = EItemType.Fiend;
                    break;
                case "weapon.melee.turbo.axemelee":
                    ItemType = EItemType.ThorStormBreaker;
                    break;
                case "weapon.ranged.boombox":
                    ItemType = EItemType.BoomBox;
                    break;
                case "weapon.ranged.chicago":
                    ItemType = EItemType.CaptainAmericaShield;
                    break;
                case "weapon.ranged.fireworksmortar":
                    // Ignore
                    break;
                case "weapon.ranged.grenade.gas":
                    // Ignore
                    break;
                case "weapon.ranged.grenade.molotov":
                    // Ignore
                    break;
                case "weapon.ranged.grenade.standard":
                    // Ignore
                    break;
                case "weapon.ranged.grenade.tnt":
                    // Ignore
                    break;
                case "weapon.ranged.heavy.grenade":
                    // Ignore
                    break;
                case "weapon.ranged.heavy.mortar":
                    // Ignore
                    break;
                case "weapon.ranged.trap.spike":
                    ItemType = EItemType.Trap;
                    break;
                case "weapon.ranged.turret":
                    // Ignore
                    break;
                case "weapon.rechargeammo":
                    // Ignore
                    break;
                case "weapon.thrown":
                    // Ignore
                    break;
                case "item.weapon.ranged.assault.auto":
                    // Ignore
                    break;
                case "phoebe.items.galileobun":
                    // Ignore
                    break;
                case "phoebe.weapon.ranged.galileobun":
                    // Ignore
                    break;
                case "item.weapon.galileo.bun.cosmos":
                    ItemType = EItemType.E11BlasterRifle;
                    break;
                case "phoebe.items.pastaripper":
                    // Ignore
                    break;
                case "item.weapon.ranged.assault.pastaripper.boss":
                    ItemType = EItemType.SlonePulseRifle;
                    break;
                case "item.ingredient.nutsbolts":
                    // Ignore
                    ItemType = EItemType.NutsBolts;
                    break;
                case "phoebe.weapon.trait.charge":
                    // Ignore
                    break;
                case "athena.quests.freshcheese.gatherurgentquest":
                    // Ignore
                    break;
                case "phoebe.items.llamaroaster":
                    // Ignore
                    break;
                case "item.weapon.ranged.scooter":
                    ItemType = EItemType.ReconScanner;
                    break;
                case "item.weapon.ranged.sniper.reactorgrade":
                    ItemType = EItemType.RailGun;
                    break;
                case "bacchus.shoot.sniper":
                    // Ignore
                    break;
                case "item.weapon.ranged.smg.llamaroaster":
                    ItemType = EItemType.KymeraRayGun;
                    break;
                case "item.weapon.ranged.smg.llamaroaster.boss":
                    ItemType = EItemType.ZygAndChoppyRayGun;
                    break;
                case "item.weapon.ranged.launcher.badnews":
                    ItemType = EItemType.PlasmaCannon;
                    break;
                case "phoebe.weapon.ranged.llamaroaster":
                    // Ignore
                    break;
                case "athena.ltm.goose.touchedground":
                    ItemType = EItemType.InstantDeath;
                    break;
                case "athena.ltm.goose.weapon.r":
                    ItemType = EItemType.BiPlane;
                    break;
                case "item.weapon.ranged.assault.llamaroasterv3":
                    ItemType = EItemType.KymeraRayGun;
                    break;
                case "phoebe.items.avacadoeaterhead":
                    // Ignore
                    break;
                case "granted.athena.item.lotus.scooter":
                    // Ignore
                    break;
                case "phoebe.items.reactorgrade":
                    // Ignore
                    break;
                case "phoebe.weapon.ranged.reactorgrade":
                    // Ignore
                    break;
                case "item.weapon.ranged.smg":
                    // Ignore
                    break;
                case "item.weapon.galileo.lobster.limo":
                case "item.weapon.galileo.lobster":
                case "item.weapon.galileo.lobster.rocket":
                case "item.weapon.galileo.lobster.kayak":
                case "item.weapon.galileo.lobster.moped":
                    ItemType = EItemType.Lightsaber;
                    break;
                case "item.weapon.galileo.bun":
                    ItemType = EItemType.E11BlasterRifle;
                    break;
                case "building.type.prop":
                case "building.type.container":
                    ItemType = EItemType.BuildingProp;
                    break;
                case "gameplaycue.abilities.tractorbeam":
                case "gameplay.status.tractorbeam":
                case "phoebe.items.nevada.1":
                case "item.weapon.vehicle.nevada.energycannon":
                    ItemType = EItemType.Ufo;
                    break;
                case "ability.hightower.power.date.chainlightning":
                case "ability.hightower.power.date.balllightning":
                    ItemType = EItemType.Ability;
                    break;
                case "item.weapon.mother.paddleginger":
                    ItemType = EItemType.Propifier;
                    break;
                case "item.power.clash.red":
                    ItemType = EItemType.CarnageSymbiote;
                    break;
                case "item.power.clash.black":
                    ItemType = EItemType.VenomSymbiote;
                    break;
                case "item.consumable.fish.floppershadow":
                    ItemType = EItemType.ShadowFlopper;
                    break;
                case "item.weapon.ranged.assault.burst.slone":
                    ItemType = EItemType.SloneBurstAssaultRifle;
                    break;
                case "weaponset.boombox":
                    ItemType = EItemType.BeatBlaster;
                    break;
                case "weapon.backpack.cagesaddle":
                    ItemType = EItemType.InflateABull;
                    break;
                case "item.consumable.goop":
                    ItemType = EItemType.AlienNanites;
                    break;
                case "item.weapon.ranged.assault.powerupminigun.boss":
                case "item.weapon.ranged.assault.powerupminigun":
                    ItemType = EItemType.SidewaysMinigun;
                    break;
                case "item.weapon.ranged.assault.poweruprifle":
                    ItemType = EItemType.SidewaysRifle;
                    break;
                case "item.weapon.ranged.bow.fiendhunter.dualies":
                    ItemType = EItemType.DualFiendHunters;
                    break;
                case "item.weapon.melee.scythe.powerup":
                    ItemType = EItemType.SidewaysScythe;
                    break;
                case "item.weapon.ranged.assault.combat":
                    ItemType = EItemType.CombatAssaultRifle;
                    break;
                case "item.weapon.ranged.smg.combat":
                    ItemType = EItemType.CombatSmg;
                    break;
                case "item.weapon.ranged.pistol.tactical":
                    ItemType = EItemType.CombatPistol;
                    break;
                case "weapon.thrown.throwingaxe":
                    ItemType = EItemType.Basketball;
                    break;
                case "item.consumable.pretzel":
                    ItemType = EItemType.ZeroPointPretzel;
                    break;
                case "item.weapon.kabob":
                    ItemType = EItemType.PaperBombKunai;
                    break;
                case "item.weapon.ranged.assault.corear":
                    ItemType = EItemType.CoreAssaultRifle;
                    break;
                case "item.weapon.ranged.smg.coresmg":
                    ItemType = EItemType.CoreSmg;
                    break;
                case "item.weapon.ranged.shotgun.coredps":
                    ItemType = EItemType.CoreShotgun;
                    break;
                case "item.weapon.ranged.shotgun.coreburst":
                    ItemType = EItemType.CoreBurstShotgun;
                    break;
                case "item.weapon.ranged.assault.heavy.recoil":
                    ItemType = EItemType.HammerAssaultRifle;
                    break;
                case "item.weapon.ranged.pistol.corepistol":
                    ItemType = EItemType.CorePistol;
                    break;
                case "item.weapon.ranged.shotgun.chrome":
                    ItemType = EItemType.ChromeShotgun;
                    break;
                case "item.weapon.ranged.assault.chrome":
                    ItemType = EItemType.ChromeAssaultRifle;
                    break;
                case "item.weapon.ranged.assault.chrome.boss":
                    ItemType = EItemType.TheHeraldBurstRifle;
                    break;
                case "item.weapon.ranged.assault.burst.reddot":
                    ItemType = EItemType.StrikerBurstRifle;
                    break;
                case "item.weapon.ranged.assault.burst.reddot.slone":
                    ItemType = EItemType.SloneStrikerBurstRifle;
                    break;
                case "item.weapon.ranged.shotgun.pump.twoshot":
                    ItemType = EItemType.TwoShotShotgun;
                    break;
                case "item.weapon.ranged.shotgun.singlebreakaction":
                    ItemType = EItemType.RangerShotgun;
                    break;
                case "item.weapon.ranged.smg.charge":
                    ItemType = EItemType.ChargeSmg;
                    break;
                case "item.consumable.sawblade":
                    ItemType = EItemType.RipsawLauncher;
                    break;
                case "item.weapon.ranged.assault.thermal.huntmastersaber":
                    ItemType = EItemType.HuntmasterSaberThermalRifle;
                    break;
                case "item.weapon.ranged.shotgun.overload":
                    ItemType = EItemType.OverloadShotgun;
                    break;
                case "item.weapon.ranged.assault.thermal":
                    ItemType = EItemType.ThermalScopedAssaultRifle;
                    break;
                case "item.weapon.ranged.sniper.coresniper":
                    ItemType = EItemType.CoreSniper;
                    break;
                case "item.weapon.ranged.dmr":
                    ItemType = EItemType.Dmr;
                    break;
                case "item.weapon.ranged.smg.coresmg.iogunnar":
                    ItemType = EItemType.GunnarStingerSmg;
                    break;
                case "weapon.ranged.pistol":
                    if (ItemType == EItemType.Unknown)
                    {
                        ItemType = EItemType.Pistol;
                    }

                    break;
                case "item.weapon.ranged.marksman":
                    break;
            }

            if (Biome == null && deathTag.StartsWith("Athena.Location.Biome", StringComparison.OrdinalIgnoreCase))
            {
                Biome = deathTag.Split('.').Last();
            }
            else if (PointOfInterest == null &&
                     (deathTag.StartsWith("Athena.Location.POI", StringComparison.OrdinalIgnoreCase) ||
                      deathTag.StartsWith("Athena.Location.UnNamedPOI", StringComparison.OrdinalIgnoreCase)))
            {
                PointOfInterest = deathTag.Split('.').Last();
            }
        }
    }
}