using UnityEngine;
using GhostWriter.Input;
using GhostWriter.Patterns.EventBus;
using GhostWriter.World.Interactables;
using GhostWriter.World.GeneralUI;
using GhostWriter.Patterns.ServiceLocator;

namespace GhostWriter.Entities.Player
{
    public class InteractHandler : MonoBehaviour
    {
        [SerializeField] private GameInputReader inputReader;
        private IInteractable currentInteractable;
        [SerializeField] private string currentInteractableName;
        [SerializeField] private bool canInteract;
        [SerializeField] private bool interacting;
        [SerializeField] private bool inTrigger;

        private WindowTracker windowTracker;

        private EventBinding<SetCanInteract> setCanInteractEvent;
        private EventBinding<SetInteracting> interactEvent;

        private void Awake()
        {
            // Set can interact
            canInteract = true;
        }

        private void OnEnable()
        {
            inputReader.Interact += Interact;

            setCanInteractEvent = new EventBinding<SetCanInteract>(SetCanInteract);
            EventBus<SetCanInteract>.Register(setCanInteractEvent);

            interactEvent = new EventBinding<SetInteracting>(SetInteracting);
            EventBus<SetInteracting>.Register(interactEvent);
        }

        private void OnDisable()
        {
            inputReader.Interact -= Interact;

            EventBus<SetCanInteract>.Deregister(setCanInteractEvent);
            EventBus<SetInteracting>.Deregister(interactEvent);
        }

        private void Start()
        {
            // Get the Window Tracker
            windowTracker = ServiceLocator.ForSceneOf(this).Get<WindowTracker>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Exit case - a UI window is open
            if (windowTracker.CheckIfAnyOpen()) return;

            // Set within the trigger
            inTrigger = true;

            // Exit case - if the collision object is not an Interactable
            if (!collision.TryGetComponent(out IInteractable interactable)) return;

            // If a current interactable exists, remove its highlight and reset it
            currentInteractable?.RemoveHighlight();
            currentInteractable?.ResetInteractable();

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

            CheckRemoveInteractor();
        }

        /// <summary>
        /// Check whether or not to remove interactor data
        /// </summary>
        private void CheckRemoveInteractor()
        {
            // Exit case - if still within the trigger
            if (inTrigger) return;

            // Remove the highlight from the current Interactable
            currentInteractable?.RemoveHighlight();

            // Nullify the current Interactable
            currentInteractable?.ResetInteractable();
            currentInteractable = null;
            currentInteractableName = string.Empty;
        }
        
        /// <summary>
        /// Interact with an Interactable within range
        /// </summary>
        public void Interact()
        {
            // Exit case - if the player cannot interact
            if (!canInteract) return;

            // Exit case - if there's no current Interactable
            if (currentInteractable == null)
            {
                // Close all interact menus
                EventBus<CloseInteractMenus>.Raise(new CloseInteractMenus());
                return;
            }

            // Interact with the current interactable
            currentInteractable.Interact(transform.position);
        }

        /// <summary>
        /// Callback function to set if the player is able to interact with objects
        /// </summary>
        private void SetCanInteract(SetCanInteract eventData)
        {
            canInteract = eventData.CanInteract;
        }

        /// <summary>
        /// Callback function to set interacting
        /// </summary>
        private void SetInteracting(SetInteracting eventData)
        {
            interacting = eventData.Interacting;

            // Check if not interacting
            if (!interacting)
                // If so, check to remove
                CheckRemoveInteractor();
        }
    }
}