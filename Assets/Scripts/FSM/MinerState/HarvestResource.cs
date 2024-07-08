#region

using Brains;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace FSM.MinerState
{
    internal class HarvestResource : IState
    {
        private readonly Miner _miner;
        private NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private float _resourcesPerSecond = 3;

        private float _nextTakeResourceTime;

        public HarvestResource(Miner miner, NavMeshAgent navMeshAgent, Animator animator)
        {
            _miner = miner;
            _navMeshAgent = navMeshAgent;
            _animator = animator;
        }

        public void Tick()
        {
            if (_miner.Target != null)
            {
                if (_nextTakeResourceTime <= Time.time)
                {
                    _nextTakeResourceTime = Time.time + (1f / _resourcesPerSecond);
                    _miner.TakeFromTarget();
                }
            }
        }

        public void OnEnter()
        {
            _animator.SetBool(Constants.IsWorkingHash, true);
            _animator.SetTrigger(Constants.Mining);
        }

        public string String()
        {
            return "harvesting resource";
        }

        public void OnExit()
        {
            _animator.SetBool(Constants.IsWorkingHash, false);
        }
    }
}