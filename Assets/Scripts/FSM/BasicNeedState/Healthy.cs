using Brains;
using UnityEngine;

namespace FSM.BasicNeedState
{
    public class Healthy : IState
    {
        private BasicNeeds _thePerson;
        public Healthy(BasicNeeds basicNeeds)
        {
            _thePerson = basicNeeds;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            Debug.Log("healthy!");
            // enable the profession or current job again
            _thePerson.GetComponent<IProfession>().Enable();
        }

        public void OnExit()
        {
        }
    }
}