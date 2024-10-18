using Sirenix.Reflection.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WriterTycoon.Entities.Competitors.Learning
{
    public class QLearner
    {
        public void RunQLearningStep(
            ReinforcementProblem problem, 
            int iterations,
            float rho
        )
        {
            // Create a new QValueStore
            QValueStore store = new();

            // Randomly get a state (Idle or Working)
            int state = 1;

            // Iterate as many times as specified
            for (int i = 0; i < iterations; i++)
            {
                // Get the available actions for the current state
                Dictionary<int, Func<float>> availableActionsDict = problem.GetAvailableActions(state);

                int action;
                Func<float> function;

                // Exploration (random) vs. Exploitation (best known)
                if (UnityEngine.Random.value < rho)
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
                (float reward, int finalState) = problem.TakeAction(state, action, function);

                // Get Q-value for the current state-action pair
                float qValue = store.GetQValue(state, action);

                // Get max Q-value for the new state
                float maxQValue = store.GetQValue(
                    finalState,
                    store.GetBestAction(finalState, problem.GetAvailableActions(finalState).Keys.ToHashSet())
                );

                // Update Q-value using the Q-learning formula
                float alpha = 0.1f; // Learning rate
                float gamma = 0.9f; // Discount factor

                qValue = (1 - alpha) * qValue + alpha * (reward + gamma * maxQValue);

                // Store the updated Q-value
                store.StoreQValue(state, action, qValue);
            }
        }
    }
}