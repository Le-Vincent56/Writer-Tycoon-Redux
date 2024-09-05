using System.Collections.Generic;
using WriterTycoon.WorkCreation.Genres;
using WriterTycoon.WorkCreation.Topics;

namespace WriterTycoon.WorkCreation.Compatibility
{
    public class GenreTopicCompatibility
    {
        private readonly Dictionary<GenreType, Dictionary<TopicType, CompatibilityType>> genreTopicCompatibilities;

        public GenreTopicCompatibility()
        {
            // Create a new Dictionary
            genreTopicCompatibilities = new();

            // Add keys and values
            DeployActionCompatibilities();
            DeployAdventureCompatibilities();
            DeployContemporaryFictionCompatibilities();
            DeployFantasyCompatibilities();
            DeployHistoricalFantasyCompatibilities();
            DeployMysteryCompatibilities();
            DeployRomanceCompatibilities();
            DeployScienceFictionCompatibilities();
            DeployThrillerCompatibilities();
        }

        /// <summary>
        /// Get the Compatibility of a Genre-Topic pairing
        /// </summary>
        public CompatibilityType GetCompatibility(GenreType genre, TopicType topic)
        {
            // Try to get a value from the dictionary using the Genre
            if(genreTopicCompatibilities.TryGetValue(
                genre, 
                out Dictionary<TopicType, CompatibilityType> topicCompatibilities)
            )
            {
                // If successful, try to get a value from the retrieved dictionary using the
                // Topic
                if(topicCompatibilities.TryGetValue(topic, out CompatibilityType compatibility))
                {
                    // If successful, return the compatibility
                    return compatibility;
                }

                // If not able to retrieve a value, return none
                return CompatibilityType.None;
            }

            // If not able to retrieve a value, return none
            return CompatibilityType.None;
        }

        /// <summary>
        /// Deploy all Action-Topic compatibilities
        /// </summary>
        private void DeployActionCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (genreTopicCompatibilities == null) return;

