using Brains;
using Entities;
using UnityEngine;
using UnityEngine.Assertions;

namespace Characters
{
    public class InitCharacter : MonoBehaviour
    {
        public BaseCharacter character;
        public ProfessionType professionType;

        private void Awake()
        {
            Assert.IsNotNull(character);
            gameObject.name = character.name;

            var metabolism = gameObject.AddComponent<Metabolism>();
            metabolism.hunger = character.hunger;
            metabolism.health = character.health;
            metabolism.stamina = character.stamina;
            metabolism.hungerFallRate = character.hungerFallRate;
            metabolism.isFemale = character.isFemale;

            gameObject.AddComponent<BasicNeeds>();
            gameObject.AddComponent<HumanoidMovement>();

            var combat = gameObject.AddComponent<CombatSystem>();
            combat.attackRange = character.attackRange;
            combat.attackSpeed = character.attackSpeed;
            combat.angle = character.attackRangeAngle;
            combat.radius = character.visionRange;
            combat.targetMask = character.targetMask;

            gameObject.AddComponent<AnimatorController>();
            var weapon = GetComponent<Weapon>();
            if (character.weapon != null && weapon != null)
                weapon.SwitchTo(character.weapon);

            if (character is DefaultCharacter defaultCharacter)
            {
                professionType = defaultCharacter.profession;
                switch (professionType)
                {
                    case ProfessionType.Gatherer:
                        gameObject.AddComponent<Gatherer>();
                        break;
                    case ProfessionType.Miner:
                        gameObject.AddComponent<Miner>();
                        break;
                    case ProfessionType.None:
                        gameObject.AddComponent<Unemployed>();
                        break;
                }
            }
        }
    }
}