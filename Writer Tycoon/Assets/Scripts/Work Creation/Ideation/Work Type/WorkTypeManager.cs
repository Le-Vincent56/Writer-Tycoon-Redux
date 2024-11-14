using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GhostWriter.Patterns.EventBus;
using GhostWriter.WorkCreation.Mediation;

namespace GhostWriter.WorkCreation.Ideation.WorkTypes
{
    public enum WorkType
    {
        None,
        Poetry,
        FlashFiction,
        ShortStory,
        Novella,
        Novel,
        Screenplay
    }

    public class WorkTypeManager : Dedicant
    {
        [SerializeField] private WorkType selectedType;
        [SerializeField] private Dictionary<WorkType, int> targetScores;

        public override string Name { get => "Work Type Manager"; }
        public override DedicantType Type { get => DedicantType.WorkType; }

        public UnityAction<WorkTypeButton> OnWorkTypeSelected = delegate { };
        public UnityAction OnWorkTypeCleared = delegate { };


        private EventBinding<ClearIdeation> clearIdeationEvent;

        private void Awake()
        {
            // Initialize the target scores
            targetScores = new()
            {
                { WorkType.None, 0 },
                { WorkType.Poetry, 100 },
                { WorkType.FlashFiction, 500 },
                { WorkType.ShortStory, 1000 },
                { WorkType.Novella, 5000 },
                { WorkType.Novel, 15000 },
                { WorkType.Screenplay, 15000 }
            };
        }

        private void OnEnable()
        {
            clearIdeationEvent = new EventBinding<ClearIdeation>(ClearWorkType);
            EventBus<ClearIdeation>.Register(clearIdeationEvent);
        }

        private void OnDisable()
        {
            EventBus<ClearIdeation>.Deregister(clearIdeationEvent);
        }

        /// <summary>
        /// Select a Work Type
        /// </summary>
        public void SelectWorkType(WorkTypeButton typeButton)
        {
            // Select the type button
            typeButton.Select();

            // Set the selected type
            selectedType = typeButton.Type;

            // Send the work type out to the mediator
            SendWorkType();

            // Invoke the type selected event
            OnWorkTypeSelected.Invoke(typeButton);
        }

        /// <summary>
        /// Clear the selected Work Type
        /// </summary>
        public void ClearWorkType()
        {
            // Set the Work type to none
            selectedType = WorkType.None;

            // Send the work type out to the mediator
            SendWorkType();

            OnWorkTypeCleared.Invoke();
        }

        /// <summary>
        /// Send the selected Work Type
        /// </summary>
        public void SendWorkType()
        {
            Send(new WorkTypePayload() 
                { Content = (selectedType, targetScores[selectedType]) }, 
                AreTypes(new DedicantType[2]
                {
                    DedicantType.TimeEstimator,
                    DedicantType.IdeaReviewer
                })
            );
        }
    }
}