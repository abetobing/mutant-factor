#region

using Brains;

#endregion

namespace Entities
{
    public class FoodSource : BaseResource
    {
        private void Awake() => displayName = "Food resource";

        public void LateUpdate()
        {
            if (IsDepleted)
                Destroy(gameObject, 0.5f);
        }

        public void OnDestroy()
        {
        }
    }
}