using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.WorkCreation.UI.Development
{
    public class ProgressBar : MonoBehaviour
    {
        public Image mask;

        private EventBinding<UpdateProgressData> updateProgressDataEvent;

        private void Awake()
        {
            // Verify the Image component
            if(mask == null)
                mask = GetComponent<Image>();
        }

        private void OnEnable()
        {
            updateProgressDataEvent = new EventBinding<UpdateProgressData>(GetCurrentFill);
            EventBus<UpdateProgressData>.Register(updateProgressDataEvent);
        }

        private void OnDisable()
        {
            EventBus<UpdateProgressData>.Deregister(updateProgressDataEvent);
        }

        private void GetCurrentFill(UpdateProgressData eventData)
        {
            float fillAmount = (float)eventData.Current / (float)eventData.Maximum;
            mask.fillAmount = fillAmount;

            Debug.Log(fillAmount);
        }
    }
}