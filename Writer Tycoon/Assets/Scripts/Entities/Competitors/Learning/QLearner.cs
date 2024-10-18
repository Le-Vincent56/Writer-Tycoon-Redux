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
        public void RunQLearning(
            ReinforcementProblem problem, 
            int iterations, 
            float alpha,
            float gamma, 
            float rho, 
            float nu
        )
        {
            // Create a new QValueStore
            QValueStore store = new();

            // Iterate as many times as specified
            for(int i = 0; i < iterations; i++)
            {
                // Initialize the state
                State state = problem.GetRandomState();

                // Get the available actions for the current state
                Dictionary<Action, Func<float>> availableActionsDict = problem.GetAvailableActions(state);

                // Create container variables
                Action action;
                Func<float> function;

                // Choose action: exploration (random) or exploitation (best known)
                if (UnityEngine.Random.value < rho)
                {
                    // Get a random action and corresponding function
                    action = (Action)UnityEngine.Random.Range(0, availableActionsDict.Keys.Count);
                    function = availableActionsDict[action];
                }
                else
                {
                    // Get the best action and corresponding function
                    action = store.GetBestAction(state, availableActionsDict.Keys.ToHashSet());
                    function = availableActionsDict[action];
                }

                // Take the action and store the results
                (float reward, State newState) = problem.TakeAction(state, action, function);

                // Get the Q-value for the current state action-pair
                float qValue = store.GetQValue(state, action);

                // Get the max Q-value for the new state (next Action's best Q-value)
                float maxQValue = store.GetQValue(
                    newState,
                    store.GetBestAction(newState, problem.GetAvailableActions(newState).Keys.ToHashSet())
                );

                // Update Q-value using the Q-learning formula
                qValue = (1 - alpha) * qValue + alpha * (reward + gamma * maxQValue);

                // Store the updated q-value
                store.StoreQValue(state, action, qValue);

                // Update the current state
                state = newState;
            }
        }
    }
}