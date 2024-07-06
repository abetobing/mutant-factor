#region

using Brains;
using UnityEngine;

#endregion

namespace FSM.BasicNeedState
{
    public class Healthy : IState
    {
        private BasicNeeds _thePerson;
        public Healthy(BasicNeeds basicNeeds)
        {
            _thePerson = basicNeeds;
        }

        public string String()
        {
            return "healthy";
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            // enable the profession or current job again
            _thePerson.GetComponent<IProfession>().Enable();
        }

        public void OnExit()
        {
        }
    }
}