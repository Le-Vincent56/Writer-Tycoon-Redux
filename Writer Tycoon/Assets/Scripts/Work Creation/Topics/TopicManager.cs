using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WriterTycoon.WorkCreation.Mediation;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.Patterns.ServiceLocator;
using WriterTycoon.Patterns.Visitor;

namespace WriterTycoon.WorkCreation.Topics
{
    public class TopicManager : Dedicant
    {
        [SerializeField] private int selectedTopicsMax;
        private List<Topic> topics = new();
        private List<TopicButton> selectedTopicButtons = new();
        private Mediator<Dedicant> mediator;

        public UnityAction<List<Topic>> OnTopicsCreated = delegate { };
        public UnityAction<List<TopicButton>> OnTopicsUpdated = delegate { };

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
        }

        private void Start()
        {
            // Register with the mediator
            mediator = ServiceLocator.ForSceneOf(this).Get<Mediator<Dedicant>>();
            mediator.Register(this);

            // Communicate that the topics have been created and updated
            OnTopicsCreated.Invoke(topics);
            OnTopicsUpdated.Invoke(selectedTopicButtons);
        }

        private void OnDestroy()
        {
            // Deregister the mediator
            mediator.Deregister(this);
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

            // Create and add all topics
            topics.Add(factory.CreateTopic("Agents", true));
            topics.Add(factory.CreateTopic("Airplanes", true));
            topics.Add(factory.CreateTopic("Aliens", true));
            topics.Add(factory.CreateTopic("Ancient Civilization", true));
            topics.Add(factory.CreateTopic("Androids", true));
            topics.Add(factory.CreateTopic("Angels", true));
            topics.Add(factory.CreateTopic("Animals", false));
            topics.Add(factory.CreateTopic("Apocalypse", false));
            topics.Add(factory.CreateTopic("Archaeology", false));
            topics.Add(factory.CreateTopic("Archery", false));
            topics.Add(factory.CreateTopic("Art", false));
            topics.Add(factory.CreateTopic("Assassins", false));
            topics.Add(factory.CreateTopic("Biology", false));
            topics.Add(factory.CreateTopic("Bounty Hunters", false));
            topics.Add(factory.CreateTopic("Cars", false));
            topics.Add(factory.CreateTopic("Cards", false));
            topics.Add(factory.CreateTopic("Chemistry", false));
            topics.Add(factory.CreateTopic("Chess", false));
            topics.Add(factory.CreateTopic("Cinema", false));
            topics.Add(factory.CreateTopic("Circus", false));
            topics.Add(factory.CreateTopic("Conspiracies", false));
            topics.Add(factory.CreateTopic("Crime", false));
            topics.Add(factory.CreateTopic("Curses", false));
            topics.Add(factory.CreateTopic("Dancing", false));
            topics.Add(factory.CreateTopic("Demons", false));
            topics.Add(factory.CreateTopic("Detective", false));
            topics.Add(factory.CreateTopic("Dinosaurs", false));
            topics.Add(factory.CreateTopic("Doctors", false));
            topics.Add(factory.CreateTopic("Dogs", false));
            topics.Add(factory.CreateTopic("Dolls", false));
            topics.Add(factory.CreateTopic("Dragons", false));
            topics.Add(factory.CreateTopic("Dreams", false));
            topics.Add(factory.CreateTopic("Erotica", false));
            topics.Add(factory.CreateTopic("Espionage", false));
            topics.Add(factory.CreateTopic("Evolution", false));
            topics.Add(factory.CreateTopic("Fey", false));
            topics.Add(factory.CreateTopic("Fashion", false));
            topics.Add(factory.CreateTopic("Food", false));
            topics.Add(factory.CreateTopic("Gambling", false));
            topics.Add(factory.CreateTopic("Games", false));
            topics.Add(factory.CreateTopic("Gangs", false));
            topics.Add(factory.CreateTopic("Gladiators", false));
            topics.Add(factory.CreateTopic("Gods", false));
            topics.Add(factory.CreateTopic("Hacking", false));
            topics.Add(factory.CreateTopic("Heaven", false));
            topics.Add(factory.CreateTopic("Heist", false));
            topics.Add(factory.CreateTopic("Hell", false));
            topics.Add(factory.CreateTopic("Hitmen", false));
            topics.Add(factory.CreateTopic("Hunting", false));
            topics.Add(factory.CreateTopic("Industrialization", false));
            topics.Add(factory.CreateTopic("Insects", false));
            topics.Add(factory.CreateTopic("Journalism", false));
            topics.Add(factory.CreateTopic("Kaiju", false));
            topics.Add(factory.CreateTopic("Language", false));
            topics.Add(factory.CreateTopic("Mathematics", false));
            topics.Add(factory.CreateTopic("Mecha", false));
            topics.Add(factory.CreateTopic("Mermaids", false));
            topics.Add(factory.CreateTopic("Military", false));
            topics.Add(factory.CreateTopic("Mummies", false));
            topics.Add(factory.CreateTopic("Music", false));
            topics.Add(factory.CreateTopic("Ninjas", false));
            topics.Add(factory.CreateTopic("Parallel World", false));
            topics.Add(factory.CreateTopic("Pets", false));
            topics.Add(factory.CreateTopic("Photography", false));
            topics.Add(factory.CreateTopic("Physics", false));
            topics.Add(factory.CreateTopic("Pirates", false));
            topics.Add(factory.CreateTopic("Plants", false));
            topics.Add(factory.CreateTopic("Police", false));
            topics.Add(factory.CreateTopic("Politics", false));
            topics.Add(factory.CreateTopic("Prison", false));
            topics.Add(factory.CreateTopic("Religion", false));
            topics.Add(factory.CreateTopic("Revolution", false));
            topics.Add(factory.CreateTopic("Samurai", false));
            topics.Add(factory.CreateTopic("Sea", false));
            topics.Add(factory.CreateTopic("Secret Society", false));
            topics.Add(factory.CreateTopic("Showbiz", false));
            topics.Add(factory.CreateTopic("Singing", false));
            topics.Add(factory.CreateTopic("Space", false));
            topics.Add(factory.CreateTopic("Sports", false));
            topics.Add(factory.CreateTopic("Stone Age", false));
            topics.Add(factory.CreateTopic("Superheroes", false));
            topics.Add(factory.CreateTopic("Supervillains", false));
            topics.Add(factory.CreateTopic("Survival", false));
            topics.Add(factory.CreateTopic("Time Travel", false));
            topics.Add(factory.CreateTopic("Treasure Hunters", false));
            topics.Add(factory.CreateTopic("University", false));
            topics.Add(factory.CreateTopic("Vampires", false));
            topics.Add(factory.CreateTopic("Vikings", false));
            topics.Add(factory.CreateTopic("Viruses", false));
            topics.Add(factory.CreateTopic("War", false));
            topics.Add(factory.CreateTopic("Werewolves", false));
            topics.Add(factory.CreateTopic("Wild West", false));
            topics.Add(factory.CreateTopic("Wizardry", false));
            topics.Add(factory.CreateTopic("Zombies", false));
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

            // Invoke the event
            OnTopicsUpdated.Invoke(selectedTopicButtons);
        }

        /// <summary>
        /// Clear the selected Topics list
        /// </summary>
        public void ClearSelectedTopics()
        {
            // Iterate over each selected Topic Button
            foreach(TopicButton topicButton in selectedTopicButtons)
            {
                // Deselect the Topic Button
                topicButton.Deselect();
            }

            // Clear the list
            selectedTopicButtons.Clear();

            // Invoke the event
            OnTopicsUpdated.Invoke(selectedTopicButtons);
        }

        /// <summary>
        /// Send the selected Topics to the Rater
        /// </summary>
        public void SendTopicsToRater()
        {
            // Create a list to store Topics in
            List<Topic> selectedTopics = new();
            foreach(TopicButton button in selectedTopicButtons)
            {
                selectedTopics.Add(button.Topic);
            }

            // Send the Topic payload
            Send(new TopicPayload() { Content = selectedTopics }, IsRater);
        }

        public override void Accept(IVisitor message) => message.Visit(this);
        protected override void Send(IVisitor message) => mediator.Broadcast(this, message);
        protected override void Send(IVisitor message, Func<Dedicant, bool> predicate) => mediator.Broadcast(this, message, predicate);
    }
}