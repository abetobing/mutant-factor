using System;
using Entities;
using FSM;
using FSM.BasicNeedState;
using UnityEngine;
using UnityEngine.AI;

namespace Brains
{
    public class BasicNeeds : MonoBehaviour
    {
        [SerializeField] private float _health = 100f;
        [SerializeField] private float _hunger = 100f;
        [SerializeField] private float _thirst = 100f;
        [SerializeField] private float _stamina = 100f;

        [SerializeField] private float _hungerFallRate = 2;

        private StateMachine _stateMachine;

        public FoodSource FoodTarget { get; set; }
        public bool IsLookingForFood;
        public bool IsEatingFood;

        public static readonly int IsWalkingHash = Animator.StringToHash("isWalking");
        public static readonly int IsRunningHash = Animator.StringToHash("isRunning");
        public static readonly int IsEating = Animator.StringToHash("isHarvesting");

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
            // var injured = new Injured(this);
            // var thirsty = new Thirsty(this);
            // var tired = new Tired(this);

            At(hungry, searchFood, () => IsLookingForFood);
            At(searchFood, moveToFood, ThereAreFoodSource());
            At(moveToFood, eatingFood, ArrivedAtFoodSource());
            At(eatingFood, healthy, () => _hunger >= 95.0f);

            Any(hungry, () =>
            {
                return _hunger <= 20f && !IsLookingForFood && !IsEatingFood;
            });

            // Any(thirsty, () => thirst <= 20f);
            // Any(tired, () => stamina <= 20f);
            // Any(injured, () => health <= 20f);
            
            Func<bool> ThereAreFoodSource() => () => FoodTarget != null;

            Func<bool> ArrivedAtFoodSource() => () => navMeshAgent.remainingDistance <= 1f;


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
            _hunger -= Time.deltaTime * (_hungerFallRate / 100);
            _hunger = Mathf.Clamp(
                _hunger,
                0f,
                Constants.DefaultMaxHunger
            );

        }

        public void EatFood()
        {
            if (FoodTarget.Take()) _hunger++;
            _hunger = Mathf.Clamp(_hunger, 0f,  Constants.DefaultMaxHunger);
        }
    }
}