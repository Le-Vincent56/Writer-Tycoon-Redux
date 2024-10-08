using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.World.Economy
{
    public class BankDisplay : MonoBehaviour
    {
        private Text displayText;

        [SerializeField] private float animationDuration;
        [SerializeField] private Color normalColor;
        [SerializeField] private Color incomeColor;
        [SerializeField] private Color debtColor;
        private Tween colorTween;

        private EventBinding<UpdatePlayerIncome> updatePlayerIncomeEvent;

        private void Awake()
        {
            // Verify the Text component
            if(displayText == null )
                displayText = GetComponent<Text>();

            // Set the normal color
            displayText.color = normalColor;
        }

        private void OnEnable()
        {
            updatePlayerIncomeEvent = new EventBinding<UpdatePlayerIncome>(UpdateDisplayText);
            EventBus<UpdatePlayerIncome>.Register(updatePlayerIncomeEvent);
        }

        private void OnDisable()
        {
            EventBus<UpdatePlayerIncome>.Deregister(updatePlayerIncomeEvent);
        }

        /// <summary>
        /// Update the text display of the Bank
        /// </summary>
        private void UpdateDisplayText(UpdatePlayerIncome eventData)
        {
            // Display the text
            displayText.text = $"{eventData.BankAmount}";

            // Change the color based on the revenue
            if(eventData.Revenue > 0)
            {
                ChangeColor(incomeColor, animationDuration);
            } 
            else if(eventData.Revenue < 0)
            {
                ChangeColor(debtColor, animationDuration);
            }
            else if (eventData.BankAmount < 0)
            {
                ChangeColor(debtColor, animationDuration);
            }
            else if(eventData.Revenue == 0)
            {
                ChangeColor(normalColor, animationDuration);
            }
        }

        /// <summary>
        /// Change the color of the display text
        /// </summary>
        private void ChangeColor(Color endValue, float duration)
        {
            // Kill the current color tween if it exists
            colorTween?.Kill(false);

            // Set the tween animation
            colorTween = displayText.DOColor(endValue, duration).SetEase(Ease.InOutSine);
        }
    }
}