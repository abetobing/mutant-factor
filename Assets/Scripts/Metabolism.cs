using Brains;
using Entities;
using UnityEngine;

namespace DefaultNamespace
{
    public class Metabolism : MonoBehaviour
    {
        public float health = 100f;
        public float hunger = 100f;
        public float stamina = 100f;

        public bool IsAlive => health > 0f;
        [SerializeField] private float hungerFallRate = 0.25f;

        private BasicNeeds _basicNeeds;

        private void Awake()
        {
            hunger -= Time.deltaTime * hungerFallRate;
            hunger = Mathf.Clamp(
                hunger,
                0f,
                Constants.DefaultMaxHunger
            );
        }

        private void Update()
        {
            if (!IsAlive)
            {
                if (GetComponent<FoodSource>() == null) // this is not a food source, so destroy it after death
                    Destroy(gameObject, 5f);
                else
                    GetComponent<FoodSource>().enabled = true; // else enable it so it can be harvested
            }

            // hunger will fall at a given rate
            hunger -= Time.deltaTime * hungerFallRate;
            hunger = Mathf.Clamp(
                hunger,
                0f,
                Constants.DefaultMaxHunger
            );
        }

        public void TakingDamage(float damage, GameObject damageSource)
        {
            health -= damage;
            health = Mathf.Clamp(health, 0, Constants.DefaultMaxHealth);

            var combatSystem = GetComponent<CombatSystem>();
            if (combatSystem != null)
            {
                combatSystem.attackedBy = damageSource.transform;
            }
        }
    }
}