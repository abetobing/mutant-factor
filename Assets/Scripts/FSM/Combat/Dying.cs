using Brains;
using UnityEngine;
using UnityEngine.AI;

namespace FSM.Combat
{
    public class Dying : IState
    {
        private CombatSystem _combat;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;

        public Dying(CombatSystem combatSystem, Animator animator, NavMeshAgent navMeshAgent)
        {
            _combat = combatSystem;
            _animator = animator;
            _navMeshAgent = navMeshAgent;
        }

        public string String() => "dying";

        public void Tick()
        {
        }

        public void OnEnter()
        {
            Debug.Log(String());
            _navMeshAgent.enabled = true;
            _navMeshAgent.ResetPath();
            // _animator.SetTrigger(Constants.DeadHash);
        }

        public void OnExit()
        {
        }
    }
}