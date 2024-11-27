using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using GhostWriter.Entities.Competitors.Learning;
using GhostWriter.Entities.Competitors.States;
using GhostWriter.Patterns.EventBus;
using GhostWriter.Patterns.StateMachine;
using GhostWriter.WorkCreation.Development.PointGeneration;
using GhostWriter.WorkCreation.Ideation.Compatibility;
using GhostWriter.WorkCreation.Ideation.Genres;
using GhostWriter.WorkCreation.Ideation.Topics;
using GhostWriter.WorkCreation.Ideation.WorkTypes;
using GhostWriter.Extensions.GameObjects;
using GhostWriter.WorkCreation.Publication;
using GhostWriter.Utilities.Hash;
using GhostWriter.WorkCreation.Ideation.About;
using GhostWriter.World.Calendar;

namespace GhostWriter.Entities.Competitors
{
    public class NPCCompetitor : SerializedMonoBehaviour, ICompetitor
    {
        private Bank bank;

        [Header("Competitor Information")]
        [SerializeField] private string competitorName;
        [SerializeField] private Dictionary<int, PublishedWork> workHistory;

        [Header("Working Variables")]
        [SerializeField] private bool working;
        [SerializeField] private int daysIdle;
        [SerializeField] private int totalDaysIdle;
        [SerializeField] private int daysWorking;
        [SerializeField] private int totalDaysWorking;
        [SerializeField] private bool progressWork;

        [Header("Brain")]
        [SerializeField] private CompetitorBrain brain;

        private Calendar calendar;
        private GeneratedStoryBank generatedStoryBank;
        private StateMachine stateMachine;

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

        private void Update()
        {
            stateMachine.Update();
        }

        /// <summary>
        /// Initialize the NPC Competitor
        /// </summary>
        public void Initialize(string competitorName, float startingMoney, Calendar calendar, GeneratedStoryBank generatedStoryBank)
        {
            // Get Components
            bank = GetComponent<Bank>();

            // Link the bank and set the starting sum
            bank.LinkCompetitor(this);
            bank.SetBankSum(startingMoney);

            // Set variables
            this.competitorName = competitorName;
            working = false;

            // Initialize the Work history
            workHistory = new();

            // Get the Calendar
            this.calendar = calendar;

            // Get the Generated Story Bank
            this.generatedStoryBank = generatedStoryBank;

            // Initialize the state machine
            CreateStateMachine();
        }

        /// <summary>
        /// Create the Competitor's Brain for learning
        /// </summary>
        public void CreateBrain(
            bool learned, 
            float learningFactor, float discountFactor, float explorationFactor,
            WorkType workType, int targetScore,
            List<Topic> availableTopics, List<Genre> availableGenres,
            HashSet<TopicType> knownTopics, HashSet<GenreType> knownGenres,
            GenreTopicCompatibility genreTopicCompatibility,
            TopicAudienceCompatibility topicAudienceCompatibility,
            GenreFocusTargets genreFocusTargets
        )
        {
            // Check if the Brain is learned
            if (learned)
            {
                // Get or add the Learned Brain
                brain = gameObject.GetOrAdd<LearnedBrain>();
                
                // Set the learning variables
                (brain as LearnedBrain).SetVariables(learningFactor, discountFactor, explorationFactor);
            } else
            {
                // Get or add the Random Brain
                brain = gameObject.GetOrAdd<RandomBrain>();
            }

            // Initialize the Brain
            brain.InitializeBrain(
                workType, targetScore,
                availableTopics, knownTopics,
                availableGenres, knownGenres,
                genreTopicCompatibility,
                topicAudienceCompatibility,
                genreFocusTargets
            );
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
            ConceptState conceptState = new ConceptState(this);
            FocusOneState focusOneState = new FocusOneState(this);
            FocusTwoState focusTwoState = new FocusTwoState(this);
            FocusThreeState focusThreeState = new FocusThreeState(this);

            // Define state transitions
            stateMachine.At(idleState, conceptState, new FuncPredicate(() => working));
            stateMachine.At(conceptState, focusOneState, new FuncPredicate(() => progressWork));
            stateMachine.At(focusOneState, focusTwoState, new FuncPredicate(() => progressWork));
            stateMachine.At(focusTwoState, focusThreeState, new FuncPredicate(() => progressWork));
            stateMachine.At(focusThreeState, idleState, new FuncPredicate(() => !working));

            // Set an initial state
            stateMachine.SetState(idleState);
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

                if(stateMachine.GetState() is FocusThreeState)
                {
                    working = false;
                } else
                {
                    progressWork = true;
                }
            }
            else
            {
                // Otherwise, increase the amount of days spent idle
                daysIdle++;

                // Exit case - if below the total amount of days to idle
                if (daysIdle <= totalDaysIdle) return;

                // Set working to true
                working = true;
            }
        }

        public void StopWorking()
        {
            daysWorking = 0;
            working = false;
            progressWork = false;
        }

        /// <summary>
        /// Set the amount of days for the Competitor to idle
        /// </summary>
        public void SetDaysToIdle()
        {
            // Reset counters
            daysIdle = 0;
            daysWorking = 0;

            // Set the amount of days to idle (2-10 days)
            totalDaysIdle = Random.Range(2, 10);
        }

        public void SetDaysToWork()
        {
            // Reset variables
            daysWorking = 0;
            daysIdle = 0;
            progressWork = false;

            // Set the amount of days to work
            totalDaysWorking = Random.Range(2, 10);
        }

        /// <summary>
        /// Learn from a problem using the CompetitorBrain
        /// </summary>
        public void Learn(Problem problem) => brain.HandleProblem(problem);

        /// <summary>
        /// Rate the current concept and slider values
        /// </summary>
        public void Rate()
        {
            // Get the final data
            RateData finalData = brain.Rate();

            // Get a title and description from the Generated Story Bank
            (string Title, string Description) generatedStory = generatedStoryBank.Get(finalData.Genre.Type);

            // Create the Published Work object
            PublishedWork publishedWork = new(
                HashUtils.GenerateHash(),
                this,
                new AboutInfo()
                {
                    Author = competitorName,
                    Title = generatedStory.Title,
                    Description = generatedStory.Description,
                },
                new List<Topic>() { finalData.Topic },
                new List<Genre>() { finalData.Genre },
                finalData.Audience,
                brain.WorkType,
                finalData.FinalScore
            );

            // Set the release date of the Published Work
            publishedWork.SetReleaseDate(calendar.Day, calendar.Month, calendar.Year);

            // Add to the dictionary
            workHistory.Add(publishedWork.Hash, publishedWork);
        }

        /// <summary>
        /// Calculate sales for the NPC Competitor
        /// </summary>
        public void CalculateSales(float amount) => bank.AddAmount(amount);
    }
}