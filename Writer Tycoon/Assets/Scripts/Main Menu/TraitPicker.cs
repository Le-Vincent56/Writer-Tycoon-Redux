using GhostWriter.Entities.Player.Traits;
using System.Collections.Generic;
using UnityEngine;

namespace GhostWriter.MainMenu
{
    public class TraitPicker : MonoBehaviour
    {
        [SerializeField] private GameObject traitPrefab;
        private PlayerTraits playerTraits;

        private List<Trait> selectedTraits;

        private void Awake()
        {
            // Instantiate the Player Traits
            playerTraits = new();

            // Instantiate the selected Traits list
            selectedTraits = new();

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
                traitObj.GetComponent<TraitButton>().Initialize(this, trait);
            }
        }
    }
}
