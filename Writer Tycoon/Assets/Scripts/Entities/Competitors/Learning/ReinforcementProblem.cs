using System;
using System.Collections.Generic;
using UnityEngine;

namespace WriterTycoon.Entities.Competitors.Learning
{
    public enum State
    {
        Idle = 0,
        Working = 1,
    }

    public enum Action
    {
        None = 0
    }

    [Serializable]
    public class ReinforcementProblem
    {
        [SerializeField] private Dictionary<Action, Func<float>> availableActionsIdle;

        public ReinforcementProblem(Dictionary<Action, Func<float>> availableActionsIdle)
        {
            // Set the available actions when idle
            this.availableActionsIdle = availableActionsIdle;
        }

        /// <summary>
        /// Get a random State within the given enums
        /// </summary>
        public State GetRandomState() => (State)UnityEngine.Random.Range(0, Enum.GetValues(typeof(State)).Length);

        /// <summary>
        /// Get the available actions for a given state
        /// </summary>
        public Dictionary<Action, Func<float>> GetAvailableActions(State state)
        {
            return state switch
            {
                State.Idle => availableActionsIdle,
                State.Working => new Dictionary<Action, Func<float>>() { { Action.None, null } },
                _ => null
            };
        }

        /// <summary>
        /// Take the Action and evaluate
        /// </summary>
        public (float reward, State newState) TakeAction(State state, Action action, Func<float> function)
        {
            // Invoke the action and store the result
            float score = function.Invoke();

            // Set the new state
            State newState = (State)((int)state + (int)action);

            // Set the reward
            float reward = score;

            return (reward, newState);
        }
    }
}