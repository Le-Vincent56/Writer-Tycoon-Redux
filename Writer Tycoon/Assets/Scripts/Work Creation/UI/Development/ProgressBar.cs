using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.WorkCreation.UI.Development
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private float lastFillAmount;
        [SerializeField] private float currentFillAmount;
        [SerializeField] private float timeToReachTarget;
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

        private void Update()
        {
            // Dynamically calculate the speed to reach the current target within a fixed time
            float dynamicFillSpeed = Mathf.Abs(currentFillAmount - lastFillAmount) / timeToReachTarget;

            // Move the last fill amount smoothly toward the current fill amount
            lastFillAmount = Mathf.MoveTowards(lastFillAmount, currentFillAmount, dynamicFillSpeed * Time.deltaTime);

            // Update the mask to visually reflect the progress
            mask.fillAmount = lastFillAmount;
        }

        private void GetCurrentFill(UpdateProgressData eventData)
        {
            // Calculate the target fill amount based on event data
            float newFillAmount = (float)eventData.Current / (float)eventData.Maximum;

            // Update the current fill target, clamping it between 0 and 1
            currentFillAmount = Mathf.Clamp01(newFillAmount);
        }
    }
}