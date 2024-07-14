using UnityEngine;
using UnityEngine.AI;

public class NonHumanoidMovement : MonoBehaviour, ICharacterMovement
{
    private NavMeshAgent _agent;
    private Animator _animator;

    private void OnValidate()
    {
        if (!_agent) _agent = GetComponent<NavMeshAgent>();
        if (!_animator) _animator = GetComponent<Animator>();
        _agent.updateRotation = false;
    }

    private void Update()
    {
        if (_agent.hasPath)
        {
            Vector3 dir = (_agent.steeringTarget - transform.position).normalized;
            RotateTo(dir);
            var isFacingMoveDirection = Vector3.Dot(dir, transform.forward) > 0f;
            _animator.SetFloat(Constants.SpeedHash, isFacingMoveDirection ? _agent.velocity.magnitude : 0f);
        }
    }

    public void MoveTo(Vector3 destination)
    {
        _agent.SetDestination(destination);
    }

    public void RotateTo(Vector3 direction)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction),
            180 * Time.deltaTime);
    }

    public void Stop()
    {
        _agent.ResetPath();
    }

    public bool HasArrived()
    {
        if (_agent.hasPath)
            return Vector3.Distance(_agent.destination, transform.position) <= 0.2f;
        return true;
    }

    private void OnDrawGizmos()
    {
        if (_agent.hasPath)
        {
            for (var i = 0; i < _agent.path.corners.Length - 1; i++)
            {
                Debug.DrawLine(_agent.path.corners[i], _agent.path.corners[i + 1], Color.black);
            }
        }
    }
}