#region

using Brains;

#endregion

namespace FSM.MinerState
{
    internal class PlaceResourcesInStockpile : IState
    {
        private readonly Miner _miner;

        public PlaceResourcesInStockpile(Miner miner)
        {
            _miner = miner;
        }

        public string String()
        {
            return "placing resource on stockpile";
        }

        public void Tick()
        {
            if (_miner is not IHarvestable g) return;
            if (g.Take(1))
                _miner.StockPile.Add();
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}