            // Create a Dictionary to store the compatible topics
            Dictionary<TopicType, CompatibilityType> compatibleTopics = new()
            {
                { TopicType.Agents, CompatibilityType.Excellent },
                { TopicType.Airplanes, CompatibilityType.Excellent },
                { TopicType.Aliens, CompatibilityType.Excellent },
                { TopicType.AncientCivilization, CompatibilityType.Good },
                { TopicType.Androids, CompatibilityType.Good },
                { TopicType.Angels, CompatibilityType.Terrible },
                { TopicType.Animals, CompatibilityType.Good },
                { TopicType.Apocalypse, CompatibilityType.Excellent },
                { TopicType.Archaeology, CompatibilityType.Poor },
                { TopicType.Archery, CompatibilityType.Excellent },
                { TopicType.Art, CompatibilityType.Terrible },
                { TopicType.Assassins, CompatibilityType.Excellent },
                { TopicType.Biology, CompatibilityType.Terrible },
                { TopicType.BountyHunters, CompatibilityType.Excellent },
                { TopicType.Cars, CompatibilityType.Good },
                { TopicType.Cards, CompatibilityType.Terrible },
                { TopicType.Chemistry, CompatibilityType.Neutral },
                { TopicType.Chess, CompatibilityType.Terrible },
                { TopicType.Cinema, CompatibilityType.Neutral },
                { TopicType.Circus, CompatibilityType.Neutral },
                { TopicType.Conspiracies, CompatibilityType.Good },
                { TopicType.Crime, CompatibilityType.Excellent },
                { TopicType.Curses, CompatibilityType.Good },
                { TopicType.Dancing, CompatibilityType.Poor },
                { TopicType.Demons, CompatibilityType.Excellent },
                { TopicType.Detective, CompatibilityType.Good },
                { TopicType.Dinosaurs, CompatibilityType.Excellent },
                { TopicType.Doctors, CompatibilityType.Terrible },
                { TopicType.Dogs, CompatibilityType.Poor },
                { TopicType.Dolls, CompatibilityType.Poor },
                { TopicType.Dragons, CompatibilityType.Excellent },
                { TopicType.Dreams, CompatibilityType.Good },
                { TopicType.Erotica, CompatibilityType.Neutral },
                { TopicType.Espionage, CompatibilityType.Excellent },
                { TopicType.Evolution, CompatibilityType.Neutral },
                { TopicType.Fey, CompatibilityType.Poor },
                { TopicType.Fashion, CompatibilityType.Terrible },
                { TopicType.Food, CompatibilityType.Neutral },
                { TopicType.Gambling, CompatibilityType.Poor },
                { TopicType.Games, CompatibilityType.Good },
                { TopicType.Gangs, CompatibilityType.Excellent },
                { TopicType.Gladiators, CompatibilityType.Excellent },
                { TopicType.Gods, CompatibilityType.Good },
                { TopicType.Hacking, CompatibilityType.Neutral },
                { TopicType.Heaven, CompatibilityType.Terrible },
                { TopicType.Heist, CompatibilityType.Good },
                { TopicType.Hell, CompatibilityType.Good },
                { TopicType.Hitmen, CompatibilityType.Excellent },
                { TopicType.Hunting, CompatibilityType.Excellent },
                { TopicType.Industrialization, CompatibilityType.Neutral },
                { TopicType.Insects, CompatibilityType.Poor },
                { TopicType.Journalism, CompatibilityType.Poor },
                { TopicType.Kaiju, CompatibilityType.Excellent },
                { TopicType.Language, CompatibilityType.Terrible },
                { TopicType.Mathematics, CompatibilityType.Terrible },
                { TopicType.Mecha, CompatibilityType.Excellent },
                { TopicType.Mermaids, CompatibilityType.Poor },
                { TopicType.Military, CompatibilityType.Excellent },
                { TopicType.Mummies, CompatibilityType.Good },
                { TopicType.Music, CompatibilityType.Terrible },
                { TopicType.Ninjas, CompatibilityType.Excellent },
                { TopicType.ParallelWorld, CompatibilityType.Neutral },
                { TopicType.Pets, CompatibilityType.Terrible },
                { TopicType.Photography, CompatibilityType.Terrible },
                { TopicType.Physics, CompatibilityType.Neutral },
                { TopicType.Pirates, CompatibilityType.Excellent },
                { TopicType.Plants, CompatibilityType.Terrible },
                { TopicType.Police, CompatibilityType.Good },
                { TopicType.Politics, CompatibilityType.Neutral },
                { TopicType.Prison, CompatibilityType.Good },
                { TopicType.Religion, CompatibilityType.Neutral },
                { TopicType.Revolution, CompatibilityType.Excellent },
                { TopicType.Samurai, CompatibilityType.Excellent },
                { TopicType.Sea, CompatibilityType.Neutral },
                { TopicType.SecretSociety, CompatibilityType.Good },
                { TopicType.Showbiz, CompatibilityType.Poor },
                { TopicType.Singing, CompatibilityType.Terrible },
                { TopicType.Space, CompatibilityType.Excellent },
                { TopicType.Sports, CompatibilityType.Neutral },
                { TopicType.StoneAge, CompatibilityType.Poor },
                { TopicType.Superheroes, CompatibilityType.Excellent },
                { TopicType.Supervillains, CompatibilityType.Excellent },
                { TopicType.Survival, CompatibilityType.Good },
                { TopicType.TimeTravel, CompatibilityType.Good },
                { TopicType.TreasureHunters, CompatibilityType.Good },
                { TopicType.University, CompatibilityType.Terrible },
                { TopicType.Vampires, CompatibilityType.Good },
                { TopicType.Vikings, CompatibilityType.Excellent },
                { TopicType.Viruses, CompatibilityType.Neutral },
                { TopicType.War, CompatibilityType.Excellent },
                { TopicType.Werewolves, CompatibilityType.Good },
                { TopicType.WildWest, CompatibilityType.Excellent },
                { TopicType.Wizardry, CompatibilityType.Good },
                { TopicType.Zombies, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            genreTopicCompatibilities.Add(GenreType.Action, compatibleTopics);
        }

        /// <summary>
        /// Deploy all Adventure-Topic compatibilities
        /// </summary>
        private void DeployAdventureCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (genreTopicCompatibilities == null) return;

            // Create a Dictionary to store the compatible topics
            Dictionary<TopicType, CompatibilityType> compatibleTopics = new()
            {
                { TopicType.Agents, CompatibilityType.Neutral },
                { TopicType.Airplanes, CompatibilityType.Neutral },
                { TopicType.Aliens, CompatibilityType.Good },
                { TopicType.AncientCivilization, CompatibilityType.Good },
                { TopicType.Androids, CompatibilityType.Neutral },
                { TopicType.Angels, CompatibilityType.Poor },
                { TopicType.Animals, CompatibilityType.Poor },
                { TopicType.Apocalypse, CompatibilityType.Excellent },
                { TopicType.Archaeology, CompatibilityType.Excellent },
                { TopicType.Archery, CompatibilityType.Neutral },
                { TopicType.Art, CompatibilityType.Poor },
                { TopicType.Assassins, CompatibilityType.Good },
                { TopicType.Biology, CompatibilityType.Neutral },
                { TopicType.BountyHunters, CompatibilityType.Excellent },
                { TopicType.Cars, CompatibilityType.Neutral },
                { TopicType.Cards, CompatibilityType.Terrible },
                { TopicType.Chemistry, CompatibilityType.Neutral },
                { TopicType.Chess, CompatibilityType.Terrible },
                { TopicType.Cinema, CompatibilityType.Neutral },
                { TopicType.Circus, CompatibilityType.Good },
                { TopicType.Conspiracies, CompatibilityType.Excellent },
                { TopicType.Crime, CompatibilityType.Excellent },
                { TopicType.Curses, CompatibilityType.Good },
                { TopicType.Dancing, CompatibilityType.Poor },
                { TopicType.Demons, CompatibilityType.Poor },
                { TopicType.Detective, CompatibilityType.Excellent },
                { TopicType.Dinosaurs, CompatibilityType.Excellent },
                { TopicType.Doctors, CompatibilityType.Terrible },
                { TopicType.Dogs, CompatibilityType.Neutral },
                { TopicType.Dolls, CompatibilityType.Poor },
                { TopicType.Dragons, CompatibilityType.Excellent },
                { TopicType.Dreams, CompatibilityType.Excellent },
                { TopicType.Erotica, CompatibilityType.Excellent },
                { TopicType.Espionage, CompatibilityType.Excellent },
                { TopicType.Evolution, CompatibilityType.Poor },
                { TopicType.Fey, CompatibilityType.Good },
                { TopicType.Fashion, CompatibilityType.Terrible },
                { TopicType.Food, CompatibilityType.Good },
                { TopicType.Gambling, CompatibilityType.Neutral },
                { TopicType.Games, CompatibilityType.Excellent },
                { TopicType.Gangs, CompatibilityType.Poor },
                { TopicType.Gladiators, CompatibilityType.Excellent },
                { TopicType.Gods, CompatibilityType.Neutral },
                { TopicType.Hacking, CompatibilityType.Excellent },
                { TopicType.Heaven, CompatibilityType.Terrible },
                { TopicType.Heist, CompatibilityType.Good },
                { TopicType.Hell, CompatibilityType.Terrible },
                { TopicType.Hitmen, CompatibilityType.Terrible },
                { TopicType.Hunting, CompatibilityType.Excellent },
                { TopicType.Industrialization, CompatibilityType.Terrible },
                { TopicType.Insects, CompatibilityType.Neutral },
                { TopicType.Journalism, CompatibilityType.Excellent },
                { TopicType.Kaiju, CompatibilityType.Excellent },
                { TopicType.Language, CompatibilityType.Terrible },
                { TopicType.Mathematics, CompatibilityType.Terrible },
                { TopicType.Mecha, CompatibilityType.Good },
                { TopicType.Mermaids, CompatibilityType.Good },
                { TopicType.Military, CompatibilityType.Terrible },
                { TopicType.Mummies, CompatibilityType.Excellent },
                { TopicType.Music, CompatibilityType.Neutral },
                { TopicType.Ninjas, CompatibilityType.Good },
                { TopicType.ParallelWorld, CompatibilityType.Excellent },
                { TopicType.Pets, CompatibilityType.Neutral },
                { TopicType.Photography, CompatibilityType.Good },
                { TopicType.Physics, CompatibilityType.Terrible },
                { TopicType.Pirates, CompatibilityType.Excellent },
                { TopicType.Plants, CompatibilityType.Terrible },
                { TopicType.Police, CompatibilityType.Poor },
                { TopicType.Politics, CompatibilityType.Terrible },
                { TopicType.Prison, CompatibilityType.Neutral },
                { TopicType.Religion, CompatibilityType.Terrible },
                { TopicType.Revolution, CompatibilityType.Neutral },
                { TopicType.Samurai, CompatibilityType.Good },
                { TopicType.Sea, CompatibilityType.Excellent },
                { TopicType.SecretSociety, CompatibilityType.Good },
                { TopicType.Showbiz, CompatibilityType.Neutral },
                { TopicType.Singing, CompatibilityType.Terrible },
                { TopicType.Space, CompatibilityType.Excellent },
                { TopicType.Sports, CompatibilityType.Poor },
                { TopicType.StoneAge, CompatibilityType.Good },
                { TopicType.Superheroes, CompatibilityType.Excellent },
                { TopicType.Supervillains, CompatibilityType.Poor },
                { TopicType.Survival, CompatibilityType.Good },
                { TopicType.TimeTravel, CompatibilityType.Excellent },
                { TopicType.TreasureHunters, CompatibilityType.Excellent },
                { TopicType.University, CompatibilityType.Terrible },
                { TopicType.Vampires, CompatibilityType.Neutral },
                { TopicType.Vikings, CompatibilityType.Neutral },
                { TopicType.Viruses, CompatibilityType.Terrible },
                { TopicType.War, CompatibilityType.Terrible },
                { TopicType.Werewolves, CompatibilityType.Neutral },
                { TopicType.WildWest, CompatibilityType.Excellent },
                { TopicType.Wizardry, CompatibilityType.Good },
                { TopicType.Zombies, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            genreTopicCompatibilities.Add(GenreType.Adventure, compatibleTopics);
        }

        /// <summary>
        /// Deploy all Contemporary Fiction-Topic compatibilities
        /// </summary>
        private void DeployContemporaryFictionCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (genreTopicCompatibilities == null) return;

            // Create a Dictionary to store the compatible topics
            Dictionary<TopicType, CompatibilityType> compatibleTopics = new()
            {
                { TopicType.Agents, CompatibilityType.Terrible },
                { TopicType.Airplanes, CompatibilityType.Neutral },
                { TopicType.Aliens, CompatibilityType.Terrible },
                { TopicType.AncientCivilization, CompatibilityType.Terrible },
                { TopicType.Androids, CompatibilityType.Terrible },
                { TopicType.Angels, CompatibilityType.Terrible },
                { TopicType.Animals, CompatibilityType.Good },
                { TopicType.Apocalypse, CompatibilityType.Terrible },
                { TopicType.Archaeology, CompatibilityType.Neutral },
                { TopicType.Archery, CompatibilityType.Poor },
                { TopicType.Art, CompatibilityType.Excellent },
                { TopicType.Assassins, CompatibilityType.Terrible },
                { TopicType.Biology, CompatibilityType.Neutral },
                { TopicType.BountyHunters, CompatibilityType.Terrible },
                { TopicType.Cars, CompatibilityType.Poor },
                { TopicType.Cards, CompatibilityType.Good },
                { TopicType.Chemistry, CompatibilityType.Neutral },
                { TopicType.Chess, CompatibilityType.Excellent },
                { TopicType.Cinema, CompatibilityType.Excellent },
                { TopicType.Circus, CompatibilityType.Good },
                { TopicType.Conspiracies, CompatibilityType.Poor },
                { TopicType.Crime, CompatibilityType.Poor },
                { TopicType.Curses, CompatibilityType.Terrible },
                { TopicType.Dancing, CompatibilityType.Excellent },
                { TopicType.Demons, CompatibilityType.Terrible },
                { TopicType.Detective, CompatibilityType.Poor },
                { TopicType.Dinosaurs, CompatibilityType.Terrible },
                { TopicType.Doctors, CompatibilityType.Good },
                { TopicType.Dogs, CompatibilityType.Good },
                { TopicType.Dolls, CompatibilityType.Neutral },
                { TopicType.Dragons, CompatibilityType.Terrible },
                { TopicType.Dreams, CompatibilityType.Neutral },
                { TopicType.Erotica, CompatibilityType.Good },
                { TopicType.Espionage, CompatibilityType.Terrible },
                { TopicType.Evolution, CompatibilityType.Terrible },
                { TopicType.Fey, CompatibilityType.Terrible },
                { TopicType.Fashion, CompatibilityType.Excellent },
                { TopicType.Food, CompatibilityType.Excellent },
                { TopicType.Gambling, CompatibilityType.Good },
                { TopicType.Games, CompatibilityType.Neutral },
                { TopicType.Gangs, CompatibilityType.Terrible },
                { TopicType.Gladiators, CompatibilityType.Terrible },
                { TopicType.Gods, CompatibilityType.Terrible },
                { TopicType.Hacking, CompatibilityType.Neutral },
                { TopicType.Heaven, CompatibilityType.Terrible },
                { TopicType.Heist, CompatibilityType.Poor },
                { TopicType.Hell, CompatibilityType.Terrible },
                { TopicType.Hitmen, CompatibilityType.Terrible },
                { TopicType.Hunting, CompatibilityType.Good },
                { TopicType.Industrialization, CompatibilityType.Excellent },
                { TopicType.Insects, CompatibilityType.Poor },
                { TopicType.Journalism, CompatibilityType.Excellent },
                { TopicType.Kaiju, CompatibilityType.Terrible },
                { TopicType.Language, CompatibilityType.Excellent },
                { TopicType.Mathematics, CompatibilityType.Excellent },
                { TopicType.Mecha, CompatibilityType.Terrible },
                { TopicType.Mermaids, CompatibilityType.Terrible },
                { TopicType.Military, CompatibilityType.Neutral },
                { TopicType.Mummies, CompatibilityType.Terrible },
                { TopicType.Music, CompatibilityType.Excellent },
                { TopicType.Ninjas, CompatibilityType.Terrible },
                { TopicType.ParallelWorld, CompatibilityType.Poor },
                { TopicType.Pets, CompatibilityType.Good },
                { TopicType.Photography, CompatibilityType.Good },
                { TopicType.Physics, CompatibilityType.Good },
                { TopicType.Pirates, CompatibilityType.Terrible },
                { TopicType.Plants, CompatibilityType.Good },
                { TopicType.Police, CompatibilityType.Poor },
                { TopicType.Politics, CompatibilityType.Good },
                { TopicType.Prison, CompatibilityType.Neutral },
                { TopicType.Religion, CompatibilityType.Terrible },
                { TopicType.Revolution, CompatibilityType.Good },
                { TopicType.Samurai, CompatibilityType.Terrible },
                { TopicType.Sea, CompatibilityType.Neutral },
                { TopicType.SecretSociety, CompatibilityType.Terrible },
                { TopicType.Showbiz, CompatibilityType.Good },
                { TopicType.Singing, CompatibilityType.Neutral },
                { TopicType.Space, CompatibilityType.Poor },
                { TopicType.Sports, CompatibilityType.Good },
                { TopicType.StoneAge, CompatibilityType.Terrible },
                { TopicType.Superheroes, CompatibilityType.Terrible },
                { TopicType.Supervillains, CompatibilityType.Terrible },
                { TopicType.Survival, CompatibilityType.Poor },
                { TopicType.TimeTravel, CompatibilityType.Terrible },
                { TopicType.TreasureHunters, CompatibilityType.Terrible },
                { TopicType.University, CompatibilityType.Excellent },
                { TopicType.Vampires, CompatibilityType.Terrible },
                { TopicType.Vikings, CompatibilityType.Terrible },
                { TopicType.Viruses, CompatibilityType.Poor },
                { TopicType.War, CompatibilityType.Good },
                { TopicType.Werewolves, CompatibilityType.Terrible },
                { TopicType.WildWest, CompatibilityType.Terrible },
                { TopicType.Wizardry, CompatibilityType.Terrible },
                { TopicType.Zombies, CompatibilityType.Terrible }
            };

            // Add the pair to the Dictionary
            genreTopicCompatibilities.Add(GenreType.ContemporaryFiction, compatibleTopics);
        }

        /// <summary>
        /// Deploy all Fantasy-Topic compatibilities
        /// </summary>
        private void DeployFantasyCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (genreTopicCompatibilities == null) return;

            // Create a Dictionary to store the compatible topics
            Dictionary<TopicType, CompatibilityType> compatibleTopics = new()
            {
                { TopicType.Agents, CompatibilityType.Good },
                { TopicType.Airplanes, CompatibilityType.Terrible },
                { TopicType.Aliens, CompatibilityType.Terrible },
                { TopicType.AncientCivilization, CompatibilityType.Neutral },
                { TopicType.Androids, CompatibilityType.Terrible },
                { TopicType.Angels, CompatibilityType.Neutral },
                { TopicType.Animals, CompatibilityType.Neutral },
                { TopicType.Apocalypse, CompatibilityType.Neutral },
                { TopicType.Archaeology, CompatibilityType.Poor },
                { TopicType.Archery, CompatibilityType.Good },
                { TopicType.Art, CompatibilityType.Poor },
                { TopicType.Assassins, CompatibilityType.Good },
                { TopicType.Biology, CompatibilityType.Terrible },
                { TopicType.BountyHunters, CompatibilityType.Neutral },
                { TopicType.Cars, CompatibilityType.Terrible },
                { TopicType.Cards, CompatibilityType.Neutral },
                { TopicType.Chemistry, CompatibilityType.Terrible },
                { TopicType.Chess, CompatibilityType.Good },
                { TopicType.Cinema, CompatibilityType.Terrible },
                { TopicType.Circus, CompatibilityType.Neutral },
                { TopicType.Conspiracies, CompatibilityType.Terrible },
                { TopicType.Crime, CompatibilityType.Good },
                { TopicType.Curses, CompatibilityType.Good },
                { TopicType.Dancing, CompatibilityType.Terrible },
                { TopicType.Demons, CompatibilityType.Good },
                { TopicType.Detective, CompatibilityType.Neutral },
                { TopicType.Dinosaurs, CompatibilityType.Neutral },
                { TopicType.Doctors, CompatibilityType.Terrible },
                { TopicType.Dogs, CompatibilityType.Neutral },
                { TopicType.Dolls, CompatibilityType.Neutral },
                { TopicType.Dragons, CompatibilityType.Excellent },
                { TopicType.Dreams, CompatibilityType.Excellent },
                { TopicType.Erotica, CompatibilityType.Excellent },
                { TopicType.Espionage, CompatibilityType.Poor },
                { TopicType.Evolution, CompatibilityType.Terrible },
                { TopicType.Fey, CompatibilityType.Excellent },
                { TopicType.Fashion, CompatibilityType.Terrible },
                { TopicType.Food, CompatibilityType.Poor },
                { TopicType.Gambling, CompatibilityType.Poor },
                { TopicType.Games, CompatibilityType.Excellent },
                { TopicType.Gangs, CompatibilityType.Terrible },
                { TopicType.Gladiators, CompatibilityType.Good },
                { TopicType.Gods, CompatibilityType.Excellent },
                { TopicType.Hacking, CompatibilityType.Terrible },
                { TopicType.Heaven, CompatibilityType.Good },
                { TopicType.Heist, CompatibilityType.Neutral },
                { TopicType.Hell, CompatibilityType.Good },
                { TopicType.Hitmen, CompatibilityType.Terrible },
                { TopicType.Hunting, CompatibilityType.Poor },
                { TopicType.Industrialization, CompatibilityType.Terrible },
                { TopicType.Insects, CompatibilityType.Neutral },
                { TopicType.Journalism, CompatibilityType.Terrible },
                { TopicType.Kaiju, CompatibilityType.Good },
                { TopicType.Language, CompatibilityType.Excellent },
                { TopicType.Mathematics, CompatibilityType.Good },
                { TopicType.Mecha, CompatibilityType.Poor },
                { TopicType.Mermaids, CompatibilityType.Good },
                { TopicType.Military, CompatibilityType.Terrible },
                { TopicType.Mummies, CompatibilityType.Excellent },
                { TopicType.Music, CompatibilityType.Neutral },
                { TopicType.Ninjas, CompatibilityType.Good },
                { TopicType.ParallelWorld, CompatibilityType.Excellent },
                { TopicType.Pets, CompatibilityType.Neutral },
                { TopicType.Photography, CompatibilityType.Terrible },
                { TopicType.Physics, CompatibilityType.Terrible },
                { TopicType.Pirates, CompatibilityType.Excellent },
                { TopicType.Plants, CompatibilityType.Neutral },
                { TopicType.Police, CompatibilityType.Terrible },
                { TopicType.Politics, CompatibilityType.Good },
                { TopicType.Prison, CompatibilityType.Terrible },
                { TopicType.Religion, CompatibilityType.Good },
                { TopicType.Revolution, CompatibilityType.Neutral },
                { TopicType.Samurai, CompatibilityType.Good },
                { TopicType.Sea, CompatibilityType.Neutral },
                { TopicType.SecretSociety, CompatibilityType.Good },
                { TopicType.Showbiz, CompatibilityType.Terrible },
                { TopicType.Singing, CompatibilityType.Terrible },
                { TopicType.Space, CompatibilityType.Terrible },
                { TopicType.Sports, CompatibilityType.Terrible },
                { TopicType.StoneAge, CompatibilityType.Terrible },
                { TopicType.Superheroes, CompatibilityType.Excellent },
                { TopicType.Supervillains, CompatibilityType.Excellent },
                { TopicType.Survival, CompatibilityType.Poor },
                { TopicType.TimeTravel, CompatibilityType.Excellent },
                { TopicType.TreasureHunters, CompatibilityType.Excellent },
                { TopicType.University, CompatibilityType.Neutral },
                { TopicType.Vampires, CompatibilityType.Excellent },
                { TopicType.Vikings, CompatibilityType.Excellent },
                { TopicType.Viruses, CompatibilityType.Neutral },
                { TopicType.War, CompatibilityType.Good },
                { TopicType.Werewolves, CompatibilityType.Excellent },
                { TopicType.WildWest, CompatibilityType.Neutral },
                { TopicType.Wizardry, CompatibilityType.Excellent },
                { TopicType.Zombies, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            genreTopicCompatibilities.Add(GenreType.Fantasy, compatibleTopics);
        }

        /// <summary>
        /// Deploy all Historical Fantasy-Topic compatibilities
        /// </summary>
        private void DeployHistoricalFantasyCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (genreTopicCompatibilities == null) return;

            // Create a Dictionary to store the compatible topics
            Dictionary<TopicType, CompatibilityType> compatibleTopics = new()
            {
                { TopicType.Agents, CompatibilityType.Good },
                { TopicType.Airplanes, CompatibilityType.Good },
                { TopicType.Aliens, CompatibilityType.Neutral },
                { TopicType.AncientCivilization, CompatibilityType.Excellent },
                { TopicType.Androids, CompatibilityType.Terrible },
                { TopicType.Angels, CompatibilityType.Neutral },
                { TopicType.Animals, CompatibilityType.Neutral },
                { TopicType.Apocalypse, CompatibilityType.Terrible },
                { TopicType.Archaeology, CompatibilityType.Excellent },
                { TopicType.Archery, CompatibilityType.Neutral },
                { TopicType.Art, CompatibilityType.Neutral },
                { TopicType.Assassins, CompatibilityType.Good },
                { TopicType.Biology, CompatibilityType.Terrible },
                { TopicType.BountyHunters, CompatibilityType.Poor },
                { TopicType.Cars, CompatibilityType.Excellent },
                { TopicType.Cards, CompatibilityType.Neutral },
                { TopicType.Chemistry, CompatibilityType.Terrible },
                { TopicType.Chess, CompatibilityType.Excellent },
                { TopicType.Cinema, CompatibilityType.Excellent },
                { TopicType.Circus, CompatibilityType.Good },
                { TopicType.Conspiracies, CompatibilityType.Excellent },
                { TopicType.Crime, CompatibilityType.Good },
                { TopicType.Curses, CompatibilityType.Poor },
                { TopicType.Dancing, CompatibilityType.Neutral },
                { TopicType.Demons, CompatibilityType.Terrible },
                { TopicType.Detective, CompatibilityType.Neutral },
                { TopicType.Dinosaurs, CompatibilityType.Good },
                { TopicType.Doctors, CompatibilityType.Neutral },
                { TopicType.Dogs, CompatibilityType.Neutral },
                { TopicType.Dolls, CompatibilityType.Neutral },
                { TopicType.Dragons, CompatibilityType.Terrible },
                { TopicType.Dreams, CompatibilityType.Poor },
                { TopicType.Erotica, CompatibilityType.Neutral },
                { TopicType.Espionage, CompatibilityType.Excellent },
                { TopicType.Evolution, CompatibilityType.Good },
                { TopicType.Fey, CompatibilityType.Terrible },
                { TopicType.Fashion, CompatibilityType.Good },
                { TopicType.Food, CompatibilityType.Good },
                { TopicType.Gambling, CompatibilityType.Neutral },
                { TopicType.Games, CompatibilityType.Good },
                { TopicType.Gangs, CompatibilityType.Good },
                { TopicType.Gladiators, CompatibilityType.Excellent },
                { TopicType.Gods, CompatibilityType.Terrible },
                { TopicType.Hacking, CompatibilityType.Neutral },
                { TopicType.Heaven, CompatibilityType.Terrible },
                { TopicType.Heist, CompatibilityType.Poor },
                { TopicType.Hell, CompatibilityType.Terrible },
                { TopicType.Hitmen, CompatibilityType.Terrible },
                { TopicType.Hunting, CompatibilityType.Neutral },
                { TopicType.Industrialization, CompatibilityType.Excellent },
                { TopicType.Insects, CompatibilityType.Neutral },
                { TopicType.Journalism, CompatibilityType.Excellent },
                { TopicType.Kaiju, CompatibilityType.Terrible },
                { TopicType.Language, CompatibilityType.Good },
                { TopicType.Mathematics, CompatibilityType.Good },
                { TopicType.Mecha, CompatibilityType.Terrible },
                { TopicType.Mermaids, CompatibilityType.Terrible },
                { TopicType.Military, CompatibilityType.Excellent },
                { TopicType.Mummies, CompatibilityType.Terrible },
                { TopicType.Music, CompatibilityType.Excellent },
                { TopicType.Ninjas, CompatibilityType.Neutral },
                { TopicType.ParallelWorld, CompatibilityType.Good },
                { TopicType.Pets, CompatibilityType.Poor },
                { TopicType.Photography, CompatibilityType.Good },
                { TopicType.Physics, CompatibilityType.Neutral },
                { TopicType.Pirates, CompatibilityType.Neutral },
                { TopicType.Plants, CompatibilityType.Neutral },
                { TopicType.Police, CompatibilityType.Good },
                { TopicType.Politics, CompatibilityType.Excellent },
                { TopicType.Prison, CompatibilityType.Good },
                { TopicType.Religion, CompatibilityType.Excellent },
                { TopicType.Revolution, CompatibilityType.Excellent },
                { TopicType.Samurai, CompatibilityType.Good },
                { TopicType.Sea, CompatibilityType.Neutral },
                { TopicType.SecretSociety, CompatibilityType.Good },
                { TopicType.Showbiz, CompatibilityType.Good },
                { TopicType.Singing, CompatibilityType.Poor },
                { TopicType.Space, CompatibilityType.Neutral },
                { TopicType.Sports, CompatibilityType.Neutral },
                { TopicType.StoneAge, CompatibilityType.Excellent },
                { TopicType.Superheroes, CompatibilityType.Poor },
                { TopicType.Supervillains, CompatibilityType.Poor },
                { TopicType.Survival, CompatibilityType.Good },
                { TopicType.TimeTravel, CompatibilityType.Neutral },
                { TopicType.TreasureHunters, CompatibilityType.Good },
                { TopicType.University, CompatibilityType.Neutral },
                { TopicType.Vampires, CompatibilityType.Neutral },
                { TopicType.Vikings, CompatibilityType.Excellent },
                { TopicType.Viruses, CompatibilityType.Terrible },
                { TopicType.War, CompatibilityType.Excellent },
                { TopicType.Werewolves, CompatibilityType.Terrible },
                { TopicType.WildWest, CompatibilityType.Excellent },
                { TopicType.Wizardry, CompatibilityType.Poor },
                { TopicType.Zombies, CompatibilityType.Terrible }
            };

            // Add the pair to the Dictionary
            genreTopicCompatibilities.Add(GenreType.HistoricalFiction, compatibleTopics);
        }

        /// <summary>
        /// Deploy all Mystery-Topic compatibilities
        /// </summary>
        private void DeployMysteryCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (genreTopicCompatibilities == null) return;

            // Create a Dictionary to store the compatible topics
            Dictionary<TopicType, CompatibilityType> compatibleTopics = new()
            {
                { TopicType.Agents, CompatibilityType.Good },
                { TopicType.Airplanes, CompatibilityType.Poor },
                { TopicType.Aliens, CompatibilityType.Good },
                { TopicType.AncientCivilization, CompatibilityType.Terrible },
                { TopicType.Androids, CompatibilityType.Poor },
                { TopicType.Angels, CompatibilityType.Terrible },
                { TopicType.Animals, CompatibilityType.Terrible },
                { TopicType.Apocalypse, CompatibilityType.Neutral },
                { TopicType.Archaeology, CompatibilityType.Good },
                { TopicType.Archery, CompatibilityType.Poor },
                { TopicType.Art, CompatibilityType.Terrible },
                { TopicType.Assassins, CompatibilityType.Excellent },
                { TopicType.Biology, CompatibilityType.Terrible },
                { TopicType.BountyHunters, CompatibilityType.Poor },
                { TopicType.Cars, CompatibilityType.Terrible },
                { TopicType.Cards, CompatibilityType.Neutral },
                { TopicType.Chemistry, CompatibilityType.Terrible },
                { TopicType.Chess, CompatibilityType.Terrible },
                { TopicType.Cinema, CompatibilityType.Terrible },
                { TopicType.Circus, CompatibilityType.Good },
                { TopicType.Conspiracies, CompatibilityType.Excellent },
                { TopicType.Crime, CompatibilityType.Neutral },
                { TopicType.Curses, CompatibilityType.Good },
                { TopicType.Dancing, CompatibilityType.Neutral },
                { TopicType.Demons, CompatibilityType.Terrible },
                { TopicType.Detective, CompatibilityType.Excellent },
                { TopicType.Dinosaurs, CompatibilityType.Terrible },
                { TopicType.Doctors, CompatibilityType.Neutral },
                { TopicType.Dogs, CompatibilityType.Poor },
                { TopicType.Dolls, CompatibilityType.Poor },
                { TopicType.Dragons, CompatibilityType.Terrible },
                { TopicType.Dreams, CompatibilityType.Good },
                { TopicType.Erotica, CompatibilityType.Excellent },
                { TopicType.Espionage, CompatibilityType.Excellent },
                { TopicType.Evolution, CompatibilityType.Terrible },
                { TopicType.Fey, CompatibilityType.Excellent },
                { TopicType.Fashion, CompatibilityType.Poor },
                { TopicType.Food, CompatibilityType.Terrible },
                { TopicType.Gambling, CompatibilityType.Terrible },
                { TopicType.Games, CompatibilityType.Terrible },
                { TopicType.Gangs, CompatibilityType.Poor },
                { TopicType.Gladiators, CompatibilityType.Poor },
                { TopicType.Gods, CompatibilityType.Poor },
                { TopicType.Hacking, CompatibilityType.Excellent },
                { TopicType.Heaven, CompatibilityType.Terrible },
                { TopicType.Heist, CompatibilityType.Neutral },
                { TopicType.Hell, CompatibilityType.Terrible },
                { TopicType.Hitmen, CompatibilityType.Good },
                { TopicType.Hunting, CompatibilityType.Terrible },
                { TopicType.Industrialization, CompatibilityType.Terrible },
                { TopicType.Insects, CompatibilityType.Terrible },
                { TopicType.Journalism, CompatibilityType.Excellent },
                { TopicType.Kaiju, CompatibilityType.Neutral },
                { TopicType.Language, CompatibilityType.Neutral },
                { TopicType.Mathematics, CompatibilityType.Neutral },
                { TopicType.Mecha, CompatibilityType.Neutral },
                { TopicType.Mermaids, CompatibilityType.Poor },
                { TopicType.Military, CompatibilityType.Terrible },
                { TopicType.Mummies, CompatibilityType.Good },
                { TopicType.Music, CompatibilityType.Poor },
                { TopicType.Ninjas, CompatibilityType.Good },
                { TopicType.ParallelWorld, CompatibilityType.Good },
                { TopicType.Pets, CompatibilityType.Poor },
                { TopicType.Photography, CompatibilityType.Good },
                { TopicType.Physics, CompatibilityType.Neutral },
                { TopicType.Pirates, CompatibilityType.Poor },
                { TopicType.Plants, CompatibilityType.Neutral },
                { TopicType.Police, CompatibilityType.Good },
                { TopicType.Politics, CompatibilityType.Good },
                { TopicType.Prison, CompatibilityType.Neutral },
                { TopicType.Religion, CompatibilityType.Poor },
                { TopicType.Revolution, CompatibilityType.Terrible },
                { TopicType.Samurai, CompatibilityType.Poor },
                { TopicType.Sea, CompatibilityType.Good },
                { TopicType.SecretSociety, CompatibilityType.Excellent },
                { TopicType.Showbiz, CompatibilityType.Neutral },
                { TopicType.Singing, CompatibilityType.Poor },
                { TopicType.Space, CompatibilityType.Excellent },
                { TopicType.Sports, CompatibilityType.Terrible },
                { TopicType.StoneAge, CompatibilityType.Terrible },
                { TopicType.Superheroes, CompatibilityType.Neutral },
                { TopicType.Supervillains, CompatibilityType.Neutral },
                { TopicType.Survival, CompatibilityType.Poor },
                { TopicType.TimeTravel, CompatibilityType.Good },
                { TopicType.TreasureHunters, CompatibilityType.Good },
                { TopicType.University, CompatibilityType.Neutral },
                { TopicType.Vampires, CompatibilityType.Neutral },
                { TopicType.Vikings, CompatibilityType.Terrible },
                { TopicType.Viruses, CompatibilityType.Excellent },
                { TopicType.War, CompatibilityType.Poor },
                { TopicType.Werewolves, CompatibilityType.Neutral },
                { TopicType.WildWest, CompatibilityType.Poor },
                { TopicType.Wizardry, CompatibilityType.Neutral },
                { TopicType.Zombies, CompatibilityType.Good }
            };

            // Add the pair to the Dictionary
            genreTopicCompatibilities.Add(GenreType.Mystery, compatibleTopics);
        }

