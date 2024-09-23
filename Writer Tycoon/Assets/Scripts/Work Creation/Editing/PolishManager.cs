using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Editing
{
    public class PolishManager : Dedicant
    {
        [SerializeField] private bool working;
        [SerializeField] private bool polish;

        [Header("Errors")]
        [SerializeField] private float totalErrors;
        [SerializeField] private float currentErrors;
        [SerializeField] private float dailyErrorGoal;
        [SerializeField] private float errorRate;

        [Header("Score")]
        [SerializeField] private float developedPoints;
        [SerializeField] private float polishRate;

        private EventBinding<ConfirmPlayerWorkState> confirmPlayerWorkState;
        private EventBinding<PassHour> passHourEvent;
        private EventBinding<PassDay> passDayEvent;
        private EventBinding<BeginEditing> beginEditingEvent;
        private EventBinding<EndEditing> endEditingEvent;

        public override DedicantType Type => DedicantType.Editor;
        public override string Name => "Editing Manager";

        private void OnEnable()
        {
            confirmPlayerWorkState = new EventBinding<ConfirmPlayerWorkState>(ChangeWorkState);
            EventBus<ConfirmPlayerWorkState>.Register(confirmPlayerWorkState);

            passHourEvent = new EventBinding<PassHour>(Polish);
            EventBus<PassHour>.Register(passHourEvent);

            passDayEvent = new EventBinding<PassDay>(SetDailyPolishGoals);
            EventBus<PassDay>.Register(passDayEvent);

            beginEditingEvent = new EventBinding<BeginEditing>(BeginPolish);
            EventBus<BeginEditing>.Register(beginEditingEvent);

            endEditingEvent = new EventBinding<EndEditing>(EndPolish);
            EventBus<EndEditing>.Register(endEditingEvent);
        }

        private void OnDisable()
        {
            EventBus<ConfirmPlayerWorkState>.Deregister(confirmPlayerWorkState);
            EventBus<PassHour>.Deregister(passHourEvent);
            EventBus<PassDay>.Deregister(passDayEvent);
            EventBus<BeginEditing>.Deregister(beginEditingEvent);
            EventBus<EndEditing>.Deregister(endEditingEvent);
        }

        /// <summary>
        /// Callback function for setting variables for polish
        /// </summary>
        private void SetDailyPolishGoals()
        {
            // Exit case - if not working
            if (!working) return;

            // Exit case - if not polishing
            if (!polish) return;

            // Check if there are errors to remove
            if(currentErrors > 0)
            {
                // Generate anywhere between 1 - 5% of errors
                dailyErrorGoal = Random.Range(totalErrors * 0.01f, totalErrors * 0.05f);

                // Get the error rate for hourly fixes
                errorRate = dailyErrorGoal / 24f;

                return;
            }
        }

        /// <summary>
        /// Callback function for handling polish
        /// </summary>
        private void Polish()
        {
            // Exit case - if not working
            if (!working) return;

            // Exit case - if not polishing
            if (!polish) return;

            // Check if there are errors to remove
            if(currentErrors > 0 && dailyErrorGoal > 0)
            {
                // Subtract from the total errors
                currentErrors -= errorRate;

                // Subtract from the daily error goal
                dailyErrorGoal -= errorRate;

                return;
            }

            // Provide polish by generating more points
            developedPoints += polishRate;
        }

        /// <summary>
        /// Begin polishing
        /// </summary>
        private void BeginPolish() => polish = true;

        /// <summary>
        /// End polishing
        /// </summary>
        private void EndPolish() => polish = false;

        /// <summary>
        /// Set the total errors for the Editing Manager to polish
        /// </summary>
        public void SetErrors(int totalErrors)
        {
            this.totalErrors = totalErrors;
            currentErrors = totalErrors;
        }

        /// <summary>
        /// Set the amount of points the player accumulated during development
        /// </summary>
        public void SetPoints(float points)
        {
            developedPoints = points;
        }

        /// <summary>
        /// Callback event to confirm the player is working
        /// </summary>
        private void ChangeWorkState(ConfirmPlayerWorkState eventData) => working = eventData.Working;
    }
}