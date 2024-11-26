using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostWriter.Entities.Competitors.Learning
{
    [Serializable]
    public class QValueStore
    {
        [OdinSerialize, ShowInInspector] private Dictionary<(int state, int action), (float value, ActionData data)> qValues;

        public QValueStore()
        {
            // Initialize the dictionary
            qValues = new();
        }

        /// <summary>
        /// Get the Q-Value of a certain state and action
        /// </summary>
        public float GetQValue(int state, int action)
        {
            // Check if the dictionary has the values
            if (qValues.TryGetValue((state, action), out (float value, ActionData data) qValue))
                return qValue.value;

            // Default to a value of 0
            return 0f;
        }

        /// <summary>
        /// Get the best action within a state
        /// </summary>
        /// <param name="state"></param>
        /// <param name="availableActions"></param>
        /// <returns></returns>
        public int GetBestAction(int state, HashSet<int> availableActions)
        {
            // Exit case - if the availableActions HashSet is null or empty
            if (availableActions == null || availableActions.Count == 0) return 0;

            // Set the first action as the best action
            float maxQ = GetQValue(state, 0);
            int bestAction = availableActions.ElementAt(0);

            // Iterate through each action
            foreach(int action in availableActions)
            {
                // Get the Q-value of the action
                float qValue = GetQValue(state, action);

                // Check if the Q-value is greater than the current max Q-value
                if(qValue > maxQ)
                {
                    // Set it as the new max Q-value and set the
                    // action as the best action
                    maxQ = qValue;
                    bestAction = action;
                }
            }

            return bestAction;
        }

        /// <summary>
        /// Get the (or one of the) most valuable action from the QValueStore
        /// </summary>
        public (float value, ActionData data) GetMostValuableAction()
        {
            // Exit case - if the Dictionary is not initialized or empty
            if (qValues == null || qValues.Count == 0) return (0f, new ActionData() { Value = 0f, Data = null });

            // Create a list to store the most valuable actions
            // This list with either store a unique, highest value action
            // or multiple highest value actions, if there are many with the 
            // same highest value
            List<(float value, ActionData data)> mostValuableActions = new();

            // Set the highest value
            float highestValue = float.MinValue;

            // Iterate through each key-value pair in the Q-Values Dictionary
            foreach (KeyValuePair<(int state, int action), (float value, ActionData data)> kvp in qValues)
            {
                // Get the current value
                float qValue = kvp.Value.value;

                // Check if the value is greater than the highest value
                if(qValue > highestValue)
                {
                    // Set the new highest value
                    highestValue = qValue;

                    // Clear the actions List
                    mostValuableActions.Clear();

                    // Add the most valuable action to the List
                    mostValuableActions.Add(kvp.Value);
                }
                // Otherwise, check if the value is equal to the highest value
                else if (qValue == highestValue)
                {
                    // If so, add it to the most valuable actions List
                    mostValuableActions.Add(kvp.Value);
                }
            }

            // Return a random most valuable action from the top actions
            return mostValuableActions[UnityEngine.Random.Range(0, mostValuableActions.Count)];
        }

        /// <summary>
        /// Store the Q-value for a given state and action
        /// </summary>
        public void StoreQValue(int state, int action, float value, ActionData data) => qValues[(state, action)] = (value, data);

        /// <summary>
        /// Get the highest Q-value and its data
        /// </summary>
        private (float value, object data) GetHighestValue()
        {
            // If the Dictionary is not instantiated or is empty, return 0
            if (qValues == null || qValues.Count == 0) return (0f, null);

            // Initialize the lowest float value
            (float value, object data) highestValueData = (float.MinValue, null);

            // Iterate through the Dictionary
            foreach (KeyValuePair<(int state, int action), (float qValue, ActionData qData)> kvp in qValues)
            {
                // Check if the value is greater than the max value
                if (kvp.Value.qValue > highestValueData.value)
                {
                    // Set the new max value
                    highestValueData.value = kvp.Value.qValue;
                    highestValueData.data = kvp.Value.qData;
                }
            }

            return highestValueData;
        }

        /// <summary>
        /// Get the lowest Q-value and its data
        /// </summary>s
        private (float value, object data) GetLowestValue()
        {
            // If the Dictionary is not instantiated or is empty, return 0
            if (qValues == null || qValues.Count == 0) return (0f, null);

            // Initialize the lowest float value
            (float value, object data) lowestValueData = (float.MaxValue, null);

            // Iterate through the Dictionary
            foreach (KeyValuePair<(int state, int action), (float qValue, ActionData qData)> kvp in qValues)
            {
                // Check if the value is greater than the max value
                if (kvp.Value.qValue < lowestValueData.value)
                {
                    // Set the new max value
                    lowestValueData.value = kvp.Value.qValue;
                    lowestValueData.data = kvp.Value.qData;
                }
            }

            return lowestValueData;
        }

        /// <summary>
        /// Debug the QValueStore
        /// </summary>
        public void Debug()
        {
            // Get the highest and lowest value in the QValueStore
            (float value, object data) highestValueData = GetHighestValue();
            (float value, object data) lowestValueData = GetLowestValue();

            // Debug the highest
            string highestLog = $"Highest Value: {highestValueData.value}";

            if(highestValueData.data is AIConceptData highestConceptData)
            {
                highestLog += $"\n\tTopic: {highestConceptData.Topic.Name}" +
                    $"\n\tGenre: {highestConceptData.Genre.Name}" +
                    $"\n\tAudience: {highestConceptData.Audience}";
            } else if(highestValueData.data is AISliderData highestSliderData)
            {
                highestLog += $"\n\t{highestSliderData.SliderOne.category}: {highestSliderData.SliderOne.value}" +
                    $"\n\t{highestSliderData.SliderTwo.category}: {highestSliderData.SliderTwo.value}" +
                    $"\n\t{highestSliderData.SliderThree.category}: {highestSliderData.SliderThree.value}";
            }

            // Debug the lowest
            string lowestLog = $"Lowest Value: {lowestValueData.value}";

            if (lowestValueData.data is AIConceptData lowestConceptData)
            {
                lowestLog += $"\n\tTopic: {lowestConceptData.Topic.Name}" +
                    $"\n\tGenre: {lowestConceptData.Genre.Name}" +
                    $"\n\tAudience: {lowestConceptData.Audience}";
            } else if(lowestValueData.data is AISliderData lowestSliderData)
            {
                lowestLog += $"\n\t{lowestSliderData.SliderOne.category}: {lowestSliderData.SliderOne.value}" +
                    $"\n\t{lowestSliderData.SliderTwo.category}: {lowestSliderData.SliderTwo.value}" +
                    $"\n\t{lowestSliderData.SliderThree.category}: {lowestSliderData.SliderThree.value}";
            }

            UnityEngine.Debug.Log(highestLog);
            UnityEngine.Debug.Log(lowestLog);
        }
    }
}