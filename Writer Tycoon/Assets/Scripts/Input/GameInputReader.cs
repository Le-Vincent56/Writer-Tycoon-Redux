using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static WriterTycoon.Input.GameInputActions;

namespace WriterTycoon.Input
{
    [CreateAssetMenu(fileName = "GameInputReader", menuName = "Input/Game Input Reader")]
    public class GameInputReader : ScriptableObject, IPlayerControlsActions, IInputReader
    {
        public event UnityAction DefaultSpeed = delegate { };
        public event UnityAction FasterSpeed = delegate { };
        public event UnityAction FastestSpeed = delegate { };
        public event UnityAction PauseCalendar = delegate { };

        GameInputActions inputActions;

        private void OnEnable()
        {
            // Check if the input actions have been initialized
            if (inputActions == null)
            {
                // Initialize the input actions and set this as the callback
                inputActions = new GameInputActions();
                inputActions.PlayerControls.SetCallbacks(this);
            }

            // Enable the input actions
            Enable();
        }

        /// <summary>
        /// Enable the input actions
        /// </summary>
        public void Enable() => inputActions.Enable();

        /// <summary>
        /// Disable the input actions
        /// </summary>
        public void Disable() => inputActions.Disable();

        public void OnSetDefaultSpeed(InputAction.CallbackContext context)
        {
            // If the button has been lifted, invoke the event
            if (context.canceled) DefaultSpeed.Invoke();
        }

        public void OnSetFasterSpeed(InputAction.CallbackContext context)
        {
            // If the button has been lifted, invoke the event
            if (context.canceled) FasterSpeed.Invoke();
        }

        public void OnSetFastestSpeed(InputAction.CallbackContext context)
        {
            // If the button has been lifted, invoke the event
            if (context.canceled) FastestSpeed.Invoke();
        }

        public void OnPauseCalendar(InputAction.CallbackContext context)
        {
            // If the button has been lifted, invoke the event
            if (context.canceled) PauseCalendar.Invoke();
        }
    }
}