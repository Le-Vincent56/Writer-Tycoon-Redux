using UnityEngine;

namespace GhostWriter.Entities.Player.Traits
{
    [CreateAssetMenu(fileName = "Base Player Data", menuName = "Data/Base Player Data")]
    public class BasePlayerData : ScriptableObject
    {
        public string Name;

        [Header("Work")]
        public float HypeGenerationMult;
        public float PreProductionSpeed;
        public float ProductionSpeed;
        public float PostProductionSpeed;
        public float ErrorGenerationMult;

        [Header("Lifestyle")]
        public float HungerSpeed;
        public float BathroomSpeed;

        [Header("Money")]
        public float RentMult;
        public float SalesWeeksMult;
        public float SalesMult;
    }
}