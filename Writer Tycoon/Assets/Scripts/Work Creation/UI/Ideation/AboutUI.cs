using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Ideation.About;

namespace WriterTycoon.WorkCreation.UI.Ideation
{
    public class AboutUI : MonoBehaviour
    {
        private AboutManager aboutManager;
        [SerializeField] private AboutInput nameInput;
        [SerializeField] private AboutInput authorInput;
        [SerializeField] private AboutInput descriptionInput;

        private EventBinding<ClearIdeation> clearIdeationEvent;

        private void Awake()
        {
            // Verify the About Manager
            if(aboutManager == null)
                aboutManager = GetComponent<AboutManager>();

            // Instantiate inputs
            nameInput.Instantiate(aboutManager);
            authorInput.Instantiate(aboutManager);
            descriptionInput.Instantiate(aboutManager);
        }

        private void OnEnable()
        {
            clearIdeationEvent = new EventBinding<ClearIdeation>(ClearInputs);
            EventBus<ClearIdeation>.Register(clearIdeationEvent);
        }

        private void OnDisable()
        {
            EventBus<ClearIdeation>.Deregister(clearIdeationEvent);
        }

        /// <summary>
        /// Callback function to clear all input fields
        /// </summary>
        private void ClearInputs()
        {
            nameInput.Clear();
            authorInput.Clear();
            descriptionInput.Clear();
        }
    }
}