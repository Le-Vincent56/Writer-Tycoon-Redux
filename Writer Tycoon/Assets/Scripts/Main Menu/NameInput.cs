using GhostWriter.Patterns.EventBus;
using UnityEngine;
using UnityEngine.UI;

namespace GhostWriter
{
    public class NameInput : MonoBehaviour
    {
        private InputField inputField;

        private void Awake()
        {
            // Get components
            inputField = GetComponent<InputField>();

            // Hook up events
            inputField.onValueChanged.AddListener(OnValueChanged);
        }

        /// <summary>
        /// Display the name when the Input Field's value is changed
        /// </summary>
        private void OnValueChanged(string value)
        {
            EventBus<DisplayName>.Raise(new DisplayName() { Name = value });
        }
    }
}
