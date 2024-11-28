using DG.Tweening;
using GhostWriter.Patterns.EventBus;
using GhostWriter.World.GeneralUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GhostWriter.MainMenu
{
    public class StartGameButton : CodeableButton
    {
        [SerializeField] private bool canStartGame;

        private EventBinding<DisplayName> displayNameEvent;

        protected override void Awake()
        {
            // Call the base Awake
            base.Awake();

            // Disable the Start Button
            button.interactable = false;
        }

        private void OnEnable()
        {
            displayNameEvent = new EventBinding<DisplayName>(CheckStartability);
            EventBus<DisplayName>.Register(displayNameEvent);
        }

        private void OnDisable()
        {
            EventBus<DisplayName>.Deregister(displayNameEvent);
        }

        /// <summary>
        /// Handle the function of the Start Button
        /// </summary>
        protected override void OnClick()
        {
            // TODO: Save traits

            // Kill all Tweens
            DOTween.KillAll();

            // Change the scene
            SceneManager.LoadScene(1);
        }

        /// <summary>
        /// Check if the Start Button can be clicked
        /// </summary>
        /// <param name="eventData"></param>
        private void CheckStartability(DisplayName eventData)
        {
            // Set whether or not the button is enabled based on if a name is given
            button.interactable = eventData.Name != "" && eventData.Name != string.Empty;
        }
    }
}
