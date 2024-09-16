using UnityEngine;

namespace WriterTycoon.Patterns.EventBus
{
    public struct ChangePlayerWorkState : IEvent
    {
        public bool Working;
    }

    public struct CommandPlayerPosition : IEvent
    {
        public Vector2Int TargetPosition;
    }
}
