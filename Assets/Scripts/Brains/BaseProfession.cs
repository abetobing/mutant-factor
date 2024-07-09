#region

using FSM;
using UnityEngine;

#endregion

namespace Brains
{
    /*
     * This abstract class represent profession of entities
     * Each entities can have one or more or no profession at all
     */
    public abstract class BaseProfession : MonoBehaviour
    {
        protected StateMachine _stateMachine;
        protected string Profession;
        public string Name() => Profession; // returns the profession name

        public string ActivtyText() => _stateMachine.CurrentActivity();

        public abstract ScriptableObject Stats(); // returns the profession stats
    }
}