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
            // Change the player to its work state
            EventBus<ChangePlayerWorkState>.Raise(new ChangePlayerWorkState()
            {
                Working = true
            });
        }
    }

    public class EatAction : IMenuActionStrategy
    {
        public void PerformAction()
        {

        }
    }
}