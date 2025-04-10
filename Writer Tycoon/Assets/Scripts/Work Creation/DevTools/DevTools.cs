using System.Collections.Generic;
using UnityEngine;
using GhostWriter.Entities;
using GhostWriter.Entities.Tracker;
using GhostWriter.Input;
using GhostWriter.Patterns.EventBus;
using GhostWriter.Patterns.ServiceLocator;
using GhostWriter.Utilities.Hash;
using GhostWriter.WorkCreation.Development.Tracker;
using GhostWriter.WorkCreation.Ideation.About;
using GhostWriter.WorkCreation.Ideation.Audience;
using GhostWriter.WorkCreation.Ideation.Compatibility;
using GhostWriter.WorkCreation.Ideation.Genres;
using GhostWriter.WorkCreation.Ideation.TimeEstimation;
using GhostWriter.WorkCreation.Ideation.Topics;
using GhostWriter.WorkCreation.Ideation.WorkTypes;

namespace GhostWriter.WorkCreation.DevTools
{
    public class DevTools : MonoBehaviour
    {
        [Header("Input Objects")]
        [SerializeField] private GameInputReader gameInputReader;
        [SerializeField] private DevInputReader devInputReader;

        [Header("Development Tools")]
        [SerializeField] private bool active;

        private WorkerRecord workerRecord;
        private CompetitorRecord competitorRecord;

        private void Start()
        {
            // Get the Worker Record
            workerRecord = ServiceLocator.ForSceneOf(this).Get<WorkerRecord>();
            competitorRecord = ServiceLocator.ForSceneOf(this).Get<CompetitorRecord>();

            // Disable the Dev Input Reader by default
            devInputReader.Disable();
        }

        private void OnEnable()
        {
            gameInputReader.ActivateDevTools += ActivateDevTools;

            devInputReader.DeactivateDevTools += DeactivateDevTools;
            devInputReader.CreateExcellentWork += CreateExcellentWork;
            devInputReader.CreateMediocreWork += CreateMediocreWork;
            devInputReader.CreateTerribleWork += CreateTerribleWork;
        }

        private void OnDisable()
        {
            gameInputReader.ActivateDevTools -= ActivateDevTools;

            devInputReader.DeactivateDevTools -= DeactivateDevTools;
            devInputReader.CreateExcellentWork -= CreateExcellentWork;
            devInputReader.CreateMediocreWork -= CreateMediocreWork;
            devInputReader.CreateTerribleWork -= CreateTerribleWork;
        }

        /// <summary>
        /// Activate Development Tools
        /// </summary>
        private void ActivateDevTools()
        {
            // Set active to true
            active = true;

            // Disable the Game Input Reader and enable the Dev Input Reader
            gameInputReader.Disable();
            devInputReader.Enable();
        }

        /// <summary>
        /// Deactivate Development Tools
        /// </summary>
        private void DeactivateDevTools()
        {
            // Set active to false
            active = false;

            // Disable the Dev Input Reader and enable the Game Input Reader
            devInputReader.Disable();
            gameInputReader.Enable();
        }

        /// <summary>
        /// Create a Perfect Work
        /// </summary>
        private void CreateExcellentWork()
        {
            // Exit case - if Development Tools are not active
            if (!active) return;

            // Create the Work object
            Work excellentWork = new Work
                (
                    competitorRecord.GetPlayer(),
                    new AboutInfo()
                    {
                        Title = "Perfect Work",
                        Author = "Development",
                        Description = "This is an example of a perfect work!"
                    },
                    new CompatibilityInfo()
                    {
                        TopicGenreTypes = new List<CompatibilityType>()
                        {
                            CompatibilityType.Excellent,
                        },
                        TopicAudienceTypes = new List<CompatibilityType>()
                        {
                            CompatibilityType.Excellent,
                        },
                        TopicAudienceScore = 2,
                        TopicGenreScore = 2,
                        TotalScore = 2,
                    },
                    new List<IWorker>()
                    {
                        workerRecord.GetPlayerWorker()
                    },
                    new TimeEstimates()
                    {
                        Total = 375,
                        PhaseOne = 75,
                        PhaseTwo = 225,
                        PhaseThree = 75
                    },
                    new List<Topic>()
                    {
                        new Topic(TopicType.Agents, true)
                    },
                    new List<Genre>()
                    {
                        new Genre(GenreType.Action, true)
                    },
                    AudienceType.Teens,
                    WorkType.Novel,
                    15000,
                    HashUtils.GenerateHash()
                 );

            // Set an excellent score
            int excellentScore = Random.Range(13500, 15000);
            excellentWork.Polisher.EndScore = excellentScore;

            // Set a day wait of 1
            excellentWork.DaysToWaitBeforeRating = 1;

            EventBus<RateWork>.Raise(new RateWork()
            {
                WorkToPublish = excellentWork
            });
        }

