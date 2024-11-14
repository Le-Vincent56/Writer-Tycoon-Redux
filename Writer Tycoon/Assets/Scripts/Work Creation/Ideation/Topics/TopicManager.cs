using System;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.Events;
using GhostWriter.Patterns.EventBus;
using GhostWriter.Patterns.ServiceLocator;
using GhostWriter.WorkCreation.Mediation;

namespace GhostWriter.WorkCreation.Ideation.Topics
{
    public class TopicManager : Dedicant
    {
        [SerializeField] private int selectedTopicsMax;
        [SerializeField] private List<Topic> topics = new();
        private List<TopicButton> selectedTopicButtons = new();

        public UnityAction<List<Topic>> OnTopicsCreated = delegate { };
        public UnityAction<List<TopicButton>> OnTopicsUpdated = delegate { };
        public UnityAction OnTopicMasteriesUpdated = delegate { };

        private EventBinding<ClearIdeation> clearIdeationEvent;

        public override string Name { get => "Topic Manager"; }
        public override DedicantType Type { get => DedicantType.Topic; }

        public int SelectedTopicsMax { get => selectedTopicsMax; }

        private void Awake()
        {
            // Initialize Lists
            topics = new();
            selectedTopicButtons = new();

            // Create the Topics
            CreateTopics();

            // Register this as a service
            ServiceLocator.ForSceneOf(this).Register(this);
        }

        private void OnEnable()
        {
            clearIdeationEvent = new EventBinding<ClearIdeation>(ClearSelectedTopics);
            EventBus<ClearIdeation>.Register(clearIdeationEvent);
        }

        private void OnDisable()
        {
            EventBus<ClearIdeation>.Deregister(clearIdeationEvent);
        }

        protected override void Start()
        {
            // Register with the mediator
            base.Start();

            // Communicate that the topics have been created and updated
            OnTopicsCreated.Invoke(topics);
            OnTopicsUpdated.Invoke(selectedTopicButtons);
        }

        /// <summary>
        /// Create all the Topics
        /// </summary>
        private void CreateTopics()
        {
            // Clear the list of topics
            topics.Clear();
            
            // Create the Topic factory
            StandardTopicFactory factory = new();

            // Get all of the TopicType values
            Array enumArray = Enum.GetValues(typeof(TopicType));

            // Iterate through each TopicType
            for (int i = 0; i < enumArray.Length; i++)
            {
                // Create the Topic
                Topic newTopic = factory.CreateTopic((TopicType)enumArray.GetValue(i), false);

                // FOR NOW: Unlock the first 10 topics
                if (i < 10)
                    newTopic.Unlock();

                // Add the Topic to the List
                topics.Add(newTopic);
            }
        }

        /// <summary>
        /// Select a Topic
        /// </summary>
        public void SelectTopic(TopicButton buttonToSelect)
        {
            // Exit case - if the list already contains the Topic Button
            if (selectedTopicButtons.Contains(buttonToSelect)) return;

            // Check if the maximum amount of selected Topics have been reached
            if (selectedTopicButtons.Count >= selectedTopicsMax)
            {
                // Deselect the Topic Button and remove it
                selectedTopicButtons[0].Deselect();
                selectedTopicButtons.RemoveAt(0);
            }

            // Add the Topic Button to the selected Topic Buttons list
            selectedTopicButtons.Add(buttonToSelect);

            // Select the Topic Button
            buttonToSelect.Select();

            // Send the Topics out to the mediator
            SendTopics();

            // Invoke the event
            OnTopicsUpdated.Invoke(selectedTopicButtons);
        }

        /// <summary>
        /// Update Topic masteries
        /// </summary>
        public void UpdateMasteries(List<Topic> topicsToUpdate)
        {
            // Iterate through eac Topic
            foreach (Topic topic in topicsToUpdate)
            {
                // Attempt to find the Topic within the Topics list
                Topic foundTopic = topics.Find(t => t == topic);

                // Continue if the Topic was not found
                if (foundTopic == null) continue;

                // Increase the mastery of the Topic
                foundTopic.IncreaseMastery();
            }

            OnTopicMasteriesUpdated.Invoke();
        }

        /// <summary>
        /// Clear the selected Topics list
        /// </summary>
        private void ClearSelectedTopics()
        {
            // Iterate over each selected Topic Button
            foreach(TopicButton topicButton in selectedTopicButtons)
            {
                // Deselect the Topic Button
                topicButton.Deselect();
            }

            // Clear the list
            selectedTopicButtons.Clear();

            // Send the Topics out to the mediator
            SendTopics();

            // Invoke the event
            OnTopicsUpdated.Invoke(selectedTopicButtons);
        }

        /// <summary>
        /// Get the List of created Topics
        /// </summary>
        public List<Topic> GetTopics() => topics;

        /// <summary>
        /// Send the selected Topics
        /// </summary>
        public void SendTopics()
        {
            // Create a list to store Topics in
            List<Topic> selectedTopics = new();
            foreach(TopicButton button in selectedTopicButtons)
            {
                selectedTopics.Add(button.Topic);
            }

            // Send the Topic payload
            Send(new TopicPayload() 
                { Content = selectedTopics }, 
                AreTypes(new DedicantType[3] {
                    DedicantType.Compatibility,
                    DedicantType.TimeEstimator,
                    DedicantType.IdeaReviewer
                })
            );
        }
    }
}