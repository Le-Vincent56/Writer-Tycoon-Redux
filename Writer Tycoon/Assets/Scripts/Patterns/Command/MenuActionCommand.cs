using WriterTycoon.Patterns.Strategy;

namespace WriterTycoon.Patterns.Command
{
    public class MenuActionCommand : ICommand
    {
        private IMenuActionStrategy menuActionStrategy;

        public MenuActionCommand(IMenuActionStrategy menuActionStrategy)
        {
            // Set the strategy
            this.menuActionStrategy = menuActionStrategy;
        }

        /// <summary>
        /// Execute the action
        /// </summary>
        public void Execute()
        {
            menuActionStrategy.PerformAction();
        }
    }

    public abstract class MenuActionCommandFactory
    {
        public abstract MenuActionCommand CreateMenuActionCommand(string commandName);
    }

    public class StandardMenuActionCommandFactory : MenuActionCommandFactory
    {
        public override MenuActionCommand CreateMenuActionCommand(string commandName)
        {
            return commandName switch
            {
                "Publication History" => new MenuActionCommand(new PublicationHistoryAction()),
                "New Work" => new MenuActionCommand(new NewWorkAction()),
                "Continue Work" => new MenuActionCommand(new ContinueWorkAction()),
                "Eat" => new MenuActionCommand(new EatAction()),
                _ => null
            };
        }
    }
}
