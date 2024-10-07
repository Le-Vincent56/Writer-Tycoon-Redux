using System.Collections.Generic;
using WriterTycoon.WorkCreation.Ideation.Genres;

namespace WriterTycoon.WorkCreation.Development.PointGeneration
{
    public enum PointCategory
    {
        CharacterSheets,
        PlotOutline,
        WorldDocument,
        Dialogue,
        Subplots,
        Descriptions,
        Emotions,
        Twists,
        Symbolism
    }

    public class GenreFocusTargets
    {
        private Dictionary<GenreType, Dictionary<PointCategory, int>> genreTargets;

        public GenreFocusTargets()
        {
            // Initialize the dictionary
            genreTargets = new();

            // Add target scores for each Genre
            SetActionTargets();
            SetAdventureTargets();
            SetContemporaryFictionTargets();
            SetFantasyTargets();
            SetHorrorTargets();
            SetHistoricalFictionTargets();
            SetMysteryTargets();
            SetRomanceTargets();
            SetScienceFictionTargets();
            SetThrillerTargets();
        }

        /// <summary>
        /// Get the target score of a specified Genre and Point Category
        /// </summary>
        public int GetTargetScore(GenreType genre, PointCategory category) => genreTargets[genre][category];

        /// <summary>
        /// Set the Action-genre point targets
        /// </summary>
        public void SetActionTargets()
        {
            Dictionary<PointCategory, int> targets = new()
            {
                { PointCategory.CharacterSheets, 4 },
                { PointCategory.PlotOutline, 9 },
                { PointCategory.WorldDocument, 2 },
                { PointCategory.Dialogue, 4 },
                { PointCategory.Subplots, 3 },
                { PointCategory.Descriptions, 8 },
                { PointCategory.Emotions, 3 },
                { PointCategory.Twists, 9 },
                { PointCategory.Symbolism, 3 },
            };

            genreTargets.Add(GenreType.Action, targets);
        }

        /// <summary>
        /// Set the Adventure-genre point targets
        /// </summary>
        public void SetAdventureTargets()
        {
            Dictionary<PointCategory, int> targets = new()
            {
                { PointCategory.CharacterSheets, 6 },
                { PointCategory.PlotOutline, 7 },
                { PointCategory.WorldDocument, 2 },
                { PointCategory.Dialogue, 4 },
                { PointCategory.Subplots, 3 },
                { PointCategory.Descriptions, 8 },
                { PointCategory.Emotions, 6 },
                { PointCategory.Twists, 5 },
                { PointCategory.Symbolism, 4 },
            };

            genreTargets.Add(GenreType.Adventure, targets);
        }

        /// <summary>
        /// Set the Contemporary Fiction-genre point targets
        /// </summary>
        public void SetContemporaryFictionTargets()
        {
            Dictionary<PointCategory, int> targets = new()
            {
                { PointCategory.CharacterSheets, 7 },
                { PointCategory.PlotOutline, 7 },
                { PointCategory.WorldDocument, 1 },
                { PointCategory.Dialogue, 8 },
                { PointCategory.Subplots, 4 },
                { PointCategory.Descriptions, 3 },
                { PointCategory.Emotions, 10 },
                { PointCategory.Twists, 2 },
                { PointCategory.Symbolism, 3 },
            };

            genreTargets.Add(GenreType.ContemporaryFiction, targets);
        }

        /// <summary>
        /// Set the Fantasy-genre point targets
        /// </summary>
        public void SetFantasyTargets()
        {
            Dictionary<PointCategory, int> targets = new()
            {
                { PointCategory.CharacterSheets, 4 },
                { PointCategory.PlotOutline, 5 },
                { PointCategory.WorldDocument, 6 },
                { PointCategory.Dialogue, 5 },
                { PointCategory.Subplots, 3 },
                { PointCategory.Descriptions, 7 },
                { PointCategory.Emotions, 5 },
                { PointCategory.Twists, 4 },
                { PointCategory.Symbolism, 6 },
            };

            genreTargets.Add(GenreType.Fantasy, targets);
        }

