using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GhostWriter.WorkCreation.Ideation.About
{
    public class AboutInput : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        public enum AboutType
        {
            Title,
            Author,
            Description
        }

        private AboutManager aboutManager;
        private InputField inputField;
        [SerializeField] private AboutType aboutInfo;

        /// <summary>
        /// Instantiate the About Input
        /// </summary>
        /// <param name="aboutManager"></param>
        public void Instantiate(AboutManager aboutManager)
        {
            // Verify the Input Field
            if(inputField == null)
                inputField = GetComponent<InputField>();

            // Set variables
            this.aboutManager = aboutManager;

            // Add the event listener
            inputField.onValueChanged.AddListener(OnValueChanged);
        }

        /// <summary>
        /// Callback function for when the About Info has changed
        /// </summary>
        private void OnValueChanged(string value)
        {
            switch (aboutInfo)
            {
                case AboutType.Title:
                    aboutManager.UpdateTitleValue(value);
                    break;

                case AboutType.Author:
                    aboutManager.UpdateAuthorValue(value);
                    break;

                case AboutType.Description:
                    aboutManager.UpdateDescriptionValue(value);
                    break;
            }
        }

        /// <summary>
        /// Clear the Input Field
        /// </summary>
        public void Clear()
        {
            inputField.text = string.Empty;
        }

        /// <summary>
        /// Handle when the Input Field is initially focused
        /// </summary>
        public void OnSelect(BaseEventData eventData) => aboutManager.OnSelected();

        /// <summary>
        /// Handle when the Input Field is initially unfocused
        /// </summary>
        public void OnDeselect(BaseEventData eventData) => aboutManager.OnDeselected();
    }
}