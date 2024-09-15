using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Development.Tracker
{
    public class WorkTracker : Dedicant
    {
        [SerializeField] private bool working;
        [SerializeField] private int currentDay;
        [SerializeField] private int dayEstimate;

        public override string Name => "Work Tracker";
        public override DedicantType Type => DedicantType.Tracker;

        private EventBinding<PassDay> passDayEvent;
        private EventBinding<ChangePlayerWorkState> changePlayerWorkStateEvent;

        private void OnEnable()
        {
            passDayEvent = new EventBinding<PassDay>(UpdateTracker);
            EventBus<PassDay>.Register(passDayEvent);

            changePlayerWorkStateEvent = new EventBinding<ChangePlayerWorkState>(ChangeWorkState);
            EventBus<ChangePlayerWorkState>.Register(changePlayerWorkStateEvent);
        }

        private void OnDisable()
        {
            EventBus<PassDay>.Deregister(passDayEvent);
            EventBus<ChangePlayerWorkState>.Deregister(changePlayerWorkStateEvent);
        }

        /// <summary>
        /// Callback event to update the Tracker
        /// </summary>
        private void UpdateTracker()
        {
            // Exit case - if the Player is not working
            if (!working) return;

            // Increment the current day
            currentDay++;

            EventBus<UpdateProgressData>.Raise(new UpdateProgressData()
            {
                Current = currentDay,
                Maximum = dayEstimate,
            });
        }

        /// <summary>
        /// Callback event to reflect the Player's Work State changes
        /// </summary>
        private void ChangeWorkState(ChangePlayerWorkState eventData) => working = eventData.Working;

        /// <summary>
        /// Set the time estimate in days
        /// </summary>
        public void SetTimeEstimate(int dayEstimate) => this.dayEstimate = dayEstimate;
    }
}