        /// <summary>
        /// Create a Mediocre Work
        /// </summary>
        private void CreateMediocreWork()
        {
            // Exit case - if Development Tools are not active
            if (!active) return;

            // Create the Work object
            Work mediocreWork = new Work
                (
                    competitorRecord.GetPlayer(),
                    new AboutInfo()
                    {
                        Title = "Mediocre Work",
                        Author = "Development",
                        Description = "This is an example of a mediocre work!"
                    },
                    new CompatibilityInfo()
                    {
                        TopicGenreTypes = new List<CompatibilityType>()
                        {
                            CompatibilityType.Neutral,
                        },
                        TopicAudienceTypes = new List<CompatibilityType>()
                        {
                            CompatibilityType.Neutral,
                        },
                        TopicAudienceScore = 0,
                        TopicGenreScore = 0,
                        TotalScore = 0,
                    },
                    new List<IWorker>()
                    {
                        workerRecord.GetPlayerWorker()
                    },
                    new TimeEstimates()
                    {
                        Total = 375,
                        PhaseOne = 75,
                        PhaseTwo = 225,
                        PhaseThree = 75
                    },
                    new List<Topic>()
                    {
                        new Topic(TopicType.Airplanes, true)
                    },
                    new List<Genre>()
                    {
                        new Genre(GenreType.Adventure, true)
                    },
                    AudienceType.Adults,
                    WorkType.Novel,
                    15000,
                    HashUtils.GenerateHash()
                 );

            // Set a mediocre score
            int mediocreScore = Random.Range(7500, 11250);
            mediocreWork.Polisher.EndScore = mediocreScore;

            // Set a day wait of 1
            mediocreWork.DaysToWaitBeforeRating = 1;

            EventBus<RateWork>.Raise(new RateWork()
            {
                WorkToPublish = mediocreWork
            });
        }

        /// <summary>
        /// Create a Terrible Work
        /// </summary>
        private void CreateTerribleWork()
        {
            // Exit case - if Development Tools are not active
            if (!active) return;

            // Exit case - if Development Tools are not active
            if (!active) return;

            // Create the Work object
            Work terribleWork = new Work
                (
                    competitorRecord.GetPlayer(),
                    new AboutInfo()
                    {
                        Title = "Mediocre Work",
                        Author = "Development",
                        Description = "This is an example of a mediocre work!"
                    },
                    new CompatibilityInfo()
                    {
                        TopicGenreTypes = new List<CompatibilityType>()
                        {
                            CompatibilityType.Neutral,
                        },
                        TopicAudienceTypes = new List<CompatibilityType>()
                        {
                            CompatibilityType.Neutral,
                        },
                        TopicAudienceScore = 0,
                        TopicGenreScore = 0,
                        TotalScore = 0,
                    },
                    new List<IWorker>()
                    {
                        workerRecord.GetPlayerWorker()
                    },
                    new TimeEstimates()
                    {
                        Total = 375,
                        PhaseOne = 75,
                        PhaseTwo = 225,
                        PhaseThree = 75
                    },
                    new List<Topic>()
                    {
                        new Topic(TopicType.Androids, true)
                    },
                    new List<Genre>()
                    {
                        new Genre(GenreType.ContemporaryFiction, true)
                    },
                    AudienceType.Children,
                    WorkType.Novel,
                    15000,
                    HashUtils.GenerateHash()
                 );

            // Set a terrible score
            int terribleScore = Random.Range(1500, 3750);
            terribleWork.Polisher.EndScore = terribleScore;

            // Set a day wait of 1
            terribleWork.DaysToWaitBeforeRating = 1;

            EventBus<RateWork>.Raise(new RateWork()
            {
                WorkToPublish = terribleWork
            });
        }
    }
}