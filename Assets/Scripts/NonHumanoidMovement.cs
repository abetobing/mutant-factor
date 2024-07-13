using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class NonHumanoidMovement : MonoBehaviour, ICharacterMovement
    {
        private NavMeshAgent _agent;
        private Animator _animator;

        private void OnValidate()
        {
            if (!_agent) _agent = GetComponent<NavMeshAgent>();
            if (!_animator) _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetFloat(Constants.SpeedHash, _agent.velocity.magnitude);
        }

        public void MoveTo(Vector3 destination)
        {
            _agent.SetDestination(destination);
        }

        public void RotateTo(Vector3 direction)
        {
        }

        public void Stop()
        {
            _agent.ResetPath();
        }
    }
}