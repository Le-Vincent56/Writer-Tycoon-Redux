using GhostWriter.Entities.Player.Traits;
using UnityEngine;
using UnityEngine.UI;

namespace GhostWriter.MainMenu
{
    public class TraitButton : MonoBehaviour
    {
        private TraitPicker traitPicker;
        private Text nameText;
        private Button button;
        private Trait trait;

        public void Initialize(TraitPicker traitPicker, Trait trait)
        {
            // Get components
            nameText = GetComponentInChildren<Text>();
            button = GetComponent<Button>();

            // Set variables
            this.traitPicker = traitPicker;
            this.trait = trait;

            // Set the name text
            nameText.text = trait.Name;

            // Add the onClick listener
            button.onClick.AddListener(OnClick);
        }
        
        /// <summary>
        /// Handle the Trait Button being clicked
        /// </summary>
        private void OnClick()
        {
            Debug.Log($"Trait: {trait.Name}, {trait.Description}");
        }
    }
}
