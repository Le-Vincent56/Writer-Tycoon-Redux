using GhostWriter.World.GeneralUI;
using UnityEngine;
using UnityEngine.UI;

namespace GhostWriter.WorkCreation.Ideation.WorkTypes
{
    public class WorkTypeButton : CodeableButton
    {
        private WorkTypeManager workTypeManager;
        [SerializeField] private WorkType type;
        [SerializeField] private float targetScore;
        [SerializeField] private Color buttonSelectedColor;

        public WorkType Type { get => type; }
        public float TargetScore { get => targetScore; }
        public bool Selected { get; set; }

        protected override void Awake()
        {
            // Noop
        }

        /// <summary>
        /// Initialize the Work Type Button
        /// </summary>
        /// <param name="workTypeManager"></param>
        public void Initialize(WorkTypeManager workTypeManager)
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
        protected override void OnClick() => workTypeManager.SelectWorkType(this);

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