using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GhostWriter.Entities.Competitors.Learning
{
    public struct ActionData
    {
        public float Value;
        public object Data;
    }

    [Serializable]
    public class QLearner
    {
        [SerializeField] private QValueStore store;

        public QLearner()
        {
            store = new();
        }

        /// <summary>
        /// Run the Q-Learning step
        /// </summary>
        public void RunQLearningStep(
            ReinforcementProblem problem, 
            int state,
            int iterations,
            float learnFactor,
            float discountFactor,
            float explorationFactor
        )
        {
            // Iterate as many times as specified
            for (int i = 0; i < iterations; i++)
            {
                // Get the available actions for the current state
                Dictionary<int, Func<(float value, object data)>> availableActionsDict = problem.GetAvailableActions(state);

                int action;
                Func<(float value, object data)> function;

                // Exploration (random) vs. Exploitation (best known)
                if (UnityEngine.Random.value < explorationFactor)
                {
                    // Exploration: choose a random action
                    action = UnityEngine.Random.Range(0, availableActionsDict.Keys.Count);
                    function = availableActionsDict[action];
                }
                else
                {
                    // Exploitation: choose the best-known action
                    action = store.GetBestAction(state, availableActionsDict.Keys.ToHashSet());
                    function = availableActionsDict[action];
                }

                // Take the action and get the result
                (float reward, int finalState, ActionData data) = problem.TakeAction(state, action, function);

                // Get Q-value for the current state-action pair
                float qValue = store.GetQValue(state, action);

                // Get max Q-value for the new state
                float maxQValue = store.GetQValue(
                    finalState,
                    store.GetBestAction(finalState, problem.GetAvailableActions(finalState).Keys.ToHashSet())
                );

                qValue = (1 - learnFactor) * qValue + learnFactor * (reward + discountFactor * maxQValue);

                // Store the updated Q-value
                store.StoreQValue(state, action, qValue, data);
            }

            // Debug the store
            //store.Debug();
        }

        /// <summary>
        /// Get the best action from the QValueStore
        /// </summary>
        public (float value, ActionData data) GetBestAction(bool debug = false)
        {
            // Get the most valuable action
            (float value, ActionData data) mostValuableAction = store.GetMostValuableAction();

            // Handle debugging
            if (debug)
            {
                string bestActionLog = $"Best Action: {mostValuableAction.value}";

                if (mostValuableAction.data.Data is AIConceptData highestConceptData)
                {
                    bestActionLog += $"\n\tTopic: {highestConceptData.Topic.Name}" +
                        $"\n\tGenre: {highestConceptData.Genre.Name}" +
                        $"\n\tAudience: {highestConceptData.Audience}";
                }
                else if (mostValuableAction.data.Data is AISliderData highestSliderData)
                {
                    bestActionLog += $"\n\t{highestSliderData.SliderOne.category}: {highestSliderData.SliderOne.value}" +
                        $"\n\t{highestSliderData.SliderTwo.category}: {highestSliderData.SliderTwo.value}" +
                        $"\n\t{highestSliderData.SliderThree.category}: {highestSliderData.SliderThree.value}";
                }

                Debug.Log(bestActionLog);
            }

            // Return the most valuable action
            return mostValuableAction;
        }
    }
}