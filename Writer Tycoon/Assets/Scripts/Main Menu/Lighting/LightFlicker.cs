using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GhostWriter.MainMenu.Lighting
{
    public class LightFlicker : MonoBehaviour
    {
        private Light2D lightToFlicker;

        private Tween intensityTween;
        [SerializeField] private float minIntensity;
        [SerializeField] private float maxIntensity;
        [SerializeField] private float flickerDuration = 0.2f;

        private void Awake()
        {
            // Get components
            lightToFlicker = GetComponent<Light2D>();
        }

        private void Start()
        {
            // Begin flickering the light
            StartFlicker();
        }

        /// <summary>
        /// Start to flicker the light
        /// </summary>
        private void StartFlicker()
        {
            // Exit case - the Light is null
            if (lightToFlicker == null) return;

            // Flicker the light
            Flicker();
        }


        /// <summary>
        /// Tween the Light2D's intensity to flicker in between values
        /// </summary>
        private void Flicker()
        {
            // Kill the intensity Tween if already set
            intensityTween?.Kill();

            // Get a random intensity within the defined range
            float targetIntensity = Random.Range(minIntensity, maxIntensity);

            // Tween to the target intensity over the duration
            intensityTween = DOTween.To(
                () => lightToFlicker.intensity, 
                x => lightToFlicker.intensity = x, 
                targetIntensity, 
                flickerDuration
            );

            // Flick again on completion
            intensityTween.OnComplete(Flicker);
        }
    }
}