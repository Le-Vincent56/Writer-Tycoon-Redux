using GhostWriter.Entities.Player.Traits;
using System.Collections.Generic;
using UnityEngine;

namespace GhostWriter.Patterns.EventBus
{
    public struct ActivateMainMenu : IEvent 
    {
        public Animator Animator;
    }

    public struct DisplayName : IEvent
    {
        public string Name;
    }

    public struct ListTraits : IEvent
    {
        public List<Trait> Traits;
    }
}
