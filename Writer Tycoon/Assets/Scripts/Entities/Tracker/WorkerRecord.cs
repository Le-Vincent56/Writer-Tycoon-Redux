using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.Patterns.ServiceLocator;

namespace WriterTycoon.Entities.Tracker
{
    public class WorkerRecord : MonoBehaviour
    {
        private List<IWorker> totalWorkers;
        private List<IWorker> availableWorkers;

        private EventBinding<NotifySuccessfulCreation> notifySuccessfulCreationEvent;

        private void Awake()
        {
            // Register to the global service locator
            ServiceLocator.ForSceneOf(this).Register(this);

            // Instantiate the lists
            totalWorkers = new();
            availableWorkers = new();
        }

        private void OnEnable()
        {
            notifySuccessfulCreationEvent = new EventBinding<NotifySuccessfulCreation>(UpdateWorkerHashes);
            EventBus<NotifySuccessfulCreation>.Register(notifySuccessfulCreationEvent);
        }

        private void OnDisable()
        {
            EventBus<NotifySuccessfulCreation>.Deregister(notifySuccessfulCreationEvent);
        }

        /// <summary>
        /// Record a worker
        /// </summary>
        public void RecordWorker(IWorker worker)
        {
            // Add the worker to the list
            totalWorkers.Add(worker);

            // Update the available workers
            UpdateAvailableWorkers();
        }

        /// <summary>
        /// Callback function to update Worker hashes after a Work has been initiated
        /// </summary>
        /// <param name="eventData"></param>
        private void UpdateWorkerHashes(NotifySuccessfulCreation eventData)
        {
            foreach(IWorker eventWorker in eventData.ReviewData.Workers)
            {
                foreach(IWorker localWorker in totalWorkers)
                {
                    // Skip case - if the local worker is not the same as the event worker
                    if (eventWorker != localWorker) continue;

                    // Assign the local worker the hash
                    localWorker.AssignedHash = eventData.ReviewData.Hash;
                }
            }

            // Update available workers
            UpdateAvailableWorkers();
        }

        /// <summary>
        /// Update which workers are available
        /// </summary>
        private void UpdateAvailableWorkers()
        {
            // Clear the available workers
            availableWorkers.Clear();

            // Iterate through the total workers
            foreach (IWorker worker in totalWorkers)
            {
                // Check if the worked is not assigned
                if (worker.AssignedHash == -1)
                    // If so, add them to the available workers
                    availableWorkers.Add(worker);
            }

            // Update the available workers list
            EventBus<AvailableWorkersUpdated>.Raise(new AvailableWorkersUpdated()
            {
                AvailableWorkers = availableWorkers
            });
        }

        /// <summary>
        /// Get the list of available Workers
        /// </summary>
        /// <returns></returns>
        public List<IWorker> GetAvailableWorkers() => availableWorkers;
    }
}