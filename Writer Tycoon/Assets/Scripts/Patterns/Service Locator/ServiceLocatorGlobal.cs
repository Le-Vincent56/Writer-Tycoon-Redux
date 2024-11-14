using UnityEngine;

namespace GhostWriter.Patterns.ServiceLocator
{
    [AddComponentMenu("ServiceLocator/Global ServiceLocator")]
    public class ServiceLocatorGlobal : Bootstrapper
    {
        [SerializeField] private bool dontDestroyOnLoad = true;

        /// <summary>
        /// Bootstrap by configuring the Service Locator for global use
        /// </summary>
        protected override void Bootstrap() => Container.ConfigureAsGlobal(dontDestroyOnLoad);
    }
}