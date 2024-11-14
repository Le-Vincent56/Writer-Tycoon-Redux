using UnityEngine;
using UnityEngine.UI;

namespace GhostWriter.WorkCreation.Ideation.Genres
{
    public class GenreButton : MonoBehaviour
    {
        [SerializeField] private Image[] masteryIcons = new Image[3];
        [SerializeField] private Color buttonSelectedColor;
        [SerializeField] private Color unmasteredColor;

        private GenreManager genreManager;
        private Button button;
        public Genre Genre { get; private set; }

        /// <summary>
        /// Initialize the Genre Button
        /// </summary>
        public void Instantiate(GenreManager genreManager, Genre topic)
        {
            // Verify the Button component
            if (button == null)
                button = GetComponent<Button>();

            // Set variables
            this.genreManager = genreManager;
            Genre = topic;

            // Check if the Genre is unlocked
            if (Genre.IsUnlocked)
                // If so, allow it to be interacted with
                button.interactable = true;
            else
                // Otherwise, don't allow it be interacted with
                button.interactable = false;

            // Update the mastery icons
            UpdateMasteryIcons();

            // Add the event listener
            button.onClick.AddListener(OnClick);
        }

        /// <summary>
        /// Callback function for when the Genre Button is clicked
        /// </summary>
        private void OnClick() => genreManager.SelectGenre(this);

        /// <summary>
        /// Select the Genre Button
        /// </summary>
        public void Select() => button.targetGraphic.color = buttonSelectedColor;

        /// <summary>
        /// Deslect the Genre Button
        /// </summary>
        public void Deselect() => button.targetGraphic.color = Color.white;

        /// <summary>
        /// Update the Button's mastery icons
        /// </summary>
        public void UpdateMasteryIcons()
        {
            // Iterate through each mastery icon
            for (int i = 0; i < masteryIcons.Length; i++)
            {
                // Check if the index is less than or equal to the mastery level
                // of the Topic
                if (i <= Genre.MasteryLevel - 1)
                    // If so, use a mastered icon (colored normally)
                    masteryIcons[i].color = Color.white;
                else
                    // otherwise, use an unmastered icon (colored black)
                    masteryIcons[i].color = unmasteredColor;
            }
        }
    }
}