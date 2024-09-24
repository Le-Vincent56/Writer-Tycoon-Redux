using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Mediation;
using WriterTycoon.WorkCreation.UI.Development;

namespace WriterTycoon.WorkCreation.Editing
{
    public class PolishManager : Dedicant
    {
        [SerializeField] private bool working;
        [SerializeField] private bool polish;

        [Header("Errors")]
        [SerializeField] private bool finishedRemovingErrors;
        [SerializeField] private float totalErrors;
        [SerializeField] private float currentErrors;
        [SerializeField] private float dailyErrorGoal;
        [SerializeField] private float dailyErrorQuota;
        [SerializeField] private float previousWholeNumber;
        [SerializeField] private float errorRate;

        [Header("Score")]
        [SerializeField] private float developedPoints;
        [SerializeField] private float currentPolishBurst;
        [SerializeField] private float polishBurstMax;
        [SerializeField] private float polishRate;

        private EventBinding<ConfirmPlayerWorkState> confirmPlayerWorkState;
        private EventBinding<PassHour> passHourEvent;
        private EventBinding<PassDay> passDayEvent;
        private EventBinding<BeginEditing> beginEditingEvent;
        private EventBinding<EndEditing> endEditingEvent;

        public override DedicantType Type => DedicantType.Editor;
        public override string Name => "Editing Manager";

        private void Awake()
        {
            polish = false;

            // Set error variables
            finishedRemovingErrors = false;
            totalErrors = 0;
            currentErrors = 0;
            dailyErrorGoal = 0;
            dailyErrorQuota = 0;
            previousWholeNumber = 0;
            errorRate = 0;

            // Set polish variables
            developedPoints = 0;
            currentPolishBurst = 0;
            polishBurstMax = 0;
        }

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

            endEditingEvent = new EventBinding<EndEditing>(ResetPolish);
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

                // Reset the current daily errors
                dailyErrorQuota = 0;

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

                // Add to the daily error goal
                dailyErrorQuota += dailyErrorGoal;

                // Update the error progress bar
                EventBus<UpdateProgressData>.Raise(new UpdateProgressData()
                {
                    Type = ProgressType.Errors,
                    Current = dailyErrorQuota,
                    Maximum = dailyErrorGoal
                });

                // Get the lowest whole number
                int currentErrorInt = Mathf.FloorToInt(dailyErrorQuota);

                // Check if the current whole error number is not equal to 0 and if it does
                // not equal the last seen whole number
                if(currentErrorInt != 0 && currentErrorInt != previousWholeNumber)
                {
                    // Show how many errors are left
                    EventBus<ShowProgressText>.Raise(new ShowProgressText()
                    {
                        Type = ProgressType.Errors,
                        Text = $"Errors: {Mathf.CeilToInt(currentErrors)}"
                    });

                    // Update the previous whole number
                    previousWholeNumber = currentErrorInt;
                }

                return;
            } 
            // Check if all of the current errors have been removed and 
            // if we have not yet finished removing errors
            else if(currentErrors <= 0 && !finishedRemovingErrors)
            {
                // Set finished removing errors to true
                finishedRemovingErrors = true;

                // Hide the errors progress bar and text
                EventBus<HideProgressText>.Raise(new HideProgressText()
                {
                    Type = ProgressType.Errors
                });

                // Show the polish progress bar and text
                EventBus<ShowProgressText>.Raise(new ShowProgressText()
                {
                    Type = ProgressType.Polish,
                    Text = "Polishing"
                });
                
                // Set a maximum for the polish burst
                polishBurstMax = Random.Range(1, 3);
            }

            // Increase the current polish burst
            currentPolishBurst += polishRate;

            // Update the progress bar data
            EventBus<UpdateProgressData>.Raise(new UpdateProgressData()
            {
                Type = ProgressType.Polish,
                Current = currentPolishBurst,
                Maximum = polishBurstMax
            });

            // Check if the current burst has exceeded the maximum
            if(currentPolishBurst >= polishBurstMax)
            {
                // Reset the current polish burst
                currentPolishBurst = 0;

                // Add the maximum to the developed points
                developedPoints += polishBurstMax;

                // Regenerate the new maximum
                polishBurstMax = Random.Range(1, 3);
            }
        }

        /// <summary>
        /// Begin polishing
        /// </summary>
        private void BeginPolish()
        {
            finishedRemovingErrors = false;
            polish = true;

            // Show the progress bar and text
            EventBus<ShowProgressText>.Raise(new ShowProgressText()
            {
                Type = ProgressType.Errors,
                Text = $"Errors: {Mathf.CeilToInt(currentErrors)}"
            });
        }

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
        /// Reset polish variables after editing
        /// </summary>
        private void ResetPolish()
        {
            polish = false;

            // Reset error variables
            finishedRemovingErrors = false;
            totalErrors = 0;
            currentErrors = 0;
            dailyErrorGoal = 0;
            dailyErrorQuota = 0;
            previousWholeNumber = 0;
            errorRate = 0;

            // Reset polish variables
            developedPoints = 0;
            currentPolishBurst = 0;
            polishBurstMax = 0;

            // Hide the polish progress bar and text
            EventBus<HideProgressText>.Raise(new HideProgressText()
            {
                Type = ProgressType.Polish,
            });
        }

        /// <summary>
        /// Callback event to confirm the player is working
        /// </summary>
        private void ChangeWorkState(ConfirmPlayerWorkState eventData) => working = eventData.Working;
    }
}