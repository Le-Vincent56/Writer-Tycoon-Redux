using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WriterTycoon.Entities.Competitors.Learning;
using WriterTycoon.Entities.Competitors.States;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.Patterns.StateMachine;
using WriterTycoon.Utilities.Hash;
using WriterTycoon.WorkCreation.Development.Tracker;
using WriterTycoon.WorkCreation.Ideation.About;
using WriterTycoon.WorkCreation.Ideation.Audience;
using WriterTycoon.WorkCreation.Ideation.Compatibility;
using WriterTycoon.WorkCreation.Ideation.Genres;
using WriterTycoon.WorkCreation.Ideation.TimeEstimation;
using WriterTycoon.WorkCreation.Ideation.Topics;
using WriterTycoon.WorkCreation.Ideation.WorkTypes;

namespace WriterTycoon.Entities.Competitors
{
    public class NPCCompetitor : SerializedMonoBehaviour, ICompetitor
    {
        private Bank bank;

        [SerializeField] private string competitorName;
        [SerializeField] private bool learned;
        [SerializeField] private float learningQ;

        [Header("Working Variables")]
        [SerializeField] private bool working;
        [SerializeField] private int daysIdle;
        [SerializeField] private int totalDaysIdle;
        [SerializeField] private int daysWorking;
        [SerializeField] private int totalDaysWorking;

        [SerializeField] private HashSet<Topic> knownTopics;
        [SerializeField] private HashSet<Genre> knownGenres;

        private StateMachine stateMachine;

        private QLearner qLearner;
        private ReinforcementProblem writeProblem;

        private EventBinding<PassDay> passDayEvent;

        private void OnEnable()
        {
            passDayEvent = new EventBinding<PassDay>(HandleDay);
            EventBus<PassDay>.Register(passDayEvent);
        }

        private void OnDisable()
        {
            EventBus<PassDay>.Deregister(passDayEvent);
        }

        /// <summary>
        /// Initialize the NPC Competitor
        /// </summary>
        public void Initialize(string competitorName, float startingMoney, bool learned, float learningQ)
        {
            // Get Components
            bank = GetComponent<Bank>();

            // Link the bank and set the starting sum
            bank.LinkCompetitor(this);
            bank.SetBankSum(startingMoney);

            // Set variables
            this.competitorName = competitorName;
            working = false;
            this.learned = learned;
            this.learningQ = learningQ;

            // Initialize the HashSets
            knownTopics = new();
            knownGenres = new();

            // Initialize the state machine
            CreateStateMachine();
        }

        /// <summary>
        /// Initialize the State Machine for the Competitor
        /// </summary>
        private void CreateStateMachine()
        {
            // Initialize the State Machine
            stateMachine = new();

            // Create states
            IdleState idleState = new IdleState(this);
            WorkState workState = new WorkState(this);

            // Define state transitions
            stateMachine.At(idleState, workState, new FuncPredicate(() => working));
            stateMachine.At(workState, idleState, new FuncPredicate(() => !working));

            // Set an initial state
            stateMachine.SetState(idleState);
        }

        /// <summary>
        /// Set the Competitor's known Topics
        /// </summary>
        public void SetKnownTopics(List<Topic> availableTopics, HashSet<TopicType> topicData)
        {
            // Iterate through each Topic
            foreach(Topic topicObj in availableTopics)
            {
                // Iterate through each TopicType
                foreach(TopicType topicType in topicData)
                {
                    // Skip if the types are not equal
                    if (topicObj.Type != topicType) continue;

                    // Add the Topic to the known Topics;
                    // create a copy to be able to unlock it for the Competitor but
                    // not the player
                    knownTopics.Add(new Topic(topicObj));
                }
            }

            // Iterate through each Topic in the known Topics
            foreach(Topic topic in knownTopics)
            {
                // Unlock the Topic for the Competitor
                topic.Unlock();
            }
        }

        /// <summary>
        /// Set the Competitor's known Genres
        /// </summary>
        public void SetKnownGenres(List<Genre> availableGenres, HashSet<GenreType> genreData)
        {
            // Iterate through each Genre
            foreach (Genre genreObj in availableGenres)
            {
                // Iterate through each GenreType
                foreach (GenreType genreType in genreData)
                {
                    // Skip if the types are not equal
                    if (genreObj.Type != genreType) continue;

                    // Add the Genre to the known Genres;
                    // create a copy to be able to unlock it for the Competitor but
                    // not the player
                    knownGenres.Add(new Genre(genreObj));
                }
            }

            // Iterate through each Genre in the known Genres
            foreach (Genre genre in knownGenres)
            {
                // Unlock the Genre for the Competitor
                genre.Unlock();
            }
        }

        public void InitializeLearning()
        {
            // Initialize the Q-Learner
            qLearner = new QLearner();

            int actionCount = 0;
            Dictionary<int, Func<float>> availableActions = new();

            Array enumArray = Enum.GetValues(typeof(AudienceType));

            // Create a list of actions
            for(int i = 0; i < knownTopics.Count; i++)
            {
                for(int j = 0; j < knownGenres.Count; j++)
                {
                    for(int k = 1; k < enumArray.Length; k++)
                    {
                        Topic topic = knownTopics.ElementAt(i);
                        Genre genre = knownGenres.ElementAt(j);
                        AudienceType audience = (AudienceType)enumArray.GetValue(k);

                        availableActions.Add(
                            actionCount,
                            () => CreateWork(
                                topic, 
                                genre, 
                                audience
                            )
                        );

                        Debug.Log($"Created Action ({actionCount}) using {topic.Name}, {genre.Name}, {audience}");

                        actionCount++;
                    }
                }
            }

            writeProblem = new ReinforcementProblem(availableActions);
        }

        private float CreateWork(Topic topic, Genre genre, AudienceType audienceType)
        {
            return 0f;
        }

        /// <summary>
        /// Callback function for handling the Competitor's actions for the day
        /// </summary>
        public void HandleDay()
        {
            // Check if working
            if(working)
            {
                // If so, increase the amount of days spent working
                daysWorking++;

                // Exit case - if below the total amount of days to work
                if (daysWorking <= totalDaysWorking) return;
                
                // Set working to false
                working = false;
            }
            else
            {
                // Otherwise, increase the amount of days spent idle
                daysIdle--;

                // Exit case - if below the total amount of days to idle
                if (daysIdle <= totalDaysIdle) return;

                // Set working to true
                working = true;
            }
        }

        /// <summary>
        /// Set the amount of days for the Competitor to idle
        /// </summary>
        public void SetDaysToIdle()
        {
            // Reset the counter
            daysIdle = 0;

            // Set the amount of days to idle (2-10 days)
            totalDaysIdle = UnityEngine.Random.Range(2, 10);
        }

        public void StartWorking()
        {
            // Create an empty work
            Work newWork = new(
                this,
                new AboutInfo() { },
                new CompatibilityInfo() { },
                null,
                new TimeEstimates() { },
                null,
                null,
                AudienceType.None,
                WorkType.None,
                0,
                HashUtils.GenerateHash()
            );

            // TODO: Decide on Topics, Genres, and AudienceTypes based on Q-Learning
        }

        /// <summary>
        /// Calculate sales for the NPC Competitor
        /// </summary>
        public void CalculateSales(float amount) => bank.AddAmount(amount);
    }
}