        /// <summary>
        /// Deploy all Romance-Topic compatibilities
        /// </summary>
        private void DeployRomanceCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (genreTopicCompatibilities == null) return;

            // Create a Dictionary to store the compatible topics
            Dictionary<TopicType, CompatibilityType> compatibleTopics = new()
            {
                { TopicType.Agents, CompatibilityType.Poor },
                { TopicType.Airplanes, CompatibilityType.Terrible },
                { TopicType.Aliens, CompatibilityType.Terrible },
                { TopicType.AncientCivilization, CompatibilityType.Neutral },
                { TopicType.Androids, CompatibilityType.Neutral },
                { TopicType.Angels, CompatibilityType.Terrible },
                { TopicType.Animals, CompatibilityType.Terrible },
                { TopicType.Apocalypse, CompatibilityType.Good },
                { TopicType.Archaeology, CompatibilityType.Terrible },
                { TopicType.Archery, CompatibilityType.Neutral },
                { TopicType.Art, CompatibilityType.Excellent },
                { TopicType.Assassins, CompatibilityType.Terrible },
                { TopicType.Biology, CompatibilityType.Neutral },
                { TopicType.BountyHunters, CompatibilityType.Terrible },
                { TopicType.Cars, CompatibilityType.Poor },
                { TopicType.Cards, CompatibilityType.Neutral },
                { TopicType.Chemistry, CompatibilityType.Neutral },
                { TopicType.Chess, CompatibilityType.Good },
                { TopicType.Cinema, CompatibilityType.Neutral },
                { TopicType.Circus, CompatibilityType.Neutral },
                { TopicType.Conspiracies, CompatibilityType.Poor },
                { TopicType.Crime, CompatibilityType.Good },
                { TopicType.Curses, CompatibilityType.Terrible },
                { TopicType.Dancing, CompatibilityType.Excellent },
                { TopicType.Demons, CompatibilityType.Terrible },
                { TopicType.Detective, CompatibilityType.Neutral },
                { TopicType.Dinosaurs, CompatibilityType.Terrible },
                { TopicType.Doctors, CompatibilityType.Neutral },
                { TopicType.Dogs, CompatibilityType.Poor },
                { TopicType.Dolls, CompatibilityType.Poor },
                { TopicType.Dragons, CompatibilityType.Terrible },
                { TopicType.Dreams, CompatibilityType.Excellent },
                { TopicType.Erotica, CompatibilityType.Excellent },
                { TopicType.Espionage, CompatibilityType.Poor },
                { TopicType.Evolution, CompatibilityType.Terrible },
                { TopicType.Fey, CompatibilityType.Excellent },
                { TopicType.Fashion, CompatibilityType.Good },
                { TopicType.Food, CompatibilityType.Neutral },
                { TopicType.Gambling, CompatibilityType.Poor },
                { TopicType.Games, CompatibilityType.Poor },
                { TopicType.Gangs, CompatibilityType.Terrible },
                { TopicType.Gladiators, CompatibilityType.Terrible },
                { TopicType.Gods, CompatibilityType.Neutral },
                { TopicType.Hacking, CompatibilityType.Terrible },
                { TopicType.Heaven, CompatibilityType.Terrible },
                { TopicType.Heist, CompatibilityType.Good },
                { TopicType.Hell, CompatibilityType.Terrible },
                { TopicType.Hitmen, CompatibilityType.Terrible },
                { TopicType.Hunting, CompatibilityType.Terrible },
                { TopicType.Industrialization, CompatibilityType.Terrible },
                { TopicType.Insects, CompatibilityType.Terrible },
                { TopicType.Journalism, CompatibilityType.Neutral },
                { TopicType.Kaiju, CompatibilityType.Terrible },
                { TopicType.Language, CompatibilityType.Good },
                { TopicType.Mathematics, CompatibilityType.Good },
                { TopicType.Mecha, CompatibilityType.Neutral },
                { TopicType.Mermaids, CompatibilityType.Good },
                { TopicType.Military, CompatibilityType.Terrible },
                { TopicType.Mummies, CompatibilityType.Terrible },
                { TopicType.Music, CompatibilityType.Excellent },
                { TopicType.Ninjas, CompatibilityType.Poor },
                { TopicType.ParallelWorld, CompatibilityType.Neutral },
                { TopicType.Pets, CompatibilityType.Good },
                { TopicType.Photography, CompatibilityType.Excellent },
                { TopicType.Physics, CompatibilityType.Neutral },
                { TopicType.Pirates, CompatibilityType.Neutral },
                { TopicType.Plants, CompatibilityType.Poor },
                { TopicType.Police, CompatibilityType.Poor },
                { TopicType.Politics, CompatibilityType.Good },
                { TopicType.Prison, CompatibilityType.Poor },
                { TopicType.Religion, CompatibilityType.Neutral },
                { TopicType.Revolution, CompatibilityType.Good },
                { TopicType.Samurai, CompatibilityType.Terrible },
                { TopicType.Sea, CompatibilityType.Good },
                { TopicType.SecretSociety, CompatibilityType.Poor },
                { TopicType.Showbiz, CompatibilityType.Excellent },
                { TopicType.Singing, CompatibilityType.Excellent },
                { TopicType.Space, CompatibilityType.Neutral },
                { TopicType.Sports, CompatibilityType.Good },
                { TopicType.StoneAge, CompatibilityType.Terrible },
                { TopicType.Superheroes, CompatibilityType.Good },
                { TopicType.Supervillains, CompatibilityType.Good },
                { TopicType.Survival, CompatibilityType.Terrible },
                { TopicType.TimeTravel, CompatibilityType.Good },
                { TopicType.TreasureHunters, CompatibilityType.Neutral },
                { TopicType.University, CompatibilityType.Excellent },
                { TopicType.Vampires, CompatibilityType.Excellent },
                { TopicType.Vikings, CompatibilityType.Terrible },
                { TopicType.Viruses, CompatibilityType.Terrible },
                { TopicType.War, CompatibilityType.Good },
                { TopicType.Werewolves, CompatibilityType.Neutral },
                { TopicType.WildWest, CompatibilityType.Neutral },
                { TopicType.Wizardry, CompatibilityType.Neutral },
                { TopicType.Zombies, CompatibilityType.Terrible }
            };

