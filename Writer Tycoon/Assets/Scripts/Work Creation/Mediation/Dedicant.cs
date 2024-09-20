using System;
using UnityEngine;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.Patterns.ServiceLocator;
using WriterTycoon.Patterns.Visitor;

namespace WriterTycoon.WorkCreation.Mediation
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
        Compatibility,
        IdeaReviewer,

        // Development
        Tracker,
        Sliders,
        PointGenerator,
        Rater,
    }

    public abstract class Dedicant : MonoBehaviour, IVisitable
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