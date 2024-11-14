using UnityEngine;
using UnityEngine.UI;

namespace GhostWriter.WorkCreation.UI.Development
{
    public class ProgressTitle : MonoBehaviour
    {
        private Text titleText;

        /// <summary>
        /// Initialize the progress title
        /// </summary>
        public void Initialize(string title)
        {
            // Verify the Text component
            if (titleText == null)
                titleText = GetComponent<Text>();

            titleText.text = title;
        }
    }
}