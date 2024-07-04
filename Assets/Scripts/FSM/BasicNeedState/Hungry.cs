using Brains;
using UnityEngine;
using UnityEngine.AI;

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
            Debug.Log("hungry!");
            _animator.SetBool(Constants.IsHarvestingHash, false);
            // stop the profession or current job
            _thePerson.GetComponent<Gatherer>().enabled = false;
            _thePerson.NeedToEat = true;
        }

        public void OnExit()
        {
        }
    }
}