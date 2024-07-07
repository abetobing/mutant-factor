#region

using Brains;
using Entities;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace FSM.MinerState
{
    internal class ReturnToStockpile : IState
    {
        private readonly Miner _miner;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private Vector3 _destination;

        public ReturnToStockpile(Miner miner, NavMeshAgent navMeshAgent, Animator animator)
        {
            _miner = miner;
            _navMeshAgent = navMeshAgent;
            _animator = animator;
        }

        public string String()
        {
            return "returning to stockpile";
        }

        public void Tick()
        {
            Debug.DrawRay(_destination, Vector3.up, Color.green, 1.0f); //so you can see with gizmos
        }

        public void OnEnter()
        {
            _miner.StockPile = Object.FindObjectOfType<StockPile>();
            _destination = _miner.StockPile.transform.position;
            _navMeshAgent.enabled = true;
            if (NavMesh.FindClosestEdge(_destination, out var hit, NavMesh.AllAreas))
            {
                _destination = hit.position;
                Debug.DrawRay(_destination, Vector3.up, Color.red);
            }

            // if (NavMesh.SamplePosition(_destination, out var hit, 2.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
            // { 
            //     _destination = hit.position;
            //     Debug.DrawRay(_destination, Vector3.up, Color.red);
            // }
            _navMeshAgent.SetDestination(_destination);
            _animator.SetBool(Constants.IsWalkingHash, true);
        }

        public void OnExit()
        {
            _navMeshAgent.enabled = false;
            _animator.SetBool(Constants.IsWalkingHash, false);
        }
    }
}