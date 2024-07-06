#region

using UnityEngine;
using UnityEngine.AI;

#endregion

namespace Entities
{
    public class GatherableResource : MonoBehaviour
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

        private void UpdateSize()
        {
            float scale = (float)Available / _initialAvailable;
            if (scale > 0 && scale < 1f)
            {
                var vectorScale = Vector3.one * scale;
                transform.localScale = vectorScale;
            }
            else if (scale <= 0)
            {
                gameObject.SetActive(false);
            }
        
        }

        [ContextMenu("Snap")]
        private void Snap()
        {
            if (NavMesh.SamplePosition(transform.position, out var hit, 5f, NavMesh.AllAreas))
            {
                transform.position = hit.position;
            }
        }

        public void SetAvailable(int amount) => Available = amount;
    }
}