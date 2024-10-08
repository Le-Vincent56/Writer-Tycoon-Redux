using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WriterTycoon.World.Economy
{
    public class SalesGraph : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        private Text workTitle;
        private RectTransform rectTransform;
        private LayoutElement layoutElement;
        private CanvasGroup card;

        [Header("Graph Variables")]
        [SerializeField] private bool updateGraph;
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

            // Set to update the graph
            updateGraph = true;
        }

        /// <summary>
        /// Add a point to the Sales Graph
        /// </summary>
        public void AddPoint(int newSale)
        {
            // Exit case - if not supposed to update the graph
            if (!updateGraph) return;

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
            // Exit case - if not supposed to update the graph
            if (!updateGraph) return;

            // Set the position count
            lineRenderer.positionCount = salesData.Count;

            // Store the current positions
            Vector3[] currentPositions = new Vector3[salesData.Count];
            lineRenderer.GetPositions(currentPositions);

            // Create a new array for the updated positions
            Vector3[] newPositions = new Vector3[salesData.Count];

            // Calculate new positions based on salesData
            for (int i = 0; i < salesData.Count; i++)
            {
                float x = i * xSpacing;
                float y = salesData[i];
                newPositions[i] = new Vector3(x, y, 0);
            }

            // Shift all existing points left if maxPoints is reached
            if (salesData.Count >= maxPoints)
            {
                for (int i = 0; i < salesData.Count - 1; i++)
                {
                    // Shift the existing points to the left by one
                    Vector3 shiftedPosition = newPositions[i];
                    lineRenderer.SetPosition(i, shiftedPosition);
                }
            }

            // Animate the last point added to smoothly move the line
            if (salesData.Count > 1)
            {
                int lastIndex = salesData.Count - 1;

                // Start from the second last point (because the first one might have been shifted)
                Vector3 startValue = currentPositions.Length > 1 ? currentPositions[lastIndex - 1] : new Vector3((lastIndex - 1) * xSpacing, 0, 0);
                Vector3 endValue = newPositions[lastIndex];

                // Tween both x and y values of the last point added
                DOTween.To(() => startValue,
                           v => {
                               // Update both x and y values
                               startValue.x = v.x;
                               startValue.y = v.y;
                               lineRenderer.SetPosition(lastIndex, startValue);
                           },
                           endValue,
                           animationDuration)
                       .OnUpdate(() => lineRenderer.SetPosition(lastIndex, startValue));
            }
            else
            {
                // If it's the first point, set it immediately
                lineRenderer.SetPosition(0, newPositions[0]);
            }
        }

        /// <summary>
        /// Stop the Sales Graph from updating
        /// </summary>
        public void Stop() => updateGraph = false;

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

            Color2 startColor = new Color2(lineRenderer.startColor, lineRenderer.endColor);
            Color2 invisible = new Color2(new Color(1f, 1f, 1f, 0f), new Color(1f, 1f, 1f, 0f));

            lineRenderer.DOColor(startColor, invisible, animationDuration).SetEase(Ease.OutQuint);

            // Fade out
            Fade(0f, fadeDuration, () =>
            {
                layoutElement.ignoreLayout = false;

                // Check whether or not to destroy the graph
                if (destroy)
                        DestroyGraph();
            }, Ease.OutQuint);
        }

        /// <summary>
        /// Destroy the Sales Graph
        /// </summary>
        private void DestroyGraph()
        {
            // Clear the linerenderer
            lineRenderer.positionCount = 0;
            lineRenderer.SetPositions(new Vector3[0]);

            // Destroy the graph
            Destroy(gameObject);
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