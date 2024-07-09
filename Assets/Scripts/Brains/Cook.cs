#region

using System;
using UnityEngine;

#endregion

namespace Brains
{
    public class Cook : BaseProfession
    {
        public void Awake() => Profession = "Cook";

        public override ScriptableObject Stats() => null;

        public void Enable() => throw new NotImplementedException();

        public void Disable() => throw new NotImplementedException();
    }
}