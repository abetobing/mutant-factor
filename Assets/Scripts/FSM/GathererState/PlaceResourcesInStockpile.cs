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

        public void Tick()
        {
            if (_gatherer.Take())
                _gatherer.StockPile.Add();
        }

        public void OnEnter()
        {
            Debug.LogFormat("{0} is placing resource in stockpile", _gatherer.gameObject.name);
        }

        public void OnExit() { }
    }
}