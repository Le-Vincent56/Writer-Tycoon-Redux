using DG.Tweening;
using GhostWriter.Patterns.EventBus;
using UnityEngine;

namespace GhostWriter.Entities.Competitors.UI
{
    public class CompetitorWindow : MonoBehaviour
    {
        [SerializeField] private CompetitorList competitorList;
        private CanvasGroup canvasGroup;

        private EventBinding<OpenCompetitorWindow> openCompetitorWindow;
        private EventBinding<CloseCompetitorWindow> closeCompetitorWindow;

        [Header("Tweening Variables")]
        [SerializeField] private float fadeDuration;
        private Tween fadeTween;

        private void Awake()
        {
            // Get components
            competitorList = GetComponentInChildren<CompetitorList>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            openCompetitorWindow = new EventBinding<OpenCompetitorWindow>(Open);
            EventBus<OpenCompetitorWindow>.Register(openCompetitorWindow);

            closeCompetitorWindow = new EventBinding<CloseCompetitorWindow>(Close);
            EventBus<CloseCompetitorWindow>.Register(closeCompetitorWindow);
        }

        private void OnDisable()
        {
            EventBus<OpenCompetitorWindow>.Deregister(openCompetitorWindow);
            EventBus<CloseCompetitorWindow>.Deregister(closeCompetitorWindow);
        }

        /// <summary>
        /// Callback function to handle opening the Competitor Window
        /// </summary>
        private void Open()
        {
            // Update the Competitor List
            competitorList.UpdateList();

            // Fade in
            Fade(1f, fadeDuration, () =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
        }

        /// <summary>
        /// Callback function to handle closing the Competitor Window
        /// </summary>
        private void Close() 
        {
            // Fade out
            Fade(0f, fadeDuration, () =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
        }

        private void Fade(float endValue, float duration, TweenCallback onComplete = null)
        {
            // Kill the Fade Tween if it already exists
            fadeTween?.Kill();

            // Set the Fade Tween
            fadeTween = canvasGroup.DOFade(endValue, duration);

            // Exit case - no completion action was given
            if (onComplete == null) return;

            // Hook up completion actions
            fadeTween.onComplete += onComplete;
        }
    }
}
