#region

using System;
using FSM;
using UnityEngine;

#endregion

namespace Brains
{
    public class Cook : MonoBehaviour, IProfession
    {
        private StateMachine _stateMachine;
        public string Name() => "Cook";

        public string ActivtyText()
        {
            return _stateMachine.CurrentActivity();
        }

        public ScriptableObject Stats() => null;

        public void Enable() => throw new NotImplementedException();

        public void Disable() => throw new NotImplementedException();
    }
}