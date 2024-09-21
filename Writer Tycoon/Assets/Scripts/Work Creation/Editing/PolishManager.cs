using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Editing
{
    public class PolishManager : Dedicant
    {
        [SerializeField] private int totalErrors;

        private List<float> resolvePath;
        private int pathCount;

        private EventBinding<PassDay> passDayEvent;

        public override DedicantType Type => DedicantType.Editor;
        public override string Name => "Editing Manager";

        private void Awake()
        {
            // Initialize the resolve path List
            resolvePath = new();
        }

        private void OnEnable()
        {
            passDayEvent = new EventBinding<PassDay>(Polish);
            EventBus<PassDay>.Register(passDayEvent);
        }

        private void OnDisable()
        {
            EventBus<PassDay>.Deregister(passDayEvent);
        }

        private void Polish()
        {

        }

        /// <summary>
        /// Set the total errors for the Editing Manager to polish
        /// </summary>
        public void SetTotalErrors(int totalErrors) => this.totalErrors = totalErrors;
    }
}