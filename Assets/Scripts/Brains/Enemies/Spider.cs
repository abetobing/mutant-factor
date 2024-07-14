using FSM;
using UnityEngine;

namespace Brains.Enemies
{
    public class Spider : BaseProfession
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