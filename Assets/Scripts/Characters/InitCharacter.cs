using Brains;
using Entities;
using UnityEngine;
using UnityEngine.Assertions;

namespace Characters
{
    [RequireComponent(typeof(HumanoidMovement), typeof(Metabolism), typeof(BasicNeeds))]
    [RequireComponent(typeof(CombatSystem), typeof(AnimatorController), typeof(Weapon))]
    public class InitCharacter : MonoBehaviour
    {
        public BaseCharacter character;
        public ProfessionType professionType;

        private void Awake()
        {
            Assert.IsNotNull(character);
            gameObject.name = character.name;


            var metabolism = GetComponent<Metabolism>();
            metabolism.hunger = character.hunger;
            metabolism.health = character.health;
            metabolism.stamina = character.stamina;
            metabolism.hungerFallRate = character.hungerFallRate;
            metabolism.isFemale = character.isFemale;


            var combat = GetComponent<CombatSystem>();
            combat.attackRange = character.attackRange;
            combat.attackSpeed = character.attackSpeed;
            combat.angle = character.attackRangeAngle;
            combat.radius = character.visionRange;
            combat.targetMask = character.targetMask;

            var weapon = GetComponent<Weapon>();
            if (character.weapon != null)
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