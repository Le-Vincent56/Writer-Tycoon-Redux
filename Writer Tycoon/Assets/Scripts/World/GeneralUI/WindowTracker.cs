using GhostWriter.Patterns.ServiceLocator;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GhostWriter.World.GeneralUI
{
    public interface IWindow
    {
        public bool Open { get; set; }
    }

    public class WindowTracker : SerializedMonoBehaviour
    {
        [SerializeField] private List<IWindow> windows;

        private void Awake()
        {
            // Initialize the list
            windows = new();

            // Register this as a service
            ServiceLocator.ForSceneOf(this).Register(this);
        }

        /// <summary>
        /// Register a Window to be tracked
        /// </summary>
        public void RegisterWindow(IWindow window) => windows.Add(window);

        /// <summary>
        /// Check if any tracked Windows are open
        /// </summary>
        public bool CheckIfAnyOpen()
        {
            // Set false as a default
            bool windowsOpen = false;

            // Iterate through each tracked Window
            foreach(IWindow window in windows)
            {
                // Check if a Window is opened
                if (window.Open) 
                    // Notify that a Window is open
                    windowsOpen = true;
            }

            return windowsOpen;
        }
    }
}
