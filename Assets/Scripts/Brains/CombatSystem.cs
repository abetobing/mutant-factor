#region

using System;
using System.Collections;
using FSM;
using FSM.Combat;
using UnityEngine;

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

        public Transform target;
        public Transform attackedBy;

        public bool canSeeTarget;
        public bool canAttackTarget;

        private StateMachine _stateMachine;
        private Metabolism _metabolism;

        private void Awake()
        {
            StartCoroutine(FOVRoutine());
        }

        private void OnEnable()
        {
            _metabolism = GetComponent<Metabolism>();

            _stateMachine = new StateMachine();


            var idle = new Idle(this);
            var moveToTarget = new MoveToTarget(this);
            var attack = new Attack(this);
            var dying = new Dying(this);
            var respondToAttack = new RespondToAttack(this);

            At(idle, moveToTarget, () => canSeeTarget);
            // At(idle, respondToAttack, () => attackedBy != null);
            // At(respondToAttack, moveToTarget, () => canSeeTarget && !canAttackTarget);
            // At(respondToAttack, attack, () => canAttackTarget);
            // At(respondToAttack, idle, () => !canSeeTarget && attackedBy == null);
            At(moveToTarget, attack, () => canAttackTarget);
            // At(moveToTarget, respondToAttack, () => !canAttackTarget && attackedBy != null);
            At(attack, idle, () => !canAttackTarget);
            Any(dying, () => !_metabolism.IsAlive);
            Any(idle, () => !canSeeTarget && attackedBy == null);

            _stateMachine.SetState(idle);

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
            WaitForSeconds wait = new WaitForSeconds(0.1f);

            while (true)
            {
                yield return wait;
                ScanTarget();
            }
        }


        private void ScanTarget()
        {
            // when no target available, do FOV scan check
            if (target == null)
                target = FieldOfViewCheck();
            // FOV scan result is zero, reset the variables
            if (target == null)
            {
                canSeeTarget = false;
                canAttackTarget = false;
                return;
            }

            // target is available, do some further check
            canSeeTarget = CheckIfCanSeeTarget();
            canAttackTarget = canSeeTarget && CheckIfTargetInsideAttackRange();
            CheckTargetIsDead();

            // cant see the target, reset the variable
            if (!canSeeTarget)
                target = null;
        }

        private Transform FieldOfViewCheck()
        {
            var targetCandidates = new Collider[1];
            var numberOfTargetAround =
                Physics.OverlapSphereNonAlloc(transform.position, radius, targetCandidates, targetMask);

            if (numberOfTargetAround != 0)
                return targetCandidates[0].transform;
            return null;
        }

        // pretty sure we cant attack dead target
        private void CheckTargetIsDead()
        {
            if (target == null)
                return;
            if (target.GetComponent<Metabolism>() == null)
                return;
            if (target.GetComponent<Metabolism>().IsAlive)
                return;
            target = null;
            canSeeTarget = false;
            canAttackTarget = false;
        }


        /// <summary>
        /// this will check if target is visible within range
        /// and vision is not obstructed by wall or any object
        /// </summary>
        /// <returns>true or false</returns>
        private bool CheckIfCanSeeTarget()
        {
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                // float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, radius, obstructionMask))
                    return true;
                return false;
            }

            return false;
        }

        /// <summary>
        /// check if target is within radius of attack range
        /// </summary>
        private bool CheckIfTargetInsideAttackRange()
        {
            if (target == null)
                return false;
            return Vector3.Distance(transform.position, target.position) <= attackRange;
        }


        public void PerformAttack()
        {
            if (target != null)
            {
                // transform.LookAt(target);
                var targetMetabolism = target.GetComponent<Metabolism>();
                targetMetabolism?.TakingDamage(baseDamage, gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            if (canSeeTarget && !canAttackTarget)
                Debug.DrawLine(transform.position, target.position, Color.yellow);
            if (canSeeTarget && canAttackTarget)
                Debug.DrawLine(transform.position, target.position, Color.red);

            if (attackedBy != null)
            {
                var direction = (attackedBy.transform.position - transform.position).normalized * radius;
                // Debug.DrawRay(transform.position, direction, Color.white);
            }
        }

        public string ActivityText()
        {
            return _stateMachine.CurrentActivity();
        }
    }
}