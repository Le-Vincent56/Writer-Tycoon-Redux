using GhostWriter.Patterns.EventBus;
using GhostWriter.World.GeneralUI;
using UnityEngine;

namespace GhostWriter.Entities.Competitors.UI
{
    public class CompetitorWindowToggle : CodeableButton
    {
        [SerializeField] private bool open;

        private EventBinding<OpenCompetitorWindow> openCompetitorWindowEvent;
        private EventBinding<CloseCompetitorWindow> closeCompetitorWindowEvent;

        protected override void Awake()
        {
            // Call the base Awake
            base.Awake();

            // Set to initially open
            open = true;
        }

        private void OnEnable()
        {
            openCompetitorWindowEvent = new EventBinding<OpenCompetitorWindow>(ToggleToOpen);
            EventBus<OpenCompetitorWindow>.Register(openCompetitorWindowEvent);

            closeCompetitorWindowEvent = new EventBinding<CloseCompetitorWindow>(ToggleToClose);
            EventBus<CloseCompetitorWindow>.Register(closeCompetitorWindowEvent);
        }


        private void OnDisable()
        {
            EventBus<OpenCompetitorWindow>.Deregister(openCompetitorWindowEvent);
            EventBus<CloseCompetitorWindow>.Deregister(closeCompetitorWindowEvent);
        }

        private void ToggleToOpen() => open = false;

        private void ToggleToClose() => open = true;

        /// <summary>
        /// Handle button clicking by toggling the Competitor Window
        /// </summary>
        protected override void OnClick()
        {
            // Check if to open
            if (open)
                // Open the Competitor Window
                EventBus<OpenCompetitorWindow>.Raise(new OpenCompetitorWindow());
            else
                // Otherwise, close the Competitor Window
                EventBus<CloseCompetitorWindow>.Raise(new CloseCompetitorWindow());
        }
    }
}
