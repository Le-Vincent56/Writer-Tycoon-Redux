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
        }

        private void OnDisable()
        {
            EventBus<NotifySuccessfulCreation>.Deregister(notifySuccessfulCreationEvent);
        }

        private void ChangeButtonsOnWorkCreation(NotifySuccessfulCreation eventData)
        {
            // Disable the "New" button
            newWorkButton.Disable();

            // Enable the "Work" button
            continueWorkButton.Enable();
        }
    }
}