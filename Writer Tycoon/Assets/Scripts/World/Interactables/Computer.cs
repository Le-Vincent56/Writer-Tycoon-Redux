using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.UI;

namespace WriterTycoon.World.Interactables
{
    public class Computer : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool opening;

        private EventBinding<NotifySuccessfulCreation> notifySuccessfulCreationEvent;

        public bool Interactable { get; set; }

        private void Awake()
        {
            Interactable = true;
        }

        private void OnEnable()
        {
            notifySuccessfulCreationEvent = new EventBinding<NotifySuccessfulCreation>(HandleSuccessfulCreation);
            EventBus<NotifySuccessfulCreation>.Register(notifySuccessfulCreationEvent);
        }

        private void OnDisable()
        {
            EventBus<NotifySuccessfulCreation>.Deregister(notifySuccessfulCreationEvent);
        }

        /// <summary>
        /// Callback for handling the successful creation of a Work
        /// </summary>
        /// <param name="successfulCreation"></param>
        private void HandleSuccessfulCreation(NotifySuccessfulCreation eventData)
        {
            // Don't allow the computer to be interacted with
            Interactable = false;
        }

        /// <summary>
        /// Interact with the Computer
        /// </summary>
        public void Interact()
        {
            // Exit case - if cannot be interacted with
            if (!Interactable) return;

            // Pause the Calendar
            EventBus<ChangeCalendarPauseState>.Raise(new ChangeCalendarPauseState()
            {
                Paused = opening,
                AllowSpeedChanges = !opening
            });

            // Open the Create Work Menu
            EventBus<OpenCreateWorkMenu>.Raise(new OpenCreateWorkMenu
            {
                IsOpening = opening
            });

            // Flip opening state
            opening = !opening;
        }

        public void Highlight()
        {

        }

        public void RemoveHighlight()
        {

        }
    }
}