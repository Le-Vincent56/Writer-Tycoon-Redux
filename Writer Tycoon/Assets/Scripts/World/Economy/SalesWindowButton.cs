using UnityEngine;
using UnityEngine.UI;

namespace WriterTycoon.World.Economy
{
    public class SalesWindowButton : MonoBehaviour
    {
        private SalesGraphController controller;

        [SerializeField] private bool open;
        private Button button;

        [SerializeField] private Sprite openButton;
        [SerializeField] private Sprite openButtonPressed;
        [SerializeField] private Sprite closeButton;
        [SerializeField] private Sprite closeButtonPressed;

        private void Awake()
        {
            // Verify the Button component
            if (button == null)
                button = GetComponent<Button>();

            // Set open to false
            open = false;

            // Add listeners
            button.onClick.AddListener(ToggleSprite);
        }

        /// <summary>
        /// Initialize the Sales Window Button
        /// </summary>
        public void Initialize(SalesGraphController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Toggle the Button sprite
        /// </summary>
        private void ToggleSprite()
        {
            // Toggle whether the window is open or not
            open = !open;

            // Change the sprite depending on whether or not the window is open or not
            if (open)
            {
                // Show the window
                controller.Show();

                // Set the base sprite
                button.image.sprite = closeButton;

                // Set up the new sprite state
                SpriteState spriteState = button.spriteState;
                spriteState.pressedSprite = closeButtonPressed;

                // Assign the updated sprite state
                button.spriteState = spriteState;
            }
            else
            {
                // Hide the window
                controller.Hide();

                // Set the base sprite
                button.image.sprite = openButton;

                // Set up the new sprite state
                SpriteState spriteState = button.spriteState;
                spriteState.pressedSprite = openButtonPressed;

                // Assign the updated sprite state
                button.spriteState = spriteState;
            }
        }
    }
}