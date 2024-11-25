using GhostWriter.Patterns.EventBus;
using UnityEngine;
using UnityEngine.UI;

namespace GhostWriter.MainMenu
{
    public class CharacterDisplay : MonoBehaviour
    {
        [SerializeField] private Text nameDisplay;
        [SerializeField] private Text traitsDisplay;

        private EventBinding<DisplayName> displayNameEvent;
        private EventBinding<ListTraits> listTraitsEvent;

        private void OnEnable()
        {
            displayNameEvent = new EventBinding<DisplayName>(DisplayName);
            EventBus<DisplayName>.Register(displayNameEvent);

            listTraitsEvent = new EventBinding<ListTraits>(DisplayTraits);
            EventBus<ListTraits>.Register(listTraitsEvent);
        }

        private void OnDisable()
        {
            EventBus<DisplayName>.Deregister(displayNameEvent);
            EventBus<ListTraits>.Deregister(listTraitsEvent);
        }

        /// <summary>
        /// Display the selected Name
        /// </summary>
        private void DisplayName(DisplayName eventData) => nameDisplay.text = eventData.Name;

        /// <summary>
        /// Display the selected Traits
        /// </summary>
        private void DisplayTraits(ListTraits eventData)
        {
            // Exit case - if the Traits List is not instantiated or there are no Traits
            if(eventData.Traits == null || eventData.Traits.Count == 0)
            {
                // Clear the display
                traitsDisplay.text = string.Empty;

                return;
            }

            // Create a final string container
            string traitsString = "";

            // Iterate through each given Trait
            for(int i = 0; i < eventData.Traits.Count; i++)
            {
                // Add the Trait's name to the string
                traitsString += eventData.Traits[i].Name;

                // Check if less than the second to last Trait
                if (i < eventData.Traits.Count - 1)
                    // Create a new line
                    traitsString += "\n";
            }

            // Set the display string
            traitsDisplay.text = traitsString;
        }
    }
}
