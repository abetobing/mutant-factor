using UnityEngine;
using UnityEngine.AI;

public class HumanoidMovement : MonoBehaviour, ICharacterMovement
{
    private NavMeshAgent _agent;
    private Animator _animator;

    private void OnValidate()
    {
        if (!_agent) _agent = GetComponent<NavMeshAgent>();
        if (!_animator) _animator = GetComponent<Animator>();

        _agent.speed = 0;
        _agent.acceleration = 0;
        _agent.angularSpeed = 0;
        _agent.updateRotation = false;
    }

    private void Update()
    {
        if (_agent.hasPath)
        {
            if (HasArrived())
                Stop();

            var dir = (_agent.steeringTarget - transform.position).normalized;
            var animDir = transform.InverseTransformDirection(dir);
            var isFacingMoveDirection = Vector3.Dot(dir, transform.forward) > 0.5f;
            _animator.SetFloat(Constants.SpeedHash, isFacingMoveDirection ? animDir.z : 0, .1f, Time.deltaTime);
            // _animator.SetFloat(Constants.VerticalHash, isFacingMoveDirection ? animDir.x : 0f, .5f, Time.deltaTime);
            // _animator.SetFloat(Constants.HorizontalHash, isFacingMoveDirection ? animDir.z : 0f, .5f, Time.deltaTime);

            _animator.SetFloat(Constants.TurnHash, animDir.x);
            RotateTo(dir);
        }
        else
        {
            _animator.SetFloat(Constants.SpeedHash, 0f, 0.25f, Time.deltaTime);
            _animator.SetFloat(Constants.TurnHash, 0f, 0.25f, Time.deltaTime);
        }
    }

    public void MoveTo(Vector3 destination)
    {
        _agent.SetDestination(destination);
    }

    public void RotateTo(Vector3 direction)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction),
            360 * Time.deltaTime);
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