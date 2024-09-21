using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Editing
{
    public class PolishManager : Dedicant
    {
        [SerializeField] private bool working;
        [SerializeField] private bool polish;
        [SerializeField] private float totalErrors;
        [SerializeField] private float currentErrors;
        [SerializeField] private float polishRate;

        private EventBinding<ConfirmPlayerWorkState> confirmPlayerWorkState;
        private EventBinding<PassDay> passDayEvent;
        private EventBinding<BeginEditing> beginEditingEvent;
        private EventBinding<EndEditing> endEditingEvent;

        public override DedicantType Type => DedicantType.Editor;
        public override string Name => "Editing Manager";

        private void OnEnable()
        {
            confirmPlayerWorkState = new EventBinding<ConfirmPlayerWorkState>(ChangeWorkState);
            EventBus<ConfirmPlayerWorkState>.Register(confirmPlayerWorkState);

            passDayEvent = new EventBinding<PassDay>(Polish);
            EventBus<PassDay>.Register(passDayEvent);

            beginEditingEvent = new EventBinding<BeginEditing>(BeginPolish);
            EventBus<BeginEditing>.Register(beginEditingEvent);

            endEditingEvent = new EventBinding<EndEditing>(EndPolish);
            EventBus<EndEditing>.Register(endEditingEvent);
        }

        private void OnDisable()
        {
            EventBus<ConfirmPlayerWorkState>.Deregister(confirmPlayerWorkState);
            EventBus<PassDay>.Deregister(passDayEvent);
            EventBus<BeginEditing>.Deregister(beginEditingEvent);
            EventBus<EndEditing>.Deregister(endEditingEvent);
        }

        /// <summary>
        /// Polish by removing errors
        /// </summary>
        private void Polish()
        {
            // Exit case - if not working
            if (!working) return;

            // Exit case - if not polishing
            if (!polish) return;

            // Check if there are errors to remove
            if(currentErrors > 0)
            {
                // Generate anywhere between 1 - 5% of errors
                polishRate = Random.Range(totalErrors * 0.01f, totalErrors * 0.05f);

                // Subtract from the total errors
                currentErrors -= polishRate;

                return;
            } 

            // TODO: Provide polish by generating more points
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
        /// Callback event to confirm the player is working
        /// </summary>
        private void ChangeWorkState(ConfirmPlayerWorkState eventData) => working = eventData.Working;
    }
}