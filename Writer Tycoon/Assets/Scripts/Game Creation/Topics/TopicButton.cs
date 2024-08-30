using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.GameCreation.UI;

namespace WriterTycoon.GameCreation.Topics
{
    public class TopicButton : MonoBehaviour
    {
        [SerializeField] private Image[] masteryIcons = new Image[3];
        [SerializeField] private Color buttonSelectedColor;
        [SerializeField] private Color unmasteredColor;

        private TopicManager topicManager;
        private Button button;
        public Topic Topic { get; private set; }

        /// <summary>
        /// Initialize the Topic Button
        /// </summary>
        public void Instantiate(TopicManager topicManager, Topic topic)
        {
            // Verify the Button component
            if(button == null)
                button = GetComponent<Button>();

            // Set variables
            this.topicManager = topicManager;
            Topic = topic;

            // Check if the topic is unlocked
            if (Topic.IsUnlocked) 
                // If so, allow it to be interacted with
                button.interactable = true;
            else
                // Otherwise, don't allow it be interacted with
                button.interactable = false;

            // Update the mastery icons
            UpdateMasteryIcons();

            // Add the event listener
            button.onClick.AddListener(OnClick);
        }

        /// <summary>
        /// Callback function for when the Topic Button is clicked
        /// </summary>
        private void OnClick() => topicManager.SelectTopic(this);

        /// <summary>
        /// Select the Topic Button
        /// </summary>
        public void Select() => button.targetGraphic.color = buttonSelectedColor;

        /// <summary>
        /// Deslect the Topic Button
        /// </summary>
        public void Deselect() => button.targetGraphic.color = Color.white;

        /// <summary>
        /// Update the Button's mastery icons
        /// </summary>
        public void UpdateMasteryIcons()
        {
            // Iterate through each mastery icon
            for(int i = 0; i < masteryIcons.Length; i++)
            {
                // Check if the index is less than or equal to the mastery level
                // of the Topic
                if (i <= Topic.MasteryLevel - 1)
                    // If so, use a mastered icon (colored normally)
                    masteryIcons[i].color = Color.white;
                else
                    // otherwise, use an unmastered icon (colored black)
                    masteryIcons[i].color = unmasteredColor;
            }
        }
    }
}