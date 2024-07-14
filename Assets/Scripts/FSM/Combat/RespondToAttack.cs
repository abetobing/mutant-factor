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

        public RespondToAttack(CombatSystem combatSystem)
        {
            _combat = combatSystem;
            _characterMovement = combatSystem.GetComponent<ICharacterMovement>();
        }

        public string String() => "responding to attacker";

        public void Tick()
        {
            // _combat.transform.LookAt(_combat.attackedBy);
            _characterMovement.RotateTo(_combat.attackedBy.position);
        }

        public void OnEnter()
        {
            _characterMovement.Stop();
        }

        public void OnExit()
        {
            _combat.attackedBy = null;
        }
    }
}