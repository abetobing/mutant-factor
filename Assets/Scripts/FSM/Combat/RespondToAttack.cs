using Brains;
using UnityEngine;
using UnityEngine.AI;

namespace FSM.Combat
{
    public class RespondToAttack : IState
    {
        private CombatSystem _combat;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;

        public RespondToAttack(CombatSystem combatSystem, Animator animator, NavMeshAgent navMeshAgent)
        {
            _combat = combatSystem;
            _animator = animator;
            _navMeshAgent = navMeshAgent;
        }

        public string String() => "responding to attacker";

        public void Tick()
        {
        }

        public void OnEnter()
        {
            Debug.Log("responding to attack");
            _navMeshAgent.enabled = false;
            _combat.transform.LookAt(_combat.attackedBy);
        }

        public void OnExit()
        {
            _navMeshAgent.enabled = true;
            _combat.attackedBy = null;
        }
    }
}