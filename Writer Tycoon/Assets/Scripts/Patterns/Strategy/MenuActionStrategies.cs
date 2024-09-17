using UnityEngine;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.Patterns.Strategy
{
    public interface IMenuActionStrategy
    {
        void PerformAction();
    }

    public class NewWorkAction : IMenuActionStrategy
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

            // Open the Create Work menu
            EventBus<OpenCreateWorkMenu>.Raise(new OpenCreateWorkMenu()
            {
                IsOpening = true
            });
        }
    }

    public class ContinueWorkAction : IMenuActionStrategy
    {
        public void PerformAction()
        {
            // Close the interact menus
            EventBus<CloseInteractMenus>.Raise(new CloseInteractMenus());

            // Set the player to not eating
            EventBus<ChangePlayerEatState>.Raise(new ChangePlayerEatState()
            {
                Eating = false
            });

            // Change the player to its work state
            EventBus<ChangePlayerWorkState>.Raise(new ChangePlayerWorkState()
            {
                Working = true
            });

            // Move the player (on top of the chair)
            EventBus<CommandPlayerPosition>.Raise(new CommandPlayerPosition()
            {
                TargetPosition = new Vector2Int(9, 5)
            });
        }
    }

    public class EatAction : IMenuActionStrategy
    {
        public void PerformAction()
        {
            // Close the interact menus
            EventBus<CloseInteractMenus>.Raise(new CloseInteractMenus());

            // Set the player to not working
            EventBus<ChangePlayerWorkState>.Raise(new ChangePlayerWorkState()
            {
                Working = false
            });

            // Set the player to eating
            EventBus<ChangePlayerEatState>.Raise(new ChangePlayerEatState()
            {
                Eating = true
            });

            // Move the player (in front of the fridge)
            EventBus<CommandPlayerPosition>.Raise(new CommandPlayerPosition()
            {
                TargetPosition = new Vector2Int(-13, 5)
            });
        }
    }
}