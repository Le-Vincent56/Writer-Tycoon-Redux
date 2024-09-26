using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;
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
        [SerializeField] private Dictionary<int, Work> worksToTrack;

        public override string Name => "Work Tracker";
        public override DedicantType Type => DedicantType.Tracker;

        private EventBinding<PassDay> passDayEvent;
        private EventBinding<NotifySuccessfulCreation> notifySuccessfulCreationEvent;

        private void Awake()
        {
            // Instantiate the dictionary
            worksToTrack = new();
        }

        private void OnEnable()
        {
            passDayEvent = new EventBinding<PassDay>(TrackWorks);
            EventBus<PassDay>.Register(passDayEvent);

            notifySuccessfulCreationEvent = new EventBinding<NotifySuccessfulCreation>(AddWorkToTrack);
            EventBus<NotifySuccessfulCreation>.Register(notifySuccessfulCreationEvent);
        }

        private void OnDisable()
        {
            EventBus<PassDay>.Deregister(passDayEvent);
            EventBus<NotifySuccessfulCreation>.Deregister(notifySuccessfulCreationEvent);
        }

        /// <summary>
        /// Callback function to add a new work to track
        /// </summary>
        public void AddWorkToTrack(NotifySuccessfulCreation eventData)
        {
            // Add a new work to track
            worksToTrack.Add(eventData.ReviewData.Hash, new Work(
                eventData.ReviewData.Workers,
                eventData.ReviewData.TimeEstimates,
                eventData.ReviewData.Genres,
                eventData.ReviewData.TargetScore,
                eventData.ReviewData.Hash
            ));

            // Send out the tracked works
            Send(new TrackerPayload()
                { Content = worksToTrack },
                AreTypes(new DedicantType[2] 
                {
                    DedicantType.PointGenerator,
                    DedicantType.ErrorGenerator
                })
            );
        }

        /// <summary>
        /// Callback function to track works
        /// </summary>
        private void TrackWorks()
        {
            // Iterate through each key-value pair in the works to track
            foreach(KeyValuePair<int, Work> kvp in worksToTrack)
            {
                // Track each work
                kvp.Value.Track();
            }
        }
    }
}