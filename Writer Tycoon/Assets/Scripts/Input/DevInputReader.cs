using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static GhostWriter.Input.GameInputActions;

namespace GhostWriter.Input
{
    [CreateAssetMenu(fileName = "DevInputReader", menuName = "Input/Development Input Reader")]
    public class DevInputReader : ScriptableObject, IDevelopmentToolsActions, IInputReader
    {
        public event UnityAction CreateExcellentWork = delegate { };
        public event UnityAction CreateMediocreWork = delegate { };
        public event UnityAction CreateTerribleWork = delegate { };
        public event UnityAction DeactivateDevTools = delegate { };

        private GameInputActions inputActions;

        private void OnEnable()
        {
            // Check if the input actions have been initialized
            if (inputActions == null)
            {
                // Initialize the input actions and set this as the callback
                inputActions = new GameInputActions();
                inputActions.DevelopmentTools.SetCallbacks(this);
            }

            // Enable the input actions
            Enable();
        }

        private void OnDisable() => Disable();

        /// <summary>
        /// Enable the input actions
        /// </summary>
        public void Enable() => inputActions.Enable();

        /// <summary>
        /// Disable the input actions
        /// </summary>
        public void Disable() => inputActions.Disable();

        public void OnCreateExcellentWork(InputAction.CallbackContext context)
        {
            if (context.canceled) CreateExcellentWork.Invoke();
        }

        public void OnCreateMediocreWork(InputAction.CallbackContext context)
        {
            if (context.canceled) CreateMediocreWork.Invoke();
        }

        public void OnCreateTerribleWork(InputAction.CallbackContext context)
        {
            if (context.canceled) CreateTerribleWork.Invoke();
        }

        public void OnDeactivateDevTools(InputAction.CallbackContext context)
        {
            if (context.started) DeactivateDevTools.Invoke();
        }
    }
}
