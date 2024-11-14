using System.Collections.Generic;
using UnityEngine;
using GhostWriter.Entities.Tracker;
using GhostWriter.Patterns.ServiceLocator;
using GhostWriter.WorkCreation.Development.PointGeneration;
using GhostWriter.WorkCreation.Ideation.Compatibility;
using GhostWriter.WorkCreation.Ideation.Genres;
using GhostWriter.WorkCreation.Ideation.Topics;
using GhostWriter.WorkCreation.Ideation.WorkTypes;

namespace GhostWriter.Entities.Competitors
{
    public class CompetitorManager : MonoBehaviour
    {
        [SerializeField] private GameObject competitorPrefab;
        [SerializeField] private List<CompetitorData> competitorDatas;
        private Dictionary<WorkType, int> targetScores;
        private CompetitorRecord competitorRecord;
        private TopicManager topicManager;
        private GenreManager genreManager;
        private CompatibilityManager compatibilityManager;
        private PointGenerationManager pointGenerationManager;

        private void Awake()
        {
            // Verify the competitor datas
            competitorDatas ??= new();

            // Initialize the target scores
            targetScores = new()
            {
                { WorkType.None, 0 },
                { WorkType.Poetry, 100 },
                { WorkType.FlashFiction, 500 },
                { WorkType.ShortStory, 1000 },
                { WorkType.Novella, 5000 },
                { WorkType.Novel, 15000 },
                { WorkType.Screenplay, 15000 }
            };
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
                data.workType, targetScores[data.workType],
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