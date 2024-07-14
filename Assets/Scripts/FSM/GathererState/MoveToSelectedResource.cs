#region

using Brains;
using UnityEngine;

#endregion

namespace FSM.GathererState
{
    internal class MoveToSelectedResource : IState
    {
        private readonly Gatherer _gatherer;
        private readonly ICharacterMovement _characterMovement;
        private readonly Animator _animator;

        private Vector3 _lastPosition = Vector3.zero;

        public float TimeStuck;

        public MoveToSelectedResource(Gatherer gatherer)
        {
            _gatherer = gatherer;
            _characterMovement = gatherer.GetComponent<ICharacterMovement>();
            _animator = gatherer.GetComponent<Animator>();
        }

        public string String()
        {
            return "going to target resource";
        }

        public void Tick()
        {
            if (Vector3.Distance(_gatherer.transform.position, _lastPosition) <= 0f)
                TimeStuck += Time.deltaTime;

            _lastPosition = _gatherer.transform.position;
        }

        public void OnEnter()
        {
            TimeStuck = 0f;
            _characterMovement.MoveTo(_gatherer.Target.transform.position);
        }

        public void OnExit()
        {
            _characterMovement.Stop();
        }
    }
}