using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WriterTycoon.WorkCreation.Rater
{
    [CreateAssetMenu(fileName = "ScoreWeightData", menuName = "Data/Score Weight Data")]
    public class ScoreWeights : ScriptableObject
    {
        [Header("Compatibility")]
        public float TopicGenre;
        public float TopicAudience;

        [Header("Mastery")]
        public float TopicMastery;
        public float GenreMastery;

        [Header("Allocation - Phase One")]
        public AllocationTrait CharacterSheets;
        public AllocationTrait PlotOutline;
        public AllocationTrait WorldDocument;

        [Header("Allocation - Phase Two")]
        public AllocationTrait Dialogue;
        public AllocationTrait Subplots;
        public AllocationTrait Descriptions;

        [Header("Allocation - Phase Three")]
        public AllocationTrait Emotions;
        public AllocationTrait Twists;
        public AllocationTrait Symbolism;
    }
}