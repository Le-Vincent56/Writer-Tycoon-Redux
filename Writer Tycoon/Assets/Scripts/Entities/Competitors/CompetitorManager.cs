using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Entities.Tracker;
using WriterTycoon.Patterns.ServiceLocator;
using WriterTycoon.WorkCreation.Ideation.Genres;
using WriterTycoon.WorkCreation.Ideation.Topics;

namespace WriterTycoon.Entities.Competitors
{
    public class CompetitorManager : MonoBehaviour
    {
        [SerializeField] private GameObject competitorPrefab;
        [SerializeField] private List<CompetitorData> competitorDatas;
        private CompetitorRecord competitorRecord;
        private TopicManager topicManager;
        private GenreManager genreManager;

        private void Awake()
        {
            // Verify the competitor datas
            competitorDatas ??= new();
        }

        private void Start()
        {
            // Get the competitor record
            competitorRecord = ServiceLocator.ForSceneOf(this).Get<CompetitorRecord>();
            topicManager = ServiceLocator.ForSceneOf(this).Get<TopicManager>();
            genreManager = ServiceLocator.ForSceneOf(this).Get<GenreManager>();

            // Iterate through each competitor data
            foreach (CompetitorData data in competitorDatas)
            {
                // Create a competitor based on the data
                CreateCompetitor(data);
            }
        }

        /// <summary>
        /// Create a Competitor based on a CompetitorData object
        /// </summary>
        private void CreateCompetitor(CompetitorData data)
        {
            // Instantiate a competitor as a child of this game object
            GameObject competitorObj = Instantiate(competitorPrefab, transform);

            // Set the object name
            competitorObj.name = data.competitorName;

            // Exit case - if there is no NPCCompetitor component attached
            if (!competitorObj.TryGetComponent(out NPCCompetitor component)) return;

            // Initialize the component
            component.Initialize(data.competitorName, data.startingMoney, data.learned, data.learningQ);
            component.SetKnownTopics(topicManager.GetTopics(), data.topics);
            component.SetKnownGenres(genreManager.GetGenres(), data.genres);
            component.InitializeLearning();

            // Record the component
            competitorRecord.RecordCompetitor(component);
        }
    }
}