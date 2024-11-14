using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GhostWriter.Patterns.EventBus;

namespace GhostWriter.MainMenu
{
    public class BookButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private static int OpenHash = Animator.StringToHash("Open");

        private Animator bookAnimator;
        private CanvasGroup canvasGroup;
        [SerializeField] private ParticleSystem leftwardDust;

        [Header("General"), Space()]
        [SerializeField] private bool active;
        
        [Header("Tweening Variables")]
        [SerializeField] private Vector3 toScale;
        [SerializeField] private Vector3 initialScale;
        [SerializeField] private Vector3 toPlace;
        [SerializeField] private Vector3 initialPlace;
        [SerializeField] private float scaleDuration;
        [SerializeField] private float translateDuration;
        [SerializeField] private float startDuration;
        [SerializeField] private float fadeDuration;
        private Tween initialTween;
        private Tween scaleTween;
        private Tween translateTween;
        private Tween fadeTween;

        private EventBinding<ActivateMainMenu> activateMainMenuEvent;

        private void Awake()
        {
            // Get components
            bookAnimator = GetComponentInChildren<Animator>();
            canvasGroup = GetComponentInChildren<CanvasGroup>();

            // Set the initial scale and to-Scale
            initialScale = transform.localScale;

            // Set the initial place and the to-place
            initialPlace = transform.position;
            toPlace = initialPlace + toPlace;

            initialTween = transform.DOLocalMoveX(transform.position.x, startDuration)
                .From(new Vector3(transform.position.x + 1000f, transform.position.y, transform.position.z))
                .SetEase(Ease.InCubic)
                .OnComplete(() => leftwardDust.Play());
        }

        private void OnEnable()
        {
            activateMainMenuEvent = new EventBinding<ActivateMainMenu>(DisableButton);
            EventBus<ActivateMainMenu>.Register(activateMainMenuEvent);
        }

        private void OnDisable()
        {
            EventBus<ActivateMainMenu>.Deregister(activateMainMenuEvent);
        }

        private void OnDestroy()
        {
            // Kill any existing tweens
            initialTween?.Kill();
            scaleTween?.Kill();
            translateTween?.Kill();
        }

        /// <summary>
        /// Deactivate the Book Button
        /// </summary>
        private void DisableButton()
        {
            // Disable components
            Image buttonImage = GetComponent<Image>();
            buttonImage.enabled = false;
        }

        /// <summary>
        /// Handle the mouse pointer entering the Book Button area
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            // Exit case - the Book Button is not active
            if (!active) return;

            Scale(toScale, scaleDuration, null, Ease.OutBounce);
        }

        /// <summary>
        /// Handle the mouse pointer exiting the Book Button area
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            // Exit case - the Book Button is not active
            if (!active) return;

            Scale(initialScale, scaleDuration, null, Ease.OutBounce);
        }

        /// <summary>
        /// Handle the mouse pointer clicking the Book Button area
        /// </summary>
        public void OnPointerClick(PointerEventData eventData)
        {
            // Exit case - the Book Button is not active
            if (!active) return;

            // Scale down
            Scale(initialScale, scaleDuration, () =>
            {
                Translate(toPlace, translateDuration, OnClick, Ease.OutExpo);
            }, Ease.OutBounce);
        }

        /// <summary>
        /// Callback function for when the Book Button is clicked
        /// </summary>
        private void OnClick()
        {
            // Fade out
            Fade(0f, 0.1f);

            // Open the book
            bookAnimator.CrossFade(OpenHash, 0f);

            // De-activate the button
            active = false;
        }

        /// <summary>
        /// Scale the Book Button
        /// </summary>
        private void Scale(Vector3 endValue, float duration, TweenCallback onComplete = null, Ease easeType = Ease.Unset)
        {
            // Kill the current scale Tween if it exists
            scaleTween?.Kill();

            // Set the Tween
            scaleTween = transform.DOScale(endValue, duration);

            // Set an Ease type if provided
            if (easeType != Ease.Unset)
                scaleTween.SetEase(easeType);

            // Exit case - no completion action was provided
            if (onComplete == null) return;

            // Hook up completion actions
            scaleTween.onComplete += onComplete;
        }

        /// <summary>
        /// Translate the Book Button
        /// </summary>
        private void Translate(Vector3 endValue, float duration, TweenCallback onComplete = null, Ease easeType = Ease.Unset)
        {
            // Kill the current translate Tween if it exists
            translateTween?.Kill();

            // Set the Tween
            translateTween = transform.DOMove(endValue, duration);

            // Set an Ease type if provided
            if (easeType != Ease.Unset)
                translateTween.SetEase(easeType);

            // Exit case - no completion action was provided
            if (onComplete == null) return;

            // Hook up completion actions
            translateTween.onComplete += onComplete;
        }

        /// <summary>
        /// Fade the Book Button
        /// </summary>
        private void Fade(float endValue, float duration, TweenCallback onComplete = null, Ease easeType = Ease.Unset)
        {
            // Kill the current fade Tween if it exists
            fadeTween?.Kill();

            // Set the Tween
            fadeTween = canvasGroup.DOFade(endValue, duration);

            // Set an Ease type if provided
            if (easeType != Ease.Unset)
                fadeTween.SetEase(easeType);

            // Exit case - no completion action was provided
            if (onComplete == null) return;

            // Hook up completion actions
            fadeTween.onComplete += onComplete;
        }
    }
}