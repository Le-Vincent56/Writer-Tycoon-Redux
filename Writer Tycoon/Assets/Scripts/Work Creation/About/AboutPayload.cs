using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Review;

namespace WriterTycoon.WorkCreation.About
{
    public class AboutPayload : Payload<AboutInfo>
    {
        public override AboutInfo Content { get; set; }
        public override void Visit<T>(T visitable)
        {
            if (visitable is IdeaReviewer ideaReviewer) ideaReviewer.SetAboutInfo(Content);
        }
    }
}