#region

using System;
using System.Collections;
using DefaultNamespace;
using FSM;
using FSM.Combat;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace Brains
{
    public class CombatSystem : MonoBehaviour
    {
        #region Stats (should be move to scriptable object)

        public float radius;
        [Range(0, 360)] public float angle;

        public float attackRange = 3f;
        [Range(1f, 100f)] public float attackSpeed = 1f;
        public float baseDamage = 5f;

        #endregion


        public LayerMask targetMask;
        public LayerMask obstructionMask;

        // [HideInInspector] 
        public Transform target;
        public Transform attackedBy;

        // [HideInInspector] 
        public bool canSeeTarget;
        public bool canAttackTarget;

        private StateMachine _stateMachine;
        private Metabolism _metabolism;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            StartCoroutine(FOVRoutine());
        }

        private void OnEnable()
        {
            _metabolism = GetComponent<Metabolism>();
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _stateMachine = new StateMachine();


            var idle = new Idle(this, _animator, _navMeshAgent);
            var moveToTarget = new MoveToTarget(this, _animator, _navMeshAgent);
            var attack = new Attack(this, _animator, _navMeshAgent);
            var dying = new Dying(this, _animator, _navMeshAgent);
            var respondToAttack = new RespondToAttack(this, _animator, _navMeshAgent);

            At(idle, moveToTarget, () => canSeeTarget);
            At(idle, respondToAttack, BeingAttacked());
            At(respondToAttack, moveToTarget, () => canSeeTarget);
            At(moveToTarget, attack, () => canAttackTarget);
            At(attack, idle, () => !canAttackTarget);
            Any(idle, () => !canSeeTarget && attackedBy == null && _metabolism.IsAlive);
            Any(dying, () => !_metabolism.IsAlive);

            _stateMachine.SetState(idle);

            Func<bool> BeingAttacked() => () => attackedBy != null && !canSeeTarget;

            void At(IState from, IState to, Func<bool> condition)
            {
                _stateMachine.AddTransition(from, to, condition);
            }

            void Any(IState to, Func<bool> condition)
            {
                _stateMachine.AddAnyTransition(to, condition);
            }
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        private IEnumerator FOVRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(0.2f);

            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }

        private void FieldOfViewCheck()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

            if (rangeChecks.Length != 0)
            {
                target = rangeChecks[0].transform;
                if (target.GetComponent<Metabolism>() != null &&
                    !target.GetComponent<Metabolism>().IsAlive)
                {
                    canSeeTarget = canAttackTarget = false;
                    return;
                }

                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                        canSeeTarget = true;
                    else
                        canSeeTarget = false;
                }
                else
                    canSeeTarget = false;
            }
            else if (canSeeTarget)
                canSeeTarget = false;


            if (target != null)
            {
                var metabolism = target.GetComponent<Metabolism>();
                canAttackTarget = metabolism.IsAlive &&
                                  canSeeTarget &&
                                  (Vector3.Distance(transform.position, target.position) <= attackRange);
            }

            if (!canSeeTarget)
            {
                target = null;
                canAttackTarget = false;
            }
        }


        public void PerformAttack()
        {
            if (target != null)
            {
                var targetMetabolism = target.GetComponent<Metabolism>();
                targetMetabolism?.TakingDamage(baseDamage, gameObject);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (target != null)
                Debug.DrawLine(transform.position, target.position, Color.magenta);
        }
    }
}