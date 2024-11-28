using UnityEngine;
using GhostWriter.WorkCreation.Ideation.Audience;

namespace GhostWriter.WorkCreation.UI.Ideation
{
    public class AudienceUI : MonoBehaviour
    {
        private AudienceManager audienceManager;
        [SerializeField] private GameObject audienceTypeContainer;
        [SerializeField] private AudienceButton[] audienceButtons;

        private void Awake()
        {
            // Verify the TopicManager
            if (audienceManager == null)
                audienceManager = GetComponent<AudienceManager>();

            // Get all of the Audience Buttons
            audienceButtons = audienceTypeContainer.GetComponentsInChildren<AudienceButton>(true);

            // Iterate through each Audience Button
            for (int i = 0; i < audienceButtons.Length; i++)
            {
                // Instantiate the Audience Button
                audienceButtons[i].Initialize(audienceManager);
            }
        }

        private void OnEnable()
        {
            // Subscribe to events
            audienceManager.OnAudienceSelected += UpdateButtonSprites;
            audienceManager.OnAudienceCleared += DeselectButtons;
        }

        private void OnDisable()
        {
            // Unsubscribe to events
            audienceManager.OnAudienceSelected -= UpdateButtonSprites;
            audienceManager.OnAudienceCleared -= DeselectButtons;
        }

        /// <summary>
        /// Update Audience Button Sprites depending on if they are selected or not
        /// </summary>
        private void UpdateButtonSprites(AudienceButton latestSelectedButton)
        {
            for(int i = 0; i < audienceButtons.Length; i++)
            {
                if (audienceButtons[i] == latestSelectedButton) continue;

                audienceButtons[i].Deselect();
            }
        }

        /// <summary>
        /// Deslect all Audience Buttons
        /// </summary>
        private void DeselectButtons()
        {
            for (int i = 0; i < audienceButtons.Length; i++)
            {
                audienceButtons[i].Deselect();
            }
        }
    }
}