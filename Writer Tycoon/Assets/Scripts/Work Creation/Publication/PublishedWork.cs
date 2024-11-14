using System.Collections.Generic;
using UnityEngine;
using GhostWriter.Entities;
using GhostWriter.WorkCreation.Ideation.About;
using GhostWriter.WorkCreation.Ideation.Audience;
using GhostWriter.WorkCreation.Ideation.Genres;
using GhostWriter.WorkCreation.Ideation.Topics;
using GhostWriter.WorkCreation.Ideation.WorkTypes;

namespace GhostWriter.WorkCreation.Publication
{
    public class PublishedWork
    {

        [Header("Work Info")]
        [SerializeField] private readonly int hash;
        [SerializeField] private ICompetitor owner;
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
        [SerializeField] private int cumulativeSales;

        public int Hash { get => hash; }
        public ICompetitor Owner { get => owner; }
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
        public int CumulativeSales { get =>  cumulativeSales; }

        public PublishedWork
        (   
            int hash, 
            ICompetitor owner,
            AboutInfo aboutInfo, 
            List<Topic> topics, List<Genre> genres, 
            AudienceType audience, WorkType type, 
            float score
        )
        {
            this.hash = hash;
            this.owner = owner;
            this.aboutInfo = aboutInfo;
            this.topics = topics;
            this.genres = genres;
            this.audience = audience;
            this.type = type;
            this.score = score;

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

            // Set an initial cumulative sales of 0
            cumulativeSales = 0;
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
        /// Add to the cumulative sales of the Published Work
        /// </summary>
        public void AddSales(int sales) => cumulativeSales += sales;
    }
}