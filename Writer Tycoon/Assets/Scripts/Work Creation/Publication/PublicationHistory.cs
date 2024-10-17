using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Development.Tracker;

namespace WriterTycoon.WorkCreation.Publication
{
    public class PublicationHistory : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<int, PublishedWork> publicationDict;

        private EventBinding<PublishWork> publishWorkEvent;

        private void Awake()
        {
            // Initialize the dictionary
            publicationDict = new();
        }

        private void OnEnable()
        {
            publishWorkEvent = new EventBinding<PublishWork>(StorePublishedWork);
            EventBus<PublishWork>.Register(publishWorkEvent);
        }

        private void OnDisable()
        {
            EventBus<PublishWork>.Deregister(publishWorkEvent);
        }

        /// <summary>
        /// Store a work in the Publication History
        /// </summary>
        /// <param name="eventData"></param>
        private void StorePublishedWork(PublishWork eventData)
        {
            // Extract the Work to publish
            Work workToPublish = eventData.WorkToPublish;

            // Create a Published Work object
            PublishedWork publishedWork = new(
                workToPublish.Hash,
                workToPublish.Owner,
                workToPublish.GetAboutInfo(),
                workToPublish.GetTopics(),
                workToPublish.GetGenres(),
                workToPublish.GetAudience(),
                workToPublish.GetWorkType(),
                eventData.FinalScore
            );

            // Sell the work
            EventBus<SellWork>.Raise(new SellWork()
            {
                WorkToSell = publishedWork
            });

            // Add the published work to the Dictionary
            publicationDict.Add(workToPublish.Hash, publishedWork);
        }
    }
}