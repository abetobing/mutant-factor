#region

using System;
using UnityEngine;

#endregion

namespace Brains
{
    public class Farmer : BaseProfession
    {
        private void Awake() => Profession = "Gatherer";

        public override ScriptableObject Stats() => null;

        public void Enable() => throw new NotImplementedException();

        public void Disable() => throw new NotImplementedException();
    }
}