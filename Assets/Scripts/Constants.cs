#region

using UnityEngine;

#endregion

public class Constants
{
    public static readonly int IsFemaleHash = Animator.StringToHash("isFemale");
    public static readonly int IsCombatHash = Animator.StringToHash("isCombat");
    public static readonly int IsWorkingHash = Animator.StringToHash("isWorking");
    public static readonly int SpeedHash = Animator.StringToHash("speed");
    public static readonly int TurnHash = Animator.StringToHash("turn"); // turning angle
    public static readonly int HorizontalHash = Animator.StringToHash("horizontal");
    public static readonly int VerticalHash = Animator.StringToHash("vertical");

    public static readonly int AttackHash = Animator.StringToHash("attack");
    public static readonly int AttackSpeedHash = Animator.StringToHash("attackSpeed");
    public static readonly int GenderHash = Animator.StringToHash("gender");
    public static readonly int WeaponTypeHash = Animator.StringToHash("weaponType");

    // public static readonly int DeadHash = Animator.StringToHash("dead");
    public static readonly int HealthHash = Animator.StringToHash("health");
    public static readonly int Gathering = Animator.StringToHash("gathering");
    public static readonly int Mining = Animator.StringToHash("mining");
    public static readonly int Farming = Animator.StringToHash("farming");
    public static readonly int Fishing = Animator.StringToHash("fishing");

    public const float NavMeshDistanceTolerance = 1.5f;

    public const float DefaultMaxHealth = 100f;
    public const float DefaultMaxHunger = 100f;
    public const float DefaultMaxStamina = 100f;

    public const float EatIntervalPerSecond = 50f; // how many food eaten per second

    public const string SelectableTag = "Selectable";

    public const ProfessionType DefaultProfession = ProfessionType.None;
}

public enum ProfessionType
{
    None,
    Gatherer,
    Miner,
    Farmer,
    Fisher,
    Guard
}

public enum WeaponType
{
    None,
    Knife,
    AxeOrBlunt,
    Sword,
    Bow,
    Handgun,
    Handgun2,
    Rifle1,
    Rifle2,
    Rifle3,
    Rifle4,
    SniperRifle
}