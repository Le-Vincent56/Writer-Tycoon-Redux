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

        private void Awake()
        {
            // Verify the Sprite Renderer
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();

            interactable = true;
            openMenu = true;
        }

        public virtual void Interact()
        {
            // Exit case - if cannot be interacted with
            if (!interactable) return;

            EventBus<HandleInteractMenu>.Raise(new HandleInteractMenu()
            {
                Opening = openMenu,
                InteractableType = type
            });

            // Toggle open menu
            openMenu = !openMenu;
        }

        public virtual void Highlight() => spriteRenderer.material = highlightMaterial;

        public virtual void RemoveHighlight() => spriteRenderer.material = defaultMaterial;
    }
}