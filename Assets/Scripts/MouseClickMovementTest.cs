using UnityEngine;
using UnityEngine.AI;

public class MouseClickMovementTest : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    RaycastHit raycastHit = new RaycastHit();

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out raycastHit))
            {
                Debug.DrawLine(ray.origin, raycastHit.point, Color.white);
                _navMeshAgent?.SetDestination(raycastHit.point);
            }
        }
    }

    private void OnDrawGizmos()
    {
    }
}