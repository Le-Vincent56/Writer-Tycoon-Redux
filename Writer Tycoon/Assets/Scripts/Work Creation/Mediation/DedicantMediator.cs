using UnityEngine;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.Patterns.ServiceLocator;

namespace WriterTycoon.WorkCreation.Mediation
{
    public class DedicantMediator : Mediator<Dedicant>
    {
        [SerializeField] private bool debug;

        private void Awake()
        {
            // Register to the global service locator
            ServiceLocator.ForSceneOf(this).Register(this as Mediator<Dedicant>);
        }

        protected override void OnRegistered(Dedicant component)
        {
            if(debug) Debug.Log($"{component.Name} registered");
        }

        protected override void OnDeregistered(Dedicant component)
        {
            if(debug) Debug.Log($"{component.Name} deregistered");
        }

        protected override bool MediatorConditionMet(Dedicant target) => true;
    }
}