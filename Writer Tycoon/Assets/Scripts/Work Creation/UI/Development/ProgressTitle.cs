using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.WorkCreation.UI.Development
{
    public class ProgressTitle : MonoBehaviour
    {
        private Text titleText;
        private EventBinding<SetProgressCardTitle> setProgressCardTitleEvent;

        private void Awake()
        {
            // Verify the Text component
            if(titleText == null)
                titleText = GetComponent<Text>();
        }

        private void OnEnable()
        {
            setProgressCardTitleEvent = new EventBinding<SetProgressCardTitle>(SetCardTitle);
            EventBus<SetProgressCardTitle>.Register(setProgressCardTitleEvent);
        }

        private void OnDisable()
        {
            EventBus<SetProgressCardTitle>.Deregister(setProgressCardTitleEvent);
        }

        /// <summary>
        /// Callback function to set the card title
        /// </summary>
        private void SetCardTitle(SetProgressCardTitle eventData)
        {
            titleText.text = eventData.Title;
        }
    }
}