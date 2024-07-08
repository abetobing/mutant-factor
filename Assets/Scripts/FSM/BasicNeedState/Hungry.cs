#region

using Brains;
using UnityEngine;

#endregion

namespace FSM.BasicNeedState
{
    public class Hungry : IState
    {
        private BasicNeeds _thePerson;
        private Animator _animator;

        public Hungry(BasicNeeds basicNeeds, Animator animator)
        {
            _thePerson = basicNeeds;
            _animator = animator;
        }

        public string String()
        {
            return "hungry";
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _animator.SetBool(Constants.IsWorkingHash, false);
            // stop the profession or current job
            // enable the profession or current job again
            var profession = _thePerson.GetComponent<BaseProfession>();
            if (profession != null)
                profession.enabled = false;
            _thePerson.HasEnteredHungryState = true;
        }

        public void OnExit()
        {
        }
    }
}