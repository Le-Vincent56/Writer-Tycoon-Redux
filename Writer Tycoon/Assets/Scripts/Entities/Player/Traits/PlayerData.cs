using GhostWriter.Entities.Player.Traits;
using System.Collections.Generic;
using UnityEngine;

namespace GhostWriter
{
    public class PlayerData
    {
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

        public PlayerData(BasePlayerData baseData)
        {
            HypeGenerationMult = baseData.HypeGenerationMult;
            PreProductionSpeed = baseData.PreProductionSpeed;
            ProductionSpeed = baseData.ProductionSpeed;
            PostProductionSpeed = baseData.PostProductionSpeed;
            ErrorGenerationMult = baseData.ErrorGenerationMult;
            HungerSpeed = baseData.HungerSpeed;
            BathroomSpeed = baseData.BathroomSpeed;
            RentMult = baseData.RentMult;
            SalesWeeksMult = baseData.SalesWeeksMult;
            SalesMult = baseData.SalesMult;
        }

        /// <summary>
        /// Set a List of Traits and apply their effects
        /// </summary>
        public void SetTraits(List<Trait> traits)
        {
            Dictionary<Attribute, float> attributesToChange = new();
            Dictionary<Attribute, int> timesAttributeHasChanged = new();

            // Iterate through each given Trait
            foreach(Trait trait in traits)
            {
                // Prepare the Trait
                PrepareTrait(trait, ref attributesToChange, ref timesAttributeHasChanged);
            }

            // Iterate through each attribute
            foreach (KeyValuePair<Attribute, float> kvp in attributesToChange)
            {
                // Check the key
                switch (kvp.Key)
                {
                    // Set the average value based on the key
                    case Attribute.HypeGenerationMult:
                        HypeGenerationMult = kvp.Value / timesAttributeHasChanged[kvp.Key];
                        break;

                    case Attribute.PreProductionSpeed:
                        PreProductionSpeed = kvp.Value / timesAttributeHasChanged[kvp.Key];
                        break;

                    case Attribute.ProductionSpeed:
                        ProductionSpeed = kvp.Value / timesAttributeHasChanged[kvp.Key];
                        break;

                    case Attribute.PostProductionSpeed:
                        PostProductionSpeed = kvp.Value / timesAttributeHasChanged[kvp.Key];
                        break;

                    case Attribute.ErrorGenerationMult:
                        ErrorGenerationMult = kvp.Value / timesAttributeHasChanged[kvp.Key];
                        break;

                    case Attribute.HungerSpeed:
                        HungerSpeed = kvp.Value / timesAttributeHasChanged[kvp.Key];
                        break;

                    case Attribute.BathroomSpeed:
                        BathroomSpeed = kvp.Value / timesAttributeHasChanged[kvp.Key];
                        break;

                    case Attribute.RentMult:
                        RentMult = kvp.Value / timesAttributeHasChanged[kvp.Key];
                        break;

                    case Attribute.SalesWeeksMult:
                        SalesWeeksMult = kvp.Value / timesAttributeHasChanged[kvp.Key];
                        break;

                    case Attribute.SalesMult:
                        SalesMult = kvp.Value / timesAttributeHasChanged[kvp.Key];
                        break;
                }
            }
        }

        /// <summary>
        /// Prepare a Trait to be applied to the Player
        /// </summary>
        private void PrepareTrait(
            Trait trait, 
            ref Dictionary<Attribute, float> attributesToChange, 
            ref Dictionary<Attribute, int> timesAttributeHasChanged
        )
        {
            // Iterate through each attribute
            foreach (KeyValuePair<Attribute, float> kvp in trait.Attributes)
            {
                // Check if the key already exists in the dictionaries
                if (attributesToChange.TryGetValue(kvp.Key, out float hypeGenerationValue) &&
                    timesAttributeHasChanged.TryGetValue(kvp.Key, out int timesHypeChanged))
                {
                    // Increment the times the attribute has changed
                    timesAttributeHasChanged[kvp.Key] = timesHypeChanged++;

                    // Accumulate the values
                    attributesToChange[kvp.Key] = hypeGenerationValue + kvp.Value;
                }
                else
                {
                    // Add the first value and notify that it has changed at least once
                    timesAttributeHasChanged.Add(kvp.Key, 1);
                    attributesToChange.Add(kvp.Key, kvp.Value);
                }
            }
        }
    }
}
