using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuParticleSpawner : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> particleSystems;

    private void Awake()
    {
        // Get the particle systems
        particleSystems = GetComponentsInChildren<ParticleSystem>().ToList();
    }

    public void Spawn()
    {
        foreach(ParticleSystem system in particleSystems)
        {
            system.Play();
        }
    }
}
