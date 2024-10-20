using System;
using System.Collections.Generic;
using UnityEngine;

namespace WriterTycoon.Entities.Competitors.Learning
{

    [Serializable]
    public class ReinforcementProblem
    {
        [SerializeField] private Dictionary<int, Func<float>> availableActions;

        public ReinforcementProblem(Dictionary<int, Func<float>> availableActions, bool debug = false)
        {
            this.availableActions = availableActions;

            if (!debug) return;

            foreach(KeyValuePair<int, Func<float>> action in availableActions)
            {
                action.Value.Invoke();
            }
        }

        /// <summary>
        /// Get the available actions for a given state
        /// </summary>
        public Dictionary<int, Func<float>> GetAvailableActions(int state)
        {
            return state switch
            {
                0 => new Dictionary<int, Func<float>>() { { 0, null } },
                1 => availableActions,
                _ => null
            };
        }

        /// <summary>
        /// Take the Action and evaluate
        /// </summary>
        public (float reward, int state) TakeAction(int state, int action, Func<float> function)
        {
            // Invoke the action and store the result
            float score = function.Invoke();

            // Set the reward
            float reward = score;

            return (reward, state);
        }
    }
}