using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using GhostWriter.Patterns.ServiceLocator;
using System.Linq;

namespace GhostWriter.Entities.Tracker
{
    public class CompetitorRecord : SerializedMonoBehaviour
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

        /// <summary>
        /// Get all NPC Competitors
        /// </summary>
        public List<ICompetitor> GetNPCCompetitors()
        {
            return competitors.Where(competitor => competitor != player).ToList();
        }
    }
}