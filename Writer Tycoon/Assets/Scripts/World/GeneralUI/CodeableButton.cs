using UnityEngine;
using UnityEngine.UI;

namespace GhostWriter.World.GeneralUI
{
    public abstract class CodeableButton : MonoBehaviour
    {
        protected Button button;

        protected virtual void Awake()
        {
            // Get components
            button = GetComponent<Button>();

            // Hook up click actions
            button.onClick.AddListener(OnClick);
        }

        protected abstract void OnClick();
    }
}
