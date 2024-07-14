using Brains;
using UnityEngine;
using UnityEngine.AI;

namespace Entities
{
    public class AnimatorController : MonoBehaviour
    {
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private Metabolism _metabolism;
        private CombatSystem _combatSystem;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _metabolism = GetComponent<Metabolism>();
            _combatSystem = GetComponent<CombatSystem>();
        }

        private void Update()
        {
            // set health parameter
            _animator?.SetFloat(Constants.HealthHash, _metabolism.health);

            // set attackSpeed parameter
            var animationAttackSpeed = _combatSystem.attackSpeed / 10f;
            _animator?.SetFloat(Constants.AttackSpeedHash, animationAttackSpeed);
            _animator?.SetFloat(Constants.AttackIndexHash, _combatSystem.attackIndex, 1 / _animator.speed,
                Time.deltaTime);

            // set gender parameter
            var gender = _metabolism.isFemale ? 1f : 0f;
            _animator?.SetFloat(Constants.GenderHash, gender);
        }


        public void AnimEventAttack()
        {
            _combatSystem.PerformAttack();
        }

        public void AnimEventHarvest()
        {
            var profession = GetComponent<BaseProfession>();
            if (profession == null)
                return;

            if (profession is IHarvestable p)
                p.TakeFromTarget();

            // switch (profession)
            // {
            //     case Miner miner:
            //         miner.TakeFromTarget();
            //         break;
            //     case Gatherer gatherer:
            //         gatherer.TakeFromTarget();
            //         break;
            // }
        }
    }
}