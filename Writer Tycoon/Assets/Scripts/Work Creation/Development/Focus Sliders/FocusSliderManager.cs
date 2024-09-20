using System.Collections.Generic;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Development.PointGeneration;
using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Development.FocusSliders
{
    public class FocusSliderManager : Dedicant
    {
        private Dictionary<PointCategory, int> allocatedPoints;

        private EventBinding<SetSliderPoints> setSliderPointsEvent;
        private EventBinding<SendSliderPoints> sendSliderPointsEvent;
        private EventBinding<EndDevelopment> endDevelopmentEvent;

        public override string Name => "Slider Manager";
        public override DedicantType Type => DedicantType.Sliders;

        private void Awake()
        {
            // Initialize the Dictionary
            InitializeDictionary();
        }

        private void OnEnable()
        {
            setSliderPointsEvent = new EventBinding<SetSliderPoints>(AllocatePoints);
            EventBus<SetSliderPoints>.Register(setSliderPointsEvent);

            sendSliderPointsEvent = new EventBinding<SendSliderPoints>(SendSliderPoints);
            EventBus<SendSliderPoints>.Register(sendSliderPointsEvent);

            endDevelopmentEvent = new EventBinding<EndDevelopment>(ResetSliders);
            EventBus<EndDevelopment>.Register(endDevelopmentEvent);
        }

        private void OnDisable()
        {
            EventBus<SetSliderPoints>.Deregister(setSliderPointsEvent);
            EventBus<SendSliderPoints>.Deregister(sendSliderPointsEvent);
            EventBus<EndDevelopment>.Deregister(endDevelopmentEvent);
        }

        /// <summary>
        /// Initialize the Dictionary
        /// </summary>
        private void InitializeDictionary()
        {
            allocatedPoints = new()
            {
                { PointCategory.CharacterSheets, 5 },
                { PointCategory.PlotOutline, 5 },
                { PointCategory.WorldDocument, 5 },
                { PointCategory.Dialogue, 5 },
                { PointCategory.Subplots, 5 },
                { PointCategory.Descriptions, 5 },
                { PointCategory.Emotions, 5 },
                { PointCategory.Twists, 5 },
                { PointCategory.Symbolism, 5 },
            };
        }

        /// <summary>
        /// Callback function to allocate slider points
        /// </summary>
        private void AllocatePoints(SetSliderPoints eventData)
        {
            // Set the value within the dictionary
            allocatedPoints[eventData.Category] = eventData.Value;
        }

        /// <summary>
        /// Send the allocated points
        /// </summary>
        private void SendSliderPoints()
        {
            Send(new SliderPayload()
            { Content = allocatedPoints },
                IsType(DedicantType.PointGenerator)
            );
        }

        /// <summary>
        /// Reset the sliders
        /// </summary>
        private void ResetSliders()
        {
            // Reset the dictionary
            InitializeDictionary();
        }
    }
}