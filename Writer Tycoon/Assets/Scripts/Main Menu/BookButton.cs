using DG.Tweening;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WriterTycoon.MainMenu
{
    public class BookButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private static int OpenHash = Animator.StringToHash("Open");
        private static int CloseHash = Animator.StringToHash("Close");

        private Animator bookAnimator;
        private Transform bookTransform;

        [Header("General"), Space()]
        [SerializeField] private bool active;

        [Header("Tweening Variables")]
        private Tween scaleTween;
        private Tween translateTween;
        [SerializeField] private Vector3 toScale;
        [SerializeField] private Vector3 initialScale;
        [SerializeField] private Vector3 toPlace;
        [SerializeField] private Vector3 initialPlace;
        [SerializeField] private float scaleDuration;
        [SerializeField] private float translateDuration;

        private void Awake()
        {
            // Get components
            bookAnimator = GetComponentInChildren<Animator>();
            bookTransform = bookAnimator.transform;

            // Set the initial scale and to-Scale
            initialScale = bookTransform.localScale;
            toScale = initialScale + toScale;

            // Set the initial place and the to-place
            initialPlace = bookTransform.localPosition;
            toPlace = initialPlace + toPlace;
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
    }
}