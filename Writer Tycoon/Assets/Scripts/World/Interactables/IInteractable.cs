using UnityEngine;

namespace WriterTycoon.World.Interactables
{
    public interface IInteractable
    {
        void ResetInteractable();
        void Interact(Vector2 cursorPosition);
        void Highlight();
        void RemoveHighlight();
    }
}
