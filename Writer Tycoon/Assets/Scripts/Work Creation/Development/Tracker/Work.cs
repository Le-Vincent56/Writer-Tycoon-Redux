using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Entities;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Development.PointGeneration;
using WriterTycoon.WorkCreation.Editing;
using WriterTycoon.WorkCreation.Ideation.Genres;
using WriterTycoon.WorkCreation.Ideation.TimeEstimation;
using WriterTycoon.WorkCreation.Rater;
using WriterTycoon.WorkCreation.UI.Development;

namespace WriterTycoon.WorkCreation.Development.Tracker
{
    [System.Serializable]
    public class Work
    {
        [SerializeField] private int hash;
        private List<IWorker> workers;

        [SerializeField] private bool developing;
        [SerializeField] private int currentDayPhase;

        [SerializeField] private DevelopmentPhase currentPhase;

        [Header("Phases")]
        [SerializeField] private int currentDayEstimate;
        [SerializeField] private int totalDayEstimate;
        [SerializeField] private int phaseOneDayEstimate;
        [SerializeField] private int phaseTwoDayEstimate;
        [SerializeField] private int phaseThreeDayEstimate;

        [SerializeField] private PointGenerator pointGenerator;
        [SerializeField] private ErrorGenerator errorGenerator;
        [SerializeField] private Polisher polisher;

        public int Hash { get => hash; }
        public List<IWorker> Workers { get => workers; }
        public PointGenerator PointGenerator { get => pointGenerator; }
        public ErrorGenerator ErrorGenerator { get => errorGenerator; }
        public Polisher Polisher { get => polisher; }

        public Work(List<IWorker> workers, TimeEstimates estimates, List<Genre> chosenGenres, float targetScore, int hash)
        {
            // Set the hash
            this.hash = hash;

            // Set workers
            this.workers = workers;

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

            // Initialize the pointGenerator
            pointGenerator = new PointGenerator(this, chosenGenres, currentPhase, targetScore);
            errorGenerator = new ErrorGenerator(this, estimates.Total, 0.33f);
            polisher = new Polisher(this);

            // Set the current phase total for the second phase
            pointGenerator.SetCurrentPhaseTotal(phaseOneDayEstimate);
        }

        /// <summary>
        /// Track the work
        /// </summary>
        public void Track()
        {
            // Exit case - if there are workers not working
            if (!IsWorkedOn()) return;

            // Exit case - if not developing
            if (!developing) return;

            // Increment the current day
            currentDayPhase++;

            // Update the progress data
            EventBus<UpdateProgressData>.Raise(new UpdateProgressData()
            {
                Hash = hash,
                Stage = ProgressStage.Development,
                Current = currentDayPhase,
                Maximum = currentDayEstimate,
            });

            // Check if the current day has reached the estimate
            if (currentDayPhase == currentDayEstimate)
                // If so, update the phase
                UpdatePhase();
        }

        /// <summary>
        /// Update the current development phase
        /// </summary>
        private void UpdatePhase()
        {
            switch (currentPhase)
            {
                case DevelopmentPhase.PhaseOne:
                    // Start the second phase
                    currentPhase = DevelopmentPhase.PhaseTwo;

                    // Set the new time estimate
                    currentDayEstimate = phaseTwoDayEstimate;

                    // Set the current phase total for the second phase
                    pointGenerator.SetCurrentPhaseTotal(phaseTwoDayEstimate);

                    // End the phase
                    EndPhase();
                    break;

                case DevelopmentPhase.PhaseTwo:
                    // Increment the third phase
                    currentPhase = DevelopmentPhase.PhaseThree;

                    // Set the new time estimate
                    currentDayEstimate = phaseThreeDayEstimate;

                    // Set the current phase total for the third phase
                    pointGenerator.SetCurrentPhaseTotal(phaseThreeDayEstimate);

                    // End the phase
                    EndPhase();
                    break;

                case DevelopmentPhase.PhaseThree:
                    // Reset the current day
                    currentDayPhase = 0;

                    // Finish development
                    FinishDevelopment();
                    break;
            }
        }

        /// <summary>
        /// End the phase
        /// </summary>
        private void EndPhase()
        {
            // Reset the current day
            currentDayPhase = 0;

            // Update the progress data
            EventBus<UpdateProgressData>.Raise(new UpdateProgressData()
            {
                Hash = hash,
                Stage = ProgressStage.Development,
                Current = currentDayPhase,
                Maximum = currentDayEstimate,
            });

            // Update the Focus Slider phase
            EventBus<SetDevelopmentPhase>.Raise(new SetDevelopmentPhase()
            {
                Hash = hash,
                Phase = currentPhase
            });

            // Open the slider window
            EventBus<OpenSliderWindow>.Raise(new OpenSliderWindow()
            {
                Hash = hash
            });
        }

        /// <summary>
        /// Finish development
        /// </summary>
        private void FinishDevelopment()
        {
            // Stop developing
            developing = false;

            // Raise the End Development event
            EventBus<EndDevelopment>.Raise(new EndDevelopment()
            {
                Hash = hash
            });

            // Delete the slider data
            EventBus<DeleteSliderData>.Raise(new DeleteSliderData()
            {
                Hash = hash
            });

            // Begin the editing phase
            EventBus<BeginEditing>.Raise(new BeginEditing()
            {
                Hash = hash
            });
        }

        /// <summary>
        /// Check if the Work is being worked on
        /// </summary>
        public bool IsWorkedOn()
        {
            // Set false as default
            bool isWorkedOn = false;

            // Iterate through each worker
            foreach(IWorker worker in workers)
            {
                // Check if at least one worker is working
                if (worker.Working)
                    // If so, set isWorking to true
                    isWorkedOn = true;
            }

            return isWorkedOn;
        }
    }
}