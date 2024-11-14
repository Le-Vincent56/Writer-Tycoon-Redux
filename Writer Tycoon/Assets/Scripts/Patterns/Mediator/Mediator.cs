using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GhostWriter.Extensions.Enumerable;
using GhostWriter.Patterns.Visitor;

namespace GhostWriter.Patterns.Mediator
{
    public abstract class Mediator<T> : MonoBehaviour where T : IVisitable
    {
        private readonly List<T> components = new();

        /// <summary>
        /// Register a Component to the Mediator
        /// </summary>
        /// <param name="component"></param>
        public void Register(T component)
        {
            // Verify that the list does not contain the Component
            if (!components.Contains(component))
            {
                // Add the Component
                components.Add(component);
                OnRegistered(component);
            }
        }

        /// <summary>
        /// Virtual function for handling component registration
        /// </summary>
        protected virtual void OnRegistered(T component) { }

        /// <summary>
        /// Deregister a Component from the Mediator
        /// </summary>
        /// <param name="component"></param>
        public void Deregister(T component)
        {
            // Verify that the list contains the Component
            if (components.Contains(component))
            {
                // Remove the Component
                components.Remove(component);
                OnDeregistered(component);
            }
        }

        /// <summary>
        /// Virtual function for handling component deregistration
        /// </summary>
        /// <param name="component"></param>
        protected virtual void OnDeregistered(T component) { }

        /// <summary>
        /// Message a single component
        /// </summary>
        public void Message(T source, T target, IVisitor message) 
        {
            // Get the first Component that matches the target
            components.FirstOrDefault(component => component.Equals(target))?.Accept(message);
        }

        /// <summary>
        /// Broadcast a message to all Components
        /// </summary>
        public void Broadcast(T source, IVisitor message, Func<T, bool> predicate = null) 
        {
            components.Where(target => !source.Equals(target) && SenderConditionMet(target, predicate) && MediatorConditionMet(target))
                      .ForEach(target => target.Accept(message));
        }

        /// <summary>
        /// Calculate if a target Component meets the conditions of a given predicate
        /// </summary>
        bool SenderConditionMet(T target, Func<T, bool> predicate) => predicate == null || predicate(target);

        /// <summary>
        /// Calculate if the target Component meets conditions given by the Mediator
        /// </summary>
        protected abstract bool MediatorConditionMet(T target);
    }
}