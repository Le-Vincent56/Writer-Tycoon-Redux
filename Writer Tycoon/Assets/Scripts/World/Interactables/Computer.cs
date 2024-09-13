using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.UI;

namespace WriterTycoon.World.Interactables
{
    public class Computer : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool opening;

        public void Interact()
        {
            EventBus<OpenWorkMenuEvent>.Raise(new OpenWorkMenuEvent
            {
                IsOpening = opening
            });

            opening = !opening;
        }

        public void Highlight()
        {

        }

        public void RemoveHighlight()
        {

        }
    }
}