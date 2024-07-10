namespace Brains
{
    public interface IHarvestable
    {
        /// <summary>
        /// total item available
        /// </summary>
        public int TotalOwned { get; set; }

        /// <summary>
        /// How many item is taken per shot
        /// </summary>
        public int HarvestPerHit { get; set; }

        /// <summary>
        /// Is resource/posessions is not available anymore
        /// </summary>
        public bool IsDepleted => TotalOwned <= 0;


        /// <summary>
        /// Take item for target for n amount specifiend in `HarvestPerHit`
        /// </summary>
        /// <param name="target"></param>
        public void TakeFromTarget()
        {
            // must override or else do nothing
        }


        /// <summary>
        /// Item is being taken by other for `howMany` amount
        /// </summary>
        /// <param name="howMany"></param>
        /// <returns></returns>
        public bool Take(int howMany)
        {
            if (TotalOwned <= 0)
                return false;

            TotalOwned -= howMany;
            return true;
        }
    }
}