using UnityEngine;
using UnityEngine.UI;
using GhostWriter.Patterns.EventBus;

namespace GhostWriter.WorkCreation.UI.Rating
{
    public class ReviewText : MonoBehaviour
    {
        [SerializeField] private int id;
        private Text reviewText;

        private EventBinding<SetReviewText> setReviewTextEvent;

        private void Awake()
        {
            // Verify the Text component
            if(reviewText == null)
                reviewText = GetComponent<Text>();
        }

        private void OnEnable()
        {
            setReviewTextEvent = new EventBinding<SetReviewText>(SetReviewText);
            EventBus<SetReviewText>.Register(setReviewTextEvent);
        }

        private void OnDisable()
        {
            EventBus<SetReviewText>.Deregister(setReviewTextEvent);
        }

        /// <summary>
        /// Set the review text
        /// </summary>
        private void SetReviewText(SetReviewText eventData)
        {
            // Exit case - if the ID does not match
            if (eventData.ID != id) return;

            // Set the text
            reviewText.text = eventData.Text;
        }
    }
}