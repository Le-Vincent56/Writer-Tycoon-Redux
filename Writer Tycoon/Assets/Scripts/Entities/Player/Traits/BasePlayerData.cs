using System.Collections.Generic;
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

        public void SetTrait(Trait trait)
        {
            // Iterate through each attribute
            foreach(KeyValuePair<Attribute, float> kvp in trait.Attributes)
            {
                // Check the key
                switch (kvp.Key)
                {
                    // Set the value based on the key
                    case Attribute.HypeGenerationMult:
                        HypeGenerationMult = kvp.Value;
                        break;

                    case Attribute.PreProductionSpeed:
                        PreProductionSpeed = kvp.Value;
                        break;

                    case Attribute.ProductionSpeed:
                        ProductionSpeed = kvp.Value;
                        break;

                    case Attribute.PostProductionSpeed:
                        PostProductionSpeed = kvp.Value;
                        break;

                    case Attribute.ErrorGenerationMult:
                        ErrorGenerationMult = kvp.Value;
                        break;

                    case Attribute.HungerSpeed:
                        HungerSpeed = kvp.Value;
                        break;

                    case Attribute.BathroomSpeed:
                        BathroomSpeed = kvp.Value;
                        break;

                    case Attribute.RentMult:
                        RentMult = kvp.Value;
                        break;

                    case Attribute.SalesWeeksMult:
                        SalesWeeksMult = kvp.Value;
                        break;

                    case Attribute.SalesMult:
                        SalesMult = kvp.Value;
                        break;
                }
            }
        }
    }
}