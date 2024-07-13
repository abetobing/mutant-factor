using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class NonHumanoidMovement : MonoBehaviour, ICharacterMovement
    {
        private NavMeshAgent _agent;
        private Animator _animator;
        private Vector3 _destination;

        private void OnValidate()
        {
            if (!_agent) _agent = GetComponent<NavMeshAgent>();
            if (!_animator) _animator = GetComponent<Animator>();
        }

        public void MoveTo(Vector3 destination)
        {
            _agent.SetDestination(destination);
        }

        public void RotateTo(Vector3 direction)
        {
        }
    }
}