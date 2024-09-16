using UnityEngine;
using WriterTycoon.Input;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.World.Interactables;

namespace WriterTycoon.Entities.Player
{
    public class InteractHandler : MonoBehaviour
    {
        [SerializeField] private GameInputReader inputReader;
        private BoxCollider2D boxCollider;
        private IInteractable currentInteractable;
        [SerializeField] string currentInteractableName;
        [SerializeField] private bool interacting;
        [SerializeField] private bool inTrigger;

        private EventBinding<SetInteracting> interactEvent;

        private void OnEnable()
        {
            inputReader.Interact += Interact;

            interactEvent = new EventBinding<SetInteracting>(SetInteracting);
            EventBus<SetInteracting>.Register(interactEvent);
        }

        private void OnDisable()
        {
            inputReader.Interact -= Interact;

            EventBus<SetInteracting>.Deregister(interactEvent);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Set within the trigger
            inTrigger = true;

            // Try to get an Interactable from the collision object
            IInteractable interactable = collision.GetComponent<IInteractable>();

            // Exit case - if the collision object is not an Interactable
            if (interactable == null) return;

            // Set the current Interactable
            currentInteractable = interactable;
            currentInteractableName = collision.name;

            // Highlight the current Interactable
            currentInteractable.Highlight();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // Set within the trigger
            inTrigger = false;

            // Return if currently interacting
            if (interacting) return;

            // Remove the interactor
            RemoveInteractor();
        }

        /// <summary>
        /// Remove interactor data
        /// </summary>
        private void RemoveInteractor()
        {
            // Exit case - if still within the trigger
            if (inTrigger) return;

            // Remove the highlight from the current Interactable
            currentInteractable.RemoveHighlight();

            // Nullify the current Interactable
            currentInteractable = null;
            currentInteractableName = string.Empty;
        }
        
        /// <summary>
        /// Interact with an Interactable within range
        /// </summary>
        public void Interact()
        {
            if(currentInteractable == null) return;

            // Interact with the current interactable
            currentInteractable.Interact();
        }

        /// <summary>
        /// Callback function to set interacting
        /// </summary>
        /// <param name="eventData"></param>
        private void SetInteracting(SetInteracting eventData)
        {
            interacting = eventData.Interacting;

            // Check if still interacting
            if(!interacting)
                // If not, then remove the interactor
                RemoveInteractor();
        }
    }
}