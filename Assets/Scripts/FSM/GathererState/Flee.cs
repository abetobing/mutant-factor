﻿#region

using Brains;
using Entities;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace FSM.GathererState
{
    public class Flee : IState
    {
        private readonly Gatherer _gatherer;
        private NavMeshAgent _navMeshAgent;
        private readonly EnemyDetector _enemyDetector;
        private Animator _animator;
        private readonly ParticleSystem _particleSystem;

        private float _initialSpeed;
        private const float FLEE_SPEED = 6F;
        private const float FLEE_DISTANCE = 5F;

        public Flee(Gatherer gatherer, NavMeshAgent navMeshAgent, EnemyDetector enemyDetector, Animator animator,
            ParticleSystem particleSystem)
        {
            _gatherer = gatherer;
            _navMeshAgent = navMeshAgent;
            _enemyDetector = enemyDetector;
            _animator = animator;
            _particleSystem = particleSystem;
        }

        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            _animator.SetFloat(Constants.SpeedHash, 2f);
            _initialSpeed = _navMeshAgent.speed;
            _navMeshAgent.speed = FLEE_SPEED;
            _particleSystem.Play();
        }

        public string String()
        {
            return "fleeing";
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
            var directionFromBeast = _gatherer.transform.position - _enemyDetector.GetNearestBeastPosition();
            directionFromBeast.Normalize();

            var endPoint = _gatherer.transform.position + (directionFromBeast * FLEE_DISTANCE);
            if (NavMesh.SamplePosition(endPoint, out var hit, 10f, NavMesh.AllAreas))
            {
                return hit.position;
            }

            return _gatherer.transform.position;
        }

        public void OnExit()
        {
            _navMeshAgent.speed = _initialSpeed;
            _navMeshAgent.enabled = false;
            _animator.SetFloat(Constants.SpeedHash, 1f);
        }
    }
}