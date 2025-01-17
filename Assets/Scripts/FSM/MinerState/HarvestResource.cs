﻿#region

using Brains;
using UnityEngine;

#endregion

namespace FSM.MinerState
{
    internal class HarvestResource : IState
    {
        private readonly Miner _miner;
        private readonly ICharacterMovement _characterMovement;
        private readonly Animator _animator;

        public HarvestResource(Miner miner)
        {
            _miner = miner;
            _characterMovement = miner.GetComponent<ICharacterMovement>();
            _animator = miner.GetComponent<Animator>();
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            if (_miner.Target != null)
                _miner.transform.LookAt(_miner.Target.transform, _miner.transform.up);
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