using System;
using System.Collections.Generic;
using UnityEngine;
using GhostWriter.WorkCreation.Ideation.Genres;
using System.Linq;

namespace GhostWriter.Entities.Competitors.Learning
{

    [Serializable]
    public class ReinforcementProblem
    {
        [SerializeField] private Dictionary<int, Func<(float value, object data)>> availableActionsConcept;
        [SerializeField] private Dictionary<Genre, List<Dictionary<int, Func<(float value, object data)>>>> genreFocusActions;

        public ReinforcementProblem(
            Dictionary<int, Func<(float value, object data)>> availableActionsConcept,
            Dictionary<Genre, List<Dictionary<int, Func<(float value, object data)>>>> genreFocusActions,
            bool debug = false)
        {
            this.availableActionsConcept = availableActionsConcept;
            this.genreFocusActions = genreFocusActions;
        }

        /// <summary>
        /// Get the available actions for a given state
        /// </summary>
        public Dictionary<int, Func<(float value, object data)>> GetAvailableActions(int state)
        {
            // If the state is 0, return the concepting actions
            if (state == 0) return availableActionsConcept;

            // Calculate genre index
            int genreIndex = (state - 1) / 3;

            // Calculate action within the genre's actions
            int actionIndex = (state - 1) % 3;

            // Check if the genre index is negative or greater than or qual to the amount of focus actions
            // for that genre
            if (genreIndex < 0 || genreIndex >= genreFocusActions.Count)
                return null; // Invalid state

            // Return the given Genre-Focus actions
            return genreFocusActions.ElementAt(genreIndex).Value[actionIndex];
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