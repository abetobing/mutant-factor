#region

using Brains;
using UnityEngine;

#endregion

namespace FSM.GathererState
{
    internal class PlaceResourcesInStockpile : IState
    {
        private readonly Gatherer _gatherer;

        public PlaceResourcesInStockpile(Gatherer gatherer)
        {
            _gatherer = gatherer;
        }

        public string String()
        {
            return "placing resource on stockpile";
        }

        public void Tick()
        {
            if (_gatherer.Take())
                _gatherer.StockPile.Add();
        }

        public void OnEnter()
        {
        }

        public void OnExit() { }
    }
}