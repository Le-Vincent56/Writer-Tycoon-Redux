using System.Collections.Generic;
using WriterTycoon.WorkCreation.Ideation.Audience;
using WriterTycoon.WorkCreation.Ideation.Topics;

namespace WriterTycoon.WorkCreation.Ideation.Compatibility
{
    public class TopicAudienceCompatibility
    {
        private readonly Dictionary<TopicType, Dictionary<AudienceType, CompatibilityType>> topicAudienceCompatibilities;

        public TopicAudienceCompatibility()
        {
            // Create a new dictionary
            topicAudienceCompatibilities = new();

            DeployAgentsCompatibilities();
            DeployAirplanesCompatibilities();
            DeployAliensCompatibilities();
            DeployAncientCivilizationCompatibilities();
            DeployAndroidsCompatibilities();
            DeployAngelsCompatibilities();
            DeployAnimalsCompatibilities();
            DeployApocalypseCompatibilities();
            DeployArchaeologyCompatibilities();
            DeployArcheryCompatibilities();
            DeployArtCompatibilities();
            DeployAssassinsCompatibilities();
            DeployBiologyCompatibilities();
            DeployBountyHuntersCompatibilities();
            DeployCarsCompatibilities();
            DeployCardsCompatibilities();
            DeployChemistryCompatibilities();
            DeployChessCompatibilities();
            DeployCinemaCompatibilities();
            DeployCircusCompatibilities();
            DeployConspiraciesCompatibilities();
            DeployCrimeCompatibilities();
            DeployCursesCompatibilities();
            DeployDancingCompatibilities();
            DeployDemonsCompatibilities();
            DeployDetectiveCompatibilities();
            DeployDinosaursCompatibilities();
            DeployDoctorsCompatibilities();
            DeployDogsCompatibilities();
            DeployDollsCompatibilities();
            DeployDragonsCompatibilities();
            DeployDreamsCompatibilities();
            DeployEroticaCompatibilities();
            DeployEspionageCompatibilities();
            DeployEvolutionCompatibilities();
            DeployFeyCompatibilities();
            DeployFashionCompatibilities();
            DeployFoodCompatibilities();
            DeployGamblingCompatibilities();
            DeployGamesCompatibilities();
            DeployGangsCompatibilities();
            DeployGladiatorsCompatibilities();
            DeployGodsCompatibilities();
            DeployHackingCompatibilities();
            DeployHeavenCompatibilities();
            DeployHeistCompatibilities();
            DeployHellCompatibilities();
            DeployHitmenCompatibilities();
            DeployHuntingCompatibilities();
            DeployIndustrializationCompatibilities();
            DeployInsectsCompatibilities();
            DeployJournalismCompatibilities();
            DeployKaijuCompatibilities();
            DeployLanguageCompatibilities();
            DeployMathematicsCompatibilities();
            DeployMechaCompatibilities();
            DeployMagicCompatibilities();
            DeployMermaidsCompatibilities();
            DeployMilitaryCompatibilities();
            DeployMummiesCompatibilities();
            DeployMusicCompatibilities();
            DeployNinjasCompatibilities();
            DeployParallelWorldCompatibilities();
            DeployPetsCompatibilities();
            DeployPhotographyCompatibilities();
            DeployPhysicsCompatibilities();
            DeployPiratesCompatibilities();
            DeployPlantsCompatibilities();
            DeployPoliceCompatibilities();
            DeployPoliticsCompatibilities();
            DeployPrisonCompatibilities();
            DeployReligionCompatibilities();
            DeployRevolutionCompatibilities();
            DeploySamuraiCompatibilities();
            DeployScienceCompatibilities();
            DeploySeaCompatibilities();
            DeploySecretSocietyCompatibilities();
            DeployShowbizCompatibilities();
            DeploySingingCompatibilities();
            DeploySpaceCompatibilities();
            DeploySportsCompatibilities();
            DeployStoneAgeCompatibilities();
            DeploySuperheroesCompatibilities();
            DeploySupervillainsCompatibilities();
            DeploySurvivalCompatibilities();
            DeployTimeTravelCompatibilities();
            DeployTreasureHuntersCompatibilities();
            DeployUniversityCompatibilities();
            DeployVampiresCompatibilities();
            DeployVikingsCompatibilities();
            DeployVirusesCompatibilities();
            DeployWarCompatibilities();
            DeployWerewolvesCompatibilities();
            DeployWildWestCompatibilities();
            DeployWizardryCompatibilities();
            DeployZombiesCompatibilities();
        }

