using UnityEngine;
using DG.Tweening;
using WriterTycoon.WorkCreation.UI.States;
using WriterTycoon.Patterns.StateMachine;

namespace WriterTycoon.WorkCreation.UI
{
    public class CreateGameWindow : MonoBehaviour
    {
        private StateMachine stateMachine;
        [SerializeField] private CanvasGroup window;
        [SerializeField] private int state;
        [SerializeField] private GameObject[] screens = new GameObject[2];

        private Sequence sequence;

        public int IDEATION { get => 0; }
        public int TOPIC { get => 1; }
        public int GENRE { get => 2; }

        private void Awake()
        {
            // Set the ideation state
            state = IDEATION;

            // Initialize the state machine
            stateMachine = new StateMachine();

            // Construct states
            IdeationState ideationState = new IdeationState(screens[IDEATION]);
            TopicState topicState = new TopicState(screens[TOPIC]);
            GenreState genreState = new GenreState(screens[GENRE]);

            // Set state transitions
            stateMachine.At(ideationState, topicState, new FuncPredicate(() => state == TOPIC));

            stateMachine.At(topicState, ideationState, new FuncPredicate(() => state == IDEATION));
            stateMachine.At(topicState, genreState, new FuncPredicate(() => state == GENRE));

            stateMachine.At(genreState, topicState, new FuncPredicate(() => state == TOPIC));

            // Set the initial state
            stateMachine.SetState(ideationState);
        }

        private void Update()
        {
            // Update the state machine
            stateMachine.Update();
        }

        private void FixedUpdate()
        {
            // Update the state machine
            stateMachine.FixedUpdate();
        }

        /// <summary>
        /// Set the state of Game Creation
        /// </summary>
        public void SetState(int state) => this.state = state;

        public void ShowWindow()
        {
            Fade(1f, 0.3f, () => {
                window.interactable = true;
                window.blocksRaycasts = true;
            });



            //windowObject.transform.DOMoveY(-20f, 0.5f)
            //    .From()
            //    .SetEase(Ease.InQuint);
        }

        public void HideWindow()
        {
            Fade(0f, 0.3f, () => {
                window.interactable = false;
                window.blocksRaycasts = false;
            });

            //windowObject.transform.DOMoveY(-20f, 0.5f)
            //    .SetEase(Ease.OutQuint);
        }

        private void Fade(float endFadeValue, float duration, TweenCallback onEnd)
        {

            // Kill the current tween sequence if it exists
            sequence?.Kill(true);

            // Set the fade tween
            sequence = DOTween.Sequence();

            Tween fadeTween = window.DOFade(endFadeValue, duration);
            Tween translateTween = (endFadeValue > 0)
                ? window.transform.DOMoveY(-2f, duration).From()
                : window.transform.DOMoveY(-2f, duration);

            sequence.Append(fadeTween);
            sequence.Append(translateTween);

            // Hook up callback events
            sequence.onComplete += onEnd;
        }
    }
}