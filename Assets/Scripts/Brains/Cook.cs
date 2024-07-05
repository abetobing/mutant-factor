#region

using System;
using UnityEngine;

#endregion

namespace Brains
{
    public class Cook : MonoBehaviour, IProfession
    {
        public string Name() => "Cook";

        public ScriptableObject Stats() => null;

        public void Enable() => throw new NotImplementedException();

        public void Disable() => throw new NotImplementedException();
    }
}