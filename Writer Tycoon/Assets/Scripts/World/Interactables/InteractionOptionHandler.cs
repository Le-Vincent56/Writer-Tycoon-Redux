using UnityEngine;
using WriterTycoon.Patterns.Command;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.World.Interactables.UI
{
    public class InteractionOptionHandler : MonoBehaviour
    {
        [SerializeField] private MenuActionButton newWorkButton;
        [SerializeField] private MenuActionButton continueWorkButton;
        [SerializeField] private MenuActionButton publishWorkButton;
        [SerializeField] private MenuActionButton eatButton;

        private EventBinding<NotifySuccessfulCreation> notifySuccessfulCreationEvent;
        private EventBinding<EndDevelopment> endDevelopmentEvent;
        private EventBinding<EndEditing> onEditingEndEvent;

        private void Awake()
        {
            // Create a standard command factory for menu actions
            StandardMenuActionCommandFactory commandFactory = new StandardMenuActionCommandFactory();

            // Initialize the Menu Action Buttons
            newWorkButton.Initialize(commandFactory.CreateMenuActionCommand("New Work"));
            continueWorkButton.Initialize(commandFactory.CreateMenuActionCommand("Continue Work"));
            publishWorkButton.Initialize(commandFactory.CreateMenuActionCommand("Publish Work"));
            eatButton.Initialize(commandFactory.CreateMenuActionCommand("Eat"));

            // Disable the "Work" button
            continueWorkButton.Disable();
            publishWorkButton.Disable();
        }

        private void OnEnable()
        {
            notifySuccessfulCreationEvent = new EventBinding<NotifySuccessfulCreation>(ChangeButtonsOnWorkCreation);
            EventBus<NotifySuccessfulCreation>.Register(notifySuccessfulCreationEvent);

            endDevelopmentEvent = new EventBinding<EndDevelopment>(ChangeButtonsOnDevelopmentEnd);
            EventBus<EndDevelopment>.Register(endDevelopmentEvent);

            onEditingEndEvent = new EventBinding<EndEditing>(ChangeButtonsOnEditingEnd);
            EventBus<EndEditing>.Register(onEditingEndEvent);
        }

        private void OnDisable()
        {
            EventBus<NotifySuccessfulCreation>.Deregister(notifySuccessfulCreationEvent);
            EventBus<EndEditing>.Deregister(onEditingEndEvent);
        }

        /// <summary>
        /// Callback function for changing button interactability after work creation
        /// </summary>
        private void ChangeButtonsOnWorkCreation(NotifySuccessfulCreation eventData)
        {
            // Disable the "New" button
            //newWorkButton.Disable();

            // Enable the "Work" button
            continueWorkButton.Enable();

            // Disable the "Publish" button
            publishWorkButton.Disable();
        }

        /// <summary>
        /// Callback function for changing button interactability after development end
        /// </summary>
        private void ChangeButtonsOnDevelopmentEnd(EndDevelopment eventData)
        {
            // Disable the "New" button
            //newWorkButton.Disable();

            // Enable the "Work" button
            continueWorkButton.Enable();

            // Enable the "Publish" button
            publishWorkButton.Enable();
        }

        /// <summary>
        /// Callback function for changing button interactability after editing end
        /// </summary>
        private void ChangeButtonsOnEditingEnd(EndEditing eventData)
        {
            // Enable the "New" button
            newWorkButton.Enable();

            // Disable the "Work" button
            continueWorkButton.Disable();

            // Disable the "Publish" button
            publishWorkButton.Disable();
        }
    }
}
