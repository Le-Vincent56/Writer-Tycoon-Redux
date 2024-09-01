using UnityEngine;
using WriterTycoon.GameCreation.Mediator;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.Patterns.ServiceLocator;

namespace WriterTycoon.GameCreation.Mediation
{
    public class DedicantMediator : Mediator<Dedicant>
    {
        private void Awake()
        {
            // Register to the global service locator
            ServiceLocator.ForSceneOf(this).Register(this as Mediator<Dedicant>);
        }

        protected override void OnRegistered(Dedicant component)
        {
            Debug.Log($"{component.Name} registered");
            Broadcast(component, new DedicantPayload { Source = component, Content = "Registered" });
        }

        protected override void OnDeregistered(Dedicant component)
        {
            Debug.Log($"{component.Name} deregistered");
            Broadcast(component, new DedicantPayload { Source = component, Content = "Registered" });
        }

        protected override bool MediatorConditionMet(Dedicant target) => true;
    }
}