using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.Command;

namespace WriterTycoon.World.Interactables.UI
{
    public class MenuActionButton : MonoBehaviour
    {
        [SerializeField] private Color disabledColor;
        private Button button;
        private Text buttonText;
        private Image buttonIcon;
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

            // Verify the button text
            if(buttonText == null)
                buttonText = GetComponentInChildren<Text>();

            // Verify the button icon
            if (buttonIcon == null)
                buttonIcon = GetComponentsInChildren<Image>().First(go => go.gameObject != gameObject);

            // Set the command
            this.command = command;

            // Add the event listener
            button.onClick.AddListener(OnClick);
        }

        /// <summary>
        /// Disable the button
        /// </summary>
        public void Disable()
        {
            button.interactable = false;
            buttonText.color = disabledColor;
            buttonIcon.color = disabledColor;
        }

        /// <summary>
        /// Enable the button
        /// </summary>
        public void Enable()
        {
            button.interactable = true;
            buttonText.color = Color.black;
            buttonIcon.color = Color.white;
        }

        /// <summary>
        /// Callback function for handling click
        /// </summary>
        private void OnClick() => command?.Execute();
    }
}