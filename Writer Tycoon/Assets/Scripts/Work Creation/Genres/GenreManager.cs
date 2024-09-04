using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WriterTycoon.WorkCreation.Mediation;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.Patterns.ServiceLocator;
using WriterTycoon.Patterns.Visitor;
using WriterTycoon.WorkCreation.UI;
using WriterTycoon.WorkCreation.Topics;

namespace WriterTycoon.WorkCreation.Genres
{
    public class GenreManager : Dedicant
    {
        [SerializeField] private int selectedGenresMax;
        private List<Genre> genres = new();
        private List<GenreButton> selectedGenreButtons = new();
        private Mediator<Dedicant> mediator;

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

        private void Start()
        {
            // Register with the mediator
            mediator = ServiceLocator.ForSceneOf(this).Get<Mediator<Dedicant>>();
            mediator.Register(this);

            // Communicate that the Genres have been created and updated
            OnGenresCreated.Invoke(genres);
            OnGenresUpdated.Invoke(selectedGenreButtons);
        }

        private void OnDestroy()
        {
            // Deregister the mediator
            mediator.Deregister(this);
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
            genres.Add(factory.CreateGenre(GenreType.Nonfiction, false));
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
        /// Send the selected Genres to the Rater
        /// </summary>
        public void SendGenresToCompatibilityManager()
        {
            // Create a list to store Genres in
            List<Genre> selectedGenres = new();
            foreach (GenreButton button in selectedGenreButtons)
            {
                selectedGenres.Add(button.Genre);
            }

            // Send the Genre payload
            Send(new GenrePayload() { Content = selectedGenres }, IsCompatibility);
        }

        public override void Accept(IVisitor message) => message.Visit(this);
        protected override void Send(IVisitor message) => mediator.Broadcast(this, message);
        protected override void Send(IVisitor message, Func<Dedicant, bool> predicate) => mediator.Broadcast(this, message, predicate);
    }
}