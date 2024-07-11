using Brains;
using UnityEngine;
using UnityEngine.AI;

namespace FSM.Combat
{
    /// <summary>
    /// make the object look at attacker an wait for _timeToWait seconds
    /// then go back to idle if it not seen
    /// </summary>
    public class RespondToAttack : IState
    {
        private CombatSystem _combat;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private float _timeToWait = 2f;

        public RespondToAttack(CombatSystem combatSystem, Animator animator, NavMeshAgent navMeshAgent)
        {
            _combat = combatSystem;
            _animator = animator;
            _navMeshAgent = navMeshAgent;
        }

        public string String() => "responding to attacker";

        public void Tick()
        {
            _combat.transform.LookAt(_combat.attackedBy);
        }

        public void OnEnter()
        {
            _navMeshAgent.enabled = false;
        }

        public void OnExit()
        {
            _navMeshAgent.enabled = true;
            _combat.attackedBy = null;
        }
    }
}