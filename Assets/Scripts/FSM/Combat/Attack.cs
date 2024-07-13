using Brains;
using DefaultNamespace;
using UnityEngine;

namespace FSM.Combat
{
    public class Attack : IState
    {
        private readonly CombatSystem _combat;
        private readonly Animator _animator;
        private float _nextAttackTime;
        private readonly ICharacterMovement _characterMovement;

        public Attack(CombatSystem combatSystem)
        {
            _combat = combatSystem;
            _animator = combatSystem.GetComponent<Animator>();
            _characterMovement = combatSystem.GetComponent<ICharacterMovement>();
        }

        public string String() => "attacking";

        public void Tick()
        {
            if (_combat.target != null)
            {
                _combat.transform.LookAt(_combat.target);
            }
        }

        public void OnEnter()
        {
            _characterMovement.Stop();
            _animator.SetBool(Constants.IsCombatHash, true);
            _animator.SetTrigger(Constants.AttackHash);
        }

        public void OnExit()
        {
            _animator.SetBool(Constants.IsCombatHash, false);
            _animator.ResetTrigger(Constants.AttackHash);
        }
    }
}