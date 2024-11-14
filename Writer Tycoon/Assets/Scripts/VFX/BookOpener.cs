using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using GhostWriter.Patterns.EventBus;

public class BookOpener : MonoBehaviour
{
    private List<ParticleSystem> particleSystems;
    private Light2D bookLight;
    private Animator animator;
    
    [Header("Tweening Variables")]
    [SerializeField] private float targetIntensity;
    [SerializeField] private float intensityDuration;
    private Tween intensityTween;

    private void Awake()
    {
        // Get the particle systems
        particleSystems = GetComponentsInChildren<ParticleSystem>().ToList();
        bookLight = GetComponentInChildren<Light2D>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Spawn the particles
    /// </summary>
    private void SpawnParticles()
    {
        // Exit case - there are no Particle Systems found
        if (particleSystems == null || particleSystems.Count == 0) return;

        // Iterate through each Particle System
        foreach(ParticleSystem system in particleSystems)
        {
            // Play the Particle System
            system.Play();
        }
    }

    private void EnableLight()
    {
        // Exit case - the Light2D is null
        if (bookLight == null) return;

        // Kill the intensity Tween if already set
        intensityTween?.Kill();

        // Tween to the target intensity over the duration
        intensityTween = DOTween.To(
        () => bookLight.intensity,
            x => bookLight.intensity = x,
            targetIntensity,
            intensityDuration
        ).SetEase(Ease.OutExpo);
    }

    public void Open()
    {
        // Spawn the particles
        SpawnParticles();

        // Enable the light
        EnableLight();

        // Activate the main menu
        EventBus<ActivateMainMenu>.Raise(new ActivateMainMenu()
        {
            Animator = animator
        });
    }
}
