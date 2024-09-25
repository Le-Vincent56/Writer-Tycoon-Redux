using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.WorkCreation.UI.Development
{
    public class ProgressCardController : MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;
        private Dictionary<int, ProgressCard> progressCards;

        private EventBinding<CreateProgressCard> createProgressCardEvent;

        private void Awake()
        {
            progressCards = new();
        }

        private void OnEnable()
        {
            createProgressCardEvent = new EventBinding<CreateProgressCard>(CreateProgressCard);
            EventBus<CreateProgressCard>.Register(createProgressCardEvent);
        }

        private void OnDisable()
        {
            EventBus<CreateProgressCard>.Deregister(createProgressCardEvent);
        }

        private void CreateProgressCard(CreateProgressCard eventData)
        {
            // Instantiate the card prefab as a child of this transform
            GameObject cardObj = Instantiate(cardPrefab, transform);

            // Extract the ProgressCard component
            ProgressCard cardComponent = cardObj.GetComponent<ProgressCard>();

            // Initialize the card component
            cardComponent.Initialize(eventData.Title);

            // Add it to the dictionary
            progressCards.Add(eventData.Hash, cardComponent);
        }
    }
}