#region

using Brains;
using UnityEngine;

#endregion

namespace FSM.BasicNeedState
{
    public class MoveToFood : IState
    {
        private BasicNeeds _thePerson;
        private readonly ICharacterMovement _characterMovement;

        private Vector3 _lastPosition = Vector3.zero;
        public float TimeStuck;

        public MoveToFood(BasicNeeds basicNeeds)
        {
            _thePerson = basicNeeds;
            _characterMovement = basicNeeds.GetComponent<ICharacterMovement>();
        }

        public string String()
        {
            return "moving to food source";
        }

        public void Tick()
        {
            if (Vector3.Distance(_thePerson.transform.position, _lastPosition) <= 0f)
                TimeStuck += Time.deltaTime;

            _lastPosition = _thePerson.transform.position;
        }

        public void OnEnter()
        {
            TimeStuck = 0f;
            _characterMovement.MoveTo(_thePerson.FoodTarget.transform.position);
        }

        public void OnExit()
        {
            _characterMovement.Stop();
        }
    }
}