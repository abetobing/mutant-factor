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
        private readonly ICharacterMovement _characterMovement;

        public MoveToTarget(CombatSystem combatSystem)
        {
            _combat = combatSystem;
            _animator = combatSystem.GetComponent<Animator>();
            _characterMovement = combatSystem.GetComponent<ICharacterMovement>();
        }

        public string String() => "moving to target";


        public void Tick()
        {
            if (_combat.target != null)
            {
                _characterMovement.MoveTo(_combat.target.transform.position);
            }
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}