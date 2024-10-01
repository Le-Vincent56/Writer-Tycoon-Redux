using System.Collections.Generic;
using UnityEngine;

namespace WriterTycoon.WorkCreation.Rater
{
    public class ReviewTextDatabase
    {
        private Dictionary<ReviewScore, HashSet<string>> reviews;

        public ReviewTextDatabase()
        {
            // Create a new dictionary
            reviews = new();

            // Initialize the reviews
            InitializeTerribleReviews();
            InitializePoorReviews();
            InitializeNeutralReviews();
            InitializeGoodReviews();
            InitializeExcellentReviews();
        }

        /// <summary>
        /// Get a number of random, unique reviews
        /// </summary>
        public string[] GetRandomReviews(ReviewScore score, int numberOfReviews)
        {
            // Exit case - if the Dictionary is not instantiated
            if (reviews == null) return null;

            // Check if the HashSet exists for the given ReviewScore
            if (reviews.TryGetValue(score, out HashSet<string> reviewTexts))
            {
                string[] results = new string[numberOfReviews];

                // Create a list from the HashSet for easier traversal
                List<string> reviewList = new(reviewTexts);

                // Exit case - if there are not enough reviews to fill the results array
                if (reviewList.Count < results.Length) return null;

                // Create a hashset to store unique indices
                HashSet<int> selectedIndices = new();

                // Iterate through the expected results length
                for(int i = 0; i < results.Length; i++)
                {
                    int index;
                    
                    // Set a do-while to ensure unique reviews are picked
                    do
                    {
                        // Set a random index
                        index = Random.Range(0, reviewList.Count - 1);
                    } while (!selectedIndices.Add(index));

                    // Set the result
                    results[i] = reviewList[index];
                }

                return results;
            }
            else return null;
        }

        /// <summary>
        /// Initialize reviews for a Terrible work
        /// </summary>
        private void InitializeTerribleReviews()
        {
            HashSet<string> reviewTexts = new()
            {
                "Dreadful.",
                "Dead on arrival.",
                "More ghastly than ghoulish, in the worst way.",
                "This monster should've srayed in its grave.",
                "Like a ghost - there, but barely worth noticing.",
                "A frightfully dull experience.",
                "Not even zombies care.",
                "No chills, just cold disappointment.",
                "More of a groan than a true howl.",
                "Should've stayed six feet under.",
                "Dead boring.",
                "Monster flop.",
                "More deadweight than dead-rite.",
                "I'd stake my life it's not worth reading.",
                "Not even a vampire could suck joy from this.",
                "Not worth the coffin space."
            };

            reviews.Add(ReviewScore.Terrible, reviewTexts);
        }

        /// <summary>
        /// Initialize reviews for a Poor work
        /// </summary>
        private void InitializePoorReviews()
        {
            HashSet<string> reviewTexts = new()
            {
                "Couldn't sink its fangs in deep enough.",
                "Shambling plot.",
                "Weaker than a zombie.",
                "More bones than flesh.",
                "Missing vital substance.",
                "Lacks bite.",
                "Limp as a mummy's bandages.",
                "Skeleton plot, barely enough to hold interest.",
                "Fails to leave lasting scars.",
                "Bare bones, lacking marrow and soul.",
                "Vampire-approved: Completely drained of any excitement.",
                "Lacked any spirit.",
                "More trick than treat.",
                "Never took flight.",
                "Tried to howl, but could only wimper.",
                "A grave misunderstanding of storytelling."
            };

            reviews.Add(ReviewScore.Poor, reviewTexts);
        }

        /// <summary>
        /// Initialize reviews for a Neutral work
        /// </summary>
        private void InitializeNeutralReviews()
        {
            HashSet<string> reviewTexts = new()
            {
                "Not bad, but lacked monstrous magic.",
                "Living themes, but missing a beating heart.",
                "A few thrills, but mostly cold bones.",
                "Grave but not great.",
                "Ghastly at times.",
                "Some meat on these bones, but not much.",
                "It's alive. Barely.",
                "A faint groan of approval."
            };

            reviews.Add(ReviewScore.Neutral, reviewTexts);
        }

        /// <summary>
        /// Initialize reviews for a Good work
        /// </summary>
        private void InitializeGoodReviews()
        {
            HashSet<string> reviewTexts = new()
            {
                "Rising from the grave is a surprisingly well-crafted tale.",
                "A spooky delight.",
                "Plenty of bite.",
                "A hauntingly good story.",
                "Ghoullish fun that kept me entertained throughout.",
                "Monstrous, chilling, and a treat to read.",
                "Raised my spirits.",
                "Wicked fun.",
                "Cast a spell on me - couldn't stop reading.",
                "Dreadfully entertaining.",
                "Frankly enjoyable - definitely pieced together well.",
                "Puts the 'fun' in 'funeral.'",
                "Brewed just right."
            };

            reviews.Add(ReviewScore.Good, reviewTexts);
        }

        /// <summary>
        /// Initialize reviews for an Excellent work
        /// </summary>
        private void InitializeExcellentReviews()
        {
            HashSet<string> reviewTexts = new()
            {
                "A fiendishly good tale.",
                "Devilishly clever.",
                "Dark and utterly thrilling.",
                "Sinister perfection.",
                "So lively, it could revive the dead.",
                "Spectral storytelling. Perfect in every detail.",
                "A monstrously good time.",
                "Will haunt you forever, in the best way.",
                "Will raise your spirits instantly.",
                "A real graveyard smash.",
                "Pure witchcraft.",
                "Rises above the rest. An instant favorite.",
                "Left me spellbound.",
                "Supernatural."
            };

            reviews.Add(ReviewScore.Excellent, reviewTexts);
        }
    }
}