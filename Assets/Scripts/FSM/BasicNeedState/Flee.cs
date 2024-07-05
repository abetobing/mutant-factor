#region

using Brains;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace FSM.BasicNeedState
{
    public class Flee : IState
    {
        private readonly BasicNeeds _entity; // the person or the creature
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private readonly ParticleSystem _particleSystem;

        private float _initialSpeed;
        private const float FLEE_SPEED = 6F;
        private const float FLEE_DISTANCE = 5F;

        public Flee(BasicNeeds entity, NavMeshAgent navMeshAgent, Animator animator, ParticleSystem particleSystem)
        {
            _entity = entity;
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _particleSystem = particleSystem;
        }

        public void OnEnter()
        {
            Debug.LogFormat("{0} is fleeing", _entity.gameObject.name);
            _navMeshAgent.enabled = true;
            _animator.SetBool(Constants.IsRunningHash, true);
            _initialSpeed = _navMeshAgent.speed;
            _navMeshAgent.speed = FLEE_SPEED;
            _particleSystem.Play();
        }

        public void Tick()
        {
            if (_navMeshAgent.remainingDistance < 1f)
            {
                var away = GetRandomPoint();
                _navMeshAgent.SetDestination(away);
            }
        }

        private Vector3 GetRandomPoint()
        {
            // TODO: get position of the enemy to avoid
            var enemyPosition = Vector3.zero;

            var directionFromBeast = _entity.transform.position - enemyPosition;
            directionFromBeast.Normalize();

            var endPoint = _entity.transform.position + (directionFromBeast * FLEE_DISTANCE);
            if (NavMesh.SamplePosition(endPoint, out var hit, 10f, NavMesh.AllAreas))
            {
                return hit.position;
            }
            return _entity.transform.position;
        }

        public void OnExit()
        {
            _navMeshAgent.speed = _initialSpeed;
            _navMeshAgent.enabled = false;
            _animator.SetBool(Constants.IsRunningHash, false);
        }
    }
}