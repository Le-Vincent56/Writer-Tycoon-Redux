using UnityEngine;
using UnityEngine.Events;
using WriterTycoon.WorkCreation.Ideation.Mediation;

namespace WriterTycoon.WorkCreation.Ideation.WorkTypes
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

        public UnityAction<WorkTypeButton> OnWorkTypeSelected = delegate { };

        public override string Name { get => "Work Type Manager"; }
        public override DedicantType Type { get => DedicantType.WorkType; }

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
        public void ClearAudience() => selectedType = WorkType.None;

        /// <summary>
        /// Send the selected Work Type
        /// </summary>
        public void SendWorkType()
        {
            Send(new WorkTypePayload() 
                { Content = selectedType }, 
                AreTypes(new DedicantType[2]
                {
                    DedicantType.TimeEstimator,
                    DedicantType.IdeaReviewer
                })
            );
        }
    }
}