using Brains;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace FSM
{
    /// <summary>
    /// General state to make the object roam around
    /// </summary>
    public class RandomWalk : IState
    {
        private GameObject _gameObject;
        private NavMeshAgent _navMeshAgent;
        private readonly float _mRange = 5f;

        public RandomWalk(GameObject gameObject)
        {
            _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            var combatSystem = gameObject.GetComponent<CombatSystem>();
            if (combatSystem != null)
                _mRange = combatSystem.attackRange;
        }

        public string String() => "roaming around";

        public void Tick()
        {
            if (_navMeshAgent.pathPending || !_navMeshAgent.isOnNavMesh || _navMeshAgent.remainingDistance > 0.1f)
                return;

            _navMeshAgent.destination = _mRange * Random.insideUnitCircle;
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}