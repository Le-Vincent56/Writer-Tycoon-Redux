using UnityEngine;
using GhostWriter.Patterns.EventBus;

namespace GhostWriter.World.Interactables
{
    public enum InteractableType
    {
        None,
        Bookshelf,
        Desk,
        Fridge
    }

    public class Interactable : MonoBehaviour, IInteractable
    {
        [Header("Highlight")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material highlightMaterial;

        [Header("Interactable")]
        [SerializeField] protected InteractableType type;
        [SerializeField] protected bool interactable;
        [SerializeField] protected bool openMenu;

        private EventBinding<CloseInteractMenus> closeInteractMenusEvent;

        private void Awake()
        {
            // Verify the Sprite Renderer
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();

            interactable = true;
            openMenu = true;
        }

        private void OnEnable()
        {
            closeInteractMenusEvent = new EventBinding<CloseInteractMenus>(ResetMenu);
            EventBus<CloseInteractMenus>.Register(closeInteractMenusEvent);
        }

        private void OnDisable()
        {
            EventBus<CloseInteractMenus>.Deregister(closeInteractMenusEvent);
        }

        /// <summary>
        /// Interact with the Interactable
        /// </summary>
        public virtual void Interact(Vector2 cursorPosition)
        {
            // Exit case - if cannot be interacted with
            if (!interactable) return;

            EventBus<SetInteracting>.Raise(new SetInteracting()
            {
                Interacting = true,
            });

            EventBus<ToggleInteractMenu>.Raise(new ToggleInteractMenu()
            {
                Interactable = this,
                CursorPosition = cursorPosition,
                Opening = openMenu,
                InteractableType = type
            });
        }

        /// <summary>
        /// Callback function to reset the menu on close
        /// </summary>
        protected void ResetMenu(CloseInteractMenus eventData)
        {
            // Reset the Interactable
            ResetInteractable();
        }

        /// <summary>
        /// Reset the Interactable
        /// </summary>
        public void ResetInteractable()
        {
            // Set open menu to true as it would have closed
            openMenu = true;
        }

        /// <summary>
        /// Set whether or not the Interactable's menu should open on interact
        /// </summary>
        public void SetOpenMenu(bool openMenu) => this.openMenu = openMenu;

        /// <summary>
        /// Highlight the Interactable
        /// </summary>
        public virtual void Highlight() => spriteRenderer.material = highlightMaterial;

        /// <summary>
        /// Remove the Interactable's highlight
        /// </summary>
        public virtual void RemoveHighlight() => spriteRenderer.material = defaultMaterial;
    }
}