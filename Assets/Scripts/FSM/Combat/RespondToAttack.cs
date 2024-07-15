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
        private BaseProfession _profession;
        private BasicNeeds _basicNeeds;

        public RespondToAttack(CombatSystem combatSystem)
        {
            _combat = combatSystem;
            _characterMovement = combatSystem.GetComponent<ICharacterMovement>();
            _healthBar = combatSystem.GetComponent<HealthBar>();
        }

        public string String() => "responding to attacker";

        public void Tick()
        {
        }

        public void OnEnter()
        {
            if (_combat.attackedBy != null)
            {
                _combat.transform.LookAt(_combat.attackedBy);
                _characterMovement.MoveTo(_combat.attackedBy.position);
            }

            if (_healthBar)
                _healthBar.enabled = true;
        }

        public void OnExit()
        {
            _combat.attackedBy = null;
        }
    }
}