using System;
using UnityEngine;
using UnityEngine.Events;
using WriterTycoon.Input;
using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Ideation.About
{
    [Serializable]
    public struct AboutInfo
    {
        public string Title;
        public string Author;
        public string Description;

        public AboutInfo(string title, string author, string description)
        {
            Title = title;
            Author = author;
            Description = description;
        }
    }

    public class AboutManager : Dedicant
    {
        [SerializeField] private GameInputReader inputReader;
        [SerializeField] private string currentTitle;
        [SerializeField] private string currentAuthor;
        [SerializeField] private string currentDescription;

        public override string Name { get => "About Manager"; }
        public override DedicantType Type { get => DedicantType.About; }

        private readonly UnityAction<string> OnTitleChange = delegate { };
        private readonly UnityAction<string> OnAuthorChange = delegate { };
        private readonly UnityAction<string> OnDescriptionChange = delegate { };

        /// <summary>
        /// Callback for selecting an About Input
        /// </summary>
        public void OnSelected() => inputReader.Disable();

        /// <summary>
        /// Callback for deselecting an About INput
        /// </summary>
        public void OnDeselected() => inputReader.Enable();

        /// <summary>
        /// Update the Title of the work
        /// </summary>
        public void UpdateTitleValue(string value)
        {
            // Set the current title
            currentTitle = value;

            // Send the About Info to the mediator
            SendAboutInfo();

            // Invoke the Title Change event
            OnTitleChange.Invoke(currentTitle);
        }

        /// <summary>
        /// Update the Author of the work
        /// </summary>
        public void UpdateAuthorValue(string value)
        {
            // Set the current author
            currentAuthor = value;

            // Send the About Info to the mediator
            SendAboutInfo();

            // Invoke the Author Change event
            OnAuthorChange.Invoke(currentAuthor);
        }
        
        /// <summary>
        /// Update the Description of the work
        /// </summary>
        public void UpdateDescriptionValue(string value)
        {
            // Set the current description
            currentDescription = value;

            // Send the About Info to the mediator
            SendAboutInfo();

            // Invoke the Description Change event
            OnDescriptionChange.Invoke(currentDescription);
        }

        /// <summary>
        /// Send the About Info
        /// </summary>
        private void SendAboutInfo()
        {
            Send(new AboutPayload()
            { Content = new AboutInfo(currentTitle, currentAuthor, currentDescription) },
                IsType(DedicantType.IdeaReviewer)
            );
        }
    }
}