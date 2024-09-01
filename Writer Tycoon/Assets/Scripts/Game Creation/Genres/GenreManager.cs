using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.GameCreation.Mediation;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.Patterns.ServiceLocator;
using WriterTycoon.Patterns.Visitor;

namespace WriterTycoon.GameCreation.Genres
{
    public class GenreManager : Dedicant
    {
        private Mediator<Dedicant> mediator;

        private void Start()
        {
            // Register with the mediator
            mediator = ServiceLocator.ForSceneOf(this).Get<Mediator<Dedicant>>();
            mediator.Register(this);
        }

        private void OnDestroy()
        {
            // Deregister the mediator
            mediator.Deregister(this);
        }

        public override string Name { get => "Genre Manager"; }
        public override DedicantType Type { get => DedicantType.Genre; }

        public override void Accept(IVisitor message) => message.Visit(this);
        protected override void Send(IVisitor message) => mediator.Broadcast(this, message);
        protected override void Send(IVisitor message, Func<Dedicant, bool> predicate) => mediator.Broadcast(this, message, predicate);
    }
}