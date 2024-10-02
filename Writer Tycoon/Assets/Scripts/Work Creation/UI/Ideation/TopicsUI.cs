using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.WorkCreation.Ideation.Topics;

namespace WriterTycoon.WorkCreation.UI.Ideation
{
    public class TopicsUI : MonoBehaviour
    {
        [SerializeField] private TopicManager topicManager;
        [SerializeField] private GameObject topicPrefab;
        [SerializeField] private GameObject contentObject;
        [SerializeField] private Text headerText;
        [SerializeField] private Text selectedTopicsText;
        [SerializeField] private List<TopicButton> topicButtons;

        private void Awake()
        {
            // Verify the TopicManager
            if(topicManager == null)
                topicManager = GetComponent<TopicManager>();

            // Initialize the Topic Button List
            topicButtons = new();
        }

        private void OnEnable()
        {
            // Subscribe to events
            topicManager.OnTopicsCreated += InstantiateTopics;
            topicManager.OnTopicsUpdated += UpdateSelectedTopics;
            topicManager.OnTopicMasteriesUpdated += UpdateMasteries;
        }

        private void OnDisable()
        {
            // Unsubscribe to events
            topicManager.OnTopicsCreated -= InstantiateTopics;
            topicManager.OnTopicsUpdated -= UpdateSelectedTopics;
            topicManager.OnTopicMasteriesUpdated -= UpdateMasteries;
        }

        /// <summary>
        /// Instantiate each Topic button
        /// </summary>
        private void InstantiateTopics(List<Topic> topics)
        {
            // Iterate through each topic
            foreach(Topic topic in topics)
            {
                // Instantiate the object
                GameObject topicObject = Instantiate(topicPrefab, contentObject.transform);

                // Set text and name
                topicObject.GetComponentInChildren<Text>().text = topic.Name;
                topicObject.name = $"Topic Button ({topic.Name})";

                // Set button functionality
                TopicButton topicButton = topicObject.GetComponent<TopicButton>();
                topicButton.Instantiate(topicManager, topic);

                // Add the Topic Button to the List
                topicButtons.Add(topicButton);
            }
        }

        /// <summary>
        /// Update the mastery icon for each Topic Button
        /// </summary>
        private void UpdateMasteries()
        {
            // Iterate through each Topic Button
            foreach(TopicButton topicButton in topicButtons)
            {
                // Update the mastery icon
                topicButton.UpdateMasteryIcons();
            }
        }

        /// <summary>
        /// Update the UI according to the currently selected Topics
        /// </summary>
        private void UpdateSelectedTopics(List<TopicButton> selectedTopics)
        {
            // Create a starting string
            string selectedText = "";

            // Iterate through each selected Topic
            for(int i = 0; i < selectedTopics.Count; i++)
            {
                // Add the selected Topic's name
                selectedText += $"{selectedTopics[i].Topic.Name}";

                // Add commas if not the last element
                if (i != selectedTopics.Count - 1)
                    selectedText += ", ";
            }

            // Set the subheader text
            selectedTopicsText.text = selectedText;

            // Set the header text
            headerText.text = $"Topic(s) ({selectedTopics.Count}/{topicManager.SelectedTopicsMax})";
        }
    }
}