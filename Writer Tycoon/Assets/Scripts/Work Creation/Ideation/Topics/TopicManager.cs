using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Ideation.Topics
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

            // Create and add all topics
            topics.Add(factory.CreateTopic(TopicType.Agents, true));
            topics.Add(factory.CreateTopic(TopicType.Airplanes, true));
            topics.Add(factory.CreateTopic(TopicType.Aliens, true));
            topics.Add(factory.CreateTopic(TopicType.AncientCivilization, true));
            topics.Add(factory.CreateTopic(TopicType.Androids, true));
            topics.Add(factory.CreateTopic(TopicType.Angels, true));
            topics.Add(factory.CreateTopic(TopicType.Animals, false));
            topics.Add(factory.CreateTopic(TopicType.Apocalypse, false));
            topics.Add(factory.CreateTopic(TopicType.Archaeology, false));
            topics.Add(factory.CreateTopic(TopicType.Archery, false));
            topics.Add(factory.CreateTopic(TopicType.Art, false));
            topics.Add(factory.CreateTopic(TopicType.Assassins, false));
            topics.Add(factory.CreateTopic(TopicType.Biology, false));
            topics.Add(factory.CreateTopic(TopicType.BountyHunters, false));
            topics.Add(factory.CreateTopic(TopicType.Cars, false));
            topics.Add(factory.CreateTopic(TopicType.Cards, false));
            topics.Add(factory.CreateTopic(TopicType.Chemistry, false));
            topics.Add(factory.CreateTopic(TopicType.Chess, false));
            topics.Add(factory.CreateTopic(TopicType.Cinema, false));
            topics.Add(factory.CreateTopic(TopicType.Circus, false));
            topics.Add(factory.CreateTopic(TopicType.Conspiracies, false));
            topics.Add(factory.CreateTopic(TopicType.Crime, false));
            topics.Add(factory.CreateTopic(TopicType.Curses, false));
            topics.Add(factory.CreateTopic(TopicType.Dancing, false));
            topics.Add(factory.CreateTopic(TopicType.Demons, false));
            topics.Add(factory.CreateTopic(TopicType.Detective, false));
            topics.Add(factory.CreateTopic(TopicType.Dinosaurs, false));
            topics.Add(factory.CreateTopic(TopicType.Doctors, false));
            topics.Add(factory.CreateTopic(TopicType.Dogs, false));
            topics.Add(factory.CreateTopic(TopicType.Dolls, false));
            topics.Add(factory.CreateTopic(TopicType.Dragons, false));
            topics.Add(factory.CreateTopic(TopicType.Dreams, false));
            topics.Add(factory.CreateTopic(TopicType.Erotica, false));
            topics.Add(factory.CreateTopic(TopicType.Espionage, false));
            topics.Add(factory.CreateTopic(TopicType.Evolution, false));
            topics.Add(factory.CreateTopic(TopicType.Fey, false));
            topics.Add(factory.CreateTopic(TopicType.Fashion, false));
            topics.Add(factory.CreateTopic(TopicType.Food, false));
            topics.Add(factory.CreateTopic(TopicType.Gambling, false));
            topics.Add(factory.CreateTopic(TopicType.Games, false));
            topics.Add(factory.CreateTopic(TopicType.Gangs, false));
            topics.Add(factory.CreateTopic(TopicType.Gladiators, false));
            topics.Add(factory.CreateTopic(TopicType.Gods, false));
            topics.Add(factory.CreateTopic(TopicType.Hacking, false));
            topics.Add(factory.CreateTopic(TopicType.Heaven, false));
            topics.Add(factory.CreateTopic(TopicType.Heist, false));
            topics.Add(factory.CreateTopic(TopicType.Hell, false));
            topics.Add(factory.CreateTopic(TopicType.Hitmen, false));
            topics.Add(factory.CreateTopic(TopicType.Hunting, false));
            topics.Add(factory.CreateTopic(TopicType.Industrialization, false));
            topics.Add(factory.CreateTopic(TopicType.Insects, false));
            topics.Add(factory.CreateTopic(TopicType.Journalism, false));
            topics.Add(factory.CreateTopic(TopicType.Kaiju, false));
            topics.Add(factory.CreateTopic(TopicType.Language, false));
            topics.Add(factory.CreateTopic(TopicType.Mathematics, false));
            topics.Add(factory.CreateTopic(TopicType.Mecha, false));
            topics.Add(factory.CreateTopic(TopicType.Mermaids, false));
            topics.Add(factory.CreateTopic(TopicType.Military, false));
            topics.Add(factory.CreateTopic(TopicType.Mummies, false));
            topics.Add(factory.CreateTopic(TopicType.Music, false));
            topics.Add(factory.CreateTopic(TopicType.Ninjas, false));
            topics.Add(factory.CreateTopic(TopicType.ParallelWorld, false));
            topics.Add(factory.CreateTopic(TopicType.Pets, false));
            topics.Add(factory.CreateTopic(TopicType.Photography, false));
            topics.Add(factory.CreateTopic(TopicType.Physics, false));
            topics.Add(factory.CreateTopic(TopicType.Pirates, false));
            topics.Add(factory.CreateTopic(TopicType.Plants, false));
            topics.Add(factory.CreateTopic(TopicType.Police, false));
            topics.Add(factory.CreateTopic(TopicType.Politics, false));
            topics.Add(factory.CreateTopic(TopicType.Prison, false));
            topics.Add(factory.CreateTopic(TopicType.Religion, false));
            topics.Add(factory.CreateTopic(TopicType.Revolution, false));
            topics.Add(factory.CreateTopic(TopicType.Samurai, false));
            topics.Add(factory.CreateTopic(TopicType.Sea, false));
            topics.Add(factory.CreateTopic(TopicType.SecretSociety, false));
            topics.Add(factory.CreateTopic(TopicType.Showbiz, false));
            topics.Add(factory.CreateTopic(TopicType.Singing, false));
            topics.Add(factory.CreateTopic(TopicType.Space, false));
            topics.Add(factory.CreateTopic(TopicType.Sports, false));
            topics.Add(factory.CreateTopic(TopicType.StoneAge, false));
            topics.Add(factory.CreateTopic(TopicType.Superheroes, false));
            topics.Add(factory.CreateTopic(TopicType.Supervillains, false));
            topics.Add(factory.CreateTopic(TopicType.Survival, false));
            topics.Add(factory.CreateTopic(TopicType.TimeTravel, false));
            topics.Add(factory.CreateTopic(TopicType.TreasureHunters, false));
            topics.Add(factory.CreateTopic(TopicType.University, false));
            topics.Add(factory.CreateTopic(TopicType.Vampires, false));
            topics.Add(factory.CreateTopic(TopicType.Vikings, false));
            topics.Add(factory.CreateTopic(TopicType.Viruses, false));
            topics.Add(factory.CreateTopic(TopicType.War, false));
            topics.Add(factory.CreateTopic(TopicType.Werewolves, false));
            topics.Add(factory.CreateTopic(TopicType.WildWest, false));
            topics.Add(factory.CreateTopic(TopicType.Wizardry, false));
            topics.Add(factory.CreateTopic(TopicType.Zombies, false));
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