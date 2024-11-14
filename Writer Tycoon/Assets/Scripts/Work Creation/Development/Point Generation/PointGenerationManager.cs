using System.Collections.Generic;
using UnityEngine;
using GhostWriter.Patterns.EventBus;
using GhostWriter.Patterns.ServiceLocator;
using GhostWriter.WorkCreation.Development.Tracker;
using GhostWriter.WorkCreation.Mediation;

namespace GhostWriter.WorkCreation.Development.PointGeneration
{
    public class PointGenerationManager : Dedicant
    {
        [SerializeField] private Dictionary<int, Work> worksInProgress;
        private GenreFocusTargets genreFocusTargets;

        public override string Name => "Point Generation Manager";
        public override DedicantType Type => DedicantType.PointGenerator;

        private EventBinding<PassHour> passHourEvent;
        private EventBinding<PassDay> passDayEvent;
        private EventBinding<SetDevelopmentPhase> setDevelopmentPhaseEvent;
        private EventBinding<EndDevelopment> endDevelopmentEvent;

        private void Awake()
        {
            // Initialize variables
            worksInProgress = new();
            genreFocusTargets = new();

            // Register this as a service
            ServiceLocator.ForSceneOf(this).Register(this);
        }

        private void OnEnable()
        {
            passHourEvent = new EventBinding<PassHour>(GeneratePoints);
            EventBus<PassHour>.Register(passHourEvent);

            passDayEvent = new EventBinding<PassDay>(ManageSplits);
            EventBus<PassDay>.Register(passDayEvent);

            setDevelopmentPhaseEvent = new EventBinding<SetDevelopmentPhase>(SetDevelopmentPhase);
            EventBus<SetDevelopmentPhase>.Register(setDevelopmentPhaseEvent);

            endDevelopmentEvent = new EventBinding<EndDevelopment>(SendPoints);
            EventBus<EndDevelopment>.Register(endDevelopmentEvent);
        }

        private void OnDisable()
        {
            EventBus<PassHour>.Deregister(passHourEvent);
            EventBus<PassDay>.Deregister(passDayEvent);
            EventBus<SetDevelopmentPhase>.Deregister(setDevelopmentPhaseEvent);
            EventBus<EndDevelopment>.Deregister(endDevelopmentEvent);
        }

        /// <summary>
        /// Get the Genre Focus Slider targets
        /// </summary>
        public GenreFocusTargets GetGenreFocusTargets() => genreFocusTargets;

        /// <summary>
        /// Generate points per hour
        /// </summary>
        private void GeneratePoints()
        {
            // Iterate through each Work in progress
            foreach(KeyValuePair<int, Work> kvp in worksInProgress)
            {
                // Generate points for that Work
                kvp.Value.PointGenerator.GeneratePoints();
            }
        }

        /// <summary>
        /// Callback function to generate points every day
        /// </summary>
        private void ManageSplits()
        {
            foreach(KeyValuePair<int, Work> kvp in worksInProgress)
            {
                // Manage the splits for that Work
                kvp.Value.PointGenerator.ManageSplits();
            }
        }

        /// <summary>
        /// Callback function to set the current development phase for a given Work
        /// </summary>
        private void SetDevelopmentPhase(SetDevelopmentPhase eventData)
        {
            // Try to get a Work from the event Hash
            if(worksInProgress.TryGetValue(eventData.Hash, out Work work))
            {
                // Set the development phase of the Work
                work.PointGenerator.SetDevelopmentPhase(eventData.Phase);
            }
        }

        /// <summary>
        /// Reset the Point Generator
        /// </summary>
        private void SendPoints(EndDevelopment eventData)
        {
            // Send the points out for editing
            Send(new PointPayload()
            { Content = worksInProgress },
                IsType(DedicantType.Editor)
            );
        }

        /// <summary>
        /// Set the Works in progress
        /// </summary>
        public void SetWorksInProgress(Dictionary<int, Work> worksInProgress)
        {
            this.worksInProgress = worksInProgress;

            // Iterate through each Work in progress
            foreach(KeyValuePair<int, Work> kvp in worksInProgress)
            {
                // Set their genre focus targets
                kvp.Value.PointGenerator.SetGenreFocusTargets(genreFocusTargets);
            }
        }

        /// <summary>
        /// Set the allocated points dictionary for a given Work
        /// </summary>
        public void SetAllocatedPoints((int Hash, Dictionary<PointCategory, int> Points) allocatedPoints)
        {
            // Try to get a Work from the event Hash
            if (worksInProgress.TryGetValue(allocatedPoints.Hash, out Work work))
            {
                // Set the allocated points for the given hash
                work.PointGenerator.SetAllocatedPoints(allocatedPoints.Points);
            }
        }
    }
}