            // Add the pair to the Dictionary
            genreTopicCompatibilities.Add(GenreType.Romance, compatibleTopics);
        }

        /// <summary>
        /// Deploy all Science Fiction-Topic compatibilities
        /// </summary>
        private void DeployScienceFictionCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (genreTopicCompatibilities == null) return;

            // Create a Dictionary to store the compatible topics
            Dictionary<TopicType, CompatibilityType> compatibleTopics = new()
            {
                { TopicType.Agents, CompatibilityType.Neutral },
                { TopicType.Airplanes, CompatibilityType.Poor },
                { TopicType.Aliens, CompatibilityType.Excellent },
                { TopicType.AncientCivilization, CompatibilityType.Terrible },
                { TopicType.Androids, CompatibilityType.Excellent },
                { TopicType.Angels, CompatibilityType.Terrible },
                { TopicType.Animals, CompatibilityType.Neutral },
                { TopicType.Apocalypse, CompatibilityType.Good },
                { TopicType.Archaeology, CompatibilityType.Poor },
                { TopicType.Archery, CompatibilityType.Terrible },
                { TopicType.Art, CompatibilityType.Neutral },
                { TopicType.Assassins, CompatibilityType.Neutral },
                { TopicType.Biology, CompatibilityType.Excellent },
                { TopicType.BountyHunters, CompatibilityType.Neutral },
                { TopicType.Cars, CompatibilityType.Neutral },
                { TopicType.Cards, CompatibilityType.Poor },
                { TopicType.Chemistry, CompatibilityType.Excellent },
                { TopicType.Chess, CompatibilityType.Poor },
                { TopicType.Cinema, CompatibilityType.Terrible },
                { TopicType.Circus, CompatibilityType.Terrible },
                { TopicType.Conspiracies, CompatibilityType.Neutral },
                { TopicType.Crime, CompatibilityType.Neutral },
                { TopicType.Curses, CompatibilityType.Terrible },
                { TopicType.Dancing, CompatibilityType.Terrible },
                { TopicType.Demons, CompatibilityType.Terrible },
                { TopicType.Detective, CompatibilityType.Good },
                { TopicType.Dinosaurs, CompatibilityType.Poor },
                { TopicType.Doctors, CompatibilityType.Neutral },
                { TopicType.Dogs, CompatibilityType.Terrible },
                { TopicType.Dolls, CompatibilityType.Terrible },
                { TopicType.Dragons, CompatibilityType.Terrible },
                { TopicType.Dreams, CompatibilityType.Excellent },
                { TopicType.Erotica, CompatibilityType.Neutral },
                { TopicType.Espionage, CompatibilityType.Neutral },
                { TopicType.Evolution, CompatibilityType.Excellent },
                { TopicType.Fey, CompatibilityType.Terrible },
                { TopicType.Fashion, CompatibilityType.Terrible },
                { TopicType.Food, CompatibilityType.Terrible },
                { TopicType.Gambling, CompatibilityType.Poor },
                { TopicType.Games, CompatibilityType.Excellent },
                { TopicType.Gangs, CompatibilityType.Terrible },
                { TopicType.Gladiators, CompatibilityType.Terrible },
                { TopicType.Gods, CompatibilityType.Terrible },
                { TopicType.Hacking, CompatibilityType.Excellent },
                { TopicType.Heaven, CompatibilityType.Terrible },
                { TopicType.Heist, CompatibilityType.Neutral },
                { TopicType.Hell, CompatibilityType.Terrible },
                { TopicType.Hitmen, CompatibilityType.Poor },
                { TopicType.Hunting, CompatibilityType.Terrible },
                { TopicType.Industrialization, CompatibilityType.Good },
                { TopicType.Insects, CompatibilityType.Terrible },
                { TopicType.Journalism, CompatibilityType.Neutral },
                { TopicType.Kaiju, CompatibilityType.Good },
                { TopicType.Language, CompatibilityType.Neutral },
                { TopicType.Mathematics, CompatibilityType.Good },
                { TopicType.Mecha, CompatibilityType.Excellent },
                { TopicType.Mermaids, CompatibilityType.Terrible },
                { TopicType.Military, CompatibilityType.Neutral },
                { TopicType.Mummies, CompatibilityType.Terrible },
                { TopicType.Music, CompatibilityType.Poor },
                { TopicType.Ninjas, CompatibilityType.Poor },
                { TopicType.ParallelWorld, CompatibilityType.Excellent },
                { TopicType.Pets, CompatibilityType.Terrible },
                { TopicType.Photography, CompatibilityType.Neutral },
                { TopicType.Physics, CompatibilityType.Excellent },
                { TopicType.Pirates, CompatibilityType.Terrible },
                { TopicType.Plants, CompatibilityType.Neutral },
                { TopicType.Police, CompatibilityType.Good },
                { TopicType.Politics, CompatibilityType.Good },
                { TopicType.Prison, CompatibilityType.Neutral },
                { TopicType.Religion, CompatibilityType.Poor },
                { TopicType.Revolution, CompatibilityType.Good },
                { TopicType.Samurai, CompatibilityType.Terrible },
                { TopicType.Sea, CompatibilityType.Neutral },
                { TopicType.SecretSociety, CompatibilityType.Neutral },
                { TopicType.Showbiz, CompatibilityType.Terrible },
                { TopicType.Singing, CompatibilityType.Terrible },
                { TopicType.Space, CompatibilityType.Excellent },
                { TopicType.Sports, CompatibilityType.Poor },
                { TopicType.StoneAge, CompatibilityType.Terrible },
                { TopicType.Superheroes, CompatibilityType.Good },
                { TopicType.Supervillains, CompatibilityType.Good },
                { TopicType.Survival, CompatibilityType.Terrible },
                { TopicType.TimeTravel, CompatibilityType.Excellent },
                { TopicType.TreasureHunters, CompatibilityType.Poor },
                { TopicType.University, CompatibilityType.Neutral },
                { TopicType.Vampires, CompatibilityType.Terrible },
                { TopicType.Vikings, CompatibilityType.Terrible },
                { TopicType.Viruses, CompatibilityType.Excellent },
                { TopicType.War, CompatibilityType.Neutral },
                { TopicType.Werewolves, CompatibilityType.Terrible },
                { TopicType.WildWest, CompatibilityType.Terrible },
                { TopicType.Wizardry, CompatibilityType.Terrible },
                { TopicType.Zombies, CompatibilityType.Good }
            };

