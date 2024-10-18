using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.WorkCreation.Ideation.Genres;
using WriterTycoon.WorkCreation.Ideation.Topics;

namespace WriterTycoon.Entities.Competitors
{
    public class NPCCompetitor : SerializedMonoBehaviour, ICompetitor
    {
        private Bank bank;

        [SerializeField] private string competitorName;
        [SerializeField] private bool learned;
        [SerializeField] private float learningQ;
        [SerializeField] private HashSet<Topic> knownTopics;
        [SerializeField] private HashSet<Genre> knownGenres;

        /// <summary>
        /// Initialize the NPC Competitor
        /// </summary>
        public void Initialize(string competitorName, float startingMoney, bool learned, float learningQ)
        {
            // Get Components
            bank = GetComponent<Bank>();

            // Link the bank and set the starting sum
            bank.LinkCompetitor(this);
            bank.SetBankSum(startingMoney);

            // Set variables
            this.competitorName = competitorName;
            this.learned = learned;
            this.learningQ = learningQ;

            // Initialize the HashSets
            knownTopics = new();
            knownGenres = new();
        }

        /// <summary>
        /// Set the Competitor's known Topics
        /// </summary>
        public void SetKnownTopics(List<Topic> availableTopics, HashSet<TopicType> topicData)
        {
            // Iterate through each Topic
            foreach(Topic topicObj in availableTopics)
            {
                // Iterate through each TopicType
                foreach(TopicType topicType in topicData)
                {
                    // Skip if the types are not equal
                    if (topicObj.Type != topicType) continue;

                    // Add the Topic to the known Topics;
                    // create a copy to be able to unlock it for the Competitor but
                    // not the player
                    knownTopics.Add(new Topic(topicObj));
                }
            }

            // Iterate through each Topic in the known Topics
            foreach(Topic topic in knownTopics)
            {
                // Unlock the Topic for the Competitor
                topic.Unlock();
            }
        }

        /// <summary>
        /// Set the Competitor's known Genres
        /// </summary>
        public void SetKnownGenres(List<Genre> availableGenres, HashSet<GenreType> genreData)
        {
            // Iterate through each Genre
            foreach (Genre genreObj in availableGenres)
            {
                // Iterate through each GenreType
                foreach (GenreType genreType in genreData)
                {
                    // Skip if the types are not equal
                    if (genreObj.Type != genreType) continue;

                    // Add the Genre to the known Genres;
                    // create a copy to be able to unlock it for the Competitor but
                    // not the player
                    knownGenres.Add(new Genre(genreObj));
                }
            }

            // Iterate through each Genre in the known Genres
            foreach (Genre genre in knownGenres)
            {
                // Unlock the Genre for the Competitor
                genre.Unlock();
            }
        }

        /// <summary>
        /// Calculate sales for the NPC Competitor
        /// </summary>
        public void CalculateSales(float amount) => bank.AddAmount(amount);
    }
}