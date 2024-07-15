using FSM;
using UnityEngine;

namespace Brains
{
    public class Unemployed : BaseProfession
    {
        public override ScriptableObject Stats() => null;

        private void OnEnable()
        {
            _stateMachine = new StateMachine();
            var roam = new RandomWalk(this.gameObject);
            _stateMachine.SetState(roam);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }
    }
}