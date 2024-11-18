using System.Collections.Generic;

namespace GhostWriter.Entities.Player.Traits
{
    public class PlayerTraits
    {
        private readonly List<Trait> allTraits;

        public PlayerTraits()
        {
            // Instantaite the List
            allTraits = new();

            // Create a TraitBuilder
            TraitBuilder builder = new();

            // Plotter
            Trait plotter = builder
                .WithName("Plotter")
                .WithDescription(
                    "The plot comes first, writing comes later. Way later.",
                    new string[1]
                    {
                        "+1.25x Pre-Production Speed"
                    }
                )
                .WithPreProductionSpeed(1.25f)
                .Build();
            allTraits.Add(plotter);

            // Pantser
            Trait pantser = builder
                .WithName("Pantser")
                .WithDescription(
                    "You'll finish your first draft before you have any idea what the plot is.",
                    new string[1]
                    {
                        "+1.25x Production Speed"
                    }
                )
                .WithProductionSpeed(1.25f)
                .Build();
            allTraits.Add(pantser);

            // Polisher
            Trait polisher = builder
                .WithName("Polisher")
                .WithDescription(
                    "You can polish your book faster than anyone else.",
                    new string[1]
                    {
                        "+1.25x Post-Production Speed"
                    }
                 )
                .WithPostProductionSpeed(1.25f)
                .Build();
            allTraits.Add(polisher);

            // Perfectionist
            Trait perfectionist = builder
                .WithName("Perfectionist")
                .WithDescription(
                    "At the cost of speed in all areas, you make sure there are no mistakes.",
                    new string[4]
                    {
                        "-0.65x Pre-Production Speed",
                        "-0.65x Production Speed",
                        "-0.65x Post-Production Speed",
                        "+No Errors Generated"
                    }
                 )
                .WithPreProductionSpeed(0.65f)
                .WithProductionSpeed(0.65f)
                .WithPostProductionSpeed(0.65f)
                .WithErrorMult(0f)
                .Build();
            allTraits.Add(perfectionist);

            // Anomaly
            Trait anomaly = builder
                .WithName("Anomaly")
                .WithDescription(
                    "You're a biological anomaly: your bodily functions take a backseat to your work.",
                    new string[2]
                    {
                        "+0.8x Hunger Reduction Speed",
                        "+0.8x Bathroom Reduction Speed"
                    }
                )
                .WithHungerSpeed(0.8f)
                .WithBathroomSpeed(0.8f)
                .Build();
            allTraits.Add(anomaly);
        }

        public List<Trait> GetTraits() => allTraits;
    }
}
