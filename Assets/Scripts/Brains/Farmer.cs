#region

using System;
using UnityEngine;

#endregion

namespace Brains
{
    public class Farmer : MonoBehaviour, IProfession
    {
        public string Name() => "Farmer";

        public ScriptableObject Stats() => null;

        public void Enable() => throw new NotImplementedException();

        public void Disable() => throw new NotImplementedException();
    }
}