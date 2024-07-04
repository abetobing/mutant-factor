using UnityEngine;

public class Constants
{
    public static readonly int IsWalkingHash = Animator.StringToHash("isWalking");
    public static readonly int IsRunningHash = Animator.StringToHash("isRunning");
    public static readonly int IsHarvestingHash = Animator.StringToHash("isHarvesting");

    public const float NavMeshDistanceTolerance = 1.5f;

    public const float DefaultMaxHealth = 100f;
    public const float DefaultMaxHunger = 100f;
    public const float DefaultMaxThirst = 100f;
    public const float DefaultMaxStamina = 100f;

    public const float EatIntervalPerSecond = 50f; // default interval per sec to hunger++ and food--
}
