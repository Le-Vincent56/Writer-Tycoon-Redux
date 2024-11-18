using GhostWriter.Entities.Player.Traits;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GhostWriter.MainMenu
{
    public class TraitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private TraitPicker traitPicker;
        private Text nameText;
        private Button button;
        private Trait trait;
        private TraitTooltip tooltip;

        public void Initialize(TraitPicker traitPicker, TraitTooltip tooltip, Trait trait)
        {
            // Get components
            nameText = GetComponentInChildren<Text>();
            button = GetComponent<Button>();

            // Set variables
            this.traitPicker = traitPicker;
            this.tooltip = tooltip;
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
            Debug.Log($"Trait: {trait.Name}, {trait.Flavor}");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tooltip.ShowTooltip(trait.Flavor, trait.Effects);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltip.HideTooltip();
        }
    }
}
