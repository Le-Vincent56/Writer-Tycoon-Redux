using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.Patterns.ServiceLocator;
using WriterTycoon.Patterns.Visitor;
using WriterTycoon.WorkCreation.Mediation;

namespace WrtierTycoon.WorkCreation.Rater
{
    public class WorkRater : Dedicant
    {
        private Mediator<Dedicant> mediator;

        public override string Name { get => "Work Rater"; }
        public override DedicantType Type { get => DedicantType.Rater; }

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

        public override void Accept(IVisitor message) => message.Visit(this);

        protected override void Send(IVisitor message)
        {
            throw new System.NotImplementedException();
        }
    }
}