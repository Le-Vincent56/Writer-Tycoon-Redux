using UnityEngine;
using GhostWriter.Patterns.EventBus;

namespace GhostWriter.WorkCreation.Ideation.WorkTypes
{
    public class WorkTypeUI : MonoBehaviour
    {
        private WorkTypeManager workTypeManager;
        [SerializeField] private GameObject workTypeContainer;
        [SerializeField] private WorkTypeButton[] workTypeButtons;

        private void Awake()
        {
            // Verify the TopicManager
            if (workTypeManager == null)
                workTypeManager = GetComponent<WorkTypeManager>();

            // Get all of the Work Type Buttons
            workTypeButtons = workTypeContainer.GetComponentsInChildren<WorkTypeButton>(true);

            // Iterate through each Work Type Button
            for (int i = 0; i < workTypeButtons.Length; i++)
            {
                // Instantiate the Work Type Button
                workTypeButtons[i].Instantiate(workTypeManager);
            }
        }

        private void OnEnable()
        {
            // Subscribe to events
            workTypeManager.OnWorkTypeSelected += UpdateButtonSprites;
            workTypeManager.OnWorkTypeCleared += DeselectButtons;
        }

        private void OnDisable()
        {
            // Unsubscribe to events
            workTypeManager.OnWorkTypeSelected -= UpdateButtonSprites;
            workTypeManager.OnWorkTypeCleared -= DeselectButtons;
        }

        /// <summary>
        /// Update Work Type Button Sprites depending on if they are selected or not
        /// </summary>
        private void UpdateButtonSprites(WorkTypeButton latestSelectedButton)
        {
            for (int i = 0; i < workTypeButtons.Length; i++)
            {
                if (workTypeButtons[i] == latestSelectedButton) continue;

                workTypeButtons[i].Deselect();
            }
        }

        /// <summary>
        /// Clear the work type by deselecting each button
        /// </summary>
        private void DeselectButtons()
        {
            for(int i = 0; i < workTypeButtons.Length; i++)
            {
                workTypeButtons[i].Deselect();
            }
        }
    }
}