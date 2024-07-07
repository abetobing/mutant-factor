using Brains;
using UnityEngine;
using UnityEngine.AI;

namespace FSM.Combat
{
    public class Attack : IState
    {
        private CombatSystem _combat;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private float _nextAttackTime;

        public Attack(CombatSystem combatSystem, Animator animator, NavMeshAgent navMeshAgent)
        {
            _combat = combatSystem;
            _animator = animator;
            _navMeshAgent = navMeshAgent;
        }

        public string String() => "attacking";

        public void Tick()
        {
            if (_combat.target != null)
            {
                // _combat.PerformAttack();
                _combat.transform.LookAt(_combat.target);
                if (_nextAttackTime <= Time.time)
                {
                    _nextAttackTime = Time.time + (10f / _combat.attackSpeed);
                    _combat.PerformAttack();
                    _animator.SetTrigger(Constants.AttackHash);
                }
                else
                {
                    _animator.ResetTrigger(Constants.AttackHash);
                }
            }
        }

        public void OnEnter()
        {
            _navMeshAgent.enabled = false;
        }

        public void OnExit()
        {
            _navMeshAgent.enabled = true;
            _animator.ResetTrigger(Constants.AttackHash);
        }
    }
}