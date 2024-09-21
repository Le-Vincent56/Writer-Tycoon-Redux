using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.WorkCreation.Development.ErrorGeneraton
{
    public class ErrorGenerator : MonoBehaviour
    {
        [SerializeField] private int totalErrors;
        [SerializeField] private float currentErrors;
        [SerializeField] private float errorScalar;

        private int pathCount;
        private List<float> errorPath;

        private EventBinding<PassDay> passDayEvent;
        private EventBinding<NotifySuccessfulCreation> notifySuccessfulCreationEvent;
        private EventBinding<EndDevelopment> endDevelopmentEvent;

        private void Awake()
        {
            // Initialize the error path List
            errorPath = new();
            pathCount = 0;
        }

        private void OnEnable()
        {
            passDayEvent = new EventBinding<PassDay>(GenerateErrors);
            EventBus<PassDay>.Register(passDayEvent);

            notifySuccessfulCreationEvent = new EventBinding<NotifySuccessfulCreation>(GetTotalErrors);
            EventBus<NotifySuccessfulCreation>.Register(notifySuccessfulCreationEvent);

            endDevelopmentEvent = new EventBinding<EndDevelopment>(ResetErrors);
            EventBus<EndDevelopment>.Register(endDevelopmentEvent);
        }

        private void OnDisable()
        {
            EventBus<PassDay>.Deregister(passDayEvent);
            EventBus<NotifySuccessfulCreation>.Deregister(notifySuccessfulCreationEvent);
            EventBus<EndDevelopment>.Deregister(endDevelopmentEvent);
        }

        /// <summary>
        /// Get the total number of errors for the development phase
        /// </summary>
        private void GetTotalErrors(NotifySuccessfulCreation eventData)
        {
            // Get the total amount of time the development will take
            int totalTime = eventData.ReviewData.TimeEstimates.Total;

            // Calculate the total number of errors
            totalErrors = (int)(totalTime * errorScalar);

            // Create up to 50% more errors for unpredictability
            int randomErrors = Random.Range(0, totalErrors / 2);

            // Calculate the total number of errors
            totalErrors += randomErrors;

            // Calculate the error path
            CalculateErrorPath(totalTime);
        }

        /// <summary>
        /// Calculate the error path for day-to-day generation
        /// </summary>
        private void CalculateErrorPath(int totalTime)
        {
            // Verify that the List is instanstiated
            errorPath ??= new();

            // Clear the error path to start fresh
            errorPath.Clear();

            // Temporary list to hold random weights
            List<float> weights = new();

            // Increment over the total amount of days for development
            for(int i = 0; i < totalTime; i++)
            {
                // Add a weight for each day
                weights.Add(Random.Range(0.5f, 1.5f));
            }

            // Calculate the sum of the weights
            float totalWeight = 0;

            foreach (float weight in weights)
            {
                totalWeight += weight;
            }

            // Iterate through each day
            for(int i = 0; i < totalTime; i++)
            {
                // Calculate the number of errors for the specific day
                float errorsForDay = (weights[i] / totalWeight) * totalErrors;

                // Add the errors to the error path
                errorPath.Add(errorsForDay);
            }

            // Check the sum
            float currentSum = 0;
            foreach(float errors in errorPath)
            {
                currentSum += errors;
            }

            // Adjust if the sum of the path doesn't match the total errors (usually du to float rounding)
            float difference = totalErrors - currentSum;
            if(Mathf.Abs(difference) > Mathf.Epsilon)
            {
                // Get a random day from the total time
                int randomDay = Random.Range(0, totalTime);

                // Add the difference to the error calculation on that day
                errorPath[randomDay] += difference;
            }
        }

        /// <summary>
        /// Callback function to reset the error variables on the end of development
        /// </summary>
        private void ResetErrors()
        {
            // Reset variables
            totalErrors = 0;
            currentErrors = 0;
            pathCount = 0;
            errorPath.Clear();
        }

        /// <summary>
        /// Generate the errors for each day
        /// </summary>
        private void GenerateErrors()
        {
            // Exit case - if traversed all the days
            if (pathCount >= errorPath.Count) return;

            // Add the number of errors to the path count
            currentErrors += errorPath[pathCount];

            // Increment the path count
            pathCount++;

            // Display the current errors
        }
    }
}