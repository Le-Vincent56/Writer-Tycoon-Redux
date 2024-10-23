using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Entities.Tracker;
using WriterTycoon.Patterns.ServiceLocator;
using WriterTycoon.WorkCreation.Development.PointGeneration;
using WriterTycoon.WorkCreation.Ideation.Compatibility;
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
        private CompatibilityManager compatibilityManager;
        private PointGenerationManager pointGenerationManager;

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
            compatibilityManager = ServiceLocator.ForSceneOf(this).Get<CompatibilityManager>();
            pointGenerationManager = ServiceLocator.ForSceneOf(this).Get<PointGenerationManager>();

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
            component.Initialize(data.competitorName, data.startingMoney);
            component.CreateBrain(
                data.learned, 
                data.learningFactor,data.discountFactor, data.explorationFactor,
                topicManager.GetTopics(), genreManager.GetGenres(),
                data.topics, data.genres,
                compatibilityManager.GetGenreTopicCompatibilities(),
                compatibilityManager.GetTopicAudienceCompatibilities(),
                pointGenerationManager.GetGenreFocusTargets()
            );

            // Record the component
            competitorRecord.RecordCompetitor(component);
        }
    }
}