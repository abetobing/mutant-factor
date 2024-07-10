#region

using Brains;
using UnityEngine;

#endregion

namespace Entities
{
    public class FoodSource : MonoBehaviour, IHarvestable
    {
        // private void Awake() => displayName = "Food resource";
        public int TotalOwned { get; set; }
        public int HarvestPerHit { get; set; }

        public bool IsDepleted => TotalOwned <= 0;

        public void LateUpdate()
        {
            if (IsDepleted)
                Destroy(gameObject, 0.5f);
        }

        public void OnDestroy()
        {
        }
    }
}