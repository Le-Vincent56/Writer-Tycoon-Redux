using System;
using System.Text;
using WriterTycoon.WorkCreation.Rater;

namespace WriterTycoon.WorkCreation.Ideation.Topics
{
    public enum TopicType
    {
        Agents, Airplanes, Aliens, AncientCivilization, Androids,
        Angels, Animals, Apocalypse, Archaeology, Archery, Art,
        Assassins, Biology, BountyHunters, Cars, Cards, Chemistry,
        Chess, Cinema, Circus, Conspiracies, Crime, Curses, Dancing,
        Demons, Detective, Dinosaurs, Doctors, Dogs, Dolls, Dragons, Dreams,
        Erotica, Espionage, Evolution, Fey, Fashion, Food, Gambling, Games,
        Gangs, Gladiators, Gods, Hacking, Heaven, Heist, Hell, Hitmen, Hunting,
        Industrialization, Insects, Journalism, Kaiju, Language, Mathematics,
        Mecha, Magic, Mermaids, Military, Mummies, Music, Ninjas, ParallelWorld,
        Pets, Photography, Physics, Pirates, Plants, Police, Politics, Prison,
        Religion, Revolution, Samurai, Science, Sea, SecretSociety, Showbiz, Singing,
        Space, Sports, StoneAge, Superheroes, Supervillains, Survival, TimeTravel,
        TreasureHunters, University, Vampires, Vikings, Viruses, War, Werewolves,
        WildWest, Wizardry, Zombies
    }

    [Serializable]
    public class Topic : IMasterable
    {
        public TopicType Type { get; private set; }
        public string Name { get; private set; }
        public bool IsUnlocked { get; private set; }
        public int MasteryLevel { get; set; }
        public bool IgnoreGenreCompatibility { get; set; }

        public Topic(TopicType topicType, bool unlocked, bool ignoreGenreCompatibility = false)
        {
            Type = topicType;
            Name = DecideName(topicType);
            IsUnlocked = unlocked;
            MasteryLevel = 0;
            IgnoreGenreCompatibility = ignoreGenreCompatibility;
        }

        /// <summary>
        /// Calculate the points generated by the Topic
        /// </summary>
        public int CalculatePoints() => MasteryLevel * 10;

        /// <summary>
        /// Unlock the Topic
        /// </summary>
        public void Unlock() => IsUnlocked = true;

        /// <summary>
        /// Increase the mastery of the Topic
        /// </summary>
        public void IncreaseMastery()
        {
            // Exit case - if the mastery level is already at 3
            if (MasteryLevel >= 3) return;

            // Increment the mastery level
            MasteryLevel++;
        }

        /// <summary>
        /// Decide the display name of the Topic
        /// </summary>
        public string DecideName(TopicType topicType)
        {
            string topicName = topicType.ToString();
            StringBuilder result = new StringBuilder();

            for(int i = 0; i < topicName.Length; i++)
            {
                // Check for uppercases
                if(i > 0 && char.IsUpper(topicName[i]))
                {
                    // If so, add a space to show
                    // word differentiation
                    result.Append(' ');
                }

                // Append the letter
                result.Append(topicName[i]);
            }

            return result.ToString();
        }
    }
}