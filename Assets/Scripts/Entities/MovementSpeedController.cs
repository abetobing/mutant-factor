using UnityEngine;
using UnityEngine.AI;

namespace Entities
{
    public class MovementSpeedController : MonoBehaviour
    {
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            var movementSpeed = _navMeshAgent.velocity.magnitude / _navMeshAgent.speed;
            _animator.SetFloat(Constants.SpeedHash, movementSpeed);
        }
    }
}