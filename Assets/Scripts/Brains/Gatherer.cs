#region

using System;
using Entities;
using FSM;
using FSM.GathererState;
using UnityEngine;
using UnityEngine.AI;

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
            var navMeshAgent = GetComponent<NavMeshAgent>();
            var animator = GetComponent<Animator>();
            var enemyDetector = gameObject.AddComponent<EnemyDetector>();
            var fleeParticleSystem = gameObject.GetComponentInChildren<ParticleSystem>();

            _stateMachine = new StateMachine();

            var search = new SearchForResource(this);
            var moveToSelected = new MoveToSelectedResource(this, navMeshAgent, animator);
            var harvest = new HarvestResource(this, animator);
            var returnToStockpile = new ReturnToStockpile(this, navMeshAgent, animator);
            var placeResourcesInStockpile = new PlaceResourcesInStockpile(this);
            var flee = new Flee(this, navMeshAgent, enemyDetector, animator, fleeParticleSystem);

            At(search, moveToSelected, HasTargetAndCanCarryMore());
            At(search, returnToStockpile, InventoryFull());
            At(moveToSelected, search, StuckForOverASecond());
            At(moveToSelected, harvest, ReachedResource());
            At(harvest, search, TargetIsDepletedAndICanCarryMore());
            At(harvest, returnToStockpile, InventoryFull());
            At(returnToStockpile, placeResourcesInStockpile, ReachedStockpile());
            At(placeResourcesInStockpile, search, () => TotalOwned == 0);

            _stateMachine.AddAnyTransition(flee, () => enemyDetector.EnemyInRange);
            At(flee, search, () => enemyDetector.EnemyInRange == false);

            _stateMachine.SetState(search);

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

            Func<bool> HasTargetAndCanCarryMore() => () => Target != null &&
                                                           TotalOwned < _maxCarried;

            Func<bool> StuckForOverASecond() => () => moveToSelected.TimeStuck > 1f;

            Func<bool> ReachedResource() => () => Target != null &&
                                                  Vector3.Distance(transform.position, Target.transform.position) <
                                                  Constants.NavMeshDistanceTolerance;

            Func<bool> TargetIsDepletedAndICanCarryMore() =>
                () => (Target == null || Target.IsDepleted) && !InventoryFull().Invoke();

            Func<bool> InventoryFull() => () => TotalOwned >= _maxCarried;

            Func<bool> ReachedStockpile() => () => StockPile != null &&
                                                   Vector3.Distance(transform.position, StockPile.transform.position) <=
                                                   Constants.NavMeshDistanceTolerance;
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