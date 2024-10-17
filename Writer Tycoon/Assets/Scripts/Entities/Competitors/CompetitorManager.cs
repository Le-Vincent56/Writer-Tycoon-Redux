using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Entities.Tracker;
using WriterTycoon.Patterns.ServiceLocator;

namespace WriterTycoon.Entities.Competitors
{
    public class CompetitorManager : MonoBehaviour
    {
        [SerializeField] private GameObject competitorPrefab;
        private CompetitorRecord competitorRecord;

        private void Start()
        {
            // Get the competitor record
            competitorRecord = ServiceLocator.ForSceneOf(this).Get<CompetitorRecord>();
        }

        private void CreateCompetitor(bool learned)
        {
            // Instantiate a competitor as a child of this game object
            GameObject competitorObj = Instantiate(competitorPrefab, transform);

            // Exit case - if there is no NPCCompetitor component attached
            if (!competitorObj.TryGetComponent(out NPCCompetitor component)) return;

            // Initialize the component
            component.Initialize(learned);
        }
    }
}