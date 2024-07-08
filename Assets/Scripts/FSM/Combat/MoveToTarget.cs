using Brains;
using UnityEngine;
using UnityEngine.AI;

namespace FSM.Combat
{
    public class MoveToTarget : IState
    {
        private CombatSystem _combat;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;

        public MoveToTarget(CombatSystem combatSystem, Animator animator, NavMeshAgent navMeshAgent)
        {
            _combat = combatSystem;
            _animator = animator;
            _navMeshAgent = navMeshAgent;
        }

        public string String() => "moving to target";


        public void Tick()
        {
            if (_combat.target != null)
            {
                _combat.transform.LookAt(_combat.target.transform.position);
                _navMeshAgent.SetDestination(_combat.target.transform.position);
            }
        }

        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_combat.target.transform.position);
        }

        public void OnExit()
        {
        }
    }
}