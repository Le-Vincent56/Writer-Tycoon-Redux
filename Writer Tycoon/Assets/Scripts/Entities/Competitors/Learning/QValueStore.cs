using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WriterTycoon.Entities.Competitors.Learning
{
    [Serializable]
    public class QValueStore
    {
        [SerializeField] private Dictionary<(State, Action), float> qValues;

        public float GetQValue(State state, Action action)
        {
            // Check if the dictionary has the values
            if (qValues.TryGetValue((state, action), out float value))
                return value;

            // Default to a value of 0
            return 0f;
        }

        public Action GetBestAction(State state, HashSet<Action> availableActions)
        {
            // Set the first action as the best action
            float maxQ = GetQValue(state, 0);
            Action bestAction = availableActions.ElementAt(0);

            // Iterate through each action
            foreach(Action action in availableActions)
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
        public void StoreQValue(State state, Action action, float value) => qValues[(state, action)] = value;
    }
}