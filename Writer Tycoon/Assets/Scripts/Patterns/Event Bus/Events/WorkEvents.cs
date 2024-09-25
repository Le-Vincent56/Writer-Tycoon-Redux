using UnityEngine;
using WriterTycoon.WorkCreation.Development.PointGeneration;
using WriterTycoon.WorkCreation.Development.Tracker;
using WriterTycoon.WorkCreation.Ideation.Review;
using WriterTycoon.WorkCreation.UI.Development;

namespace WriterTycoon.Patterns.EventBus
{
    #region WORK CREATION EVENTS
    public struct SendReviewData : IEvent
    {
        public ReviewData ReviewData;
    }
    #endregion

    #region WORK DEVELOPMENT EVENTS
    public struct UpdateProgressData : IEvent
    {
        public int Hash;
        public float Current;
        public float Maximum;
    }

    public struct SendPhaseTime : IEvent
    {
        public int Hash;
        public int TimeEstimate;
    }

    public struct SetDevelopmentPhase : IEvent
    {
        public DevelopmentPhase Phase;
    }

    public struct SetSliderPoints : IEvent
    {
        public PointCategory Category;
        public int Value;
    }

    public struct SendSliderPoints : IEvent
    {
        public int Hash;
    }

    public struct ClearIdeation : IEvent { }

    public struct EndDevelopment : IEvent 
    {
        public int Hash;
    }

    public struct BeginEditing : IEvent
    {
        public int Hash;
    }

    public struct EndEditing : IEvent
    {
        public int Hash;
    }
    #endregion
}
