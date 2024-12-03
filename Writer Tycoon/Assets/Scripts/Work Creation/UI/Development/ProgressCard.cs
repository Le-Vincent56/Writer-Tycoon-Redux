using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using GhostWriter.Patterns.EventBus;
using GhostWriter.Patterns.StateMachine;
using GhostWriter.WorkCreation.UI.Development.States;

namespace GhostWriter.WorkCreation.UI.Development
{
    public enum ProgressStage
    {
        Development,
        Error,
        Polish
    }

    public class ProgressCard : MonoBehaviour
    {
        [SerializeField] private int workHash;
        [SerializeField] private ProgressStage currentStage;
        [SerializeField] private ProgressTitle progressTitle;

        [SerializeField] private ProgressBar developmentBar;
        [SerializeField] private ProgressText developmentText;

        [SerializeField] private ProgressBar errorBar;
        [SerializeField] private ProgressText errorText;

        [SerializeField] private ProgressBar polishBar;
        [SerializeField] private ProgressText polishText;

        [SerializeField] private CanvasGroup[] canvasGroups;
        [SerializeField] private float translateValue;
        [SerializeField] private float animateDuration;
        private RectTransform rectTransform;
        private LayoutElement layoutElement;
        private CanvasGroup card;
        private Vector3 originalPosition;
        private Tween fadeTween;
        private Tween translateTween;

        private StateMachine stateMachine;

        private EventBinding<SetProgressStage> setProgressStageEvent;
        private EventBinding<UpdateProgressData> updateProgressDataEvent;
        private EventBinding<ShowProgressText> showProgressTextEvent;
        private EventBinding<UpdateProgressText> updateProgressTextEvent;
        private EventBinding<HideProgressText> hideProgressTextEvent;

        private void OnEnable()
        {
            setProgressStageEvent = new EventBinding<SetProgressStage>(SetState);
            EventBus<SetProgressStage>.Register(setProgressStageEvent);

            updateProgressDataEvent = new EventBinding<UpdateProgressData>(UpdateProgressBar);
            EventBus<UpdateProgressData>.Register(updateProgressDataEvent);

            showProgressTextEvent = new EventBinding<ShowProgressText>(ShowText);
            EventBus<ShowProgressText>.Register(showProgressTextEvent);

            updateProgressTextEvent = new EventBinding<UpdateProgressText>(UpdateText);
            EventBus<UpdateProgressText>.Register(updateProgressTextEvent);

            hideProgressTextEvent = new EventBinding<HideProgressText>(HideText);
            EventBus<HideProgressText>.Register(hideProgressTextEvent);
        }

        private void OnDisable()
        {
            EventBus<SetProgressStage>.Deregister(setProgressStageEvent);
            EventBus<UpdateProgressData>.Deregister(updateProgressDataEvent);
            EventBus<ShowProgressText>.Deregister(showProgressTextEvent);
            EventBus<UpdateProgressText>.Deregister(updateProgressTextEvent);
            EventBus<HideProgressText>.Deregister(hideProgressTextEvent);
        }

        private void Update()
        {
            // Update the state machine
            stateMachine.Update();
        }

        private void FixedUpdate()
        {
            // Fixed update the state machine
            stateMachine.FixedUpdate();
        }

        /// <summary>
        /// Initialize the Progress Card
        /// </summary>
        public void Initialize(int workHash, string title)
        {
            // Verify the Progress Title
            if (progressTitle == null)
                progressTitle ??= GetComponentInChildren<ProgressTitle>();

            // Verify the Rect Transform
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();

            // Verify the Layout Element
            if (layoutElement == null)
                layoutElement = GetComponent<LayoutElement>();

            // Verify the Canvas Group
            if (card == null)
                card = GetComponent<CanvasGroup>();

            // Set variables
            this.workHash = workHash;
            originalPosition = rectTransform.anchoredPosition;
            currentStage = ProgressStage.Development;

            // Initialize the Progress Title
            progressTitle.Initialize(title);

            // Initialize the state machine
            InitializeStateMachine();
        }

        /// <summary>
        /// Callback function to update the Progress Bar
        /// </summary>
        /// <param name="eventData"></param>
        private void UpdateProgressBar(UpdateProgressData eventData)
        {
            // Exit case - if the event Hash does not equal the card's hash
            if (eventData.Hash != workHash) return;

            switch (eventData.Stage)
            {
                case ProgressStage.Development:
                    developmentBar.SetCurrentFill(eventData.Current, eventData.Maximum);
                    break;

                case ProgressStage.Error:
                    errorBar.SetCurrentFill(eventData.Current, eventData.Maximum);
                    break;

                case ProgressStage.Polish:
                    polishBar.SetCurrentFill(eventData.Current, eventData.Maximum);
                    break;
            }
        }

        /// <summary>
        /// Callback function to handle showing Progress Text
        /// </summary>
        private void ShowText(ShowProgressText eventData)
        {
            // Exit case - if the event Hash does not equal the card's hash
            if (eventData.Hash != workHash) return;

            // Fade in and set text depending on the stage of progress
            switch (eventData.Stage)
            {
                case ProgressStage.Development:
                    developmentText.FadeInAndSetText(eventData.Text);
                    break;
                case ProgressStage.Error:
                    errorText.FadeInAndSetText(eventData.Text);
                    break;
                case ProgressStage.Polish:
                    polishText.FadeInAndSetText(eventData.Text);
                    break;
            }
        }

