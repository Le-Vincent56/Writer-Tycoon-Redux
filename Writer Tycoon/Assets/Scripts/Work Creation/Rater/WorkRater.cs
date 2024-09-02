using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.Patterns.ServiceLocator;
using WriterTycoon.Patterns.Visitor;
using WriterTycoon.WorkCreation.Genres;
using WriterTycoon.WorkCreation.Mediation;
using WriterTycoon.WorkCreation.Topics;

namespace WrtierTycoon.WorkCreation.Rater
{
    public class WorkRater : Dedicant
    {
        private Mediator<Dedicant> mediator;
        [SerializeField] private List<Topic> topics;
        [SerializeField] private List<Genre> genres;

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

        /// <summary>
        /// Set the selected Topics
        /// </summary>
        public void SetTopics(List<Topic> topics) => this.topics = topics;

        /// <summary>
        /// Set the selected Genres
        /// </summary>
        public void SetGenres(List<Genre> genres) => this.genres = genres;

        public override void Accept(IVisitor message) => message.Visit(this);

        protected override void Send(IVisitor message)
        {
            throw new System.NotImplementedException();
        }
    }
}