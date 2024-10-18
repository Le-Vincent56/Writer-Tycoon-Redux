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
        [SerializeField] private HashSet<Action> availableActionsIdle;

        public ReinforcementProblem(HashSet<Action> availableActionsIdle)
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
        public HashSet<Action> GetAvailableActions(State state)
        {
            return state switch
            {
                State.Idle => availableActionsIdle,
                State.Working => new HashSet<Action>() { Action.None },
                _ => null
            };
        }
    }
}