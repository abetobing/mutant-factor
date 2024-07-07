#region

using Brains;
using Entities;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace FSM.GathererState
{
    internal class ReturnToStockpile : IState
    {
        private readonly Gatherer _gatherer;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private Vector3 _destination;

        public ReturnToStockpile(Gatherer gatherer, NavMeshAgent navMeshAgent, Animator animator)
        {
            _gatherer = gatherer;
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
            _gatherer.StockPile = Object.FindObjectOfType<StockPile>();
            _destination = _gatherer.StockPile.transform.position;
            _navMeshAgent.enabled = true;
            if (NavMesh.SamplePosition(_destination, out var hit, 2.0f,
                    NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
            {
                _destination = hit.position;
            }

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