using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.WorkCreation.Development.Tracker;
using WriterTycoon.WorkCreation.Ideation.About;
using WriterTycoon.WorkCreation.Ideation.Review;
using WriterTycoon.WorkCreation.Publication;
using WriterTycoon.WorkCreation.UI.Development;
using WriterTycoon.World.Interactables;

namespace WriterTycoon.Patterns.EventBus
{
    #region INTERACT UI EVENTS
    public struct SetInteracting : IEvent
    {
        public bool Interacting;
    }

    public struct CloseInteractMenus : IEvent { }

    public struct OpenPublicationHistory : IEvent { }
    #endregion

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

    public struct OpenSliderWindow : IEvent 
    {
        public int Hash;
    }

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

    public struct DeleteProgressCard : IEvent
    {
        public int Hash;
    }

    public struct SetProgressCardTitle : IEvent
    {
        public string Title;
    }

    public struct ShowProgressText : IEvent
    {
        public int Hash;
        public ProgressStage Stage;
        public string Text;
    }
    
    public struct UpdateProgressText : IEvent
    {
        public int Hash;
        public ProgressStage Stage;
        public string Text;
    }

    public struct SetProgressStage : IEvent
    {
        public int Hash;
        public ProgressStage Stage;
    }

    public struct HideProgressText : IEvent 
    {
        public int Hash;
        public ProgressStage Stage;
    }

    public struct SetCanInteract : IEvent
    {
        public bool CanInteract;
    }

    public struct ToggleInteractMenu : IEvent
    {
        public Interactable Interactable;
        public Vector2 CursorPosition;
        public bool Opening;
        public InteractableType InteractableType;
    }
    #endregion

    #region RATING UI EVENTS
    public struct SetReviewText : IEvent
    {
        public int ID;
        public string Text;
    }

    public struct ShowReviewWindow : IEvent
    {
        public int Score;
        public AboutInfo AboutInfo;
    }
    #endregion

    #region PUBLICATION HISTORY EVENTS
    public struct CreatePublicationCard : IEvent
    {
        public PublishedWork PublishedWork;
    }

    public struct UpdatePublicationCard : IEvent
    {
        public int Hash;
        public PublishedWork PublishedWork;
    }

    public struct ClosePublicationHistory : IEvent { }
    #endregion
}
