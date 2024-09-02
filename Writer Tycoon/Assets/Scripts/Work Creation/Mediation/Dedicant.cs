using System;
using UnityEngine;
using WriterTycoon.Patterns.Visitor;

namespace WriterTycoon.WorkCreation.Mediation
{
    public enum DedicantType
    {
        Topic,
        Genre,
        Audience,
        Slider,
        Rater
    }

    public abstract class Dedicant : MonoBehaviour, IVisitable
    {
        public abstract DedicantType Type { get; }
        public abstract string Name { get; }

        public abstract void Accept(IVisitor visitor);

        protected abstract void Send(IVisitor message);
        protected virtual void Send(IVisitor message, Func<Dedicant, bool> predicate) { }

        protected Func<Dedicant, bool> IsTopic => target => target.Type == DedicantType.Topic;
        protected Func<Dedicant, bool> IsGenre => target => target.Type == DedicantType.Genre;
        protected Func<Dedicant, bool> IsAudience => target => target.Type == DedicantType.Audience;
        protected Func<Dedicant, bool> IsSlider => target => target.Type == DedicantType.Slider;
        protected Func<Dedicant, bool> IsRater => target => target.Type == DedicantType.Rater;
    }
}