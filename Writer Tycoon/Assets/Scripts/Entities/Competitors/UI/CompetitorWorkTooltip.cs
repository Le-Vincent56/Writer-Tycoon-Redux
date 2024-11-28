using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GhostWriter.Entities.Competitors.UI
{
    public class CompetitorWorkTooltip : MonoBehaviour
    {
        [Header("UI Components"), Space()]
        [SerializeField] private Canvas canvas;
        private CanvasGroup canvasGroup;
        [SerializeField] private Text titleText;
        [SerializeField] private Text descriptionText;
        [SerializeField] private Text topicText;
        [SerializeField] private Text genreText;
        [SerializeField] private Text audienceText;
        [SerializeField] private Text workTypeText;
        [SerializeField] private Text salesText;

        [Header("Tweening Variables")]
        [SerializeField] private float fadeDuration;
        private Tween fadeTween;

        private void Awake()
        {
            // Get components
            canvasGroup = GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// Show the Competitor Work Tooltip
        /// </summary>
        public void ShowTooltip(
            string title, string description, string topic, 
            string genre, string audience, string workType, 
            string sales
        )
        {
            // Get the mouse position
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            // Convert screen space to world space for the Canvas
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, canvas.planeDistance));

            // Add padding
            Vector3 positionWithPadding = new Vector3(worldPosition.x + 0.1f, worldPosition.y + 0.1f, worldPosition.z);

            // Set the position
            transform.position = positionWithPadding;

            // Set the data
            titleText.text = title;
            descriptionText.text = description;
            topicText.text = topic;
            genreText.text = genre;
            audienceText.text = audience;
            workTypeText.text = workType;
            salesText.text = sales;
            
            // Fade in
            Fade(1f, fadeDuration, () =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
        }

        /// <summary>
        /// Hide the Competitor Work Tooltip
        /// </summary>
        public void HideTooltip()
        {
            // Fade out
            Fade(0f, fadeDuration, () =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
        }

        /// <summary>
        /// Handle fading for the Trait Tooltip
        /// </summary>
        private void Fade(float endValue, float duration, TweenCallback onComplete = null)
        {
            // Kill any existing fade tween
            fadeTween?.Kill();

            // Set the fade
            fadeTween = canvasGroup.DOFade(endValue, duration);

            // Exit case - there's no completion action
            if (onComplete == null) return;

            // Hook up completion actions
            fadeTween.onComplete += onComplete;
        }
    }
}
