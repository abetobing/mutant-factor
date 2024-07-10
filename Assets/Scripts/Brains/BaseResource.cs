using UnityEngine;

namespace Brains
{
    public abstract class BaseResource : MonoBehaviour
    {
        public string displayName = "Resource";
        public int available = 100;
        public virtual bool IsDepleted => available <= 0;

        // public bool Take(int quantity)
        // {
        //     if (available <= 0)
        //     {
        //         return false;
        //     }
        //
        //     available -= quantity;
        //     return true;
        // }
    }
}