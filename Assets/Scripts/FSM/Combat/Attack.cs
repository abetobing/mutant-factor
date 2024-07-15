using Brains;
using UnityEngine;

namespace FSM.Combat
{
    public class Attack : IState
    {
        private readonly CombatSystem _combat;
        private readonly Animator _animator;
        private float _nextAttackTime;
        private readonly ICharacterMovement _characterMovement;
        private readonly HealthBar _healthBar;

        public Attack(CombatSystem combatSystem)
        {
            _combat = combatSystem;
            _animator = combatSystem.GetComponent<Animator>();
            _characterMovement = combatSystem.GetComponent<ICharacterMovement>();
            _healthBar = combatSystem.GetComponent<HealthBar>();
        }

        public string String() => "attacking";

        public void Tick()
        {
            if (_combat.target != null && _combat.canSeeTarget)
            {
                _characterMovement.RotateTo(_combat.target.position);
                // _combat.transform.LookAt(_combat.target);
            }
        }

        public void OnEnter()
        {
            _animator.SetBool(Constants.IsCombatHash, true);
            _animator.SetTrigger(Constants.AttackHash);
            if (_healthBar != null)
                _healthBar.enabled = true;
        }

        public void OnExit()
        {
            _animator.SetBool(Constants.IsCombatHash, false);
            _animator.ResetTrigger(Constants.AttackHash);
        }
    }
}