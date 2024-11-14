using UnityEngine;

namespace GhostWriter.Patterns.EventBus
{
    public struct ActivateMainMenu : IEvent 
    {
        public Animator Animator;
    }
}
