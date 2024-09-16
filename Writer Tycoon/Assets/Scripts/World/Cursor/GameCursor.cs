using UnityEngine;
using UnityEngine.InputSystem;

namespace WriterTycoon.World.GameCursor
{
    public class GameCursor : MonoBehaviour
    {
        [SerializeField] private Vector2 cursorPosition;
        [SerializeField] private Bounds cursorBounds;

        private void Start()
        {
            // Hide the main cursor and confine it to the window
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;

            // Set the bounds
            cursorBounds = CalculateCameraBounds();

            // Set the initial cursor position
            cursorPosition = transform.position;
        }

        private void Update()
        {
            // Set the cursor position
            cursorPosition = Camera.main.ScreenToWorldPoint(
                new Vector2(
                    Mouse.current.position.ReadValue().x,
                    Mouse.current.position.ReadValue().y
                )
            );

            // Clamp the cursor bounds within the screen
            ClampCursorBounds();

            // Finalize the cursor position
            transform.position = cursorPosition;
        }

        /// <summary>
        /// Calculate the Cursor bounds
        /// </summary>
        private Bounds CalculateCameraBounds()
        {
            float height = 2f * Camera.main.orthographicSize;
            float width = height * Camera.main.aspect;

            Vector3 center = Camera.main.transform.position;
            Vector3 size = new Vector3(width, height);

            return new Bounds(center, size);
        }

        /// <summary>
        /// Clamp the curosr position within the screen bounds
        /// </summary>
        private void ClampCursorBounds()
        {
            // Clamp the cursor position into the bounds of the screen
            cursorPosition.x = Mathf.Clamp(cursorPosition.x, cursorBounds.min.x, cursorBounds.max.x);
            cursorPosition.y = Mathf.Clamp(cursorPosition.y, cursorBounds.min.y, cursorBounds.max.y);
        }
    }

}