using UnityEngine;
using WriterTycoon.Entities.Player;

namespace WriterTycoon.Patterns.EventBus
{
    public struct ChangePlayerWorkState : IEvent
    {
        public bool Working;
    }

    public struct ChangePlayerEatState : IEvent
    {
        public bool Eating;
    }

    public struct ConfirmPlayerWorkState : IEvent
    {
        public bool Working;
    }

    public struct ConfirmPlayerEatState : IEvent
    {
        public bool Eating;
    }

    public struct CommandPlayerPosition : IEvent
    {
        public Vector2Int TargetPosition;
        public CommandType Type;
    }

    
}
