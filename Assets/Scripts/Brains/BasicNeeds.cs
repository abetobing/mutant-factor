#region

using System;
using DefaultNamespace;
using Entities;
using FSM;
using FSM.BasicNeedState;
using UnityEngine;

#endregion

namespace Brains
{
    public class BasicNeeds : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private Metabolism _metabolism;

        [HideInInspector] public FoodSource FoodTarget;
        [HideInInspector] public bool HasEnteredHungryState;
        [HideInInspector] public string CurrentStateString => _stateMachine.CurrentActivity();

        private void Awake()
        {
            var characterMovement = GetComponent<ICharacterMovement>();

            _metabolism = GetComponent<Metabolism>();
            _stateMachine = new StateMachine();

            var healthy = new Healthy(this);
            var hungry = new Hungry(this);
            var searchFood = new SearchFood(this);
            var moveToFood = new MoveToFood(this);
            var eatingFood = new EatingFood(this);

            At(hungry, searchFood, () => HasEnteredHungryState);
            At(searchFood, moveToFood, ThereAreFoodSource());
            At(moveToFood, searchFood, StuckForOverASecond()); // in case nav mesh is stuck
            At(moveToFood, eatingFood, ArrivedAtFoodSource());
            At(eatingFood, searchFood, NoFoodLeft());
            At(eatingFood, healthy, () => _metabolism.hunger >= 99.0f);

            // move to hungry state from any state
            Any(hungry, () => _metabolism.hunger <= 20f && !HasEnteredHungryState);
            Any(healthy, NoFoodLeftButNotHungryAnymore());

            Func<bool> ThereAreFoodSource() => () => FoodTarget != null;
            Func<bool> ArrivedAtFoodSource() => () => characterMovement.HasArrived();
            Func<bool> StuckForOverASecond() => () => moveToFood.TimeStuck > 1f;
            Func<bool> NoFoodLeft() => () => FoodTarget.IsDepleted;

            Func<bool> NoFoodLeftButNotHungryAnymore() =>
                () => (FoodTarget == null || FoodTarget.IsDepleted) && _metabolism.hunger > 30f;

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
        }

        public void EatFood()
        {
            if (FoodTarget is IHarvestable food)
                if (food.Take(1))
                    _metabolism.hunger++;
            _metabolism.hunger = Mathf.Clamp(_metabolism.hunger, 0f, Constants.DefaultMaxHunger);
        }

        public void Dead()
        {
            Debug.LogFormat("destroying {0}", gameObject.name);
            Destroy(gameObject, 2f);
        }
    }
}