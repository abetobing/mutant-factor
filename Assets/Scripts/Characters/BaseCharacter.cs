using UnityEngine;

namespace Characters
{
    public abstract class BaseCharacter : ScriptableObject
    {
        public new string name;

        // combat system
        public float visionRange = 5f;
        public float attackRange = 1f;
        public float attackRangeAngle = 120f;
        public float baseDamage = 1f;
        public float attackSpeed = 1f;
        public LayerMask targetMask;

        // metabolism
        public float health = Constants.DefaultMaxHealth;
        public float stamina = Constants.DefaultMaxStamina;
        public float hunger = Constants.DefaultMaxHunger;
        public float hungerFallRate = 0.25f;
        public bool isFemale;
        public GameObject weapon;
    }
}