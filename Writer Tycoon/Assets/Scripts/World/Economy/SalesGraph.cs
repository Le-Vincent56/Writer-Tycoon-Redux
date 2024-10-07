using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WriterTycoon.World.Economy
{
    public class SalesGraph : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        private Text workTitle;
        private RectTransform rectTransform;
        private LayoutElement layoutElement;
        private CanvasGroup card;

        [Header("Graph Variables")]
        [SerializeField] private int maxPoints = 50;
        [SerializeField] private float xSpacing = 10f;

        [Header("Tweening Variables")]
        [SerializeField] private float fadeDuration;
        [SerializeField] private float animationDuration;
        private Tween fadeTween;

        [SerializeField] private List<int> salesData;

        /// <summary>
        /// Initialize the graph with existing sales data
        /// </summary>>
        public void Iniitalize(string title)
        {
            // Verify the LineRenderer component
            if (lineRenderer == null)
                lineRenderer = GetComponentInChildren<LineRenderer>();

            // Verify the Text component
            if (workTitle == null)
                workTitle = GetComponentInChildren<Text>();

            // Verify the Rect Transform component
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();

            // Verify the Layout Element component
            if (layoutElement == null)
                layoutElement = GetComponent<LayoutElement>();

            // Verify the Canvas Group component
            if (card == null)
                card = GetComponent<CanvasGroup>();

            // Set the title
            workTitle.text = title;

            // Initialize the list
            salesData = new();
        }

        /// <summary>
        /// Add a point to the Sales Graph
        /// </summary>
        public void AddPoint(int newSale)
        {
            // Add the sale to the list
            salesData.Add(newSale);

            // Check if the list count exceeds the max amount
            // of points to display
            if (salesData.Count > maxPoints)
            {
                // If so, remove the oldest piece of data
                salesData.RemoveAt(0);
            }

            // Update the graph
            UpdateGraph();
        }

        /// <summary>
        /// Update the Sales Graph with the current sales data
        /// </summary>
        private void UpdateGraph()
        {
            // Set the position count
            lineRenderer.positionCount = salesData.Count;
            Vector3[] positions = new Vector3[salesData.Count];

            // Iterate through each sale data
            for (int i = 0; i < salesData.Count; i++)
            {
                // Create the point
                float x = i * xSpacing;
                float y = salesData[i];
                positions[i] = new Vector3(x, y, 0);
            }

            // Set the positions
            lineRenderer.SetPositions(positions);
        }

        /// <summary>
        /// Show the Progress Bar
        /// </summary>
        public void Show()
        {
            // Ignore the layout
            layoutElement.ignoreLayout = true;

            // Fade in
            Fade(1f, fadeDuration, () => layoutElement.ignoreLayout = false);
        }

        /// <summary>
        /// Hide the Progress Bar
        /// </summary>
        public void Hide(bool destroy = false)
        {
            // Ignore the layout
            layoutElement.ignoreLayout = true;

            // Fade out
            Fade(0f, fadeDuration, () => layoutElement.ignoreLayout = false, Ease.OutQuint);

            // Check whether or not to destroy the graph
            if (destroy)
                Destroy(this);
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
    }
}