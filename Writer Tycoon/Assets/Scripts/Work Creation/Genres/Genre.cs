using System;
using WriterTycoon.WorkCreation.Rater;

namespace WriterTycoon.WorkCreation.Genres
{
    [Serializable]
    public class Genre : IMasterable
    {
        public string Name { get; private set; }
        public bool IsUnlocked { get; private set; }
        public int MasteryLevel { get; set; }

        public Genre(string name, bool unlocked)
        {
            Name = name;
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
    }
}