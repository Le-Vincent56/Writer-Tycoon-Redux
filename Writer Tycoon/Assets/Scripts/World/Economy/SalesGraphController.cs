using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.World.Economy
{
    public class SalesGraphController : SerializedMonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Dictionary<int, SalesGraph> graphedWorks;

        [SerializeField] private GameObject cardContainer;
        [SerializeField] private SalesWindowButton button;

        private bool showing;

        private RectTransform rectTransform;

        [SerializeField] private float translateValue;
        [SerializeField] private float animateDuration;
        private Tween translateTween;

        private EventBinding<CreateSalesGraph> createSalesGraphEvent;
        private EventBinding<DestroySalesGraph> destroySalesGraphEvent;

        private void Awake()
        {
            // Verify the Rect Transform component
            if(rectTransform == null)
                rectTransform = GetComponent<RectTransform>();

            // Set showing to false
            showing = false;

            // Initialize the dictionary
            graphedWorks = new();

            // Initialize the button
            button.Initialize(this);
        }

        private void OnEnable()
        {
            createSalesGraphEvent = new EventBinding<CreateSalesGraph>(CreateSalesGraph);
            EventBus<CreateSalesGraph>.Register(createSalesGraphEvent);

            destroySalesGraphEvent = new EventBinding<DestroySalesGraph>(RemoveSalesGraph);
            EventBus<DestroySalesGraph>.Register(destroySalesGraphEvent);
        }

        private void OnDisable()
        {
            EventBus<CreateSalesGraph>.Deregister(createSalesGraphEvent);
            EventBus<DestroySalesGraph>.Deregister(destroySalesGraphEvent);
        }

        /// <summary>
        /// Create a progress card
        /// </summary>
        private void CreateSalesGraph(CreateSalesGraph eventData)
        {
            // Instantiate the card prefab as a child of this transform
            GameObject cardObj = Instantiate(cardPrefab, cardContainer.transform);

            // Extract the ProgressCard component
            SalesGraph cardComponent = cardObj.GetComponent<SalesGraph>();

            // Initialize the card component
            cardComponent.Initialize(eventData.WorkToGraph);

            // Add it to the dictionary
            graphedWorks.Add(eventData.WorkToGraph.Hash, cardComponent);

            // Show the graph area if not already shown
            if(!showing)
                Show();

            // Show the card component
            cardComponent.Show();
        }

        /// <summary>
        /// Remove a progress card
        /// </summary>
        /// <param name="eventData"></param>
        private void RemoveSalesGraph(DestroySalesGraph eventData)
        {
            // Hide and destoy the progress card
            graphedWorks[eventData.Hash].Hide(true);

            // Remove the progress card from the dictionary
            graphedWorks.Remove(eventData.Hash);
        }

        public void Show()
        {
            // Translate in
            Translate(translateValue, animateDuration);
        }

        public void Hide()
        {
            // Translate out
            Translate(-translateValue, animateDuration);
        }

        /// <summary>
        /// Handle translating for the Sales Graph
        /// </summary>
        private void Translate(float endTranslateValue, float duration, TweenCallback onEnd = null, Ease easeType = Ease.InQuint)
        {
            // Kill the current translate tween if it exists
            translateTween?.Kill(false);

            // Calculate the target position
            float targetPos = rectTransform.localPosition.x + endTranslateValue;

            // Set the tween animation
            translateTween = rectTransform.DOLocalMoveX(targetPos, duration)
                .SetEase(easeType);

            // Exit case - if there is no given Tween Callback
            if (onEnd == null) return;

            // Hook up callback events
            translateTween.onComplete += onEnd;
        }
    }
}