#region

using System.Linq;
using Brains;
using Entities;
using UnityEngine;

#endregion

namespace FSM.BasicNeedState
{
    public class SearchFood : IState
    {
        private readonly BasicNeeds _thePerson;

        public SearchFood(BasicNeeds basicNeeds)
        {
            _thePerson = basicNeeds;
        }

        public string String()
        {
            return "searching for food";
        }

        public void Tick()
        {
            _thePerson.FoodTarget = ChooseOneOfTheNearestResources(3);
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }

        private FoodSource ChooseOneOfTheNearestResources(int pickFromNearest)
        {
            return Object.FindObjectsOfType<FoodSource>()
                .OrderBy(t=> Vector3.Distance(_thePerson.transform.position, t.transform.position))
                .Where(t=> t.IsDepleted == false)
                .Take(pickFromNearest)
                .OrderBy(t => Random.Range(0, int.MaxValue))
                .FirstOrDefault();
        }
    }
}