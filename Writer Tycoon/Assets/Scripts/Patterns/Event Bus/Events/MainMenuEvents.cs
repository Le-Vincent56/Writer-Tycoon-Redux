using UnityEngine;

namespace WriterTycoon.Patterns.EventBus
{
    public struct ActivateMainMenu : IEvent 
    {
        public Animator Animator;
    }
}
