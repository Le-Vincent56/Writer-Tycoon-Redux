using System.Collections.Generic;
using UnityEngine;
using GhostWriter.Patterns.EventBus;

namespace GhostWriter.WorkCreation.UI.Development
{
    public class ProgressCardController : MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;
        private Dictionary<int, ProgressCard> progressCards;

        private EventBinding<CreateProgressCard> createProgressCardEvent;
        private EventBinding<DeleteProgressCard> deleteProgressCardEvent;

        private void Awake()
        {
            progressCards = new();
        }

        private void OnEnable()
        {
            createProgressCardEvent = new EventBinding<CreateProgressCard>(CreateProgressCard);
            EventBus<CreateProgressCard>.Register(createProgressCardEvent);

            deleteProgressCardEvent = new EventBinding<DeleteProgressCard>(RemoveProgressCard);
            EventBus<DeleteProgressCard>.Register(deleteProgressCardEvent);
        }

        private void OnDisable()
        {
            EventBus<CreateProgressCard>.Deregister(createProgressCardEvent);
            EventBus<DeleteProgressCard>.Deregister(deleteProgressCardEvent);
        }

        /// <summary>
        /// Create a progress card
        /// </summary>
        private void CreateProgressCard(CreateProgressCard eventData)
        {
            // Instantiate the card prefab as a child of this transform
            GameObject cardObj = Instantiate(cardPrefab, transform);

            // Extract the ProgressCard component
            ProgressCard cardComponent = cardObj.GetComponent<ProgressCard>();

            // Initialize the card component
            cardComponent.Initialize(eventData.Hash, eventData.Title);

            // Add it to the dictionary
            progressCards.Add(eventData.Hash, cardComponent);

            // Show the card
            cardComponent.Show();
        }

        /// <summary>
        /// Remove a progress card
        /// </summary>
        /// <param name="eventData"></param>
        private void RemoveProgressCard(DeleteProgressCard eventData)
        {
            // Hide and destoy the progress card
            progressCards[eventData.Hash].Hide(true);

            // Remove the progress card from the dictionary
            progressCards.Remove(eventData.Hash);
        }
    }
}