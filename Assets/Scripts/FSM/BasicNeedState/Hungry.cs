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

        public void Tick()
        {
        }

        public void OnEnter()
        {
            Debug.LogFormat("{0} is hungry", _thePerson.gameObject.name);
            _animator.SetBool(Constants.IsHarvestingHash, false);
            // stop the profession or current job
            _thePerson.GetComponent<IProfession>().Disable();
            _thePerson.HasEnteredHungryState = true;
        }

        public void OnExit()
        {
        }
    }
}