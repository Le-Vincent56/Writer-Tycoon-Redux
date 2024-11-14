using GhostWriter.WorkCreation.Development.PointGeneration;
using GhostWriter.WorkCreation.Development.Tracker;
using GhostWriter.WorkCreation.Ideation.Review;
using GhostWriter.WorkCreation.UI.Development;

namespace GhostWriter.Patterns.EventBus
{
    #region WORK CREATION EVENTS
    public struct CalculateCompatibility : IEvent { }

    public struct SendReviewData : IEvent
    {
        public ReviewData ReviewData;
    }
    #endregion

    #region WORK DEVELOPMENT EVENTS
    public struct UpdateProgressData : IEvent
    {
        public int Hash;
        public ProgressStage Stage;
        public float Current;
        public float Maximum;
    }

    public struct SetDevelopmentPhase : IEvent
    {
        public int Hash;
        public DevelopmentPhase Phase;
    }

    public struct PrepareSliders : IEvent
    {
        public int Hash;
    }

    public struct SetSliderPoints : IEvent
    {
        public int Hash;
        public PointCategory Category;
        public int Value;
    }

    public struct SendSliderPoints : IEvent
    {
        public int Hash;
    }

    public struct  DeleteSliderData : IEvent
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

    public struct RateWork : IEvent
    {
        public Work WorkToPublish;
    }

    public struct PublishWork : IEvent
    {
        public float FinalScore;
        public Work WorkToPublish;
    }
    #endregion
}
