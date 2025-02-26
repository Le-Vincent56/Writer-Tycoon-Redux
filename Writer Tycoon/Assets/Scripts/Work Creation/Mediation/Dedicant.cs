using Sirenix.OdinInspector;
using System;
using UnityEngine;
using GhostWriter.Patterns.Mediator;
using GhostWriter.Patterns.ServiceLocator;
using GhostWriter.Patterns.Visitor;

namespace GhostWriter.WorkCreation.Mediation
{
    public enum DedicantType
    {
        // Ideation
        About,
        TimeEstimator,
        WorkType,
        Audience,
        Topic,
        Genre,
        Workers,
        Compatibility,
        IdeaReviewer,

        // Development
        Tracker,
        Sliders,
        PointGenerator,
        ErrorGenerator,
        Rater,

        // Editing
        Editor
    }

    public abstract class Dedicant : SerializedMonoBehaviour, IVisitable
    {
        protected Mediator<Dedicant> mediator;

        public abstract DedicantType Type { get; }
        public abstract string Name { get; }

        protected virtual void Start()
        {
            // Register with the mediator
            mediator = ServiceLocator.ForSceneOf(this).Get<Mediator<Dedicant>>();
            mediator.Register(this);
        }
        protected void OnDestroy()
        {
            // Deregister the mediator
            mediator.Deregister(this);
        }

        /// <summary>
        /// Accept a message from the Mediator
        /// </summary>
        public virtual void Accept(IVisitor message) => message.Visit(this);

        protected virtual void Send(IVisitor message) => mediator.Broadcast(this, message);
        protected virtual void Send(IVisitor message, Func<Dedicant, bool> predicate) => mediator.Broadcast(this, message, predicate);
        protected Func<Dedicant, bool> IsType(DedicantType type) => target => target.Type == type;
        protected Func<Dedicant, bool> AreTypes(DedicantType[] types) => target =>
        {
            foreach (DedicantType type in types)
            {
                if (target.Type == type) return true;
            }

            return false;
        };
    }
}