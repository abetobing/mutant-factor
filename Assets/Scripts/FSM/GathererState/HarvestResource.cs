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
        private readonly ICharacterMovement _characterMovement;

        public HarvestResource(Gatherer gatherer)
        {
            _gatherer = gatherer;
            _characterMovement = gatherer.GetComponent<ICharacterMovement>();
            _animator = gatherer.GetComponent<Animator>();
        }

        public void Tick()
        {
            if (_gatherer.Target != null)
            {
                // _gatherer.transform.LookAt(_gatherer.Target.transform, _gatherer.transform.up);
            }
        }

        public void OnEnter()
        {
            if (_gatherer.Target != null)
                _gatherer.transform.LookAt(_gatherer.Target.transform, _gatherer.transform.up);
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