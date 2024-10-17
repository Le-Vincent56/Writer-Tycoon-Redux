using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.ServiceLocator;

namespace WriterTycoon.Entities.Tracker
{
    public class CompetitorRecord : MonoBehaviour
    {
        [SerializeField] private HashSet<ICompetitor> competitors;
        private ICompetitor player;

        public ICompetitor Player { get => player; }

        private void Awake()
        {
            // Register to the global service locator
            ServiceLocator.ForSceneOf(this).Register(this);

            // Instantiate the Competitors List
            competitors = new();
        }

        /// <summary>
        /// Record a competitor
        /// </summary>
        public void RecordCompetitor(ICompetitor competitor, bool isPlayer = false)
        {
            // Add the Competitor to the list
            competitors.Add(competitor);

            // Register the player
            if (isPlayer)
                player = competitor;
        }

        /// <summary>
        /// Get the player
        /// </summary>
        public ICompetitor GetPlayer() => player;
    }
}