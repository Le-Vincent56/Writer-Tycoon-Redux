using System.Collections.Generic;

namespace GhostWriter.Entities.Player.Traits
{
    public enum Attribute
    {
        HypeGenerationMult,
        PreProductionSpeed,
        ProductionSpeed,
        PostProductionSpeed,
        ErrorGenerationMult,
        HungerSpeed,
        BathroomSpeed,
        RentMult,
        SalesWeeksMult,
        SalesMult
    }

    public class TraitBuilder
    {
        private string name;
        private string description;
        private Dictionary<Attribute, float> attributes = new();

        /// <summary>
        /// Add a name to the Trait
        /// </summary>
        public TraitBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        /// <summary>
        /// Add a description to the Trait
        /// </summary>
        public TraitBuilder WithDescription(string description)
        {
            this.description = description; 
            return this;
        }

        /// <summary>
        /// Add a change in the Entity's hype multiplier
        /// </summary>
        public TraitBuilder WithHypeMult(float hypeMult)
        {
            attributes.Add(Attribute.HypeGenerationMult, hypeMult);
            return this;
        }

        /// <summary>
        /// Add a change in how fast the Entity works in pre-production
        /// </summary>
        public TraitBuilder WithPreProductionSpeed(float preProductionSpeed)
        {
            attributes.Add(Attribute.PreProductionSpeed, preProductionSpeed);
            return this;
        }

        /// <summary>
        /// Add a change in how fast the Entity works in production
        /// </summary>
        public TraitBuilder WithProductionSpeed(float productionSpeed)
        {
            attributes.Add(Attribute.ProductionSpeed, productionSpeed);
            return this;
        }

        /// <summary>
        /// Add a change in how fast the Entity works in post-production
        /// </summary>
        public TraitBuilder WithPostProductionSpeed(float postProductionSpeed)
        {
            attributes.Add(Attribute.PostProductionSpeed, postProductionSpeed);
            return this;
        }

        /// <summary>
        /// Add a change in the Entity's error multiplier
        /// </summary>
        public TraitBuilder WithErrorMult(float errorMult)
        {
            attributes.Add(Attribute.ErrorGenerationMult, errorMult);
            return this;
        }

        /// <summary>
        /// Add a change in the rate of which the Entity needs to eat
        /// </summary>
        public TraitBuilder WithHungerSpeed(float hungerSpeed)
        {
            attributes.Add(Attribute.HungerSpeed, hungerSpeed);
            return this;
        }

        /// <summary>
        /// Add a change in the rate of which the Entity needs to go to the bathroom
        /// </summary>
        public TraitBuilder WithBathroomSpeed(float bathroomSpeed)
        {
            attributes.Add(Attribute.BathroomSpeed, bathroomSpeed);
            return this;
        }

        /// <summary>
        /// Add a change in the rent multiplier
        /// </summary>
        public TraitBuilder WithRentMult(float rentMult)
        {
            attributes.Add(Attribute.RentMult, rentMult);
            return this;
        }

        /// <summary>
        /// Add a change in the length of weeks a Player's work is on sale
        /// </summary>
        public TraitBuilder WithSalesWeeksMult(float salesWeeksMult)
        {
            attributes.Add(Attribute.SalesWeeksMult, salesWeeksMult);
            return this;
        }

        /// <summary>
        /// Add a change in the sales multiplier
        /// </summary>
        public TraitBuilder WithSalesMult(float salesMult)
        {
            attributes.Add(Attribute.SalesMult, salesMult);
            return this;
        }

        /// <summary>
        /// Build the Trait
        /// </summary>
        public Trait Build()
        {
            // Built the Trait
            Trait builtTrait = new Trait(name, description, attributes);

            // Clear the attributes for further building
            attributes.Clear();

            return builtTrait;
        }
    }
}
