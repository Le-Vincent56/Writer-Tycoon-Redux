using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WriterTycoon.WorkCreation.Ideation.About;
using WriterTycoon.WorkCreation.Ideation.Audience;
using WriterTycoon.WorkCreation.Ideation.Genres;
using WriterTycoon.WorkCreation.Ideation.Topics;
using WriterTycoon.WorkCreation.Ideation.WorkTypes;
using WriterTycoon.World.Economy;

namespace WriterTycoon.WorkCreation.Publication
{
    public class PublishedWork
    {

        [Header("Work Info")]
        [SerializeField] private readonly int hash;
        [SerializeField] private readonly AboutInfo aboutInfo;
        [SerializeField] private readonly List<Topic> topics;
        [SerializeField] private readonly List<Genre> genres;
        [SerializeField] private readonly AudienceType audience;
        [SerializeField] private readonly WorkType type;
        [SerializeField] private readonly float score;
        [SerializeField] private float price;
        [SerializeField] private (int Day, int Month, int Year) releaseDate;

        [Header("Selling Info")]
        [SerializeField] private bool isSelling;
        [SerializeField] private int weeksSinceRelease;
        [SerializeField] private int peakWeek;
        [SerializeField] private int peakSales;
        [SerializeField] private float growthRate;
        [SerializeField] private float decayRate;
        [SerializeField] private float currentSales;
        [SerializeField] private List<SalesData> salesHistory;

        public UnityAction<List<SalesData>> UpdateSalesHistory = delegate { };

        public int Hash { get => hash; }
        public string Title { get => aboutInfo.Title; }
        public string Author { get => aboutInfo.Author; }
        public string Description { get => aboutInfo.Description; }
        public List<Topic> Topics { get => topics; }
        public List<Genre> Genres { get => genres; }
        public AudienceType Audience { get => audience; }
        public WorkType Type { get => type; }
        public float Score { get => score; }
        public float Price { get => price; }
        public (int Day, int Month, int Year) ReleaseDate { get => releaseDate; }
        public bool IsSelling { get => isSelling; }
        public int WeeksSinceRelease { get => weeksSinceRelease; set => weeksSinceRelease = value; }
        public int PeakWeek { get => peakWeek; set => peakWeek = value; }
        public int PeakSales { get => peakSales; set => peakSales = value; }
        public float DecayRate { get => decayRate; set => decayRate = value; }
        public float GrowthRate { get => growthRate; set => growthRate = value; }
        public float CurrentSales { get => currentSales; set => currentSales = value; }

        public PublishedWork
        (   
            int hash, 
            AboutInfo aboutInfo, 
            List<Topic> topics, List<Genre> genres, 
            AudienceType audience, WorkType type, 
            float score
        )
        {
            this.hash = hash;
            this.aboutInfo = aboutInfo;
            this.topics = topics;
            this.genres = genres;
            this.audience = audience;
            this.type = type;
            this.score = score;

            currentSales = 0f;

            // Calcualte the price based on the work type
            switch (type)
            {
                case WorkType.None:
                    price = 0.0f;
                    break;

                case WorkType.Poetry:
                    price = 1.0f;
                    break;

                case WorkType.FlashFiction:
                    price = 5.0f;
                    break;

                case WorkType.ShortStory:
                    price = 10.0f;
                    break;

                case WorkType.Novella:
                    price = 20.0f;
                    break;

                case WorkType.Novel:
                    price = 30.0f;
                    break;

                case WorkType.Screenplay:
                    price = 30.0f;
                    break;
            }

            // Initialize the sales history
            salesHistory = new();
        }

        /// <summary>
        /// Set whether or not the Published Work is selling
        /// </summary>
        public void SetIsSelling(bool isSelling) => this.isSelling = isSelling;

        /// <summary>
        /// Set the date of the Work's release
        /// </summary>
        public void SetReleaseDate(int day, int month, int year)
        {
            // Set the date
            releaseDate.Day = day;
            releaseDate.Month = month;
            releaseDate.Year = year;
        }

        /// <summary>
        /// Calculate the current sales for the Published Work
        /// </summary>
        public void CalculateSales()
        {
            // Check if in the growing or decaying phase
            if(weeksSinceRelease < peakWeek)
            {
                // Add the growth rate to the current sales
                currentSales += growthRate;

                // Ensure sales do not exceed peak sales
                if (currentSales > peakSales)
                    currentSales = peakSales;
            }
            else
            {
                // Deecay the current sales
                currentSales -= decayRate;

                // Ensure sales do not go below 0
                if(currentSales < 0f)
                    currentSales = 0f;
            }
        }

        /// <summary>
        /// Add a new SalesData to the sales history
        /// </summary>
        public void AddSalesData(int copiesSold, float income)
        {
            // Add new data to the sales history
            salesHistory.Add(new SalesData(weeksSinceRelease, copiesSold, income));

            // Invoke the event
            UpdateSalesHistory.Invoke(salesHistory);
        }
    }
}