using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.WorkCreation.Publication;
using WriterTycoon.WorkCreation.UI.Development;
using WriterTycoon.World.Interactables;

namespace WriterTycoon.World.Economy
{
    public class SalesGraph : MonoBehaviour
    {
        private PublishedWork workToGraph;

        [Header("Tweening Variables")]
        [SerializeField] private float translateValue;
        [SerializeField] private float animateDuration;

        [Header("Graph Variables")]
        [SerializeField] private float xSpacing;
        [SerializeField] private float yScale;
        private LineRenderer lineRenderer;

        private Text titleText;
        private RectTransform rectTransform;
        private LayoutElement layoutElement;
        private CanvasGroup card;
        private Tween fadeTween;

        /// <summary>
        /// Initialize the Sales Graph
        /// </summary>
        public void Initialize(PublishedWork workToGraph)
        {
            // Verif the LineRenderer component
            if (lineRenderer == null)
                lineRenderer = GetComponentInChildren<LineRenderer>();

            // Verify the Text component
            if(titleText == null)
                titleText = GetComponentInChildren<Text>();

            // Verify the Rect Transform component
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();

            // Verify the Layout Element component
            if (layoutElement == null)
                layoutElement = GetComponent<LayoutElement>();

            // Verify the Canvas Group component
            if (card == null)
                card = GetComponent<CanvasGroup>();

            // Set the work to graph
            this.workToGraph = workToGraph;

            // Set the title text
            titleText.text = workToGraph.Title;
        }

        /// <summary>
        /// Update the Sales graph according to a set of SalesData
        /// </summary>
        private void UpdateSalesGraph(List<SalesData> salesHistory)
        {
            // Exit case - there's nothing to graph
            if (salesHistory.Count <= 0) return;

            // Create a list to store the points
            List<Vector3> points = new();

            foreach(SalesData salesData in salesHistory)
            {
                // Calculate the point to graph
                float x = salesData.WeekNumber * xSpacing;
                float y = salesData.CopiesSold * yScale;

                // Add the point to the list
                points.Add(new Vector3(x, y, 0));
            }

            // Set the position count
            lineRenderer.positionCount = points.Count;

            // Set the positions
            lineRenderer.SetPositions(points.ToArray());
        }

        /// <summary>
        /// Destroy the Sales Graph
        /// </summary>
        private void DestroyGraph()
        {
            // Destroy the object
            Destroy(this);
        }

        /// <summary>
        /// Show the Progress Bar
        /// </summary>
        public void Show()
        {
            // Ignore the layout
            layoutElement.ignoreLayout = true;

            // Fade in
            Fade(1f, animateDuration);
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

            // Check whether or not to destroy the graph
            if (destroy)
                DestroyGraph();
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