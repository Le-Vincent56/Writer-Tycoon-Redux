using UnityEngine;
using GhostWriter.Entities.Player;
using GhostWriter.Patterns.EventBus;

namespace GhostWriter.Patterns.Strategy
{
    public interface IMenuActionStrategy
    {
        void PerformAction();
    }

    public class PublicationHistoryAction : IMenuActionStrategy
    {
        public void PerformAction()
        {
            // Close the interact menus
            EventBus<CloseInteractMenus>.Raise(new CloseInteractMenus());

            // Pause the Calendar
            EventBus<ChangeCalendarPauseState>.Raise(new ChangeCalendarPauseState()
            {
                Paused = true,
                AllowSpeedChanges = false
            });

            // Move the player (in front of the fridge)
            EventBus<CommandPlayerPosition>.Raise(new CommandPlayerPosition()
            {
                TargetPosition = new Vector2Int(-14, 7),
                Type = CommandType.BookShelf
            });
        }
    }

    public class NewWorkAction : IMenuActionStrategy
    {
        public void PerformAction()
        {
            // Open the Create Work menu
            EventBus<OpenCreateWorkMenu>.Raise(new OpenCreateWorkMenu());
        }
    }

    public class ContinueWorkAction : IMenuActionStrategy
    {
        public void PerformAction()
        {
            // Close the interact menus
            EventBus<CloseInteractMenus>.Raise(new CloseInteractMenus());

            // Move the player (on top of the chair)
            EventBus<CommandPlayerPosition>.Raise(new CommandPlayerPosition()
            {
                TargetPosition = new Vector2Int(9, 3),
                Type = CommandType.Computer
            });
        }
    }

    public class EatAction : IMenuActionStrategy
    {
        public void PerformAction()
        {
            // Close the interact menus
            EventBus<CloseInteractMenus>.Raise(new CloseInteractMenus());

            // Move the player (in front of the fridge)
            EventBus<CommandPlayerPosition>.Raise(new CommandPlayerPosition()
            {
                TargetPosition = new Vector2Int(-13, 5),
                Type = CommandType.Fridge
            });
        }
    }
}