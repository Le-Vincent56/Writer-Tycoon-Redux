using UnityEngine;
using WriterTycoon.Entities.Player;
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
                TargetPosition = new Vector2Int(9, 5),
                Type = CommandType.Computer
            });
        }
    }

    public class PublishWorkAction : IMenuActionStrategy
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

            // End editing
            EventBus<EndEditing>.Raise(new EndEditing());

            // TODO: Send score and rate
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