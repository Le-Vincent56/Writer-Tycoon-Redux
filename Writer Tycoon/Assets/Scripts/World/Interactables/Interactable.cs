using UnityEngine;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.World.Interactables
{
    public enum InteractableType
    {
        Computer,
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

        public virtual void Interact(Vector2 cursorPosition)
        {
            // Exit case - if cannot be interacted with
            if (!interactable) return;

            EventBus<ToggleInteractMenu>.Raise(new ToggleInteractMenu()
            {
                CursorPosition = cursorPosition,
                Opening = openMenu,
                InteractableType = type
            });

            // Toggle open menu
            openMenu = !openMenu;
        }

        /// <summary>
        /// Callback function to reset the menu on close
        /// </summary>
        protected void ResetMenu(CloseInteractMenus eventData)
        {
            // Reset the Interactable
            ResetInteractable();
        }

        public void ResetInteractable()
        {
            // Set open menu to true as it would have closed
            openMenu = true;
        }

        public virtual void Highlight() => spriteRenderer.material = highlightMaterial;

        public virtual void RemoveHighlight() => spriteRenderer.material = defaultMaterial;
    }
}