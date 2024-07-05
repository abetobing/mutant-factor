#region

using UnityEngine;

#endregion

namespace Brains
{
    /*
     * This interface represent profession of entities
     * Each entities can have one or more or no profession at all
     */
    public interface IProfession
    {
        public string Name(); // returns the profession name
        public ScriptableObject Stats(); // returns the profession stats
        public void Enable();
        public void Disable();

    }
}