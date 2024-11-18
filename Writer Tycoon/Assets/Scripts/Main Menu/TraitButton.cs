using DG.Tweening;
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

        [Header("Tweening Variables")]
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color unselectedColor;
        [SerializeField] private float colorDuration;
        private Tween colorTween;

        public Trait Trait { get => trait; }

        /// <summary>
        /// Initialize the Trait Button
        /// </summary>
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
        private void OnClick() => traitPicker.SelectTrait(this);

        /// <summary>
        /// Select the Button
        /// </summary>
        public void Select() => ChangeColor(selectedColor, colorDuration);

        /// <summary>
        /// Deselect the Button
        /// </summary>
        public void Deselect() => ChangeColor(unselectedColor, colorDuration);

        /// <summary>
        /// Handle the changing of the Button's color
        /// </summary>
        private void ChangeColor(Color endColor, float duration)
        {
            // Kill the color Tween if it exists already
            colorTween?.Kill();

            colorTween = button.image.DOColor(endColor, duration);
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
