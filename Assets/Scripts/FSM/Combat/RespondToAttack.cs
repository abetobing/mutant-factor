using Brains;

namespace FSM.Combat
{
    /// <summary>
    /// make the object look at attacker an wait for _timeToWait seconds
    /// then go back to idle if it not seen
    /// </summary>
    public class RespondToAttack : IState
    {
        private CombatSystem _combat;
        private readonly ICharacterMovement _characterMovement;
        private readonly HealthBar _healthBar;

        public RespondToAttack(CombatSystem combatSystem)
        {
            _combat = combatSystem;
            _characterMovement = combatSystem.GetComponent<ICharacterMovement>();
            _healthBar = combatSystem.GetComponent<HealthBar>();
        }

        public string String() => "responding to attacker";

        public void Tick()
        {
            if (_characterMovement.HasArrived())
            {
                _characterMovement.RotateTo(_combat.transform.position);
                // _combat.attackedBy = null;
            }
        }

        public void OnEnter()
        {
            if (_healthBar)
                _healthBar.enabled = true;
            // var direction = (_combat.attackedBy.transform.position - _combat.transform.position).normalized * 3f;
            _characterMovement.MoveTo(_combat.attackedBy.transform.position);
        }

        public void OnExit()
        {
            _combat.attackedBy = null;
        }
    }
}