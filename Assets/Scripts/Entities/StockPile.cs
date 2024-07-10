#region

using Brains;
using UnityEngine;

#endregion

namespace Entities
{
    [ExecuteInEditMode]
    public class StockPile : MonoBehaviour, IHarvestable
    {
        [SerializeField] private int maxHeld = 2000;

        public int TotalOwned { get; set; }
        public int HarvestPerHit { get; set; }

        private void Awake()
        {
            TotalOwned = 0;
        }

        public void Add()
        {
            TotalOwned++;
            var pct = Mathf.Clamp01((float)TotalOwned / maxHeld);
        }
    }
}