using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.Patterns.ServiceLocator;
using WriterTycoon.WorkCreation.Publication;

namespace WriterTycoon.World.Economy
{
    public class EconomyManager : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<int, PublishedWork> sellingWorksDict;
        [SerializeField] private Dictionary<int, PublishedWorkLifecycle> lifecycleDict;
        private Calendar.Calendar calendar;

        [Header("Sell Variables")]
        [SerializeField] private float peakCopies = 500f;
        [SerializeField] private float scoreExponent = 1.0f;

        // Determines the rate at which sales decline during the Decay phase
        // - decayFactor < 1: exponential decay; sales decrease rapidly at first then level off
        // - decayFactor = 1: no decay; sales remain constant indefinitely
        // - decayFactor > 1: exponential growth during decay
        [SerializeField] private float decayFactor = 0.5f;

        // Serves as a lower bound for the decay multiplier, ensuring that a Work
        // still generates some minimal sales, preventing it from abruptly dropping to 0
        // - Higer minDecayMult: Sustains higher sales at the end of the Decay phase
        // - Lower minDecayMult: Allows sales to approach zero more closely
        [SerializeField] private float minDecayMult = 0.05f;

        [SerializeField] private float playerBank;

        private EventBinding<SellWork> sellWorkEvent;
        private EventBinding<PassWeek> passWeekEvent;

        private void Awake()
        {
            // Initialize the dictionaries
            sellingWorksDict = new();
            lifecycleDict = new();
        }

        private void OnEnable()
        {
            sellWorkEvent = new EventBinding<SellWork>(StartSellingWork);
            EventBus<SellWork>.Register(sellWorkEvent);

            passWeekEvent = new EventBinding<PassWeek>(ProcessWeeklySales);
            EventBus<PassWeek>.Register(passWeekEvent);
        }

        private void OnDisable()
        {
            EventBus<SellWork>.Deregister(sellWorkEvent);
            EventBus<PassWeek>.Deregister(passWeekEvent);
        }

        private void Start()
        {
            // Get the Calendar to use as a service
            calendar = ServiceLocator.ForSceneOf(this).Get<Calendar.Calendar>();
        }

        /// <summary>
        /// Start selling a work
        /// </summary>
        private void StartSellingWork(SellWork eventData)
        {
            // Extract the Published Work
            PublishedWork publishedWork = eventData.WorkToSell;

            // Set the Published Work to selling
            publishedWork.SetIsSelling(true);

            // Set the release date
            publishedWork.SetReleaseDate(calendar.Day, calendar.Month, calendar.Year);

            // Add the Published Work to the dictionary
            sellingWorksDict.Add(publishedWork.Hash, publishedWork);

            // Get the weekly sales of the Published WOrk
            List<int> weeklySales = GenerateWeeklySales(publishedWork.Score);

            // Create a work lifecycle
            PublishedWorkLifecycle lifecycle = new PublishedWorkLifecycle(publishedWork, weeklySales);

            // Add to the lifecycle dictionary
            lifecycleDict.Add(publishedWork.Hash, lifecycle);

            // Send out the selling works
            EventBus<CreateSalesGraph>.Raise(new CreateSalesGraph()
            {
                WorkToGraph = publishedWork
            });
        }

        /// <summary>
        /// Calculate the weekly sales
        /// </summary>
        private void ProcessWeeklySales()
        {
            // Exit case - there are no Works selling
            if (sellingWorksDict.Count <= 0) return;

            // Create a list to hold hashes of works whose lifecycles have ended
            List<int> completedHashes = new();

            foreach(KeyValuePair<int, PublishedWork> kvp in  sellingWorksDict)
            {
                // Get the hash and work
                int hash = kvp.Key;
                PublishedWork work = kvp.Value;

                // Exit case - the lifecycle object does not exist within the dictionary
                if (!lifecycleDict.TryGetValue(hash, out PublishedWorkLifecycle lifecycle))
                    return;

                // Get the sales for the current week
                int weeklySales = lifecycle.GetSalesForCurrentWeek();

                // Calculate revenue
                float revenue = weeklySales * work.Price;

                // Add the weekl sales to the Published Work for data
                work.AddSales(weeklySales);

                // Update the player's bank
                playerBank += revenue;

                // Advance the lifecycle week
                lifecycle.AdvanceWeek();

                // Check if the lifecycle has ended
                if (lifecycle.IsLifecycleEnded())
                    // If so, mark for removal
                    completedHashes.Add(hash);
            }
            
            // Iterate through each completed hash
            foreach(int hash in completedHashes)
            {
                // Exit case - the dictionaries don't contain the hash
                if (!sellingWorksDict.TryGetValue(hash, out PublishedWork work))
                    return;

                if (!lifecycleDict.TryGetValue(hash, out PublishedWorkLifecycle lifecycle))
                    return;

                // Set the Published Work to no longer selling
                work.SetIsSelling(false);

                // Remove the Published Work from the 
                sellingWorksDict.Remove(hash);

                // Remove the lifecycle object from the dictionary
                lifecycleDict.Remove(hash);

                EventBus<DestroySalesGraph>.Raise(new DestroySalesGraph()
                {
                    Hash = hash
                });
            }
        }

        /// <summary>
        /// Generate the weekly sales lifecycle according to a score
        /// </summary>
        private List<int> GenerateWeeklySales(float score)
        {
            // Calculate the maximum weekly sales
            float salesMax = peakCopies * Mathf.Pow(score / 100f, scoreExponent);

            // Determine the total lifecycle duration
            float baseDuration = 20f;   // Base weeks for a medium score
            float durationTotal = baseDuration * (1f + (score / 100f));

            // Define phase proportions
            float growthProportion = 0.2f + 0.1f * (score / 100f);              // 20% to 30% of the lifecycle
            float peakProportion = 0.5f + 0.2f * (score / 100f);                // 50% to 70% of the lifecycle
            float decayProportion = 1.0f - growthProportion - peakProportion;   // Remainder of the life cycle

            // Calculate phase durations
            int growthDuration = Mathf.RoundToInt(durationTotal * growthProportion);
            int peakDuration = Mathf.RoundToInt(durationTotal * peakProportion);
            int decayDuration = Mathf.RoundToInt(durationTotal * decayProportion);

            // Generate weekly sales
            List<int> weeklySales = new();

            // Get weekly sales for the growth phase
            for(int i = 0; i < growthDuration; i++)
            {
                // Linear growth
                float progress = (float)(i + 1) / growthDuration;
                int sales = Mathf.RoundToInt(salesMax * progress);
                weeklySales.Add(sales);
            }

            // Get weekly sales for the peak phase
            for(int i = 0; i < peakDuration; i++)
            {
                // Create random variation around the sales
                float variation = 0.1f; // +- 10$%
                float randomFactor = 1f + Random.Range(-variation, variation);
                int sales = Mathf.RoundToInt(salesMax * randomFactor);
                weeklySales.Add(sales);
            }

            // Get weekly sales for the decay phase
            for(int i = 0; i < decayDuration; i++)
            {
                float decayProgress = (float)(i + 1) / decayDuration;
                float decayMultiplier = Mathf.Max(Mathf.Pow(decayFactor, decayProgress), minDecayMult);
                int sales = Mathf.RoundToInt(salesMax * decayMultiplier);

                // Ensure sales don't go negative
                sales = Mathf.Max(sales, 0);

                weeklySales.Add(sales);
            }

            // Add a number of weeks to confirm no sales
            int confirmationWeeks = 2;
            for(int i = 0; i < confirmationWeeks; i++)
            {
                weeklySales.Add(0);
            }

            return weeklySales;
        }
    }
}