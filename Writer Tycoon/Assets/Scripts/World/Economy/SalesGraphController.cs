using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using GhostWriter.Patterns.EventBus;
using UnityEngine.UI;

namespace GhostWriter.World.Economy
{
    public class SalesGraphController : SerializedMonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject salesGraphPrefab;
        [SerializeField] private Transform container;
        [SerializeField] private SalesWindowButton button;

        private bool showing;
        private RectTransform rectTransform;

        [Header("Tweening Values")]
        [SerializeField] private float translateValue;
        [SerializeField] private float animateDuration;
        private Tween translateTween;

        [Header("Sales Graphs")]
        [SerializeField] private Dictionary<int, SalesGraph> salesGraphsDict;

        private EventBinding<CreateSalesGraph> createSalesGraphEvent;
        private EventBinding<UpdateSalesGraph> updateSaleGraphEvent;
        private EventBinding<StopSalesGraph> stopSalesGraphEvent;
        private EventBinding<DestroySalesGraph> destroySalesGraphEvent;

        public bool Showing { get => showing; set => showing = value; }

        private void Awake()
        {
            // Verify the SalesWindowButton component
            if (button == null)
                button = GetComponentInChildren<SalesWindowButton>();

            // Verify the RectTransform component
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();

            // Initialize the dictionary
            salesGraphsDict = new();

            // Initialize the button
            button.Initialize(this);

            // Translate off screen
            Translate(-translateValue, 0f);

            showing = false;
        }

        private void OnEnable()
        {
            createSalesGraphEvent = new EventBinding<CreateSalesGraph>(CreateGraph);
            EventBus<CreateSalesGraph>.Register(createSalesGraphEvent);

            updateSaleGraphEvent = new EventBinding<UpdateSalesGraph>(UpdateGraph);
            EventBus<UpdateSalesGraph>.Register(updateSaleGraphEvent);

            stopSalesGraphEvent = new EventBinding<StopSalesGraph>(StopGraph);
            EventBus<StopSalesGraph>.Register(stopSalesGraphEvent);

            destroySalesGraphEvent = new EventBinding<DestroySalesGraph>(DestroyGraph);
            EventBus<DestroySalesGraph>.Register(destroySalesGraphEvent);
        }

        private void OnDisable()
        {
            EventBus<CreateSalesGraph>.Deregister(createSalesGraphEvent);
            EventBus<UpdateSalesGraph>.Deregister(updateSaleGraphEvent);
            EventBus<StopSalesGraph>.Deregister(stopSalesGraphEvent);
            EventBus<DestroySalesGraph>.Deregister(destroySalesGraphEvent);
        }

        /// <summary>
        /// Callback function to create a Graph
        /// </summary>
        private void CreateGraph(CreateSalesGraph eventData)
        {
            // Create and initialize the SalesGraph
            GameObject graphObj = Instantiate(salesGraphPrefab, container);
            SalesGraph salesGraph = graphObj.GetComponent<SalesGraph>();
            salesGraph.Iniitalize(eventData.WorkToGraph.Title);

            // Add to the dictionary
            salesGraphsDict.Add(eventData.WorkToGraph.Hash, salesGraph);

            // Show the graph area if not already shown
            if (!showing)
                Show();

            // Show the Sales Graph
            salesGraph.Show();
        }

        /// <summary>
        /// Callback function to update the Graph
        /// </summary>
        private void UpdateGraph(UpdateSalesGraph eventData)
        {
            // Exit case - the SalesGraph doesn't exist within the dictionary
            if (!salesGraphsDict.TryGetValue(eventData.Hash, out SalesGraph salesGraph))
                return;

            // Add a point to the graph
            salesGraph.AddPoint(eventData.Sales);
        }

        /// <summary>
        /// Callback function to stop updating a Graph
        /// </summary>
        private void StopGraph(StopSalesGraph eventData)
        {
            // Exit case - the SalesGraph doesn't exist within the dictionary
            if (!salesGraphsDict.TryGetValue(eventData.Hash, out SalesGraph salesGraph))
                return;

            // Stop the Sales Graph from updating
            salesGraph.Stop();
        }

        /// <summary>
        /// Callback function to destroy the Graph
        /// </summary>
        private void DestroyGraph(DestroySalesGraph eventData)
        {
            // Exit case - the SalesGraph doesn't exist within the dictionary
            if (!salesGraphsDict.TryGetValue(eventData.Hash, out SalesGraph salesGraph))
                return;

            // Hide the graph and destroy it
            salesGraph.Hide(true);

            // Remove it from the dictionary
            salesGraphsDict.Remove(eventData.Hash);
        }

        /// <summary>
        /// Show the window
        /// </summary>
        public void Show()
        {
            // Exit case - if already showing
            if (showing) return;

            // Animate
            Translate(translateValue, animateDuration);

            // Set to showing
            showing = true;

            // Set button sprites
            button.SetToCloseSprites();
        }

        /// <summary>
        /// Hide the window
        /// </summary>
        public void Hide()
        {
            // Exit case - if not showing
            if (!showing) return;

            // Animate
            Translate(-translateValue, animateDuration);

            // Set to not showing
            showing = false;

            // Set button sprites
            button.SetToOpenSprites();
        }

        /// <summary>
        /// Handle translating for the Sales Graph
        /// </summary>
        private void Translate(float endTranslateValue, float duration, TweenCallback onEnd = null, Ease easeType = Ease.InQuint)
        {
            // Kill the current translate tween if it exists
            translateTween?.Kill(false);

            // Calculate the target position based on anchoredPosition
            float targetPos = rectTransform.anchoredPosition.x + endTranslateValue;

            // Set the tween animation
            translateTween = rectTransform.DOAnchorPosX(targetPos, duration)
                .SetEase(easeType);

            // Exit case - if there is no given Tween Callback
            if (onEnd == null) return;

            // Hook up callback events
            translateTween.onComplete += onEnd;
        }
    }
}