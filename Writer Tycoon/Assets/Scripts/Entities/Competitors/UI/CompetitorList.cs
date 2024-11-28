using GhostWriter.Entities.Tracker;
using GhostWriter.Extensions.GameObjects;
using GhostWriter.Patterns.ServiceLocator;
using System.Collections.Generic;
using UnityEngine;

namespace GhostWriter.Entities.Competitors.UI
{
    public class CompetitorList : MonoBehaviour
    {
        [SerializeField] private GameObject competitorButtonPrefab;
        private List<ICompetitor> currentCompetitors;
        private CompetitorRecord competitorRecord;

        private void Start()
        {
            // Get the competitor record
            competitorRecord = ServiceLocator.ForSceneOf(this).Get<CompetitorRecord>();

            // Initialize the current Competitors
            currentCompetitors = new();
        }

        /// <summary>
        /// Update the Competitor List
        /// </summary>
        public void UpdateList()
        {
            // Get all NPC Competitors
            List<ICompetitor> npcCompetitors = competitorRecord.GetNPCCompetitors();

            foreach(ICompetitor competitor in npcCompetitors)
            {
                // Skip if the current List already contains the Competitor
                if (currentCompetitors.Contains(competitor)) continue;

                // Add the Competitor to the current LIst
                currentCompetitors.Add(competitor);

                // Instantiate the prefab as a child of this object
                GameObject competitorButtonObj = Instantiate(competitorButtonPrefab, transform);

                // Get or add the Competitor Button and get a reference to it
                CompetitorButton competitorButtonComp = competitorButtonObj.GetOrAdd<CompetitorButton>();

                // Initialize the Competitor Button using an NPCCompetitor
                competitorButtonComp.Initialize(competitor as NPCCompetitor);
            }
        }
    }
}
