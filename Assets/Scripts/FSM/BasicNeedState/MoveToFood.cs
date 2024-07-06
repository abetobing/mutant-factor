#region

using Brains;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace FSM.BasicNeedState
{
    public class MoveToFood : IState
    {
        private BasicNeeds _thePerson;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;

        private Vector3 _lastPosition = Vector3.zero;
        public float TimeStuck;
        public MoveToFood(BasicNeeds basicNeeds, NavMeshAgent navMeshAgent, Animator animator)
        {
            _thePerson = basicNeeds;
            _navMeshAgent = navMeshAgent;
            _animator = animator;
        }

        public string String()
        {
            return "moving to food source";
        }

        public void Tick()
        {
            if (Vector3.Distance(_thePerson.transform.position, _lastPosition) <= 0f)
                TimeStuck += Time.deltaTime;

            _lastPosition = _thePerson.transform.position;
        }

        public void OnEnter()
        {
            TimeStuck = 0f;
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_thePerson.FoodTarget.transform.position);
            _animator.SetBool(Constants.IsWalkingHash, true);
        }

        public void OnExit()
        {
            _navMeshAgent.enabled = false;
            _animator.SetBool(Constants.IsWalkingHash, false);
        }
    }
}