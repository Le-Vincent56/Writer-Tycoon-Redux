using GhostWriter.Entities.Tracker;
using GhostWriter.Extensions.GameObjects;
using GhostWriter.Patterns.EventBus;
using GhostWriter.Patterns.ServiceLocator;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace GhostWriter.Entities.Competitors.UI
{
    public class CompetitorList : SerializedMonoBehaviour
    {
        [SerializeField] private GameObject competitorButtonPrefab;
        [SerializeField] private GameObject contentObject;
        private Dictionary<ICompetitor, CompetitorButton> competitorDict;
        private CompetitorRecord competitorRecord;

        private EventBinding<UpdateCompetitorList> updateCompetitorListEvent;

        private void OnEnable()
        {
            updateCompetitorListEvent = new EventBinding<UpdateCompetitorList>(UpdateList);
            EventBus<UpdateCompetitorList>.Register(updateCompetitorListEvent);
        }

        private void OnDisable()
        {
            EventBus<UpdateCompetitorList>.Deregister(updateCompetitorListEvent);
        }

        private void Start()
        {
            // Get the competitor record
            competitorRecord = ServiceLocator.ForSceneOf(this).Get<CompetitorRecord>();

            // Initialize the current Competitors
            competitorDict = new();
        }

        /// <summary>
        /// Callback function for updating the Competitor List
        /// </summary>
        private void UpdateList(UpdateCompetitorList eventData)
        {
            // Check if requiring a full List update
            if (eventData.FullUpdate)
                // Perform a full update, checking for new Competitors
                UpdateListFull();
            else
                // Otherwise, update the existing list
                UpdateListExisting();
        }

        /// <summary>
        /// Update the Competitor List, checking for new Competitors
        /// </summary>
        private void UpdateListFull()
        {
            // Get all NPC Competitors
            List<ICompetitor> npcCompetitors = competitorRecord.GetNPCCompetitors();

            // Iterate through each NPC Competitor
            foreach(ICompetitor competitor in npcCompetitors)
            {
                // Skip if the current List already contains the Competitor
                if (competitorDict.ContainsKey(competitor))
                {
                    // Update the info of the Competitor's Button
                    competitorDict[competitor].UpdateInfo();

                    // Skip
                    continue;
                }

                // Instantiate the prefab as a child of this object
                GameObject competitorButtonObj = Instantiate(competitorButtonPrefab, contentObject.transform);

                // Get or add the Competitor Button and get a reference to it
                CompetitorButton competitorButtonComp = competitorButtonObj.GetOrAdd<CompetitorButton>();

                // Initialize the Competitor Button using an NPCCompetitor
                competitorButtonComp.Initialize(competitor as NPCCompetitor);

                // Add the Competitor to the current Dictionary
                competitorDict.Add(competitor, competitorButtonComp);
            }
        }

        /// <summary>
        /// Update the existing list of Competitors
        /// </summary>
        public void UpdateListExisting()
        {
            // Iterate through each NPC Competitor
            foreach (KeyValuePair<ICompetitor, CompetitorButton> kvp in competitorDict)
            {
                // Update the Info
                kvp.Value.UpdateInfo();
            }
        }
    }
}
