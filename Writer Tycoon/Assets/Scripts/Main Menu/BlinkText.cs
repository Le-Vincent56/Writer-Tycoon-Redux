using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GhostWriter.MainMenu
{
    public class BlinkText : MonoBehaviour
    {
        private Text textToBlink;

        private bool upBlink;

        [Header("Tweening Values")]
        [SerializeField] private float minOpacity;
        [SerializeField] private float maxOpacity;
        [SerializeField] private float blinkDuration;
        private Tween blinkTween;

        private void Awake()
        {
            // Get components
            textToBlink = GetComponent<Text>();
        }

        private void Start()
        {
            // Start the text blink
            StartBlink();
        }

        /// <summary>
        /// Start the text blink
        /// </summary>
        private void StartBlink()
        {
            // Exit case - the Text to blink is not set
            if (textToBlink == null) return;

            // Start the blink
            Blink();
        }

        /// <summary>
        /// Tween the opacity of the text repeatedly
        /// </summary>
        private void Blink()
        {
            // Kill the blink tween if it exists
            blinkTween?.Kill();

            // Check the direction to blink
            if(upBlink)
                // If upblink, then fade to the maximum opacity
                blinkTween = textToBlink.DOFade(maxOpacity, blinkDuration);
            else
                // Otherwise, fade to the minimum opacity
                blinkTween = textToBlink.DOFade(minOpacity, blinkDuration);

            blinkTween.onComplete += () =>
            {
                // Change the direction to blink
                upBlink = !upBlink;

                // Call the function again
                Blink();
            };
        }
    }
}
