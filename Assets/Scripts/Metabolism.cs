using Brains;
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

        public void TakingDamage(float damage)
        {
            health -= damage;
            health = Mathf.Clamp(health, 0, Constants.DefaultMaxHealth);
        }
    }
}