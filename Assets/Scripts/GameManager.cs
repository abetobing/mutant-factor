#region

using System.Collections;
using System.Linq;
using Entities;
using TMPro;
using UnityEngine;

#endregion

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        public static GameManager Instance => _instance;

        [SerializeField] private TMP_Text txtStats;

        void Awake()
        {
            _instance = this;
            StartCoroutine(DisplayStatistic_TotalResource());
        }

        private IEnumerator DisplayStatistic_TotalResource()
        {
            var wait = new WaitForSeconds(0.2f);
            while (true)
            {
                yield return wait;
                var totalFoodSourceAvailable = FindObjectsOfType<FoodSource>()
                    .Sum(t => t.Available);
                var totalGatherableResourceAvailable = FindObjectsOfType<GatherableResource>()
                    .Sum(t => t.Available);
                txtStats.text = $"Food source: {totalFoodSourceAvailable} / Resource: {totalGatherableResourceAvailable}";
            }
        }

        private void OnDestroy()
        {
            StopCoroutine(DisplayStatistic_TotalResource());
        }
    }
}