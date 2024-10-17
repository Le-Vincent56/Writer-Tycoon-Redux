using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Entities.Tracker;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.Patterns.ServiceLocator;

namespace WriterTycoon.World.Economy
{
    public class PlayerBankDisplay : MonoBehaviour
    {
        private CompetitorRecord competitorRecord;
        private Text displayText;

        [SerializeField] private float animationDuration;
        [SerializeField] private Color normalColor;
        [SerializeField] private Color incomeColor;
        [SerializeField] private Color debtColor;
        private Tween colorTween;

        private EventBinding<DisplayBank> updatePlayerIncomeEvent;

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
            updatePlayerIncomeEvent = new EventBinding<DisplayBank>(UpdateDisplayText);
            EventBus<DisplayBank>.Register(updatePlayerIncomeEvent);
        }

        private void OnDisable()
        {
            EventBus<DisplayBank>.Deregister(updatePlayerIncomeEvent);
        }

        private void Start()
        {
            // Get the competitor record to use as a service
            competitorRecord = ServiceLocator.ForSceneOf(this).Get<CompetitorRecord>();
        }

        /// <summary>
        /// Update the text display of the Bank
        /// </summary>
        private void UpdateDisplayText(DisplayBank eventData)
        {
            // Exit case - if not the player
            if (eventData.Competitor != competitorRecord.GetPlayer()) return;

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