using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WriterTycoon.Entities;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Ideation.Topics;
using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Ideation.Workers
{
    public class WorkerManager : Dedicant
    {
        [SerializeField] private int selectedWorkersMax;
        private List<IWorker> availableWorkers = new();
        private List<WorkerButton> selectedWorkerButtons = new();

        public UnityAction<List<IWorker>> OnWorkersUpdated = delegate { };

        private EventBinding<AvailableWorkersUpdated> availableWorkersUpdatedEvent;
        private EventBinding<ClearIdeation> clearIdeationEvent;

        public override DedicantType Type => DedicantType.Workers;
        public override string Name => "Worker Manager";

        private void Awake()
        {
            // Initialize Lists
            availableWorkers = new();
            selectedWorkerButtons = new();
        }

        private void OnEnable()
        {
            availableWorkersUpdatedEvent = new EventBinding<AvailableWorkersUpdated>(SetWorkers);
            EventBus<AvailableWorkersUpdated>.Register(availableWorkersUpdatedEvent);

            clearIdeationEvent = new EventBinding<ClearIdeation>(ClearSelectedWorkers);
            EventBus<ClearIdeation>.Register(clearIdeationEvent);        
        }

        private void OnDisable()
        {
            EventBus<AvailableWorkersUpdated>.Deregister(availableWorkersUpdatedEvent);
            EventBus<ClearIdeation>.Deregister(clearIdeationEvent);
        }

        /// <summary>
        /// Callback function to set the available workers
        /// </summary>
        private void SetWorkers(AvailableWorkersUpdated eventData)
        {
            // Set the available workers
            availableWorkers = eventData.AvailableWorkers;

            // Notify that the available workers list has been updated
            OnWorkersUpdated.Invoke(availableWorkers);
        }

        /// <summary>
        /// Select a worker
        /// </summary>
        public void SelectWorker(WorkerButton workerButton)
        {
            // Exit case - if the list already contains the Worker Button
            if (selectedWorkerButtons.Contains(workerButton)) return;

            // Check if the maximum amount of selected Workers have been reached
            if (selectedWorkerButtons.Count >= selectedWorkersMax)
            {
                // Deselect the Worker Button and remove it
                selectedWorkerButtons[0].Deselect();
                selectedWorkerButtons.RemoveAt(0);
            }

            // Add the Worker Button to the selected Worker Buttons list
            selectedWorkerButtons.Add(workerButton);

            // Select the Worker Button
            workerButton.Select();

            // Send the Workers out to the mediator
            SendWorkers();
        } 

        /// <summary>
        /// Clear the selected workers
        /// </summary>
        public void ClearSelectedWorkers()
        {
            // Iterate through each worker button
            foreach(WorkerButton workerButton in selectedWorkerButtons)
            {
                // Deselect the button
                workerButton.Deselect();
            }

            // Clear the selected workers
            selectedWorkerButtons.Clear();
        }

        /// <summary>
        /// Send the selected workers
        /// </summary>
        private void SendWorkers()
        {
            // Create a list to store Workers in
            List<IWorker> selectedWorkers = new();

            // Copy the selected Workers into the list
            foreach (WorkerButton button in selectedWorkerButtons)
            {
                selectedWorkers.Add(button.Worker);
            }

            // Send the Worker payload
            Send(new WorkerPayload()
            { Content = selectedWorkers },
                AreTypes(new DedicantType[2] {
                    DedicantType.TimeEstimator,
                    DedicantType.IdeaReviewer
                })
            );
        }
    }
}