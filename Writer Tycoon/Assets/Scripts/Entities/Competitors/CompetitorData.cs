using UnityEngine;

namespace WriterTycoon.Entities.Competitors
{
    [CreateAssetMenu(fileName = "Competitor Data", menuName = "Data/Competitor")]
    public class CompetitorData : ScriptableObject
    {
        [SerializeField] public string competitorName;
        [SerializeField] public int startingMoney;
        [SerializeField] public bool learned;
        [SerializeField] public float learningQ;
    }
}