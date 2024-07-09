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
                _combat.transform.LookAt(_combat.target);
                // _animator.SetTrigger(Constants.AttackHash);
                // if (_nextAttackTime <= Time.time)
                // {
                //     _nextAttackTime = Time.time + (10f / _combat.attackSpeed);
                //     _animator.SetTrigger(Constants.AttackHash);
                // }
                // else
                // {
                //     _animator.ResetTrigger(Constants.AttackHash);
                // }
            }
        }

        public void OnEnter()
        {
            _navMeshAgent.ResetPath();
            _navMeshAgent.enabled = false;
            _animator.SetBool(Constants.IsCombatHash, true);
            _animator.SetTrigger(Constants.AttackHash);
        }

        public void OnExit()
        {
            _navMeshAgent.enabled = true;
            _animator.SetBool(Constants.IsCombatHash, false);
            _animator.ResetTrigger(Constants.AttackHash);
        }
    }
}