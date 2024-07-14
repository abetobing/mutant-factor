#region

using Brains;
using Entities;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace FSM.GathererState
{
    internal class ReturnToStockpile : IState
    {
        private readonly Gatherer _gatherer;
        private readonly Animator _animator;
        private readonly ICharacterMovement _characterMovement;
        private Vector3 _destination;

        public ReturnToStockpile(Gatherer gatherer)
        {
            _gatherer = gatherer;
            _characterMovement = gatherer.GetComponent<ICharacterMovement>();
            _animator = gatherer.GetComponent<Animator>();
        }

        public string String()
        {
            return "returning to stockpile";
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _gatherer.StockPile = Object.FindObjectOfType<StockPile>();
            _destination = _gatherer.StockPile.transform.position;
            if (NavMesh.SamplePosition(_destination, out var hit, 2.0f,
                    NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
            {
                _destination = hit.position;
            }

            _characterMovement.MoveTo(_destination);
        }

        public void OnExit()
        {
            _characterMovement.Stop();
        }
    }
}