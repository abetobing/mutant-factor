using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;

namespace Entities
{
    public class AnimatorController : MonoBehaviour
    {
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private Metabolism _metabolism;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _metabolism = GetComponent<Metabolism>();
        }

        private void Update()
        {
            var movementSpeed = _navMeshAgent.velocity.magnitude / _navMeshAgent.speed;
            _animator?.SetFloat(Constants.SpeedHash, movementSpeed);
            GetComponent<Animator>().SetFloat(Constants.HealthHash, _metabolism.health);
        }
    }
}