using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GhostWriter.WorkCreation.Ideation.Genres;

namespace GhostWriter.Entities.Competitors.Learning
{

    [Serializable]
    public class ReinforcementProblem
    {
        [SerializeField] private Dictionary<int, Func<(float value, object data)>> availableActionsConcept;
        [SerializeField] private Dictionary<int, Func<(float value, object data)>> availableActionsFocusOne;
        [SerializeField] private Dictionary<int, Func<(float value, object data)>> availableActionsFocusTwo;
        [SerializeField] private Dictionary<int, Func<(float value, object data)>> availableActionsFocusThree;

        public ReinforcementProblem(
            Dictionary<int, Func<(float value, object data)>> availableActionsConcept,
            Dictionary<int, Func<(float value, object data)>> availableActionsFocusOne,
            Dictionary<int, Func<(float value, object data)>> availableActionsFocusTwo,
            Dictionary<int, Func<(float value, object data)>> availableActionsFocusThree,
            bool debug = false)
        {
            this.availableActionsConcept = availableActionsConcept;
            this.availableActionsFocusOne = availableActionsFocusOne;
            this.availableActionsFocusTwo = availableActionsFocusTwo;
            this.availableActionsFocusThree = availableActionsFocusThree;
        }

        /// <summary>
        /// Get the available actions for a given state
        /// </summary>
        public Dictionary<int, Func<(float value, object data)>> GetAvailableActions(int state)
        {
            return state switch
            {
                0 => availableActionsConcept,
                1 => availableActionsFocusOne,
                2 => availableActionsFocusTwo,
                3 => availableActionsFocusThree,
                _ => null
            };
        }

        /// <summary>
        /// Take the Action and evaluate
        /// </summary>
        public (float reward, int state, ActionData data) TakeAction(int state, int action, Func<(float value, object data)> function)
        {
            // Invoke the action and store the result
            (float value, object data) data = function.Invoke();
            float score = data.value;

            // Cast to ActionData
            ActionData actionData = new ActionData() { Value = data.value, Data = data.data };

            // Set the reward
            float reward = score;

            return (reward, state, actionData);
        }
    }
}