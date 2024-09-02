using UnityEngine;

namespace WriterTycoon.WorkCreation.UI.States
{
    public class IdeationState : CreateWorkState
    {
        public IdeationState(GameObject uiObject) : base(uiObject)
        {
        }

        public override async void OnEnter()
        {
            // If the UI object is not active, activate it
            if (!uiObject.activeSelf) uiObject.SetActive(true);

            // Make elements invisible to prepare for the fade
            MakeElementsInvisible();

            // Hide the UI object
            await Show();
        }

        public override async void OnExit()
        {
            // Hide the UI object
            await Hide();

            // If the UI object is active, deactivate it
            if(uiObject.activeSelf) uiObject.SetActive(false);
        }
    }
}