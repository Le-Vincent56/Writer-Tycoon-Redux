using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.WorkCreation.UI.Publication
{
    public class PublicationBackButton : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            // Verify the Button component
            if(button == null) 
                button = GetComponent<Button>();

            // Add OnClick listener
            button.onClick.AddListener(OnClick);
        }
        
        /// <summary>
        // Set the Publication History Window state to 0 (Shelf)
        /// </summary>
        private void OnClick()
        {
            // Set the Shelf state
            EventBus<SetPublicationHistoryState>.Raise(new SetPublicationHistoryState()
            {
                State = 0
            });
        }
    }
}