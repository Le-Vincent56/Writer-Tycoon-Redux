using System;
using System.Text;
using WriterTycoon.WorkCreation.Rater;

namespace WriterTycoon.WorkCreation.Ideation.Genres
{
    public enum GenreType
    {
        Action, Adventure, ContemporaryFiction,
        Fantasy, Horror, HistoricalFiction, Mystery,
        Romance, ScienceFiction, Thriller
    }

    [Serializable]
    public class Genre : IMasterable
    {
        public GenreType Type { get; private set; }
        public string Name { get; private set; }
        public bool IsUnlocked { get; private set; }
        public int MasteryLevel { get; set; }

        public Genre(GenreType genreType, bool unlocked)
        {
            Type = genreType;
            Name = DecideName(genreType);
            IsUnlocked = unlocked;
            MasteryLevel = 0;
        }

        /// <summary>
        /// Calculate the points generated by the Genre
        /// </summary>
        public int CalculatePoints() => MasteryLevel * 10;

        /// <summary>
        /// Unlock the Genre
        /// </summary>
        public void Unlock() => IsUnlocked = true;

        /// <summary>
        /// Increase the mastery of the Genre
        /// </summary>
        public void IncreaseMastery()
        {
            // Exit case - if the mastery level is already at 3
            if (MasteryLevel >= 3) return;

            // Increment the mastery level
            MasteryLevel++;
        }

        /// <summary>
        /// Decide the display name of the Genre
        /// </summary>
        /// <param name="genreType"></param>
        public string DecideName(GenreType genreType)
        {
            string genreName = genreType.ToString();
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < genreName.Length; i++)
            {
                // Check for uppercases
                if (i > 0 && char.IsUpper(genreName[i]))
                {
                    // If so, add a space to show
                    // word differentiation
                    result.Append(' ');
                }

                // Append the letter
                result.Append(genreName[i]);
            }

            return result.ToString();
        }
    }
}