using GhostWriter.World.GeneralUI;
using UnityEngine;
using UnityEngine.UI;

namespace GhostWriter.World.Economy
{
    public class SalesWindowButton : CodeableButton
    {
        private SalesGraphController controller;

        [SerializeField] private Sprite openButton;
        [SerializeField] private Sprite openButtonPressed;
        [SerializeField] private Sprite closeButton;
        [SerializeField] private Sprite closeButtonPressed;

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
        protected override void OnClick()
        {
            // Change the sprite depending on whether or not the window is open or not
            if (controller.Showing)
            {
                // Hide the window
                controller.Hide();

                // Set to open sprites
                SetToOpenSprites();
            }
            else
            {
                // Show the window
                controller.Show();

                // Set to close sprites
                SetToCloseSprites();
            }
        }

        /// <summary>
        /// Set the button sprites to signify closing
        /// </summary>
        public void SetToCloseSprites()
        {
            // Set the base sprite
            button.image.sprite = closeButton;

            // Set up the new sprite state
            SpriteState spriteState = button.spriteState;
            spriteState.pressedSprite = closeButtonPressed;

            // Assign the updated sprite state
            button.spriteState = spriteState;
        }

        /// <summary>
        /// Set the button sprites to signify opening
        /// </summary>
        public void SetToOpenSprites()
        {
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