using DG.Tweening;
using GhostWriter.Patterns.EventBus;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GhostWriter.MainMenu
{
    public class StartGameButton : MonoBehaviour
    {
        private Button startButton;
        [SerializeField] private bool canStartGame;

        private EventBinding<DisplayName> displayNameEvent;

        private void Awake()
        {
            // Get components
            startButton = GetComponent<Button>();

            // Hook up events
            startButton.onClick.AddListener(OnClick);

            // Disable the Start Button
            startButton.interactable = false;
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
        private void OnClick()
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
            startButton.interactable = eventData.Name != "" && eventData.Name != string.Empty;
        }
    }
}
