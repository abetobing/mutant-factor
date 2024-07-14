#region

using Brains;
using Entities;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace FSM.MinerState
{
    internal class ReturnToStockpile : IState
    {
        private readonly Miner _miner;
        private readonly ICharacterMovement _characterMovement;
        private readonly Animator _animator;

        private Vector3 _destination;
        private Vector3 _lastPosition = Vector3.zero;

        public float TimeStuck;

        public ReturnToStockpile(Miner miner)
        {
            _miner = miner;
            _characterMovement = miner.GetComponent<ICharacterMovement>();
            _animator = miner.GetComponent<Animator>();
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
            _miner.StockPile = Object.FindObjectOfType<StockPile>();
            _destination = _miner.StockPile.transform.position;
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