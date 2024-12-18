using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using GhostWriter.Patterns.EventBus;
using GhostWriter.Patterns.ServiceLocator;
using GhostWriter.WorkCreation.Publication;
using GhostWriter.Entities.Tracker;
using GhostWriter.Entities;

namespace GhostWriter.World.Economy
{
    public class EconomyManager : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<int, PublishedWork> sellingWorksDict;
        [SerializeField] private Dictionary<int, PublishedWorkLifecycle> lifecycleDict;
        private Calendar.Calendar calendar;
        private CompetitorRecord competitorRecord;

        private ICompetitor player;

        [Header("Sell Variables")]
        [SerializeField] private float peakCopies = 500f;
        [SerializeField] private float scoreExponent = 1.0f;

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
            competitorRecord = ServiceLocator.ForSceneOf(this).Get<CompetitorRecord>();

            // Store the Player character
            player = competitorRecord.GetPlayer();
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

            // Exit case - the Published Work owner is not the Player
            if (publishedWork.Owner != player) return;

            EventBus<CreateSalesGraph>.Raise(new CreateSalesGraph()
            {
                WorkToGraph = publishedWork,
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

                // Update the owner's money
                work.Owner.CalculateSales(revenue);

                // Advance the lifecycle week
                lifecycle.AdvanceWeek();

                // Check if the lifecycle has ended
                if (lifecycle.IsLifecycleEnded())
                {
                    // If so, mark for removal
                    completedHashes.Add(hash);

                    // Stop the sales graph to prevent update errors
                    EventBus<StopSalesGraph>.Raise(new StopSalesGraph()
                    {
                        Hash = hash
                    });
                }

                // Check if the Owner is the Player
                if (work.Owner == player)
                {
                    // Update the associated Sales Graph
                    EventBus<UpdateSalesGraph>.Raise(new UpdateSalesGraph()
                    {
                        Hash = hash,
                        Sales = weeklySales
                    });

                    // Update the Publication Card
                    EventBus<UpdatePublicationCard>.Raise(new UpdatePublicationCard()
                    {
                        Hash = hash,
                        PublishedWork = work
                    });
                }
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

                // Destroy the sales graph
                EventBus<DestroySalesGraph>.Raise(new DestroySalesGraph()
                {
                    Hash = hash
                });

                // Remove the Published Work from the 
                sellingWorksDict.Remove(hash);

                // Remove the lifecycle object from the dictionary
                lifecycleDict.Remove(hash);
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
            float growthProportion = 0.15f + (0.1f * (score / 100f));              // 15% to 25% of the lifecycle
            float peakProportion = 0.5f + (0.1f * (score / 100f));                // 50% to 60% of the lifecycle
            float decayProportion = 1.0f - growthProportion - peakProportion;   // Remainder of the life cycle

            // Calculate phase durations
            int growthDuration = Mathf.RoundToInt(durationTotal * growthProportion);
            int peakDuration = Mathf.RoundToInt(durationTotal * peakProportion);
            int decayDuration = Mathf.RoundToInt(durationTotal * decayProportion);

            // Generate weekly sales
            List<int> weeklySales = new();

            // Get weekly sales for the growth phase
            for (int i = 0; i < growthDuration; i++)
            {
                // Linear growth with random fluctuation
                float progress = (float)(i + 1) / growthDuration;

                // Introduce some randomness in the growth (+- 20%)
                float randomFactor = 1f + Random.Range(-0.2f, 0.2f);

                // Adjust the sales value by applying random fluctuation
                int sales = Mathf.RoundToInt(salesMax * progress * randomFactor);

                // Ensure sales remain non-negative
                sales = Mathf.Max(sales, 0);

                weeklySales.Add(sales);
            }

            // Get weekly sales for the peak phase
            for (int i = 0; i < peakDuration; i++)
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
                // Decay progress (from 0 to 1 across the decay phase)
                float decayProgress = (float)(i + 1) / decayDuration;

                // Calculate the decay multiplier using smooth decay towards 0
                float decayMultiplier = Mathf.Lerp(1f, 0f, decayProgress);

                // Apply the decay multiplier to reduce sales towards 0
                int sales = Mathf.RoundToInt(salesMax * decayMultiplier);

                // Ensure sales don't go negative
                sales = Mathf.Max(sales, 0);

                weeklySales.Add(sales);
            }

            // Add a number of weeks to confirm no sales
            int confirmationWeeks = 5;
            for(int i = 0; i < confirmationWeeks; i++)
            {
                weeklySales.Add(0);
            }

            return weeklySales;
        }
    }
}