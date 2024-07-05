#region

using UnityEngine;

#endregion

namespace Entities
{
    public class FoodSource : MonoBehaviour
    {
        
        [SerializeField] private int _initialAvailable = 1000;
    
        public int Available { get; private set; }
        public bool IsDepleted => Available <= 0;

        private void OnEnable()
        {
            Available = _initialAvailable;
        }
        
        public bool Take()
        {
            if (Available <= 0)
                return false;
            Available--;

            return true;
        }
    }
}