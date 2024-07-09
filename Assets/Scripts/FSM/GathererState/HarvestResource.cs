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

        public HarvestResource(Gatherer gatherer, Animator animator)
        {
            _gatherer = gatherer;
            _animator = animator;
        }

        public void Tick()
        {
            if (_gatherer.Target != null)
            {
                _gatherer.transform.LookAt(_gatherer.Target.transform);
            }
        }

        public void OnEnter()
        {
            _animator.SetBool(Constants.IsWorkingHash, true);
            _animator.SetTrigger(Constants.Gathering);
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