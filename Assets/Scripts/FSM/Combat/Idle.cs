using Brains;
using UnityEngine;
using UnityEngine.AI;

namespace FSM.Combat
{
    public class Idle : IState
    {
        private CombatSystem _combat;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private BaseProfession _profession;
        private BasicNeeds _basicNeeds;

        public Idle(CombatSystem combatSystem, Animator animator, NavMeshAgent navMeshAgent)
        {
            _combat = combatSystem;
            _animator = animator;
            _navMeshAgent = navMeshAgent;
        }

        public string String() => "Idle";

        public void Tick()
        {
        }

        public void OnEnter()
        {
            if (_navMeshAgent.hasPath)
                _navMeshAgent.ResetPath();
            _profession = _combat.GetComponent<BaseProfession>();
            if (_profession != null)
                _profession.enabled = true;
            _basicNeeds = _combat.GetComponent<BasicNeeds>();
            if (_basicNeeds != null)
                _basicNeeds.enabled = true;
        }

        public void OnExit()
        {
            if (_profession != null)
                _profession.enabled = false;
            if (_basicNeeds != null)
                _basicNeeds.enabled = false;
        }
    }
}