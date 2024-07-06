#region

using Brains;
using UnityEngine;

#endregion

namespace FSM.GathererState
{
    internal class HarvestResource : IState
    {
        private readonly Gatherer _gatherer;
        private readonly Animator _animator;
        private float _resourcesPerSecond = 3;

        private float _nextTakeResourceTime;

        public HarvestResource(Gatherer gatherer, Animator animator)
        {
            _gatherer = gatherer;
            _animator = animator;
        }

        public void Tick()
        {
            if (_gatherer.Target != null)
            {
                if (_nextTakeResourceTime <= Time.time)
                {
                    _nextTakeResourceTime = Time.time + (1f / _resourcesPerSecond);
                    _gatherer.TakeFromTarget();
                    _animator.SetBool(Constants.IsHarvestingHash, true);
                }
            }
        }

        public void OnEnter()
        {
        }

        public string String()
        {
            return "harvesting resource";
        }

        public void OnExit()
        {
            _animator.SetBool(Constants.IsHarvestingHash, false);
        }
    }
}