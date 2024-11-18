using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GhostWriter.MainMenu
{
    public class TraitTooltip : MonoBehaviour
    {
        [Header("UI Components"), Space()]
        [SerializeField] private Canvas canvas;
        private CanvasGroup canvasGroup;
        [SerializeField] private Text flavorText;
        [SerializeField] private Text effectText;

        [Header("Colors"), Space()]
        [SerializeField] private Color positiveEffectColor;
        [SerializeField] private Color negativeEffectColor;
        private string richPositiveColor;
        private string richNegativeColor;

        [Header("Tweening Variables")]
        [SerializeField] private float fadeDuration;
        private Tween fadeTween;

        private void Awake()
        {
            // Get components
            canvasGroup = GetComponent<CanvasGroup>();

            // Set hexadecimal colors
            richPositiveColor = ColorUtility.ToHtmlStringRGBA(positiveEffectColor);
            richNegativeColor = ColorUtility.ToHtmlStringRGBA(negativeEffectColor);
        }

        /// <summary>
        /// Show the Trait Tooltip
        /// </summary>
        public void ShowTooltip(string flavor, string[] effects)
        {
            // Get the mouse position
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            // Convert screen space to world space for the Canvas
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, canvas.planeDistance));

            // Add padding
            Vector3 positionWithPadding = new Vector3(worldPosition.x + 0.1f, worldPosition.y + 0.1f, worldPosition.z);

            transform.position = positionWithPadding;

            // Set the flavor text
            flavorText.text = flavor;

            // Create the effect text
            string effectTextComplete = "";

            // Iterate through each effect
            for(int i = 0; i < effects.Length; i++)
            {
                // Reference delimiters
                switch (effects[i][0])
                {
                    // Use the positive color
                    case '+':
                        effectTextComplete += $"<color=#{richPositiveColor}>{effects[i].Substring(1)}</color>";
                        break;

                    // Use the negative color
                    case '-':
                        effectTextComplete += $"<color=#{richNegativeColor}>{effects[i].Substring(1)}</color>";
                        break;

                    // Use no colors
                    default:
                        effectTextComplete += $"{effects[i]}";
                        break;
                }

                // Check if the index is anything but the second to last index
                if(i != effects.Length - 1)
                {
                    // Add spacing
                    effectTextComplete += "\n";
                }
            }

            // Set the text
            effectText.text = effectTextComplete;

            // Fade in
            Fade(1f, fadeDuration, () =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
        }

        /// <summary>
        /// Hide the Trait Tooltip
        /// </summary>
        public void HideTooltip()
        {
            // Fade out
            Fade(0f, fadeDuration, () =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
        }

        /// <summary>
        /// Handle fading for the Trait Tooltip
        /// </summary>
        private void Fade(float endValue, float duration, TweenCallback onComplete = null)
        {
            // Kill any existing fade tween
            fadeTween?.Kill();

            // Set the fade
            fadeTween = canvasGroup.DOFade(endValue, duration);

            // Exit case - there's no completion action
            if (onComplete == null) return;

            // Hook up completion actions
            fadeTween.onComplete += onComplete;
        }
    }
}