            // Add the pair to the Dictionary
            genreTopicCompatibilities.Add(GenreType.ScienceFiction, compatibleTopics);
        }

        /// <summary>
        /// Deploy all Thriller-Topic compatibilities
        /// </summary>
        private void DeployThrillerCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (genreTopicCompatibilities == null) return;

            // Create a Dictionary to store the compatible topics
            Dictionary<TopicType, CompatibilityType> compatibleTopics = new()
            {
                { TopicType.Agents, CompatibilityType.Good },
                { TopicType.Airplanes, CompatibilityType.Neutral },
                { TopicType.Aliens, CompatibilityType.Neutral },
                { TopicType.AncientCivilization, CompatibilityType.Terrible },
                { TopicType.Androids, CompatibilityType.Good },
                { TopicType.Angels, CompatibilityType.Poor },
                { TopicType.Animals, CompatibilityType.Terrible },
                { TopicType.Apocalypse, CompatibilityType.Neutral },
                { TopicType.Archaeology, CompatibilityType.Neutral },
                { TopicType.Archery, CompatibilityType.Terrible },
                { TopicType.Art, CompatibilityType.Neutral },
                { TopicType.Assassins, CompatibilityType.Excellent },
                { TopicType.Biology, CompatibilityType.Terrible },
                { TopicType.BountyHunters, CompatibilityType.Good },
                { TopicType.Cars, CompatibilityType.Terrible },
                { TopicType.Cards, CompatibilityType.Good },
                { TopicType.Chemistry, CompatibilityType.Terrible },
                { TopicType.Chess, CompatibilityType.Neutral },
                { TopicType.Cinema, CompatibilityType.Neutral },
                { TopicType.Circus, CompatibilityType.Neutral },
                { TopicType.Conspiracies, CompatibilityType.Good },
                { TopicType.Crime, CompatibilityType.Excellent },
                { TopicType.Curses, CompatibilityType.Good },
                { TopicType.Dancing, CompatibilityType.Terrible },
                { TopicType.Demons, CompatibilityType.Terrible },
                { TopicType.Detective, CompatibilityType.Excellent },
                { TopicType.Dinosaurs, CompatibilityType.Neutral },
                { TopicType.Doctors, CompatibilityType.Terrible },
                { TopicType.Dogs, CompatibilityType.Terrible },
                { TopicType.Dolls, CompatibilityType.Neutral },
                { TopicType.Dragons, CompatibilityType.Terrible },
                { TopicType.Dreams, CompatibilityType.Excellent },
                { TopicType.Erotica, CompatibilityType.Excellent },
                { TopicType.Espionage, CompatibilityType.Excellent },
                { TopicType.Evolution, CompatibilityType.Terrible },
                { TopicType.Fey, CompatibilityType.Good },
                { TopicType.Fashion, CompatibilityType.Terrible },
                { TopicType.Food, CompatibilityType.Terrible },
                { TopicType.Gambling, CompatibilityType.Good },
                { TopicType.Games, CompatibilityType.Neutral },
                { TopicType.Gangs, CompatibilityType.Good },
                { TopicType.Gladiators, CompatibilityType.Good },
                { TopicType.Gods, CompatibilityType.Poor },
                { TopicType.Hacking, CompatibilityType.Excellent },
                { TopicType.Heaven, CompatibilityType.Terrible },
                { TopicType.Heist, CompatibilityType.Excellent },
                { TopicType.Hell, CompatibilityType.Terrible },
                { TopicType.Hitmen, CompatibilityType.Excellent },
                { TopicType.Hunting, CompatibilityType.Neutral },
                { TopicType.Industrialization, CompatibilityType.Terrible },
                { TopicType.Insects, CompatibilityType.Poor },
                { TopicType.Journalism, CompatibilityType.Excellent },
                { TopicType.Kaiju, CompatibilityType.Terrible },
                { TopicType.Language, CompatibilityType.Terrible },
                { TopicType.Mathematics, CompatibilityType.Terrible },
                { TopicType.Mecha, CompatibilityType.Terrible },
                { TopicType.Mermaids, CompatibilityType.Neutral },
                { TopicType.Military, CompatibilityType.Neutral },
                { TopicType.Mummies, CompatibilityType.Neutral },
                { TopicType.Music, CompatibilityType.Neutral },
                { TopicType.Ninjas, CompatibilityType.Neutral },
                { TopicType.ParallelWorld, CompatibilityType.Good },
                { TopicType.Pets, CompatibilityType.Terrible },
                { TopicType.Photography, CompatibilityType.Good },
                { TopicType.Physics, CompatibilityType.Terrible },
                { TopicType.Pirates, CompatibilityType.Neutral },
                { TopicType.Plants, CompatibilityType.Neutral },
                { TopicType.Police, CompatibilityType.Excellent },
                { TopicType.Politics, CompatibilityType.Excellent },
                { TopicType.Prison, CompatibilityType.Good },
                { TopicType.Religion, CompatibilityType.Terrible },
                { TopicType.Revolution, CompatibilityType.Neutral },
                { TopicType.Samurai, CompatibilityType.Terrible },
                { TopicType.Sea, CompatibilityType.Good },
                { TopicType.SecretSociety, CompatibilityType.Excellent },
                { TopicType.Showbiz, CompatibilityType.Good },
                { TopicType.Singing, CompatibilityType.Terrible },
                { TopicType.Space, CompatibilityType.Excellent },
                { TopicType.Sports, CompatibilityType.Terrible },
                { TopicType.StoneAge, CompatibilityType.Terrible },
                { TopicType.Superheroes, CompatibilityType.Terrible },
                { TopicType.Supervillains, CompatibilityType.Excellent },
                { TopicType.Survival, CompatibilityType.Excellent },
                { TopicType.TimeTravel, CompatibilityType.Poor },
                { TopicType.TreasureHunters, CompatibilityType.Good },
                { TopicType.University, CompatibilityType.Neutral },
                { TopicType.Vampires, CompatibilityType.Good },
                { TopicType.Vikings, CompatibilityType.Terrible },
                { TopicType.Viruses, CompatibilityType.Neutral },
                { TopicType.War, CompatibilityType.Poor },
                { TopicType.Werewolves, CompatibilityType.Neutral },
                { TopicType.WildWest, CompatibilityType.Poor },
                { TopicType.Wizardry, CompatibilityType.Poor },
                { TopicType.Zombies, CompatibilityType.Good }
            };

            // Add the pair to the Dictionary
            genreTopicCompatibilities.Add(GenreType.Thriller, compatibleTopics);
        }
    }
}