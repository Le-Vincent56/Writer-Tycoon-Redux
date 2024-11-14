using System.Collections.Generic;
using GhostWriter.Patterns.EventBus;
using GhostWriter.WorkCreation.Development.PointGeneration;
using GhostWriter.WorkCreation.Mediation;

namespace GhostWriter.WorkCreation.Development.FocusSliders
{
    public class FocusSliderManager : Dedicant
    {
        private Dictionary<int, Dictionary<PointCategory, int>> allocatedPoints;
        //private Dictionary<PointCategory, int> allocatedPoints;

        private EventBinding<PrepareSliders> prepareSlidersEvent;
        private EventBinding<SetSliderPoints> setSliderPointsEvent;
        private EventBinding<SendSliderPoints> sendSliderPointsEvent;
        private EventBinding<DeleteSliderData> deleteSliderDataEvent;

        public override string Name => "Slider Manager";
        public override DedicantType Type => DedicantType.Sliders;

        private void OnEnable()
        {
            prepareSlidersEvent = new EventBinding<PrepareSliders>(InitializeDictionary);
            EventBus<PrepareSliders>.Register(prepareSlidersEvent);

            setSliderPointsEvent = new EventBinding<SetSliderPoints>(AllocatePoints);
            EventBus<SetSliderPoints>.Register(setSliderPointsEvent);

            sendSliderPointsEvent = new EventBinding<SendSliderPoints>(SendSliderPoints);
            EventBus<SendSliderPoints>.Register(sendSliderPointsEvent);

            deleteSliderDataEvent = new EventBinding<DeleteSliderData>(DeleteSliderData);
            EventBus<DeleteSliderData>.Register(deleteSliderDataEvent);
        }

        private void OnDisable()
        {
            EventBus<PrepareSliders>.Deregister(prepareSlidersEvent);
            EventBus<SetSliderPoints>.Deregister(setSliderPointsEvent);
            EventBus<SendSliderPoints>.Deregister(sendSliderPointsEvent);
            EventBus<DeleteSliderData>.Deregister(deleteSliderDataEvent);
        }

        /// <summary>
        /// Initialize the Dictionary for a Work
        /// </summary>
        private void InitializeDictionary(PrepareSliders eventData)
        {
            allocatedPoints = new()
            {
                { eventData.Hash, new Dictionary<PointCategory, int>()
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
                    }
                }
            };
        }

        /// <summary>
        /// Callback function to allocate slider points
        /// </summary>
        private void AllocatePoints(SetSliderPoints eventData)
        {
            // Set the value within the dictionary
            allocatedPoints[eventData.Hash][eventData.Category] = eventData.Value;
        }

        /// <summary>
        /// Send the allocated points
        /// </summary>
        private void SendSliderPoints(SendSliderPoints eventData)
        {
            Send(new SliderPayload()
            { Content = (eventData.Hash, allocatedPoints[eventData.Hash]) },
                IsType(DedicantType.PointGenerator)
            );
        }

        /// <summary>
        /// Delete the slider data
        /// </summary>
        private void DeleteSliderData(DeleteSliderData eventData)
        {
            // Remove the slider data
            allocatedPoints.Remove(eventData.Hash);
        }
    }
}