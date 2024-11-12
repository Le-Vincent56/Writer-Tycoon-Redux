using DG.Tweening;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

namespace WriterTycoon.MainMenu
{
    public class BookButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private static int OpenHash = Animator.StringToHash("Open");
        private static int CloseHash = Animator.StringToHash("Close");

        private Animator bookAnimator;
        private Transform bookTransform;
        private CanvasGroup canvasGroup;

        [Header("General"), Space()]
        [SerializeField] private bool active;

        
        private Tween initialTween;
        private Tween scaleTween;
        private Tween translateTween;
        private Tween fadeTween;
        [Header("Tweening Variables")]
        [SerializeField] private Vector3 toScale;
        [SerializeField] private Vector3 initialScale;
        [SerializeField] private Vector3 toPlace;
        [SerializeField] private Vector3 initialPlace;
        [SerializeField] private float scaleDuration;
        [SerializeField] private float translateDuration;
        [SerializeField] private float startDuration;
        [SerializeField] private float fadeDuration;

        private void Awake()
        {
            // Get components
            bookAnimator = GetComponentInChildren<Animator>();
            bookTransform = bookAnimator.transform;
            canvasGroup = GetComponentInChildren<CanvasGroup>();

            // Set the initial scale and to-Scale
            initialScale = bookTransform.localScale;
            toScale = initialScale + toScale;

            // Set the initial place and the to-place
            initialPlace = bookTransform.localPosition;
            toPlace = initialPlace + toPlace;

            initialTween = transform.DOLocalMoveX(transform.position.x, startDuration)
                .From(new Vector3(transform.position.x + 1000f, transform.position.y, transform.position.z))
                .SetEase(Ease.OutExpo)
                .OnComplete(ShowText);
        }

        private void OnDestroy()
        {
            // Kill any existing tweens
            initialTween?.Kill();
            scaleTween?.Kill();
            translateTween?.Kill();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Exit case - the Book Button is not active
            if (!active) return;

            Scale(toScale, scaleDuration, null, Ease.OutBounce);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // Exit case - the Book Button is not active
            if (!active) return;

            Scale(initialScale, scaleDuration, null, Ease.OutBounce);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // Exit case - the Book Button is not active
            if (!active) return;

            Scale(initialScale, scaleDuration, () =>
            {
                Translate(toPlace, translateDuration, OnClick, Ease.OutExpo);
            }, Ease.OutBounce);
        }

        private void ShowText()
        {
            Fade(1f, fadeDuration);
        }

        private void OnClick()
        {
            // Open the book
            bookAnimator.CrossFade(OpenHash, 0f);

            // De-activate the button
            active = false;
        }

        private void Scale(Vector3 endValue, float duration, TweenCallback onComplete = null, Ease easeType = Ease.Unset)
        {
            // Kill the current scale Tween if it exists
            scaleTween?.Kill();

            // Set the Tween
            scaleTween = bookTransform.DOScale(endValue, duration);

            // Set an Ease type if provided
            if (easeType != Ease.Unset)
                scaleTween.SetEase(easeType);

            // Exit case - no completion action was provided
            if (onComplete == null) return;

            // Hook up completion actions
            scaleTween.onComplete += onComplete;
        }

        private void Translate(Vector3 endValue, float duration, TweenCallback onComplete = null, Ease easeType = Ease.Unset)
        {
            // Kill the current translate Tween if it exists
            translateTween?.Kill();

            // Set the Tween
            translateTween = bookTransform.DOLocalMove(endValue, duration);

            // Set an Ease type if provided
            if (easeType != Ease.Unset)
                translateTween.SetEase(easeType);

            // Exit case - no completion action was provided
            if (onComplete == null) return;

            // Hook up completion actions
            translateTween.onComplete += onComplete;
        }

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