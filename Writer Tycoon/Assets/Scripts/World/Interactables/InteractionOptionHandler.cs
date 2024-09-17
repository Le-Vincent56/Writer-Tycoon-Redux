using UnityEngine;
using WriterTycoon.Patterns.Command;

namespace WriterTycoon.World.Interactables.UI
{
    public class InteractionOptionHandler : MonoBehaviour
    {
        [SerializeField] private MenuActionButton newWorkButton;
        [SerializeField] private MenuActionButton continueWorkButton;
        [SerializeField] private MenuActionButton eatButton;

        private void Awake()
        {
            // Create a standard command factory for menu actions
            StandardMenuActionCommandFactory commandFactory = new StandardMenuActionCommandFactory();

            newWorkButton.Initialize(commandFactory.CreateMenuActionCommand("New Work"));
            continueWorkButton.Initialize(commandFactory.CreateMenuActionCommand("Continue Work"));
            eatButton.Initialize(commandFactory.CreateMenuActionCommand("Eat"));
        }
    }
}
