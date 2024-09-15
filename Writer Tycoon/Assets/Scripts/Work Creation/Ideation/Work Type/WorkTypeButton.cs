using UnityEngine;
using UnityEngine.UI;

namespace WriterTycoon.WorkCreation.Ideation.WorkTypes
{
    public class WorkTypeButton : MonoBehaviour
    {
        private WorkTypeManager workTypeManager;
        private Button button;
        [SerializeField] private WorkType type;
        [SerializeField] private Color buttonSelectedColor;

        public WorkType Type { get => type; }
        public bool Selected { get; set; }

        public void Instantiate(WorkTypeManager workTypeManager)
        {
            // Verify the Button component
            if (button == null)
                button = GetComponent<Button>();

            // Set variables
            this.workTypeManager = workTypeManager;
            Selected = false;

            // Add the event listener
            button.onClick.AddListener(OnClick);
        }

        /// <summary>
        /// Callback function for when the Work Type Button is clicked
        /// </summary>
        private void OnClick() => workTypeManager.SelectWorkType(this);

        /// <summary>
        /// Select the Work Type Button
        /// </summary>
        public void Select() => button.targetGraphic.color = buttonSelectedColor;

        /// <summary>
        /// Deslect the Work Type Button
        /// </summary>
        public void Deselect() => button.targetGraphic.color = Color.white;
    }
}