using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WriterTycoon.Entities.Competitors.Learning
{
    [Serializable]
    public class QValueStore
    {
        [SerializeField] private Dictionary<(int state, int action), (float value, object data)> qValues;

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

        /// <summary>
        /// Debug the QValueStore
        /// </summary>
        public void Debug()
        {
            string log = "Q Value Store:";

            foreach(KeyValuePair<(int state, int action), (float value, object data)> kvp in qValues)
            {
                log += $"\nState: {kvp.Key.state}, Action: {kvp.Key.action}, Value: {kvp.Value.value}, " +
                    $"\n\tData:";

                if(kvp.Value.data is AIConceptData conceptData)
                {
                    log += $"\n\tTopic: {conceptData.Topic.Name}" +
                        $"\n\tGenre: {conceptData.Genre.Name}" +
                        $"\n\tAudience: {conceptData.Audience}";
                }
            }

            UnityEngine.Debug.Log(log);
        }
    }
}