#region

using Brains;
using DefaultNamespace;
using UnityEngine;

#endregion

namespace FSM.MinerState
{
    internal class MoveToSelectedResource : IState
    {
        private Vector3 _lastPosition = Vector3.zero;
        public float TimeStuck;

        private readonly Miner _miner;
        private readonly ICharacterMovement _characterMovement;
        private readonly Animator _animator;

        public MoveToSelectedResource(Miner miner)
        {
            _miner = miner;
            _characterMovement = miner.GetComponent<ICharacterMovement>();
            _animator = miner.GetComponent<Animator>();
        }

        public string String()
        {
            return "going to target resource";
        }

        public void Tick()
        {
            if (Vector3.Distance(_miner.transform.position, _lastPosition) <= 0f)
                TimeStuck += Time.deltaTime;

            _lastPosition = _miner.transform.position;
        }

        public void OnEnter()
        {
            TimeStuck = 0f;
            _characterMovement.MoveTo(_miner.Target.transform.position);
        }

        public void OnExit()
        {
            _characterMovement.Stop();
        }
    }
}