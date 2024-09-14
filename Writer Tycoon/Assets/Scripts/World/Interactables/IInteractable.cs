namespace WriterTycoon.World.Interactables
{
    public interface IInteractable
    {
        bool Interactable { get; set; }
        void Interact();
        void Highlight();
        void RemoveHighlight();
    }
}
