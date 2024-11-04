using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WriterTycoon.Entities.Competitors.Learning
{
    [Serializable]
    public class QValueStore
    {
        [OdinSerialize, ShowInInspector] private Dictionary<(int state, int action), (float value, object data)> qValues;

        public QValueStore()
        {
            // Initialize the dictionary
            qValues = new();
        }

        public float GetQValue(int state, int action)
        {
            // Check if the dictionary has the values
            if (qValues.TryGetValue((state, action), out (float value, object data) qValue))
                return qValue.value;

            // Default to a value of 0
            return 0f;
        }

        public int GetBestAction(int state, HashSet<int> availableActions)
        {
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
        /// Store the Q-value for a given state and action
        /// </summary>
        public void StoreQValue(int state, int action, float value, object data) => qValues[(state, action)] = (value, data);

        private (float value, object data) GetHighestValue()
        {
            // If the Dictionary is not instantiated or is empty, return 0
            if (qValues == null || qValues.Count == 0) return (0f, null);

            // Initialize the lowest float value
            (float value, object data) highestValueData = (float.MinValue, null);

            // Iterate through the Dictionary
            foreach (KeyValuePair<(int state, int action), (float qValue, object qData)> kvp in qValues)
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

        private (float value, object data) GetLowestValue()
        {
            // If the Dictionary is not instantiated or is empty, return 0
            if (qValues == null || qValues.Count == 0) return (0f, null);

            // Initialize the lowest float value
            (float value, object data) lowestValueData = (float.MaxValue, null);

            // Iterate through the Dictionary
            foreach (KeyValuePair<(int state, int action), (float qValue, object qData)> kvp in qValues)
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
            //string log = "Q Value Store:";

            //foreach(KeyValuePair<(int state, int action), (float value, object data)> kvp in qValues)
            //{
            //    log += $"\nState: {kvp.Key.state}, Action: {kvp.Key.action}, Value: {kvp.Value.value}, " +
            //        $"\n\tData:";

            //    if(kvp.Value.data is AIConceptData conceptData)
            //    {
            //        log += $"\n\tTopic: {conceptData.Topic.Name}" +
            //            $"\n\tGenre: {conceptData.Genre.Name}" +
            //            $"\n\tAudience: {conceptData.Audience}";
            //    }
            //}

            (float value, object data) highestValueData = GetHighestValue();
            (float value, object data) lowestValueData = GetLowestValue();

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