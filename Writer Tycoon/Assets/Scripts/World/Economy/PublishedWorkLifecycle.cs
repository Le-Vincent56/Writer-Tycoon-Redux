using System.Collections.Generic;
using UnityEngine;
using GhostWriter.WorkCreation.Publication;

namespace GhostWriter.World.Economy
{
    public class PublishedWorkLifecycle
    {
        [SerializeField] private PublishedWork work;
        [SerializeField] private List<int> weeklySales;
        [SerializeField] private int currentWeek;

        public PublishedWork Work { get => work; }
        public List<int> WeeklySales { get => weeklySales; }
        public int CurrentWeek { get => currentWeek; }

        public PublishedWorkLifecycle(PublishedWork work, List<int> weeklySales)
        {
            this.work = work;
            this.weeklySales = weeklySales;
            currentWeek = 0;
        }

        /// <summary>
        /// Get the sales for the current week
        /// </summary>
        public int GetSalesForCurrentWeek()
        {
            // Check if the current week is less than the weekly sales count
            if(currentWeek < weeklySales.Count)
                // If so, return the sales for the current week
                return weeklySales[currentWeek];

            return 0;
        }

        /// <summary>
        /// Advance to the next week in the lifecycle
        /// </summary>
        public void AdvanceWeek() => currentWeek++;

        /// <summary>
        /// Determine if the lifecycle has ended
        /// </summary>
        public bool IsLifecycleEnded() => currentWeek >= weeklySales.Count;
    }
}