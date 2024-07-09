using Brains;
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
        private CombatSystem _combatSystem;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _metabolism = GetComponent<Metabolism>();
            _combatSystem = GetComponent<CombatSystem>();
        }

        private void Update()
        {
            // set health parameter
            GetComponent<Animator>().SetFloat(Constants.HealthHash, _metabolism.health);

            // set speed parameter
            var movementSpeed = _navMeshAgent.velocity.magnitude / _navMeshAgent.speed;
            _animator?.SetFloat(Constants.SpeedHash, movementSpeed);

            // set angle parameter
            var movementAngleMagnitude = transform.InverseTransformDirection(_navMeshAgent.desiredVelocity).x;
            _animator?.SetFloat(Constants.AngleHash, movementAngleMagnitude);

            // set attackSpeed parameter
            var animationAttackSpeed = _combatSystem.attackSpeed / 10f;
            _animator?.SetFloat(Constants.AttackSpeedHash, animationAttackSpeed);
        }
    }
}