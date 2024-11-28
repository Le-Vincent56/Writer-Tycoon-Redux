using GhostWriter.Patterns.EventBus;
using UnityEngine;
using UnityEngine.UI;

namespace GhostWriter.Entities.Competitors.UI
{
    public class CompetitorWindowToggle : MonoBehaviour
    {
        [SerializeField] private bool open;
        private Button button;

        private void Awake()
        {
            // Get components
            button = GetComponent<Button>();

            // Hook up click actions
            button.onClick.AddListener(OnClick);

            // Set to initially open
            open = true;
        }

        /// <summary>
        /// Handle button clicking by toggling the Competitor Window
        /// </summary>
        private void OnClick()
        {
            // Check if to open
            if (open)
                // Open the Competitor Window
                EventBus<OpenCompetitorWindow>.Raise(new OpenCompetitorWindow());
            else
                // Otherwise, close the Competitor Window
                EventBus<CloseCompetitorWindow>.Raise(new CloseCompetitorWindow());

            // Toggle whether to open
            open = !open;
        }
    }
}
