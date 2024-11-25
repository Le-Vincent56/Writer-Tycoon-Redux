using GhostWriter.Entities.Player.Traits;
using GhostWriter.Patterns.EventBus;
using System.Collections.Generic;
using UnityEngine;

namespace GhostWriter.MainMenu
{
    public class TraitPicker : MonoBehaviour
    {
        [SerializeField] private GameObject traitPrefab;
        [SerializeField] private TraitTooltip tooltip;
        private PlayerTraits playerTraits;

        [SerializeField] private int maxSelectedTraits;
        private List<Trait> selectedTraits;

        private void Awake()
        {
            // Instantiate the Player Traits
            playerTraits = new();

            // Instantiate the selected Traits list
            selectedTraits = new();

            // Create Trait Buttons
            CreateTraitButtons();
        }

        /// <summary>
        /// Create the Trait Buttons
        /// </summary>
        private void CreateTraitButtons()
        {
            // Iterate through each Player trait
            foreach(Trait trait in playerTraits.GetTraits())
            {
                // Instantiate the Trait object
                GameObject traitObj = Instantiate(traitPrefab, transform);

                // Initialize it's Trait Button
                traitObj.GetComponent<TraitButton>().Initialize(this, tooltip, trait);
            }
        }

        /// <summary>
        /// Select a Trait from a Trait Button
        /// </summary>
        public void SelectTrait(TraitButton traitButton)
        {
            // Exit case - if already reached the maximum amount of selected traits and the list
            // doesn't already contain the selected trait
            if (selectedTraits.Count >= maxSelectedTraits && !selectedTraits.Contains(traitButton.Trait)) return;

            // Check if the Trait is already selected
            if(selectedTraits.Contains(traitButton.Trait))
            {
                // Remove the Trait from the selected Traits List
                selectedTraits.Remove(traitButton.Trait);

                // Deselect the button
                traitButton.Deselect();

                // List the selected traits
                EventBus<ListTraits>.Raise(new ListTraits() { Traits = selectedTraits });

                return;
            }

            // Add the selected Trait
            selectedTraits.Add(traitButton.Trait);

            // List the selected traits
            EventBus<ListTraits>.Raise(new ListTraits() { Traits =  selectedTraits });

            // Select the button
            traitButton.Select();
        }
    }
}
