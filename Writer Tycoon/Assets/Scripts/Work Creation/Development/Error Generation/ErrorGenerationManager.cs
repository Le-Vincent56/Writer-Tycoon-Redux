using System.Collections.Generic;
using UnityEngine;
using GhostWriter.Patterns.EventBus;
using GhostWriter.WorkCreation.Development.Tracker;
using GhostWriter.WorkCreation.Mediation;

namespace GhostWriter.WorkCreation.Development.ErrorGeneration
{
    public class ErrorGenerationManager : Dedicant
    {
        [SerializeField] private Dictionary<int, Work> worksInProgress;

        private EventBinding<PassDay> passDayEvent;
        private EventBinding<EndDevelopment> endDevelopmentEvent;

        public override string Name => "Error Generation Manager";
        public override DedicantType Type => DedicantType.ErrorGenerator;

        private void Awake()
        {
            // Initialize the Dictionary
            worksInProgress = new();
        }

        private void OnEnable()
        {
            passDayEvent = new EventBinding<PassDay>(GenerateErrors);
            EventBus<PassDay>.Register(passDayEvent);

            endDevelopmentEvent = new EventBinding<EndDevelopment>(SendErrors);
            EventBus<EndDevelopment>.Register(endDevelopmentEvent);
        }

        private void OnDisable()
        {
            EventBus<PassDay>.Deregister(passDayEvent);
            EventBus<EndDevelopment>.Deregister(endDevelopmentEvent);
        }

        /// <summary>
        /// Set the Works in progress
        /// </summary>
        public void SetWorksInProgress(Dictionary<int, Work> worksInProgress)
        {
            // Set the Works in progress
            this.worksInProgress = worksInProgress;
        }

        /// <summary>
        /// Callback function to send the error variables on the end of development
        /// </summary>
        private void SendErrors(EndDevelopment eventData)
        {
            // Try to get a Work from the hash
            if(worksInProgress.TryGetValue(eventData.Hash, out Work value))
            {
                // Send the total errors
                Send(new ErrorPayload()
                    {
                        Content = (eventData.Hash, value.ErrorGenerator.GetTotalErrors())
                    }, IsType(DedicantType.Editor)
                );
            }
        }

        /// <summary>
        /// Generate the errors for each day
        /// </summary>
        private void GenerateErrors()
        {
            foreach(KeyValuePair<int, Work> kvp in worksInProgress)
            {
                kvp.Value.ErrorGenerator.GenerateErrors();
            }
        }
    }
}