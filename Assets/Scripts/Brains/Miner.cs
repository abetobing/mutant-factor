#region

using System;
using Entities;
using FSM;
using FSM.MinerState;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace Brains
{
    public class Miner : BaseProfession
    {
        public event Action<int> OnGatheredChanged;

        [SerializeField] private int _maxCarried = 20;

        private int _gathered;

        public GatherableResource Target { get; set; }
        public StockPile StockPile { get; set; }

        private void Awake() => Profession = "Gatherer";

        private void OnEnable()
        {
            var navMeshAgent = GetComponent<NavMeshAgent>();
            var animator = GetComponent<Animator>();

            _stateMachine = new StateMachine();

            var search = new SearchForResource(this);
            var moveToSelected = new MoveToSelectedResource(this, navMeshAgent, animator);
            var harvest = new HarvestResource(this, navMeshAgent, animator);
            var returnToStockpile = new ReturnToStockpile(this, navMeshAgent, animator);
            var placeResourcesInStockpile = new PlaceResourcesInStockpile(this);

            At(search, moveToSelected, HasTargetAndCanCarryMore());
            At(search, returnToStockpile, InventoryFull());
            At(moveToSelected, search, StuckForOverASecond());
            At(moveToSelected, harvest, ReachedResource());
            At(harvest, search, TargetIsDepletedAndICanCarryMore());
            At(harvest, returnToStockpile, InventoryFull());
            At(returnToStockpile, placeResourcesInStockpile, ReachedStockpile());
            At(placeResourcesInStockpile, search, () => _gathered == 0);

            _stateMachine.SetState(search);

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

            Func<bool> HasTargetAndCanCarryMore() => () => Target != null &&
                                                           _gathered < _maxCarried;

            Func<bool> StuckForOverASecond() => () => moveToSelected.TimeStuck > 1f;

            Func<bool> ReachedResource() => () => Target != null &&
                                                  Vector3.Distance(transform.position, Target.transform.position) <
                                                  Constants.NavMeshDistanceTolerance;

            Func<bool> TargetIsDepletedAndICanCarryMore() =>
                () => (Target == null || Target.IsDepleted) && !InventoryFull().Invoke();

            Func<bool> InventoryFull() => () => _gathered >= _maxCarried;

            Func<bool> ReachedStockpile() => () => StockPile != null &&
                                                   Vector3.Distance(transform.position, StockPile.transform.position) <=
                                                   Constants.NavMeshDistanceTolerance;
        }

        private void Update() => _stateMachine.Tick();

        public override ScriptableObject Stats() => null;

        public void Enable()
        {
            this.enabled = true;
            // _stateMachine.SetState(new SearchForResource(this));
        }

        public void Disable() => this.enabled = false;

        public void TakeFromTarget()
        {
            if (Target.Take())
            {
                _gathered++;
                OnGatheredChanged?.Invoke(_gathered);
            }
        }

        public bool Take()
        {
            if (_gathered <= 0)
                return false;

            _gathered--;
            OnGatheredChanged?.Invoke(_gathered);
            return true;
        }
    }
}