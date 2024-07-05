using UnityEngine;

namespace Brains
{
    public class Farmer : MonoBehaviour, IProfession
    {
        public string Name() => "Farmer";

        public ScriptableObject Stats() => throw new System.NotImplementedException();

        public void Enable() => throw new System.NotImplementedException();

        public void Disable() => throw new System.NotImplementedException();
    }
}