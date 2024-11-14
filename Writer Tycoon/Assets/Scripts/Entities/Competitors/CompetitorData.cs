using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using GhostWriter.WorkCreation.Ideation.Genres;
using GhostWriter.WorkCreation.Ideation.Topics;
using GhostWriter.WorkCreation.Ideation.WorkTypes;

namespace GhostWriter.Entities.Competitors
{
    [CreateAssetMenu(fileName = "Competitor Data", menuName = "Data/Competitor")]
    public class CompetitorData : SerializedScriptableObject
    {
        [SerializeField] public string competitorName;
        [SerializeField] public int startingMoney;
        [SerializeField] public bool learned;
        [SerializeField] public float learningFactor;
        [SerializeField] public float discountFactor;
        [SerializeField] public float explorationFactor;
        [SerializeField] public WorkType workType;
        [SerializeField] public HashSet<TopicType> topics = new();
        [SerializeField] public HashSet<GenreType> genres = new();
    }
}