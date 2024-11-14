using UnityEngine;
using GhostWriter.Patterns.Command;
using GhostWriter.Patterns.EventBus;

namespace GhostWriter.World.Interactables.UI
{
    public class InteractionOptionHandler : MonoBehaviour
    {
        [SerializeField] private MenuActionButton publicationHistoryButton;
        [SerializeField] private MenuActionButton newWorkButton;
        [SerializeField] private MenuActionButton continueWorkButton;
        [SerializeField] private MenuActionButton eatButton;

        private EventBinding<NotifySuccessfulCreation> notifySuccessfulCreationEvent;
        private EventBinding<EndDevelopment> endDevelopmentEvent;

        private void Awake()
        {
            // Create a standard command factory for menu actions
            StandardMenuActionCommandFactory commandFactory = new();

            // Initialize the Menu Action Buttons
            publicationHistoryButton.Initialize(commandFactory.CreateMenuActionCommand("Publication History"));
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

            endDevelopmentEvent = new EventBinding<EndDevelopment>(ChangeButtonsOnDevelopmentEnd);
            EventBus<EndDevelopment>.Register(endDevelopmentEvent);
        }

        private void OnDisable()
        {
            EventBus<NotifySuccessfulCreation>.Deregister(notifySuccessfulCreationEvent);
        }

        /// <summary>
        /// Callback function for changing button interactability after work creation
        /// </summary>
        private void ChangeButtonsOnWorkCreation(NotifySuccessfulCreation eventData)
        {
            // Enable the "Work" button
            continueWorkButton.Enable();
        }

        /// <summary>
        /// Callback function for changing button interactability after development end
        /// </summary>
        private void ChangeButtonsOnDevelopmentEnd(EndDevelopment eventData)
        {
            // Enable the "Work" button
            continueWorkButton.Enable();
        }
    }
}
