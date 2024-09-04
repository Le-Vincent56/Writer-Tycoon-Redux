using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.WorkCreation.Genres;
using WriterTycoon.WorkCreation.Mediation;
using WriterTycoon.WorkCreation.Topics;

namespace WriterTycoon.WorkCreation.Compatibility
{
    public class CompatibilityManager : Dedicant
    {
        private GenreTopicCompatibility genreTopicCompatibility;

        [SerializeField] private List<Topic> topics;
        [SerializeField] private List<Genre> genres;

        public override string Name { get => "Compatibility Manager"; }
        public override DedicantType Type { get => DedicantType.Compatibility; }

        private void Awake()
        {
            // Create a new data base for Genre-Topic compatibility
            genreTopicCompatibility = new GenreTopicCompatibility();
        }

        /// <summary>
        /// Set the selected Topics
        /// </summary>
        public void SetTopics(List<Topic> topics) => this.topics = topics;

        /// <summary>
        /// Set the selected Genres
        /// </summary>
        public void SetGenres(List<Genre> genres) => this.genres = genres;

        /// <summary>
        /// Check the Genre-Topic compatibilities
        /// </summary>
        /// <returns></returns>
        public List<Compatibility> CheckGenreTopicCompatibilities()
        {
            // Exit case - there are no Topics selected
            if (topics.Count <= 0) return null;

            // Exit case - there are no Genres selected
            if (genres.Count <= 0) return null;

            // Exit case - the Genre-Topic compatibility object does not exist
            if (genreTopicCompatibility == null) return null;

            // Create a list to store the compatibilities
            List<Compatibility> compatibilities = new List<Compatibility>();

            // Iterate through each genre
            for(int i = 0; i < genres.Count; i++)
            {
                // Iterate through each topic
                for(int j = 0; j < topics.Count; j++)
                {
                    // Ignore if the topic has compatibility ignored
                    if (topics[j].IgnoreGenreCompatibility) continue;

                    // Add the compatibility for the Genre-Topic to the list
                    compatibilities.Add(
                        genreTopicCompatibility.GetCompatibility(genres[i].Type, topics[j].Type)
                    );
                }
            }

            return compatibilities;
        }

        /// <summary>
        /// Calculate the total compatibility score
        /// </summary>
        public void CalculateCompatibilityScore()
        {
            // Get the compatibilities
            List<Compatibility> genreTopicCompatibilities = CheckGenreTopicCompatibilities();

            string compatibilities = "";

            foreach(Compatibility compatibility in genreTopicCompatibilities)
            {
                compatibilities += $"{compatibility}, ";
            }

            Debug.Log(compatibilities);
        }
    }
}