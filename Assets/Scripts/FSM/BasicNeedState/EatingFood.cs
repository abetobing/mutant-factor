#region

using Brains;
using UnityEngine;

#endregion

namespace FSM.BasicNeedState
{
    public class EatingFood : IState
    {
        private BasicNeeds _thePerson;
        private Animator _animator;

        private float _nextEatingTime; // time to delay eating

        public EatingFood(BasicNeeds basicNeeds, Animator animator)
        {
            _thePerson = basicNeeds;
            _animator = animator;
        }

        public string String()
        {
            return "eating food";
        }

        public void Tick()
        {
            if (_thePerson.FoodTarget != null)
            {
                if (_nextEatingTime <= Time.time)
                {
                    _nextEatingTime = Time.time + (1f / Constants.EatIntervalPerSecond);
                    _thePerson.EatFood();
                }
            }
        }

        public void OnEnter()
        {
            _animator.SetBool(Constants.IsWorkingHash, true);
            _thePerson.HasEnteredHungryState = true;
        }

        public void OnExit()
        {
            _animator.SetBool(Constants.IsWorkingHash, false);
        }
    }
}