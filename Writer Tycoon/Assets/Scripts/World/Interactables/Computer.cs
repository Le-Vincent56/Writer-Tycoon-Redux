using UnityEngine;
using WriterTycoon.WorkCreation.UI;

namespace WriterTycoon.World.Interactables
{
    public class Computer : MonoBehaviour, IInteractable
    {
        [SerializeField] CreateGameWindow gameWindow;
        [SerializeField] private bool opening;

        public void Interact()
        {
            if (opening)
                gameWindow.ShowWindow();
            else
                gameWindow.HideWindow();

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