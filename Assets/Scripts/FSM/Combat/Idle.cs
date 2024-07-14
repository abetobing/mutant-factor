using Brains;
using UnityEngine;

namespace FSM.Combat
{
    public class Idle : IState
    {
        private CombatSystem _combat;
        private Animator _animator;
        private BaseProfession _profession;
        private BasicNeeds _basicNeeds;
        private HealthBar _healthBar;

        public Idle(CombatSystem combatSystem)
        {
            _combat = combatSystem;
            _animator = combatSystem.GetComponent<Animator>();
            _healthBar = combatSystem.GetComponent<HealthBar>();
        }

        public string String() => "Idle";

        public void Tick()
        {
        }

        public void OnEnter()
        {
            if (_healthBar != null)
                _healthBar.enabled = false;
            _animator.SetBool(Constants.IsCombatHash, false);

            // enable profession
            _profession = _combat.GetComponent<BaseProfession>();
            if (_profession != null)
                _profession.enabled = true;
            _basicNeeds = _combat.GetComponent<BasicNeeds>();
            if (_basicNeeds != null)
                _basicNeeds.enabled = true;
        }

        public void OnExit()
        {
            // must disable profession, so its not finding another path when combat
            if (_profession != null)
                _profession.enabled = false;
            _basicNeeds = _combat.GetComponent<BasicNeeds>();
            if (_basicNeeds != null)
                _basicNeeds.enabled = false;
        }
    }
}