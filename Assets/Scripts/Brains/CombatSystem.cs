#region

using System;
using System.Collections;
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
        [Range(1f, 100f)] public float attackSpeed = 2f;
        public float baseDamage = 5f;

        #endregion


        public LayerMask targetMask;
        public LayerMask obstructionMask;

        // [HideInInspector] 
        public Transform target;

        // [HideInInspector] 
        public bool canSeeTarget;
        public bool canAttackTarget;

        private StateMachine _stateMachine;
        private BasicNeeds _basicNeeds;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            StartCoroutine(FOVRoutine());
        }

        private void OnEnable()
        {
            _basicNeeds = GetComponent<BasicNeeds>();
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _stateMachine = new StateMachine();


            var idle = new Idle(this, _animator, _navMeshAgent);
            var moveToTarget = new MoveToTarget(this, _animator, _navMeshAgent);
            var attack = new Attack(this, _animator, _navMeshAgent);
            var dying = new Dying(this, _animator, _navMeshAgent);

            At(idle, moveToTarget, () => canSeeTarget);
            At(idle, moveToTarget, BeingAttacked());
            At(moveToTarget, attack, () => canAttackTarget);
            At(attack, idle, () => !canAttackTarget);
            Any(idle, () => !canSeeTarget);
            Any(dying, () => !_basicNeeds.IsAlive);

            _stateMachine.SetState(idle);

            Func<bool> BeingAttacked() => () =>
            {
                // TODO: are we being attacked?
                return false;
            };

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
            Debug.LogFormat("{0} is {1}", gameObject.name, _stateMachine.CurrentActivity());
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
                if (target.GetComponent<BasicNeeds>() != null &&
                    !target.GetComponent<BasicNeeds>().IsAlive)
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


            // canAttackTarget = target != null &&
            //                   canSeeTarget &&
            //                   (Vector3.Distance(transform.position, target.position) <= attackRange);
            if (target != null)
            {
                var targetBasicNeeds = target.GetComponent<BasicNeeds>();
                canAttackTarget = targetBasicNeeds.IsAlive &&
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
            Debug.Log("performing attack");
            target.GetComponent<BasicNeeds>()?.TakingDamage(baseDamage);
        }

        private void OnDrawGizmosSelected()
        {
            if (target != null)
                Debug.DrawLine(transform.position, target.position, Color.magenta);
        }
    }
}