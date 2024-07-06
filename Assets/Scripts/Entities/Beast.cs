#region

using UnityEngine;
using UnityEngine.AI;

#endregion

namespace Entities
{
    public class Beast : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        public float range = 10f; //radius of sphere
        private Vector3 _centrePoint; //centre of the area the agent wants to move around in

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _centrePoint = _navMeshAgent.transform.position;
        }
    
        private void Update()
        {
            if(_navMeshAgent.isPathStale || (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)) //done with path
            {
                Vector3 point = RandomPoint(_centrePoint, range);
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                _navMeshAgent.SetDestination(point);
            }
        }
    
        private Vector3 RandomPoint(Vector3 center, float range)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
            if (NavMesh.SamplePosition(randomPoint, out var hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
            { 
                return hit.position;
            }
            return Vector3.zero;
        }
    }
}