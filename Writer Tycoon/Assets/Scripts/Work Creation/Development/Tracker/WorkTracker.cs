using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Mediation;
using static UnityEngine.Rendering.DebugUI;

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
        private EventBinding<EndEditing> endEditingEvent;

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

            endEditingEvent = new EventBinding<EndEditing>(PublishAndStopTracking);
            EventBus<EndEditing>.Register(endEditingEvent);
        }

        private void OnDisable()
        {
            EventBus<PassDay>.Deregister(passDayEvent);
            EventBus<NotifySuccessfulCreation>.Deregister(notifySuccessfulCreationEvent);
            EventBus<EndEditing>.Deregister(endEditingEvent);
        }

        /// <summary>
        /// Callback function to add a new Work to track
        /// </summary>
        private void AddWorkToTrack(NotifySuccessfulCreation eventData)
        {
            // Add a new work to track
            worksToTrack.Add(eventData.ReviewData.Hash, new Work(
                eventData.ReviewData.AboutInfo,
                eventData.ReviewData.CompatibilityInfo,
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
        /// Callback function to stop tracking a Work
        /// </summary>
        private void PublishAndStopTracking(EndEditing eventData)
        {
            if(worksToTrack.TryGetValue(eventData.Hash, out Work work))
            {
                // Publish the Work
                EventBus<PublishWork>.Raise(new PublishWork()
                {
                    WorkToPublish = work
                });

                // Remove the work using the hash
                worksToTrack.Remove(eventData.Hash);

                // Update all the tracked works
                Send(new TrackerPayload()
                { Content = worksToTrack },
                    AreTypes(new DedicantType[3]
                    {
                    DedicantType.PointGenerator,
                    DedicantType.ErrorGenerator,
                    DedicantType.Editor
                    })
                );
            }
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