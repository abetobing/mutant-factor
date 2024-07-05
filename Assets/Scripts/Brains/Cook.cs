using UnityEngine;

namespace Brains
{
    public class Cook : MonoBehaviour, IProfession
    {
        public string Name() => "Cook";

        public ScriptableObject Stats() => throw new System.NotImplementedException();

        public void Enable() => throw new System.NotImplementedException();

        public void Disable() => throw new System.NotImplementedException();
    }
}