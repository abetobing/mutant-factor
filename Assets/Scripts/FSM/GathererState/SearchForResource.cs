﻿#region

using System.Linq;
using Brains;
using Entities;
using UnityEngine;

#endregion

namespace FSM.GathererState
{
    public class SearchForResource : IState
    {
        private readonly Gatherer _gatherer;

        public SearchForResource(Gatherer gatherer)
        {
            _gatherer = gatherer;
        }

        public string String()
        {
            return "searching for resource";
        }

        public void Tick()
        {
            _gatherer.Target = ChooseOneOfTheNearestResources(5);
        }

        private GatherableResource ChooseOneOfTheNearestResources(int pickFromNearest)
        {
            return Object.FindObjectsOfType<GatherableResource>()
                .OrderBy(t=> Vector3.Distance(_gatherer.transform.position, t.transform.position))
                .Where(t=> t.IsDepleted == false)
                .Take(pickFromNearest)
                .OrderBy(t => Random.Range(0, int.MaxValue))
                .FirstOrDefault();
        }

        public void OnEnter()
        {
        }
        public void OnExit() { }
    }
}