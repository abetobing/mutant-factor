#region

using System;
using Entities;
using FSM;
using FSM.GathererState;
using UnityEngine;

#endregion

namespace Brains
{
    public class Gatherer : BaseProfession, IHarvestable
    {
        public event Action<int> OnGatheredChanged;

        // gathered resource
        public int TotalOwned { get; set; }

        // how many item harvested per shot
        public int HarvestPerHit { get; set; } = 5;

        [SerializeField] private int _maxCarried = 20;

        public GatherableResource Target;
        public StockPile StockPile;

        private void Awake() => Profession = "Gatherer";

        private void OnEnable()
        {
            var characterMovement = GetComponent<ICharacterMovement>();

            _stateMachine = new StateMachine();

            var search = new SearchForResource(this);
            var moveToSelected = new MoveToSelectedResource(this);
            var harvest = new HarvestResource(this);
            var returnToStockpile = new ReturnToStockpile(this);
            var placeResourcesInStockpile = new PlaceResourcesInStockpile(this);

            At(search, moveToSelected, HasTargetAndCanCarryMore());
            At(search, returnToStockpile, InventoryFull());
            At(moveToSelected, search, StuckForOverASecond());
            At(moveToSelected, harvest, ReachedResource());
            At(harvest, search, TargetIsDepletedAndICanCarryMore());
            At(harvest, returnToStockpile, InventoryFull());
            At(returnToStockpile, placeResourcesInStockpile, ReachedStockpile());
            At(placeResourcesInStockpile, search, () => TotalOwned == 0);

            _stateMachine.SetState(search);

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

            Func<bool> HasTargetAndCanCarryMore() => () => Target != null &&
                                                           TotalOwned < _maxCarried;

            Func<bool> StuckForOverASecond() => () => moveToSelected.TimeStuck > 1f;

            Func<bool> ReachedResource() => () => Target != null &&
                                                  characterMovement.HasArrived();

            Func<bool> TargetIsDepletedAndICanCarryMore() =>
                () => (Target == null || Target.IsDepleted) && !InventoryFull().Invoke();

            Func<bool> InventoryFull() => () => TotalOwned >= _maxCarried;

            Func<bool> ReachedStockpile() => () => StockPile != null &&
                                                   characterMovement.HasArrived();
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        public override ScriptableObject Stats() => null;

        public void Enable()
        {
            this.enabled = true;
        }

        public void Disable() => this.enabled = false;

        void IHarvestable.TakeFromTarget()
        {
            if (Target is not IHarvestable t)
                return;
            if (t.IsDepleted)
                return;
            TotalOwned += HarvestPerHit;
            t.TotalOwned -= HarvestPerHit;
        }
    }
}