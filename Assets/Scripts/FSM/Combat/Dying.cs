using Brains;

namespace FSM.Combat
{
    public class Dying : IState
    {
        private readonly ICharacterMovement _characterMovement;

        public Dying(CombatSystem combatSystem)
        {
            _characterMovement = combatSystem.GetComponent<ICharacterMovement>();
        }

        public string String() => "dying";

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _characterMovement.Stop();
        }

        public void OnExit()
        {
        }
    }
}