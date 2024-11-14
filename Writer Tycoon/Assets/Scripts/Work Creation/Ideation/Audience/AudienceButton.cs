using UnityEngine;
using UnityEngine.UI;

namespace GhostWriter.WorkCreation.Ideation.Audience
{
    public class AudienceButton : MonoBehaviour
    {
        private AudienceManager audienceManager;
        private Button button;
        [SerializeField] private AudienceType type;
        [SerializeField] private Color buttonSelectedColor;

        public AudienceType Type { get => type; }
        public bool Selected { get; set; }

        /// <summary>
        /// Instantiate the Audience Button
        /// </summary>
        /// <param name="audienceManager"></param>
        public void Instantiate(AudienceManager audienceManager)
        {
            // Verify the Button component
            if (button == null)
                button = GetComponent<Button>();

            // Set variables
            this.audienceManager = audienceManager;
            Selected = false;

            // Add the event listener
            button.onClick.AddListener(OnClick);
        }

        /// <summary>
        /// Callback function for when the Audience Button is clicked
        /// </summary>
        private void OnClick() => audienceManager.SelectAudience(this);

        /// <summary>
        /// Select the Audience Button
        /// </summary>
        public void Select() => button.targetGraphic.color = buttonSelectedColor;

        /// <summary>
        /// Deslect the Audience Button
        /// </summary>
        public void Deselect() => button.targetGraphic.color = Color.white;
    }
}