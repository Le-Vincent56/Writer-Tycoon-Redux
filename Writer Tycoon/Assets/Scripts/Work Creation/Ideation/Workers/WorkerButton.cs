using UnityEngine;
using UnityEngine.UI;
using GhostWriter.Entities;
using GhostWriter.World.GeneralUI;

namespace GhostWriter.WorkCreation.Ideation.Workers
{
    public class WorkerButton : CodeableButton
    {
        [SerializeField] private Color buttonSelectedColor;

        private WorkerManager workerManager;
        private Text nameText;

        public IWorker Worker { get; private set; }

        protected override void Awake()
        {
            // Noop
        }

        /// <summary>
        /// Initialize the Topic Button
        /// </summary>
        public void Initialize(WorkerManager workerManager, IWorker worker)
        {
            // Verify the Button component
            if (button == null)
                button = GetComponent<Button>();

            // Verify the Text component
            if (nameText == null)
                nameText = GetComponentInChildren<Text>();

            // Set variables
            this.workerManager = workerManager;
            Worker = worker;
            nameText.text = worker.Name;

            // Add the event listener
            button.onClick.AddListener(OnClick);
        }

        /// <summary>
        /// Callback function for when the Worker Button is clicked
        /// </summary>
        protected override void OnClick() => workerManager.SelectWorker(this);

        /// <summary>
        /// Select the Worker Button
        /// </summary>
        public void Select() => button.targetGraphic.color = buttonSelectedColor;

        /// <summary>
        /// Deslect the Worker Button
        /// </summary>
        public void Deselect() => button.targetGraphic.color = Color.white;
    }
}