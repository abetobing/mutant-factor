using UnityEngine;

namespace Entities
{
    public class FoodSource : MonoBehaviour
    {
        
        [SerializeField] private int _totalAvailable = 1000;
    
        private int _available;
        public bool IsDepleted => _available <= 0;

        private void OnEnable()
        {
            _available = _totalAvailable;
        }
        
        public bool Take()
        {
            if (_available <= 0)
                return false;
            _available--;

            return true;
        }
    }
}