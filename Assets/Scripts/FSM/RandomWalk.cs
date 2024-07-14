using Brains;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FSM
{
    /// <summary>
    /// General state to make the object roam around
    /// </summary>
    public class RandomWalk : IState
    {
        private GameObject _gameObject;
        private ICharacterMovement _characterMovement;
        private readonly float _mRange = 5f;

        public RandomWalk(GameObject gameObject)
        {
            _gameObject = gameObject;
            _characterMovement = _gameObject.GetComponent<ICharacterMovement>();
            var combatSystem = _gameObject.GetComponent<CombatSystem>();
            if (combatSystem != null)
                _mRange = combatSystem.radius;
        }

        public string String() => "roaming around";

        public void Tick()
        {
            if (!_characterMovement.HasArrived())
                return;
            var randomVec2 = _mRange * Random.insideUnitCircle;
            var randomPos = _gameObject.transform.position + new Vector3(randomVec2.x, 0, randomVec2.y);
            _characterMovement.MoveTo(randomPos);
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
            _characterMovement.Stop();
        }
    }
}