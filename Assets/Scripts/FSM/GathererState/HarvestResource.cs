using Entities;
using UnityEngine;

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
                    _animator.SetBool(Gatherer.IsHarvestingHash, true);
                }
            }
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
            _animator.SetBool(Gatherer.IsHarvestingHash, false);
        }
    }
}