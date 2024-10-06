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
        private Calendar.Calendar calendar;

        [Header("Sell Variables")]
        [SerializeField] private float peakCopies;
        [SerializeField] private float scoreExponent;
        [SerializeField] private float decayFactor;
        [SerializeField] private float decayRate;
        [SerializeField] private float minDecayFactor;

        [SerializeField] private float playerBank;

        private EventBinding<SellWork> sellWorkEvent;
        private EventBinding<PassWeek> passWeekEvent;

        private void Awake()
        {
            // Initialize the dictionary
            sellingWorksDict = new();
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

            // Assign the Published Works' sales lifecycle parameters
            AssignLifecycleParameters(publishedWork);

            // Add the Published Work to the dictionary
            sellingWorksDict.Add(publishedWork.Hash, publishedWork);

            // Send out the selling works
            EventBus<CreateSalesGraph>.Raise(new CreateSalesGraph()
            {
                WorkToGraph = publishedWork
            });
        }

        /// <summary>
        /// Assign the selling lifecycle parameters for each book
        /// </summary>
        /// <param name="publishedWork"></param>
        private void AssignLifecycleParameters(PublishedWork publishedWork)
        {
            // Calculate the peak number of sales
            float scoreRatio = Mathf.Pow(publishedWork.Score / 100f, scoreExponent);
            float peakSales = peakCopies * scoreRatio;
            publishedWork.PeakSales = Mathf.RoundToInt(peakSales);

            // Determine the growth rate and decay rate
            publishedWork.GrowthRate = Mathf.Lerp(100f, 5000f, (float)publishedWork.Score / 100f);
            publishedWork.DecayRate = Mathf.Lerp(50f, 300f, 1f - ((float)publishedWork.Score / 100f));

            // Determine when the peak week is
            publishedWork.PeakWeek = Mathf.RoundToInt(Mathf.Lerp(6f, 20f, (float)publishedWork.Score / 100f));
        }

        /// <summary>
        /// Calculate the weekly sales
        /// </summary>
        private void ProcessWeeklySales()
        {
            // Exit case - there are no Works selling
            if (sellingWorksDict.Count <= 0) return;

            foreach(KeyValuePair<int, PublishedWork> kvp in  sellingWorksDict)
            {
                // Increment how many weeks it's been since the Work's release
                kvp.Value.WeeksSinceRelease++;

                // Calculate the number of copies sold
                int copiesSold = CalculateWeeklySales(kvp.Value);

                // Calculate the income
                float income = copiesSold * kvp.Value.Price;

                // Add the income to the player bank
                playerBank += income;

                // Update the Published Work's sales history
                kvp.Value.AddSalesData(copiesSold, income);

                // Check if no copies are sold
                if (copiesSold < 1 && kvp.Value.PeakWeek != 0)
                {
                    // Stop selling the Work
                    kvp.Value.SetIsSelling(false);
                }
            }

            // Update which Works are being sold
            //UpdateSellingWorks();
        }

        /// <summary>
        /// Calcualte the weekly sales for the Published Work
        /// </summary>
        /// <param name="publishedWork"></param>
        /// <returns></returns>
        private int CalculateWeeklySales(PublishedWork publishedWork)
        {
            // Calculate the sales for the Published Work
            publishedWork.CalculateSales();

            return Mathf.RoundToInt(publishedWork.CurrentSales);
        }

        /// <summary>
        /// Update which Works are being sold
        /// </summary>
        private void UpdateSellingWorks()
        {
            // Create a HashSet to store the hashes of Published Works that are
            // no longer selling
            HashSet<int> notSellingHashes = new();

            // Iterate through the Dictionary
            foreach(KeyValuePair<int, PublishedWork> kvp in sellingWorksDict)
            {
                Debug.Log("Is Selling: " + kvp.Value.IsSelling);

                // Skip if the Published Work is selling
                if (kvp.Value.IsSelling) continue;

                // If not selling, add the Hash to the HashSet
                notSellingHashes.Add(kvp.Key);
            }

            // Exit case - there are no hashes to remove
            if (notSellingHashes.Count <= 0) return;

            // Iterate through each hash
            foreach (int hash in notSellingHashes)
            {
                // Remove the hash from the selling works Dictionary
                sellingWorksDict.Remove(hash);

                // Destroy it's Sales Graph
                EventBus<DestroySalesGraph>.Raise(new DestroySalesGraph()
                {
                    Hash = hash
                });
            }
        }
    }
}