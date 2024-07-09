using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class MouseClickMovementTest : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        RaycastHit raycastHit = new RaycastHit();

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out raycastHit))
                {
                    _navMeshAgent?.SetDestination(raycastHit.transform.position);
                }
            }
        }
    }
}