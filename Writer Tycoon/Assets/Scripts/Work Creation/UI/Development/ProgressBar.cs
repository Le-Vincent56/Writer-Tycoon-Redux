using UnityEngine;
using UnityEngine.UI;

namespace WriterTycoon.WorkCreation.UI.Development
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private float lastFillAmount;
        [SerializeField] private float currentFillAmount;
        [SerializeField] private float timeToReachTarget;
        public Image mask;

        private void Awake()
        {
            // Verify the Image component
            if(mask == null)
                mask = GetComponent<Image>();
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

        /// <summary>
        /// Set the current fill of the Progress Bar
        /// </summary>
        public void SetCurrentFill(float current, float maximum)
        {
            // Calculate the target fill amount based on event data
            float newFillAmount = current / maximum;

            // Update the current fill target, clamping it between 0 and 1
            currentFillAmount = Mathf.Clamp01(newFillAmount);
        }
    }
}