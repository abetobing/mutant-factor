#region

using Brains;
using UnityEngine;

#endregion

namespace Entities
{
    public class GatherableResource : MonoBehaviour, IHarvestable
    {
        public int TotalOwned { get; set; } = 1000;
        public int HarvestPerHit { get; set; } = 1;
        public bool IsDepleted => TotalOwned <= 0;
    }
}