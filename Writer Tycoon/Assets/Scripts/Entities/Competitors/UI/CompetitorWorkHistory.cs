using GhostWriter.Extensions.GameObjects;
using GhostWriter.Patterns.EventBus;
using GhostWriter.WorkCreation.Publication;
using System.Collections.Generic;
using UnityEngine;

namespace GhostWriter.Entities.Competitors.UI
{
    public class CompetitorWorkHistory : MonoBehaviour
    {
        [SerializeField] private GameObject competitorWorkPrefab;
        [SerializeField] private GameObject contentObject;
        [SerializeField] private CompetitorWorkTooltip workTooltip;
        private List<PublishedWork> competitorWorks;

        private EventBinding<SetCompetitorWorkHistory> setCompetitorWorkHistoryEvent;

        private void Awake()
        {
            // Initialize the Competitor Works List
            competitorWorks = new();
        }

        private void OnEnable()
        {
            setCompetitorWorkHistoryEvent = new EventBinding<SetCompetitorWorkHistory>(SetCompetitorHistory);
            EventBus<SetCompetitorWorkHistory>.Register(setCompetitorWorkHistoryEvent);
        }

        private void OnDisable()
        {
            EventBus<SetCompetitorWorkHistory>.Deregister(setCompetitorWorkHistoryEvent);
        }

        /// <summary>
        /// Callback function to set the Competitor Work History
        /// </summary>
        private void SetCompetitorHistory(SetCompetitorWorkHistory eventData)
        {
            // Destroy all the children
            DestroyAllChildren();

            // Clear the current Competitor Works List
            competitorWorks.Clear();

            // Set the Published Works
            competitorWorks = eventData.Competitor.PublishedWorks;

            // Iterate through each Published Work
            foreach (PublishedWork publishedWork in competitorWorks)
            {
                // Instantiate the prefab
                GameObject competitorWorkObj = Instantiate(competitorWorkPrefab, contentObject.transform);

                // Get or add the Competitor Work Highlightable componnet
                CompetitorWorkHighlightable highlightable = competitorWorkObj.GetOrAdd<CompetitorWorkHighlightable>();

                // Initialize the highlightable
                highlightable.Initialize(publishedWork, workTooltip);
            }
        }

        /// <summary>
        /// Destroy all child objects of the Competitor Work History
        /// </summary>
        private void DestroyAllChildren()
        {
            // Exit case - There are no child objects
            if (contentObject.transform.childCount <= 0) return;

            // Store all child transforms in an array to avoid modifying the hierarchy during iteration
            Transform[] children = new Transform[contentObject.transform.childCount];
            for (int i = 0; i < contentObject.transform.childCount; i++)
            {
                children[i] = contentObject.transform.GetChild(i);
            }

            // Iterate through each child object
            foreach (Transform child in children)
            {
                // Destroy the child object
                Destroy(child.gameObject);
            }
        }
    }
}