        /// <summary>
        /// Callback function to handle updating the Progress Text
        /// </summary>
        private void UpdateText(UpdateProgressText eventData)
        {
            // Exit case - if the event Hash does not equal the card's hash
            if (eventData.Hash != workHash) return;

            // Update the text (without fading) depending on the stage of progress
            switch (eventData.Stage)
            {
                case ProgressStage.Development:
                    developmentText.UpdateText(eventData.Text);
                    break;

                case ProgressStage.Error:
                    errorText.UpdateText(eventData.Text);
                    break;

                case ProgressStage.Polish:
                    polishText.UpdateText(eventData.Text);
                    break;
            }
        }

        /// <summary>
        /// Callback function to handle hiding Progress Text
        /// </summary>
        private void HideText(HideProgressText eventData)
        {
            // Exit case - if the event Hash does not equal the card's hash
            if (eventData.Hash != workHash) return;

            // Fade out the text depending on the stage of progress
            switch (eventData.Stage)
            {
                case ProgressStage.Development:
                    developmentText.FadeOutText();
                    break;
                case ProgressStage.Error:
                    errorText.FadeOutText();
                    break;
                case ProgressStage.Polish:
                    polishText.FadeOutText();
                    break;
            }
        }

        /// <summary>
        /// Publish a work
        /// </summary>
        public void Publish()
        {
            // Close the interact menus
            EventBus<CloseInteractMenus>.Raise(new CloseInteractMenus());

            // Set the player to not working
            EventBus<ChangePlayerWorkState>.Raise(new ChangePlayerWorkState()
            {
                Working = false
            });

            // End editing
            EventBus<EndEditing>.Raise(new EndEditing()
            {
                Hash = workHash
            });

            // Delete this progress card
            EventBus<DeleteProgressCard>.Raise(new DeleteProgressCard()
            {
                Hash = workHash
            });
        }

        /// <summary>
        /// Initialize the state machine for the Progress Card
        /// </summary>
        private void InitializeStateMachine()
        {
            // Initialize the state machine
            stateMachine = new StateMachine();

            // Create the states
            ProgressDevelopmentState developmentState = new(canvasGroups[0]);
            ProgressErrorState errorState = new(canvasGroups[1]);
            ProgressPolishState polishState = new(this, canvasGroups[2]);

            // Define state transitions
            stateMachine.At(developmentState, errorState, new FuncPredicate(() => currentStage == ProgressStage.Error));
            stateMachine.At(errorState, polishState, new FuncPredicate(() => currentStage == ProgressStage.Polish));

            // Set an initial state
            stateMachine.SetState(developmentState);
        }

        /// <summary>
        /// Callback function to handle state changing
        /// </summary>
        public void SetState(SetProgressStage eventData)
        {
            // Exit case - if the event Hash does not equal the card's hash
            if (eventData.Hash != workHash) return;

            // Set the current stage
            currentStage = eventData.Stage;
        }

        /// <summary>
        /// Set Canvas Group settings
        /// </summary>
        public void SetCanvasGroupSettings(bool interactable, bool blockRaycasts)
        {
            card.interactable = interactable;
            card.blocksRaycasts = blockRaycasts;
        }

        /// <summary>
        /// Show the Progress Bar
        /// </summary>
        public void Show()
        {
            // Ignore the layout
            layoutElement.ignoreLayout = true;

            // Set the window's initial position to be below
            Vector3 startPos = new Vector3(
                originalPosition.x,
                originalPosition.y - translateValue,
                originalPosition.z
            );
            rectTransform.anchoredPosition = startPos;

            // Fade in
            Fade(1f, animateDuration);

            // Translate up
            Translate(translateValue, animateDuration, () => layoutElement.ignoreLayout = false);
        }

        /// <summary>
        /// Hide the Progress Bar
        /// </summary>
        public void Hide(bool destroy = false)
        {
            // Ignore the layout
            layoutElement.ignoreLayout = true;

            // Fade out
            Fade(0f, animateDuration, null, Ease.OutQuint);

            // Translate down
            Translate(-translateValue, animateDuration, 
                () => {
                    // Ignore the layout
                    layoutElement.ignoreLayout = false;

                    // Check whether or not to destroy the object
                    if (destroy)
                        // If so, destroy the object
                        Destroy(this);
                }, Ease.OutQuint
            );
        }

        /// <summary>
        /// Handle fading for the Progress Bar
        /// </summary>
        private void Fade(float endFadeValue, float duration, TweenCallback onEnd = null, Ease easeType = Ease.InQuint)
        {
            // Kill the current fade tween if it exists
            fadeTween?.Kill(false);

            // Set the fade animation
            fadeTween = card.DOFade(endFadeValue, duration)
                .SetEase(easeType);

            // Exit case - if there is no given Tween Callback
            if (onEnd == null) return;

            // Hook up callback events
            fadeTween.onComplete += onEnd;
        }

        /// <summary>
        /// Handle translating for the Window
        /// </summary>
        private void Translate(float endTranslateValue, float duration, TweenCallback onEnd = null, Ease easeType = Ease.InQuint)
        {
            // Kill the current translate tween if it exists
            translateTween?.Kill(false);

            // Calculate the target position
            float targetPos = rectTransform.anchoredPosition.y + endTranslateValue;

            // Set the tween animation
            translateTween = rectTransform.DOAnchorPosY(targetPos, duration)
                .SetEase(easeType);

            // Exit case - if there is no given Tween Callback
            if (onEnd == null) return;

            // Hook up callback events
            translateTween.onComplete += onEnd;
        }
    }
}