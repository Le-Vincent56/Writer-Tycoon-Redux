using UnityEngine;

namespace GhostWriter.Patterns.ServiceLocator
{
    [AddComponentMenu("ServiceLocator/Scene ServiceLocator")]
    public class ServiceLocatorScene : Bootstrapper
    {
        /// <summary>
        /// Bootstrap by configuring the Service Locator for scene use
        /// </summary>
        protected override void Bootstrap() => Container.ConfigureForScene();
    }
}