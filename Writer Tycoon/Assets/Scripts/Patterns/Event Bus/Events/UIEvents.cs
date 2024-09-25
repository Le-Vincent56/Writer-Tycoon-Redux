using UnityEngine;
using WriterTycoon.WorkCreation.Development.Tracker;
using WriterTycoon.WorkCreation.Ideation.Review;
using WriterTycoon.WorkCreation.UI.Development;
using WriterTycoon.World.Interactables;

namespace WriterTycoon.Patterns.EventBus
{
    #region WORK CREATION UI EVENTS
    public struct OpenCreateWorkMenu : IEvent { }

    public struct CloseCreateWorkMenu : IEvent { }

    public struct ShowEstimationText : IEvent
    {
        public bool ShowText;
    }

    public struct NotifyFailedCreation : IEvent
    {
        public ReviewData ReviewData;
    }

    public struct NotifySuccessfulCreation : IEvent
    {
        public ReviewData ReviewData;
    }

    public struct SetInteracting : IEvent
    {
        public bool Interacting;
    }

    public struct CloseInteractMenus : IEvent { }

    public struct OpenSliderWindow : IEvent { }

    public struct CloseSliderWindow : IEvent { }

    public struct SetSliderPointText : IEvent
    {
        public DevelopmentPhase IntendedPhase;
        public int PointsRemaining;
    }

    public struct CreateProgressCard : IEvent 
    {
        public int Hash;
        public string Title;
    }

    public struct SetProgressCardTitle : IEvent
    {
        public string Title;
    }

    public struct ShowProgressText : IEvent
    {
        public string Text;
    }

    public struct HideProgressText : IEvent { }

    public struct SetCanInteract : IEvent
    {
        public bool CanInteract;
    }

    public struct ToggleInteractMenu : IEvent
    {
        public Vector2 CursorPosition;
        public bool Opening;
        public InteractableType InteractableType;
    }
    #endregion
}
