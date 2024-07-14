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
        public float attackIndex;
        private Animator _animator;

        private void Awake()
        {
            StartCoroutine(FOVRoutine());
        }

        private void OnEnable()
        {
            _metabolism = GetComponent<Metabolism>();
            _animator = GetComponent<Animator>();

            _stateMachine = new StateMachine();


            var idle = new Idle(this);
            var moveToTarget = new MoveToTarget(this);
            var attack = new Attack(this);
            var dying = new Dying(this);
            var respondToAttack = new RespondToAttack(this);

            At(idle, moveToTarget, () => canSeeTarget);
            At(idle, respondToAttack, BeingAttacked());
            At(respondToAttack, moveToTarget, () => canSeeTarget && !canAttackTarget);
            At(respondToAttack, attack, () => canAttackTarget);
            At(respondToAttack, idle, () => !canSeeTarget);
            At(moveToTarget, attack, () => canAttackTarget);
            At(moveToTarget, respondToAttack, () => !canAttackTarget && attackedBy != null);
            At(attack, idle, () => !canAttackTarget);
            Any(dying, () => !_metabolism.IsAlive);
            Any(idle, () => !canSeeTarget && attackedBy == null);

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
            // if (TargetLocked())
            //     return;

            var targetCandidates = new Collider[5];
            var numberOfTargetAround =
                Physics.OverlapSphereNonAlloc(transform.position, radius, targetCandidates, targetMask);

            if (numberOfTargetAround != 0)
            {
                target = targetCandidates[0].transform;
                // if being attacked, set the target to the attacker
                if (attackedBy != null)
                    target = attackedBy;

                canSeeTarget = CheckIfCanSeeTarget();
            }
            else if (canSeeTarget)
                canSeeTarget = false;


            canAttackTarget = CheckIfTargetInsideAttackRange();

            CheckTargetIsDead();

            if (!canSeeTarget)
            {
                target = null;
                canAttackTarget = false;
            }
        }


        // if currently is busy chasing enemy
        private bool TargetLocked()
        {
            CheckTargetIsDead();
            return target != null && canSeeTarget && canAttackTarget;
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
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
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
            if (target == null)
                return;
            var targetMetabolism = target.GetComponent<Metabolism>();
            targetMetabolism?.TakingDamage(baseDamage, gameObject);
            attackIndex++;
            if (attackIndex > 5)
                attackIndex = 0;
        }

        private void OnDrawGizmosSelected()
        {
            if (target != null)
                Debug.DrawLine(transform.position, target.position, Color.magenta);
        }

        public string ActivityText()
        {
            return _stateMachine.CurrentActivity();
        }
    }
}