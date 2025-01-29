using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using GhostWriter.Entities.Tracker;
using GhostWriter.Patterns.EventBus;
using GhostWriter.Patterns.ServiceLocator;

namespace GhostWriter.World.Economy
{
    public class PlayerBankDisplay : MonoBehaviour
    {
        private CompetitorRecord competitorRecord;
        private Text displayText;
        private BankPopup bankPopup;

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

            // Verofy tje Bank Popup component
            if(bankPopup == null)
                bankPopup = GetComponentInChildren<BankPopup>();

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

            // choose the display color
            Color displayColor = eventData.Change > 0 
                ? incomeColor 
                : eventData.Change < 0 
                    ? debtColor 
                    : normalColor;

            // Change the color of the text
            ChangeColor(displayColor, animationDuration);

            // Activate the bank popup
            bankPopup.Popup(displayColor, eventData.Change);
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