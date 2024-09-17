using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.Command;

namespace WriterTycoon.World.Interactables.UI
{
    public class MenuActionButton : MonoBehaviour
    {
        private Button button;
        private ICommand command;

        /// <summary>
        /// Initialize the Menu Action Button
        /// </summary>
        /// <param name="command"></param>
        public void Initialize(ICommand command)
        {
            // Verify the Button
            if(button == null)
                button = GetComponent<Button>();

            // Set the command
            this.command = command;

            // Add the event listener
            button.onClick.AddListener(OnClick);
        }

        /// <summary>
        /// Callback function for handling click
        /// </summary>
        private void OnClick() => command?.Execute();
    }
}