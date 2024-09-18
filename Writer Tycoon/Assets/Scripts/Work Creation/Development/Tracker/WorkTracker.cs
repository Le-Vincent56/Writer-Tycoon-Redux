using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Ideation.TimeEstimation;
using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Development.Tracker
{
    public enum DevelopmentPhase
    {
        PhaseOne = 1,
        PhaseTwo = 2,
        PhaseThree = 3
    }

    public class WorkTracker : Dedicant
    {
        [SerializeField] private bool working;
        [SerializeField] private bool developing;
        [SerializeField] private int currentDay;

        [SerializeField] private DevelopmentPhase currentPhase;

        [SerializeField] private int currentDayEstimate;
        [SerializeField] private int totalDayEstimate;
        [SerializeField] private int phaseOneDayEstimate;
        [SerializeField] private int phaseTwoDayEstimate;
        [SerializeField] private int phaseThreeDayEstimate;

        public override string Name => "Work Tracker";
        public override DedicantType Type => DedicantType.Tracker;

        private EventBinding<PassDay> passDayEvent;
        private EventBinding<NotifySuccessfulCreation> notifySuccessfulCreationEvent;
        private EventBinding<ConfirmPlayerWorkState> confirmPlayerWorkState;

        private void OnEnable()
        {
            passDayEvent = new EventBinding<PassDay>(UpdateTracker);
            EventBus<PassDay>.Register(passDayEvent);

            notifySuccessfulCreationEvent = new EventBinding<NotifySuccessfulCreation>(StartTracker);
            EventBus<NotifySuccessfulCreation>.Register(notifySuccessfulCreationEvent);

            confirmPlayerWorkState = new EventBinding<ConfirmPlayerWorkState>(ChangeWorkState);
            EventBus<ConfirmPlayerWorkState>.Register(confirmPlayerWorkState);
        }

        private void OnDisable()
        {
            EventBus<PassDay>.Deregister(passDayEvent);
            EventBus<NotifySuccessfulCreation>.Deregister(notifySuccessfulCreationEvent);
            EventBus<ConfirmPlayerWorkState>.Deregister(confirmPlayerWorkState);
        }

        /// <summary>
        /// Callback event to update the Tracker
        /// </summary>
        private void UpdateTracker()
        {
            // Exit case - if the Player is not working
            if (!working) return;

            // Exit case - if not developing
            if (!developing) return;

            // Update the progress data
            EventBus<UpdateProgressData>.Raise(new UpdateProgressData()
            {
                Current = currentDay,
                Maximum = currentDayEstimate,
            });

            // Increment the current day
            currentDay++;

            // Check if the current day has reached the estimate
            if (currentDay == currentDayEstimate)
                // If so, update the phase
                UpdatePhase();
        }

        /// <summary>
        /// Callback function to start the Work Tracker
        /// </summary>
        /// <param name="notifySuccessfulCreation"></param>
        private void StartTracker(NotifySuccessfulCreation notifySuccessfulCreation)
        {
            // Set relevant data
            TimeEstimates estimates = notifySuccessfulCreation.ReviewData.TimeEstimates;

            // Set estimates
            totalDayEstimate = estimates.Total;
            phaseOneDayEstimate = estimates.PhaseOne;
            phaseTwoDayEstimate = estimates.PhaseTwo;
            phaseThreeDayEstimate = estimates.PhaseThree;

            // Set the first phase of development
            currentPhase = DevelopmentPhase.PhaseOne;
            currentDayEstimate = phaseOneDayEstimate;

            // Set developing
            developing = true;
        }

        /// <summary>
        /// Update the current development phase
        /// </summary>
        private void UpdatePhase()
        {
            switch (currentPhase)
            {
                case DevelopmentPhase.PhaseOne:
                    // Reset the current day
                    currentDay = 0;

                    // Start the second phase
                    currentPhase = DevelopmentPhase.PhaseTwo;

                    // Set the new time estimate
                    currentDayEstimate = phaseTwoDayEstimate;
                    break;

                case DevelopmentPhase.PhaseTwo:
                    // Reset the current day
                    currentDay = 0;

                    // Increment the third phase
                    currentPhase = DevelopmentPhase.PhaseThree;

                    // Set the new time estimate
                    currentDayEstimate = phaseThreeDayEstimate;
                    break;

                case DevelopmentPhase.PhaseThree:
                    // Reset the current day
                    currentDay = 0;

                    // Finish development
                    FinishDevelopment();
                    break;
            }
        }

        /// <summary>
        /// Finish development
        /// </summary>
        private void FinishDevelopment()
        {
            // Stop developing
            developing = false;

            // Raise the End Development event
            EventBus<EndDevelopment>.Raise(new EndDevelopment());
        }

        /// <summary>
        /// Callback event to confirm the player is working
        /// </summary>
        private void ChangeWorkState(ConfirmPlayerWorkState eventData) => working = eventData.Working;
    }
}