using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using GhostWriter.Patterns.EventBus;

namespace GhostWriter.WorkCreation.Ideation.Compatibility
{
    public class CompatibilityButton : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            // Verify the button component
            if(button == null )
                button = GetComponent<Button>();

            // Add event listener
            button.onClick.AddListener(OnClick);
        }

        /// <summary>
        /// Handler for clicking the Compatibility button
        /// </summary>
        private void OnClick()
        {
            EventBus<CalculateCompatibility>.Raise(new CalculateCompatibility());
        }
    }
}