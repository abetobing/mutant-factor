#region

using Brains;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace FSM.MinerState
{
    internal class MoveToSelectedResource : IState
    {
        private readonly Miner _miner;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;

        private Vector3 _lastPosition = Vector3.zero;

        public float TimeStuck;

        public MoveToSelectedResource(Miner miner, NavMeshAgent navMeshAgent, Animator animator)
        {
            _miner = miner;
            _navMeshAgent = navMeshAgent;
            _animator = animator;
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
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_miner.Target.transform.position);
            _animator.SetBool(Constants.IsWalkingHash, true);
        }

        public void OnExit()
        {
            _navMeshAgent.enabled = false;
            _animator.SetBool(Constants.IsWalkingHash, false);
        }
    }
}