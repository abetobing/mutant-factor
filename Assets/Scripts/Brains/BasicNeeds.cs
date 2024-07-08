#region

using System;
using Entities;
using FSM;
using FSM.BasicNeedState;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace Brains
{
    public class BasicNeeds : MonoBehaviour
    {
        private float _health = 100f;
        private float _hunger = 100f;
        private float _thirst = 100f;
        private float _stamina = 100f;

        public float HealthLevel => _health;
        public float HungerLevel => _hunger;
        public float ThirstLevel => _thirst;
        public float StaminaLevel => _stamina;

        [SerializeField] private float _hungerFallRate = 0.5f; // hunger fall rate per second

        private StateMachine _stateMachine;

        [HideInInspector] public FoodSource FoodTarget;
        [HideInInspector] public bool HasEnteredHungryState;
        [HideInInspector] public string CurrentStateString => _stateMachine.CurrentActivity();

        private void Awake()
        {
            var navMeshAgent = GetComponent<NavMeshAgent>();
            var animator = GetComponent<Animator>();

            _stateMachine = new StateMachine();

            var healthy = new Healthy(this);
            var hungry = new Hungry(this, animator);
            var searchFood = new SearchFood(this);
            var moveToFood = new MoveToFood(this, navMeshAgent, animator);
            var eatingFood = new EatingFood(this, animator);

            At(hungry, searchFood, () => HasEnteredHungryState);
            At(searchFood, moveToFood, ThereAreFoodSource());
            At(moveToFood, searchFood, StuckForOverASecond()); // in case nav mesh is stuck
            At(moveToFood, eatingFood, ArrivedAtFoodSource());
            At(eatingFood, searchFood, NoFoodLeft());
            At(eatingFood, healthy, () => _hunger >= 99.0f);

            // move to hungry state from any state
            Any(hungry, () => _hunger <= 20f && !HasEnteredHungryState);
            Any(healthy, NoFoodLeftButNotHungryAnymore());

            Func<bool> ThereAreFoodSource() => () => FoodTarget != null;
            Func<bool> ArrivedAtFoodSource() => () => navMeshAgent.remainingDistance <= 1f;
            Func<bool> StuckForOverASecond() => () => moveToFood.TimeStuck > 1f;
            Func<bool> NoFoodLeft() => () => FoodTarget.IsDepleted;

            Func<bool> NoFoodLeftButNotHungryAnymore() =>
                () => (FoodTarget == null || FoodTarget.IsDepleted) && _hunger > 30f;

            _stateMachine.SetState(healthy);


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

            // hunger will fall at a given rate
            _hunger -= Time.deltaTime * _hungerFallRate;
            _hunger = Mathf.Clamp(
                _hunger,
                0f,
                Constants.DefaultMaxHunger
            );
        }

        public void EatFood()
        {
            if (FoodTarget.Take()) _hunger++;
            _hunger = Mathf.Clamp(_hunger, 0f, Constants.DefaultMaxHunger);
        }

        public void TakingDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0f)
                _health = 0f;
        }

        public void Dead()
        {
            Destroy(gameObject, 2f);
        }
    }
}