using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Development.Tracker;
using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Editing
{
    public class PolishManager : Dedicant
    {
        [SerializeField] private Dictionary<int, Work> worksInProgress;

        private EventBinding<PassHour> passHourEvent;
        private EventBinding<PassDay> passDayEvent;
        private EventBinding<BeginEditing> beginEditingEvent;
        private EventBinding<EndEditing> endEditingEvent;

        public override DedicantType Type => DedicantType.Editor;
        public override string Name => "Editing Manager";

        private void Awake()
        {
            // Initialize the dictionary
            worksInProgress = new();
        }

        private void OnEnable()
        {
            passHourEvent = new EventBinding<PassHour>(Polish);
            EventBus<PassHour>.Register(passHourEvent);

            passDayEvent = new EventBinding<PassDay>(SetDailyErrorGoals);
            EventBus<PassDay>.Register(passDayEvent);

            beginEditingEvent = new EventBinding<BeginEditing>(BeginPolish);
            EventBus<BeginEditing>.Register(beginEditingEvent);

            endEditingEvent = new EventBinding<EndEditing>(ResetPolish);
            EventBus<EndEditing>.Register(endEditingEvent);
        }

        private void OnDisable()
        {
            EventBus<PassHour>.Deregister(passHourEvent);
            EventBus<PassDay>.Deregister(passDayEvent);
            EventBus<BeginEditing>.Deregister(beginEditingEvent);
            EventBus<EndEditing>.Deregister(endEditingEvent);
        }

        /// <summary>
        /// Callback function for setting how many errors should be fixed per day
        /// </summary>
        private void SetDailyErrorGoals()
        {
            // Iterate through each Work in progress
            foreach (KeyValuePair<int, Work> kvp in worksInProgress)
            {
                // Set the daily error goal for the Work
                kvp.Value.Polisher.SetDailyErrorGoals();
            }
        }

        /// <summary>
        /// Callback function for handling polish
        /// </summary>
        private void Polish()
        {
            // Iterate through each Work in progress
            foreach(KeyValuePair<int, Work> kvp in worksInProgress)
            {
                // Polish the Work
                kvp.Value.Polisher.Polish();
            }
        }

        /// <summary>
        /// Begin polishing a work
        /// </summary>
        private void BeginPolish(BeginEditing eventData)
        {
            // Try to get a Work from the hash
            if(worksInProgress.TryGetValue(eventData.Hash, out Work value))
            {
                // Begin polishing the work
                value.Polisher.BeginPolish();

                // Update the progress card to the new state
                EventBus<SetProgressStage>.Raise(new SetProgressStage()
                {
                    Hash = eventData.Hash,
                    Stage = UI.Development.ProgressStage.Error
                });
            }
        }

        /// <summary>
        /// Set the total errors for the Editing Manager to polish
        /// </summary>
        public void SetErrors(int hash, int totalErrors)
        {
            // Try to get a Work from the hash
            if(worksInProgress.TryGetValue(hash, out Work value))
            {
                // Set the total errors for the work
                value.Polisher.SetTotalErrors(totalErrors);
            }
        }

        /// <summary>
        /// Set the Works in progress
        /// </summary>
        public void SetWorks(Dictionary<int, Work> worksInProgress)
        {
            this.worksInProgress = worksInProgress;
        }

        /// <summary>
        /// Reset polish variables after editing
        /// </summary>
        private void ResetPolish(EndEditing eventData)
        {
            // Try to get a Work from the hash
            if(worksInProgress.TryGetValue(eventData.Hash, out Work value))
            {
                // Reset the Polisher
                value.Polisher.Reset();
            }
        }
    }
}