        /// <summary>
        /// Set the Horror-genre point targets
        /// </summary>
        public void SetHorrorTargets()
        {
            Dictionary<PointCategory, int> targets = new()
            {
                { PointCategory.CharacterSheets, 4 },
                { PointCategory.PlotOutline, 6 },
                { PointCategory.WorldDocument, 5 },
                { PointCategory.Dialogue, 3 },
                { PointCategory.Subplots, 2 },
                { PointCategory.Descriptions, 10 },
                { PointCategory.Emotions, 1 },
                { PointCategory.Twists, 8 },
                { PointCategory.Symbolism, 6 },
            };

            genreTargets.Add(GenreType.Horror, targets);
        }

        /// <summary>
        /// Set the Historical Fiction-genre point targets
        /// </summary>
        public void SetHistoricalFictionTargets()
        {
            Dictionary<PointCategory, int> targets = new()
            {
                { PointCategory.CharacterSheets, 7 },
                { PointCategory.PlotOutline, 5 },
                { PointCategory.WorldDocument, 3 },
                { PointCategory.Dialogue, 6 },
                { PointCategory.Subplots, 3 },
                { PointCategory.Descriptions, 6 },
                { PointCategory.Emotions, 7 },
                { PointCategory.Twists, 3 },
                { PointCategory.Symbolism, 5 },
            };

            genreTargets.Add(GenreType.HistoricalFiction, targets);
        }

        /// <summary>
        /// Set the Mystery-genre point targets
        /// </summary>
        public void SetMysteryTargets()
        {
            Dictionary<PointCategory, int> targets = new()
            {
                { PointCategory.CharacterSheets, 4 },
                { PointCategory.PlotOutline, 9 },
                { PointCategory.WorldDocument, 2 },
                { PointCategory.Dialogue, 3 },
                { PointCategory.Subplots, 3 },
                { PointCategory.Descriptions, 9 },
                { PointCategory.Emotions, 4 },
                { PointCategory.Twists, 9 },
                { PointCategory.Symbolism, 2 },
            };

            genreTargets.Add(GenreType.Mystery, targets);
        }

        /// <summary>
        /// Set the Romance-genre point targets
        /// </summary>
        public void SetRomanceTargets()
        {
            Dictionary<PointCategory, int> targets = new()
            {
                { PointCategory.CharacterSheets, 9 },
                { PointCategory.PlotOutline, 4 },
                { PointCategory.WorldDocument, 2 },
                { PointCategory.Dialogue, 7 },
                { PointCategory.Subplots, 5 },
                { PointCategory.Descriptions, 3 },
                { PointCategory.Emotions, 10 },
                { PointCategory.Twists, 1 },
                { PointCategory.Symbolism, 4 },
            };

            genreTargets.Add(GenreType.Romance, targets);
        }

        /// <summary>
        /// Set the Science Fiction-genre point targets
        /// </summary>
        public void SetScienceFictionTargets()
        {
            Dictionary<PointCategory, int> targets = new()
            {
                { PointCategory.CharacterSheets, 4 },
                { PointCategory.PlotOutline, 3 },
                { PointCategory.WorldDocument, 8 },
                { PointCategory.Dialogue, 6 },
                { PointCategory.Subplots, 2 },
                { PointCategory.Descriptions, 7 },
                { PointCategory.Emotions, 3 },
                { PointCategory.Twists, 5 },
                { PointCategory.Symbolism, 7 },
            };

            genreTargets.Add(GenreType.ScienceFiction, targets);
        }

        /// <summary>
        /// Set the Thriller-genre point targets
        /// </summary>
        public void SetThrillerTargets()
        {
            Dictionary<PointCategory, int> targets = new()
            {
                { PointCategory.CharacterSheets, 3 },
                { PointCategory.PlotOutline, 9 },
                { PointCategory.WorldDocument, 3 },
                { PointCategory.Dialogue, 3 },
                { PointCategory.Subplots, 2 },
                { PointCategory.Descriptions, 10 },
                { PointCategory.Emotions, 4 },
                { PointCategory.Twists, 9 },
                { PointCategory.Symbolism, 2 },
            };

            genreTargets.Add(GenreType.Thriller, targets);
        }
    }
}