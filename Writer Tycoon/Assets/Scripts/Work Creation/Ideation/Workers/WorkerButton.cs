using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GhostWriter.Entities;

namespace GhostWriter.WorkCreation.Ideation.Workers
{
    public class WorkerButton : MonoBehaviour
    {
        [SerializeField] private Color buttonSelectedColor;

        private WorkerManager workerManager;
        private Button button;
        private Text nameText;

        public IWorker Worker { get; private set; }

        /// <summary>
        /// Initialize the Topic Button
        /// </summary>
        public void Instantiate(WorkerManager workerManager, IWorker worker)
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
        private void OnClick() => workerManager.SelectWorker(this);

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