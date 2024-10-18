using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.WorkCreation.Ideation.Genres;
using WriterTycoon.WorkCreation.Ideation.Topics;

namespace WriterTycoon.Entities.Competitors
{
    [CreateAssetMenu(fileName = "Competitor Data", menuName = "Data/Competitor")]
    public class CompetitorData : SerializedScriptableObject
    {
        [SerializeField] public string competitorName;
        [SerializeField] public int startingMoney;
        [SerializeField] public bool learned;
        [SerializeField] public float learningQ;

        [SerializeField] public HashSet<TopicType> topics = new();
        [SerializeField] public HashSet<GenreType> genres = new();
    }
}