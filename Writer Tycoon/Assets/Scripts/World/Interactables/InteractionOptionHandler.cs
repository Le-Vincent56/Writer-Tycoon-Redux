using UnityEngine;
using WriterTycoon.Patterns.Command;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.World.Interactables.UI
{
    public class InteractionOptionHandler : MonoBehaviour
    {
        [SerializeField] private MenuActionButton newWorkButton;
        [SerializeField] private MenuActionButton continueWorkButton;
        [SerializeField] private MenuActionButton eatButton;

        private EventBinding<NotifySuccessfulCreation> notifySuccessfulCreationEvent;
        private EventBinding<EndDevelopment> endDevelopmentEndEvent;

        private void Awake()
        {
            // Create a standard command factory for menu actions
            StandardMenuActionCommandFactory commandFactory = new StandardMenuActionCommandFactory();

            // Initialize the Menu Action Buttons
            newWorkButton.Initialize(commandFactory.CreateMenuActionCommand("New Work"));
            continueWorkButton.Initialize(commandFactory.CreateMenuActionCommand("Continue Work"));
            eatButton.Initialize(commandFactory.CreateMenuActionCommand("Eat"));

            // Disable the "Work" button
            continueWorkButton.Disable();
        }

        private void OnEnable()
        {
            notifySuccessfulCreationEvent = new EventBinding<NotifySuccessfulCreation>(ChangeButtonsOnWorkCreation);
            EventBus<NotifySuccessfulCreation>.Register(notifySuccessfulCreationEvent);

            endDevelopmentEndEvent = new EventBinding<EndDevelopment>(ChangeButtonsOnDevelopmentEnd);
            EventBus<EndDevelopment>.Register(endDevelopmentEndEvent);
        }

        private void OnDisable()
        {
            EventBus<NotifySuccessfulCreation>.Deregister(notifySuccessfulCreationEvent);
            EventBus<EndDevelopment>.Deregister(endDevelopmentEndEvent);
        }

        /// <summary>
        /// Callback function for changing button interactability after work creation
        /// </summary>
        private void ChangeButtonsOnWorkCreation(NotifySuccessfulCreation eventData)
        {
            // Disable the "New" button
            newWorkButton.Disable();

            // Enable the "Work" button
            continueWorkButton.Enable();
        }

        /// <summary>
        /// Callback function for changing button interactability after development end
        /// </summary>
        private void ChangeButtonsOnDevelopmentEnd(EndDevelopment eventData)
        {
            // Enable the "New" button
            newWorkButton.Enable();

            // Disable the "Work" button
            continueWorkButton.Disable();
        }
    }
}
