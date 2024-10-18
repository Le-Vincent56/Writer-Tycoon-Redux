using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.Patterns.ServiceLocator;
using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Ideation.Genres
{
    public class GenreManager : Dedicant
    {
        [SerializeField] private int selectedGenresMax;
        private List<Genre> genres = new();
        private List<GenreButton> selectedGenreButtons = new();

        public UnityAction<List<Genre>> OnGenresCreated = delegate { };
        public UnityAction<List<GenreButton>> OnGenresUpdated = delegate { };
        public UnityAction OnGenreMasteriesUpdated = delegate { };

        private EventBinding<ClearIdeation> clearIdeationEvent;

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

            // Register this as a service
            ServiceLocator.ForSceneOf(this).Register(this);
        }

        private void OnEnable()
        {
            clearIdeationEvent = new EventBinding<ClearIdeation>(ClearSelectedGenres);
            EventBus<ClearIdeation>.Register(clearIdeationEvent);
        }

        private void OnDisable()
        {
            EventBus<ClearIdeation>.Deregister(clearIdeationEvent);
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

            // Get the enum values of GenreType
            Array enumArray = Enum.GetValues(typeof(GenreType));

            // Iterate through each enum value
            for(int i = 0; i < enumArray.Length; i++)
            {
                // Create the Genre
                Genre newGenre = factory.CreateGenre((GenreType)enumArray.GetValue(i), false);

                // FOR NOW: Unlock all genres
                newGenre.Unlock();

                // Add the Genre to the List
                genres.Add(newGenre);
            }

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
        /// Update Genre masteries
        /// </summary>
        public void UpdateMasteries(List<Genre> genresToUpdate)
        {
            // Iterate through eac Topic
            foreach (Genre genre in genresToUpdate)
            {
                // Attempt to find the Topic within the Topics list
                Genre foundGenre = genres.Find(t => t == genre);

                // Continue if the Topic was not found
                if (foundGenre == null) continue;

                // Increase the mastery of the Topic
                foundGenre.IncreaseMastery();
            }

            // Invoke the event
            OnGenreMasteriesUpdated.Invoke();
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

            // Send the genres out
            SendGenres();

            // Invoke the event
            OnGenresUpdated.Invoke(selectedGenreButtons);
        }

        /// <summary>
        /// Get the List of created Genres
        /// </summary>
        public List<Genre> GetGenres() => genres;

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
                    DedicantType.IdeaReviewer,
                })
            );
        }
    }
}