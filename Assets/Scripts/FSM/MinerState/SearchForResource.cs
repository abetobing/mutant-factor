#region

using System.Linq;
using Brains;
using Entities;
using UnityEngine;

#endregion

namespace FSM.MinerState
{
    public class SearchForResource : IState
    {
        private readonly Miner _miner;

        public SearchForResource(Miner miner)
        {
            _miner = miner;
        }

        public string String()
        {
            return "searching for resource";
        }

        public void Tick()
        {
            _miner.Target = ChooseOneOfTheNearestResources(5);
        }

        private GatherableResource ChooseOneOfTheNearestResources(int pickFromNearest)
        {
            return Object.FindObjectsOfType<GatherableResource>()
                .OrderBy(t => Vector3.Distance(_miner.transform.position, t.transform.position))
                .Where(t => t.IsDepleted == false)
                .Take(pickFromNearest)
                .OrderBy(t => Random.Range(0, int.MaxValue))
                .FirstOrDefault();
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}