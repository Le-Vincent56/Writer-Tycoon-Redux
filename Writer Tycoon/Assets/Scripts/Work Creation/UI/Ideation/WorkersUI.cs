using System.Collections.Generic;
using UnityEngine;
using GhostWriter.Entities;
using GhostWriter.WorkCreation.Ideation.Workers;

namespace GhostWriter.WorkCreation.UI.Ideation
{
    public class WorkersUI : MonoBehaviour
    {
        private WorkerManager workerManager;
        [SerializeField] private GameObject workerPrefab;
        [SerializeField] private GameObject contentObject;

        private List<WorkerButton> workerButtons;

        private void Awake()
        {
            // Verify the TopicManager
            if (workerManager == null)
                workerManager = GetComponent<WorkerManager>();

            // Instantiate the lists
            workerButtons = new();
        }

        private void OnEnable()
        {
            workerManager.OnWorkersUpdated += UpdateAvailableWorkers;
        }

        private void OnDisable()
        {
            workerManager.OnWorkersUpdated -= UpdateAvailableWorkers;
        }

        /// <summary>
        /// Callback function to update the list of available workers
        /// </summary>
        private void UpdateAvailableWorkers(List<IWorker> avilableWorkers)
        {
            // Check if the worker buttons list has buttons inside of it
            if(workerButtons.Count > 0)
            {
                // If so, iterate through each worker button
                foreach(WorkerButton workerButton in workerButtons)
                {
                    // Destroy the game object
                    Destroy(workerButton.gameObject);
                }
            }

            // Clear the worker buttons list
            workerButtons.Clear();

            // Iterate through each available worker
            foreach(IWorker worker in avilableWorkers)
            {
                // Create a worker button under the content object
                GameObject workerObj = Instantiate(workerPrefab, contentObject.transform);

                // Extract the worker button
                WorkerButton workerButton = workerObj.GetComponent<WorkerButton>();

                // Instantiate the worker button
                workerButton.Instantiate(workerManager, worker);

                // Add the worker button to the list
                workerButtons.Add(workerButton);
            }
        }
    }
}