        /// <summary>
        /// Get the Compatibility of a Topic-Audience pairing
        /// </summary>
        public CompatibilityType GetCompatibility(TopicType topic, AudienceType audience)
        {
            // Try to get a value from the dictionary using the Topic
            if (topicAudienceCompatibilities.TryGetValue(
                topic,
                out Dictionary<AudienceType, CompatibilityType> topicCompatibilities)
            )
            {
                // If successful, try to get a value from the retrieved dictionary using the
                // Audience
                if (topicCompatibilities.TryGetValue(audience, out CompatibilityType compatibility))
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
        /// Deploy Agent-Audience compatibilities
        /// </summary>
        private void DeployAgentsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Good },
                { AudienceType.Teens, CompatibilityType.Excellent },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Poor }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Agents, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Airplanes-Audience compatibilities
        /// </summary>
        private void DeployAirplanesCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Excellent },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Poor },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Airplanes, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Aliens-Audience compatibilities
        /// </summary>
        private void DeployAliensCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Add the pair to the Dictionary
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Poor },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Create a Dictionary to store compatibilities
            topicAudienceCompatibilities.Add(TopicType.Aliens, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Ancient Civilizations-Audience compatibilities
        /// </summary>
        private void DeployAncientCivilizationCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.AncientCivilization, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Androids-Audience compatibilities
        /// </summary>
        private void DeployAndroidsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Androids, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Androids-Audience compatibilities
        /// </summary>
        private void DeployAngelsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Poor },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Terrible }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Angels, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Animals-Audience compatibilities
        /// </summary>
        private void DeployAnimalsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Excellent },
                { AudienceType.Teens, CompatibilityType.Neutral },
                { AudienceType.YoungAdults, CompatibilityType.Poor },
                { AudienceType.Adults, CompatibilityType.Terrible }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Animals, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Apocalypse-Audience compatibilities
        /// </summary>
        private void DeployApocalypseCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Neutral },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Apocalypse, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Archaeology-Audience compatibilities
        /// </summary>
        private void DeployArchaeologyCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Archaeology, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Archery-Audience compatibilities
        /// </summary>
        private void DeployArcheryCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Neutral },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Poor }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Archery, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Art-Audience compatibilities
        /// </summary>
        private void DeployArtCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Neutral },
                { AudienceType.Teens, CompatibilityType.Neutral },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Art, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Assassins-Audience compatibilities
        /// </summary>
        private void DeployAssassinsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Assassins, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Biology-Audience compatibilities
        /// </summary>
        private void DeployBiologyCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Terrible },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Biology, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Bounty Hunters-Audience compatibilities
        /// </summary>
        private void DeployBountyHuntersCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Neutral },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.BountyHunters, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Cars-Audience compatibilities
        /// </summary>
        private void DeployCarsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Excellent },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Good }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Cars, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Cards-Audience compatibilities
        /// </summary>
        private void DeployCardsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Neutral },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Good }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Cards, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Chemistry-Audience compatibilities
        /// </summary>
        private void DeployChemistryCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Chemistry, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Chess-Audience compatibilities
        /// </summary>
        private void DeployChessCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Poor },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Chess, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Circus-Audience compatibilities
        /// </summary>
        private void DeployCircusCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Excellent },
                { AudienceType.Teens, CompatibilityType.Neutral },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Terrible }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Circus, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Cinema-Audience compatibilities
        /// </summary>
        private void DeployCinemaCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Terrible },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Cinema, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Conspiracies-Audience compatibilities
        /// </summary>
        private void DeployConspiraciesCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Neutral },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Conspiracies, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Crime-Audience compatibilities
        /// </summary>
        private void DeployCrimeCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Crime, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Curses-Audience compatibilities
        /// </summary>
        private void DeployCursesCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Neutral },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Curses, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Dancing-Audience compatibilities
        /// </summary>
        private void DeployDancingCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Good },
                { AudienceType.Teens, CompatibilityType.Excellent },
                { AudienceType.YoungAdults, CompatibilityType.Poor },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Dancing, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Demons-Audience compatibilities
        /// </summary>
        private void DeployDemonsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Demons, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Detective-Audience compatibilities
        /// </summary>
        private void DeployDetectiveCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Good },
                { AudienceType.Teens, CompatibilityType.Excellent },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Detective, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Dinosaurs-Audience compatibilities
        /// </summary>
        private void DeployDinosaursCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Excellent },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Terrible }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Dinosaurs, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Doctors-Audience compatibilities
        /// </summary>
        private void DeployDoctorsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Doctors, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Dogs-Audience compatibilities
        /// </summary>
        private void DeployDogsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Excellent },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Poor },
                { AudienceType.Adults, CompatibilityType.Terrible }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Dogs, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Dolls-Audience compatibilities
        /// </summary>
        private void DeployDollsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Excellent },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Poor },
                { AudienceType.Adults, CompatibilityType.Terrible }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Dolls, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Dragons-Audience compatibilities
        /// </summary>
        private void DeployDragonsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Good },
                { AudienceType.Teens, CompatibilityType.Excellent },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Poor }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Dragons, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Dreams-Audience compatibilities
        /// </summary>
        private void DeployDreamsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Neutral },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Good }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Dreams, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Erotica-Audience compatibilities
        /// </summary>
        private void DeployEroticaCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Terrible },
                { AudienceType.YoungAdults, CompatibilityType.Poor },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Erotica, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Espionage-Audience compatibilities
        /// </summary>
        private void DeployEspionageCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Poor },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Good }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Espionage, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Evolution-Audience compatibilities
        /// </summary>
        private void DeployEvolutionCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Evolution, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Fey-Audience compatibilities
        /// </summary>
        private void DeployFeyCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Good },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Fey, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Fashion-Audience compatibilities
        /// </summary>
        private void DeployFashionCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Poor },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Fashion, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Food-Audience compatibilities
        /// </summary>
        private void DeployFoodCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Neutral },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Food, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Gambling-Audience compatibilities
        /// </summary>
        private void DeployGamblingCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Terrible },
                { AudienceType.YoungAdults, CompatibilityType.Poor },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Gambling, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Games-Audience compatibilities
        /// </summary>
        private void DeployGamesCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Neutral },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Poor }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Games, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Gangs-Audience compatibilities
        /// </summary>
        private void DeployGangsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Terrible },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Gangs, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Gladiators-Audience compatibilities
        /// </summary>
        private void DeployGladiatorsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Neutral },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Gladiators, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Gods-Audience compatibilities
        /// </summary>
        private void DeployGodsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Poor },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Terrible }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Gods, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Hacking-Audience compatibilities
        /// </summary>
        private void DeployHackingCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Hacking, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Heaven-Audience compatibilities
        /// </summary>
        private void DeployHeavenCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Poor }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Heaven, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Heist-Audience compatibilities
        /// </summary>
        private void DeployHeistCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Heist, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Hell-Audience compatibilities
        /// </summary>
        private void DeployHellCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Neutral },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Good }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Hell, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Hitmen-Audience compatibilities
        /// </summary>
        private void DeployHitmenCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Hitmen, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Hunting-Audience compatibilities
        /// </summary>
        private void DeployHuntingCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Hunting, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Industrialization-Audience compatibilities
        /// </summary>
        private void DeployIndustrializationCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Terrible },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Industrialization, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Insects-Audience compatibilities
        /// </summary>
        private void DeployInsectsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Excellent },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Terrible }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Insects, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Journalism-Audience compatibilities
        /// </summary>
        private void DeployJournalismCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Terrible },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Journalism, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Kaiju-Audience compatibilities
        /// </summary>
        private void DeployKaijuCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Neutral },
                { AudienceType.Teens, CompatibilityType.Excellent },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Kaiju, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Language-Audience compatibilities
        /// </summary>
        private void DeployLanguageCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Language, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Mathematics-Audience compatibilities
        /// </summary>
        private void DeployMathematicsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Neutral },
                { AudienceType.YoungAdults, CompatibilityType.Poor },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Mathematics, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Mecha-Audience compatibilities
        /// </summary>
        private void DeployMechaCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Neutral },
                { AudienceType.Teens, CompatibilityType.Excellent },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Poor }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Mecha, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Magic-Audience compatibilities
        /// </summary>
        private void DeployMagicCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Excellent },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Poor }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Magic, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Mermaids-Audience compatibilities
        /// </summary>
        private void DeployMermaidsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Excellent },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Poor },
                { AudienceType.Adults, CompatibilityType.Terrible }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Mermaids, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Military-Audience compatibilities
        /// </summary>
        private void DeployMilitaryCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Military, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Mummies-Audience compatibilities
        /// </summary>
        private void DeployMummiesCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Poor },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Poor }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Mummies, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Music-Audience compatibilities
        /// </summary>
        private void DeployMusicCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Music, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Ninjas-Audience compatibilities
        /// </summary>
        private void DeployNinjasCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Excellent },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Poor }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Ninjas, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Parallel World-Audience compatibilities
        /// </summary>
        private void DeployParallelWorldCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Poor },
                { AudienceType.Teens, CompatibilityType.Neutral },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Good }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.ParallelWorld, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Pets-Audience compatibilities
        /// </summary>
        private void DeployPetsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Excellent },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Poor },
                { AudienceType.Adults, CompatibilityType.Terrible }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Pets, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Curses-Audience compatibilities
        /// </summary>
        private void DeployPhotographyCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Photography, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Physics-Audience compatibilities
        /// </summary>
        private void DeployPhysicsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Terrible },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Physics, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Pirates-Audience compatibilities
        /// </summary>
        private void DeployPiratesCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Good },
                { AudienceType.Teens, CompatibilityType.Excellent },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Terrible }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Pirates, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Plants-Audience compatibilities
        /// </summary>
        private void DeployPlantsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Excellent },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Poor },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Plants, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Police-Audience compatibilities
        /// </summary>
        private void DeployPoliceCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Police, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Politics-Audience compatibilities
        /// </summary>
        private void DeployPoliticsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Terrible },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Politics, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Prison-Audience compatibilities
        /// </summary>
        private void DeployPrisonCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Terrible },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Prison, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Religion-Audience compatibilities
        /// </summary>
        private void DeployReligionCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Terrible },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Religion, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Revolution-Audience compatibilities
        /// </summary>
        private void DeployRevolutionCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Neutral },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Good }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Revolution, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Samurai-Audience compatibilities
        /// </summary>
        private void DeploySamuraiCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Good },
                { AudienceType.Teens, CompatibilityType.Excellent },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Poor }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Samurai, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Science-Audience compatibilities
        /// </summary>
        private void DeployScienceCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Neutral },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Science, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Sea-Audience compatibilities
        /// </summary>
        private void DeploySeaCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Neutral },
                { AudienceType.Teens, CompatibilityType.Excellent },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Sea, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Secret Society-Audience compatibilities
        /// </summary>
        private void DeploySecretSocietyCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Poor },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.SecretSociety, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Showbiz-Audience compatibilities
        /// </summary>
        private void DeployShowbizCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Showbiz, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Singing-Audience compatibilities
        /// </summary>
        private void DeploySingingCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Neutral },
                { AudienceType.Teens, CompatibilityType.Excellent },
                { AudienceType.YoungAdults, CompatibilityType.Poor },
                { AudienceType.Adults, CompatibilityType.Terrible }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Singing, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Space-Audience compatibilities
        /// </summary>
        private void DeploySpaceCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Neutral },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Space, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Sports-Audience compatibilities
        /// </summary>
        private void DeploySportsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Neutral },
                { AudienceType.Teens, CompatibilityType.Excellent },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Sports, compatibleAudiences);
        }

        /// <summary>
        /// Deploy StoneAge-Audience compatibilities
        /// </summary>
        private void DeployStoneAgeCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Good },
                { AudienceType.Teens, CompatibilityType.Excellent },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Poor }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.StoneAge, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Superheroes-Audience compatibilities
        /// </summary>
        private void DeploySuperheroesCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Excellent },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Terrible }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Superheroes, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Supervillains-Audience compatibilities
        /// </summary>
        private void DeploySupervillainsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Poor },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Supervillains, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Survival-Audience compatibilities
        /// </summary>
        private void DeploySurvivalCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Terrible },
                { AudienceType.YoungAdults, CompatibilityType.Poor },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Survival, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Time Travel-Audience compatibilities
        /// </summary>
        private void DeployTimeTravelCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Neutral },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Poor }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.TimeTravel, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Curses-Audience compatibilities
        /// </summary>
        private void DeployTreasureHuntersCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Excellent },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Terrible }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.TreasureHunters, compatibleAudiences);
        }

        /// <summary>
        /// Deploy University-Audience compatibilities
        /// </summary>
        private void DeployUniversityCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Good }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.University, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Vampires-Audience compatibilities
        /// </summary>
        private void DeployVampiresCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Poor },
                { AudienceType.Teens, CompatibilityType.Excellent },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Neutral }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Vampires, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Vikings-Audience compatibilities
        /// </summary>
        private void DeployVikingsCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Vikings, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Viruses-Audience compatibilities
        /// </summary>
        private void DeployVirusesCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Terrible },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Viruses, compatibleAudiences);
        }

        /// <summary>
        /// Deploy War-Audience compatibilities
        /// </summary>
        private void DeployWarCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Poor },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.War, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Werewolves-Audience compatibilities
        /// </summary>
        private void DeployWerewolvesCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Excellent },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Poor }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Werewolves, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Wild West-Audience compatibilities
        /// </summary>
        private void DeployWildWestCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Neutral },
                { AudienceType.Teens, CompatibilityType.Good },
                { AudienceType.YoungAdults, CompatibilityType.Neutral },
                { AudienceType.Adults, CompatibilityType.Excellent }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.WildWest, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Wizardry-Audience compatibilities
        /// </summary>
        private void DeployWizardryCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Neutral },
                { AudienceType.Teens, CompatibilityType.Excellent },
                { AudienceType.YoungAdults, CompatibilityType.Good },
                { AudienceType.Adults, CompatibilityType.Poor }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Wizardry, compatibleAudiences);
        }

        /// <summary>
        /// Deploy Zombies-Audience compatibilities
        /// </summary>
        private void DeployZombiesCompatibilities()
        {
            // Exit case - if the Dictionary is not yet initialized
            if (topicAudienceCompatibilities == null) return;

            // Create a Dictionary to store compatibilities
            Dictionary<AudienceType, CompatibilityType> compatibleAudiences = new()
            {
                { AudienceType.Children, CompatibilityType.Terrible },
                { AudienceType.Teens, CompatibilityType.Neutral },
                { AudienceType.YoungAdults, CompatibilityType.Excellent },
                { AudienceType.Adults, CompatibilityType.Good }
            };

            // Add the pair to the Dictionary
            topicAudienceCompatibilities.Add(TopicType.Zombies, compatibleAudiences);
        }
    }
}