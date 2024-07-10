#region

using UnityEngine;

#endregion

public class Constants
{
    public static readonly int IsFemaleHash = Animator.StringToHash("isFemale");
    public static readonly int IsCombatHash = Animator.StringToHash("isCombat");
    public static readonly int IsWorkingHash = Animator.StringToHash("isWorking");
    public static readonly int SpeedHash = Animator.StringToHash("speed");
    public static readonly int AngleHash = Animator.StringToHash("angle");

    public static readonly int AttackHash = Animator.StringToHash("attack");
    public static readonly int AttackSpeedHash = Animator.StringToHash("attackSpeed");

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
}