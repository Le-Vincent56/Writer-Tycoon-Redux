using UnityEngine;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.World.Interactables
{
    public class Computer : Interactable
    {
        [Header("Computer")]
        [SerializeField] private bool currentlyWorking;
        [SerializeField] private bool opening;

        private EventBinding<NotifySuccessfulCreation> notifySuccessfulCreationEvent;

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
            // Change the computer's state
            currentlyWorking = true;
        }

        /// <summary>
        /// Interact with the Computer
        /// </summary>
        //public override void Interact()
        //{
            

        //    //// Check if currently working
        //    //if (currentlyWorking)
        //    //    // Then continue working on interaction
        //    //    ContinueWorking();
        //    //else
        //    //    // Otherwise, open the Create Work menu
        //    //    OpenCreateWorkMenu();
        //}

        /// <summary>
        /// Continue working on interaction
        /// </summary>
        private void ContinueWorking()
        {
            // Change the player work state
            EventBus<ChangePlayerWorkState>.Raise(new ChangePlayerWorkState()
            {
                Working = true
            });
        }

        /// <summary>
        /// Open the Create Work menu on interaction
        /// </summary>
        private void OpenCreateWorkMenu()
        {
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

            // Notify interacting
            EventBus<SetInteracting>.Raise(new SetInteracting()
            {
                Interacting = opening
            });

            // Flip opening state
            opening = !opening;
        }
    }
}