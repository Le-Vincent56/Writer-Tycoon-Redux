using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WriterTycoon.WorkCreation.Mediation;
using WriterTycoon.WorkCreation.UI;

namespace WriterTycoon.WorkCreation.Genres
{
    public class GenreManager : Dedicant
    {
        [SerializeField] private int selectedGenresMax;
        private List<Genre> genres = new();
        private List<GenreButton> selectedGenreButtons = new();

        public UnityAction<List<Genre>> OnGenresCreated = delegate { };
        public UnityAction<List<GenreButton>> OnGenresUpdated = delegate { };

        public override string Name { get => "Genre Manager"; }
        public override DedicantType Type { get => DedicantType.Genre; }

        public int SelectedGenresMax { get => selectedGenresMax; }

        private void Awake()
        {
            // Initialize Lists
            genres = new();
            selectedGenreButtons = new();

            // Create the Genres
            CreateGenres();
        }

        protected override void Start()
        {
            // Initialize the mediator
            base.Start();

            // Communicate that the Genres have been created and updated
            OnGenresCreated.Invoke(genres);
            OnGenresUpdated.Invoke(selectedGenreButtons);
        }

        /// <summary>
        /// Create all the Genres
        /// </summary>
        private void CreateGenres()
        {
            // Clear the list of Genres
            genres.Clear();

            // Create a standard Genre factory
            StandardGenreFactory factory = new StandardGenreFactory();

            // Create and add all Genres
            genres.Add(factory.CreateGenre(GenreType.Action, true));
            genres.Add(factory.CreateGenre(GenreType.Adventure, true));
            genres.Add(factory.CreateGenre(GenreType.ContemporaryFiction, false));
            genres.Add(factory.CreateGenre(GenreType.Fantasy, true));
            genres.Add(factory.CreateGenre(GenreType.Horror, true));
            genres.Add(factory.CreateGenre(GenreType.HistoricalFiction, true));
            genres.Add(factory.CreateGenre(GenreType.Mystery, false));
            genres.Add(factory.CreateGenre(GenreType.Romance, false));
            genres.Add(factory.CreateGenre(GenreType.ScienceFiction, true));
            genres.Add(factory.CreateGenre(GenreType.Thriller, true));
        }

        /// <summary>
        /// Select a Genre
        /// </summary>
        public void SelectGenre(GenreButton buttonToSelect)
        {
            // Exit case - if the list already contains the Topic Button
            if (selectedGenreButtons.Contains(buttonToSelect)) return;

            // Check if the maximum amount of selected Topics have been reached
            if (selectedGenreButtons.Count >= selectedGenresMax)
            {
                // Deselect the Topic Button and remove it
                selectedGenreButtons[0].Deselect();
                selectedGenreButtons.RemoveAt(0);
            }

            // Add the Topic Button to the selected Topic Buttons list
            selectedGenreButtons.Add(buttonToSelect);

            // Select the Topic Button
            buttonToSelect.Select();

            // Send the genres to the mediator
            SendGenres();

            // Invoke the event
            OnGenresUpdated.Invoke(selectedGenreButtons);
        }

        /// <summary>
        /// Clear the selected Genres list
        /// </summary>
        public void ClearSelectedGenres()
        {
            // Iterate over each selected Genre Button
            foreach (GenreButton genreButton in selectedGenreButtons)
            {
                // Deselect the Genre Button
                genreButton.Deselect();
            }

            // Clear the list
            selectedGenreButtons.Clear();

            // Invoke the event
            OnGenresUpdated.Invoke(selectedGenreButtons);
        }

        /// <summary>
        /// Send the selected Genres
        /// </summary>
        public void SendGenres()
        {
            // Create a list to store Genres in
            List<Genre> selectedGenres = new();
            foreach (GenreButton button in selectedGenreButtons)
            {
                selectedGenres.Add(button.Genre);
            }

            // Send the Genre payload
            Send(new GenrePayload() 
                { Content = selectedGenres }, 
                AreTypes(new DedicantType[2]
                {
                    DedicantType.Compatibility,
                    DedicantType.IdeaReviewer
                })
            );
        }
    }
}