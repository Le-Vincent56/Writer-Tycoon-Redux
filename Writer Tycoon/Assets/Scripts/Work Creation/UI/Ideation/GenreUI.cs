using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.WorkCreation.Ideation.Genres;

namespace WriterTycoon.WorkCreation.UI.Ideation
{
    public class GenreUI : MonoBehaviour
    {
        [SerializeField] private GenreManager genreManager;
        [SerializeField] private GameObject genrePrefab;
        [SerializeField] private GameObject contentObject;
        [SerializeField] private Text headerText;
        [SerializeField] private Text selectedGenresText;

        private void Awake()
        {
            // Verify the TopicManager
            if (genreManager == null)
                genreManager = GetComponent<GenreManager>();
        }

        private void OnEnable()
        {
            // Subscribe to events
            genreManager.OnGenresCreated += InstantiateGenres;
            genreManager.OnGenresUpdated += UpdateSelectedGenres;
        }

        private void OnDisable()
        {
            // Unsubscribe to events
            genreManager.OnGenresCreated -= InstantiateGenres;
            genreManager.OnGenresUpdated -= UpdateSelectedGenres;
        }

        /// <summary>
        /// Instantiate each Genre button
        /// </summary>
        private void InstantiateGenres(List<Genre> genres)
        {
            // Iterate through each Genre
            foreach (Genre genre in genres)
            {
                // Instantiate the object
                GameObject topicObject = Instantiate(genrePrefab, contentObject.transform);

                // Set text and name
                topicObject.GetComponentInChildren<Text>().text = genre.Name;
                topicObject.name = $"Genre Button ({genre.Name})";

                // Set button functionality
                GenreButton genreButton = topicObject.GetComponent<GenreButton>();
                genreButton.Instantiate(genreManager, genre);
            }
        }

        /// <summary>
        /// Update the UI according to the currently selected Genres
        /// </summary>
        private void UpdateSelectedGenres(List<GenreButton> selectedGenres)
        {
            // Create a starting string
            string selectedText = "";

            // Iterate through each selected Topic
            for (int i = 0; i < selectedGenres.Count; i++)
            {
                // Add the selected Topic's name
                selectedText += $"{selectedGenres[i].Genre.Name}";

                // Add commas if not the last element
                if (i != selectedGenres.Count - 1)
                    selectedText += ", ";
            }

            // Set the subheader text
            selectedGenresText.text = selectedText;

            // Set the header text
            headerText.text = $"Genre(s) ({selectedGenres.Count}/{genreManager.SelectedGenresMax})";
        }
    }
}