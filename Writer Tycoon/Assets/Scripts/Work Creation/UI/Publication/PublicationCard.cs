using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Publication;

namespace WriterTycoon.WorkCreation.UI.Publication
{
    public class PublicationCard : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("UI References")]
        [SerializeField] private Image image;
        [SerializeField] private Button button;
        [SerializeField] private Text titleText;
        [SerializeField] private Text authorText;

        [Header("Hover Fields")]
        [SerializeField] private Sprite hoverSprite;
        [SerializeField] private float rotationAmount;

        [Header("Interaction Fields")]
        [SerializeField] private bool selected;

        [Header("Data")]
        [SerializeField] private PublishedWork publishedWork;

        /// <summary>
        /// Initialize the Publication Card
        /// </summary>
        public void Initialize(PublishedWork publishedWork)
        {
            // Verify the Image component
            if (image == null)
                image = GetComponent<Image>();

            // Verify the Button component
            if (button == null)
                button = GetComponent<Button>();

            // Set the data
            SetData(publishedWork);

            // Set an onClick listener
            button.onClick.AddListener(SetDisplayData);
        }

        /// <summary>
        /// Set the display data
        /// </summary>
        private void SetDisplayData()
        {
            // Set the data
            EventBus<SetPublicationDisplayData>.Raise(new SetPublicationDisplayData()
            {
                PublishedWork = publishedWork
            });

            // Set to the display state
            EventBus<SetPublicationHistoryState>.Raise(new SetPublicationHistoryState()
            {
                State = 1
            });
        }

        /// <summary>
        /// Set the Publication Card's data
        /// </summary>
        public void SetData(PublishedWork publishedWork) => this.publishedWork = publishedWork;

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Exit case - if selected
            if (selected) return;

            // Set rotations
            SetRotations(rotationAmount);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // Exit case - if selected
            if (selected) return;

            // Set default rotations
            SetRotations(0f);
        }

        public void OnSelect(BaseEventData eventData)
        {
            // Set selected to false
            selected = true;

            // Set default rotations
            SetRotations(0f);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            // Set selected to false
            selected = false;

            // Set default rotations
            SetRotations(0f);
        }

        /// <summary>
        /// Set the rotations of the Title and Author text
        /// </summary>
        private void SetRotations(float rotationAmount)
        {
            titleText.rectTransform.rotation = Quaternion.Euler(0f, 0f, rotationAmount);
            authorText.rectTransform.rotation = Quaternion.Euler(0f, 0f, rotationAmount);
        }
    }
}