using UnityEngine;
using WriterTycoon.WorkCreation.Ideation.About;

namespace WriterTycoon.WorkCreation.UI.Ideation
{
    public class AboutUI : MonoBehaviour
    {
        private AboutManager aboutManager;
        [SerializeField] private AboutInput nameInput;
        [SerializeField] private AboutInput authorInput;
        [SerializeField] private AboutInput descriptionInput;

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
    }
}