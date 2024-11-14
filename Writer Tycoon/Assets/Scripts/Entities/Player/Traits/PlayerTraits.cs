using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GhostWriter.Entities.Player.Traits
{
    public class PlayerTraits
    {
        private List<Trait> allTraits;

        public PlayerTraits()
        {
            // Instantaite the List
            allTraits = new();

            // Create a TraitBuilder
            TraitBuilder builder = new();

            // Add Plotter
            Trait plotter = builder
                .WithName("Plotter")
                .WithPreProductionSpeed(1.25f)
                .Build();
            allTraits.Add(plotter);

            // Add Pantser
            Trait pantser = builder
                .WithName("Pantser")
                .WithProductionSpeed(1.25f)
                .Build();
            allTraits.Add(pantser);

            // Add Fifth-Drafter
            Trait fifthDrafter = builder
                .WithName("Fifth-Drafter")
                .WithPostProductionSpeed(1.25f)
                .Build();
            allTraits.Add(fifthDrafter);

            // Add anomaly
            Trait anomaly = builder
                .WithName("Anomaly")
                .WithHungerSpeed(0.8f)
                .WithBathroomSpeed(0.8f)
                .Build();
            allTraits.Add(anomaly);
        }
    }
}
