using UnityEngine;
using WriterTycoon.Input;
using WriterTycoon.World.Interactables;

namespace WriterTycoon.Entities.Player
{
    public class InteractHandler : MonoBehaviour
    {
        [SerializeField] private GameInputReader inputReader;
        private BoxCollider2D boxCollider;
        private IInteractable currentInteractable;
        [SerializeField] string currentInteractableName;

        private void OnEnable()
        {
            inputReader.Interact += Interact;
        }

        private void OnDisable()
        {
            inputReader.Interact -= Interact;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
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
    }
}