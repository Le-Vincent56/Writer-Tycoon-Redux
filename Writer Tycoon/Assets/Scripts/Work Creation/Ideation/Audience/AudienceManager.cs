using UnityEngine;
using UnityEngine.Events;
using GhostWriter.Patterns.EventBus;
using GhostWriter.WorkCreation.Mediation;

namespace GhostWriter.WorkCreation.Ideation.Audience
{
    public enum AudienceType
    {
        None,
        Children,
        Teens,
        YoungAdults,
        Adults
    }

    public class AudienceManager : Dedicant
    {
        [SerializeField] private AudienceType selectedAudience;

        public UnityAction<AudienceButton> OnAudienceSelected = delegate { };
        public UnityAction OnAudienceCleared = delegate { };

        public override string Name { get => "Audience Manager"; }
        public override DedicantType Type { get => DedicantType.Audience; }

        private EventBinding<ClearIdeation> clearIdeationEvent;

        private void OnEnable()
        {
            clearIdeationEvent = new EventBinding<ClearIdeation>(ClearAudience);
            EventBus<ClearIdeation>.Register(clearIdeationEvent);
        }

        private void OnDisable()
        {
            EventBus<ClearIdeation>.Deregister(clearIdeationEvent);
        }

        /// <summary>
        /// Select an Audience
        /// </summary>
        public void SelectAudience(AudienceButton audienceButton)
        {
            // Select the Audience button
            audienceButton.Select();

            // Set the selected Audience
            selectedAudience = audienceButton.Type;

            // Send the selected Audience to the mediator
            SendAudience();

            // Invoke the Audience Selected event
            OnAudienceSelected.Invoke(audienceButton);
        }

        /// <summary>
        /// Clear the selected Audience
        /// </summary>
        public void ClearAudience()
        {
            // Set the selected audience to none
            selectedAudience = AudienceType.None;

            // Send the selected Audience to the mediator
            SendAudience();

            // Invoke the Audience Selected event
            OnAudienceCleared.Invoke();
        }

        /// <summary>
        /// Send the selected Audience
        /// </summary>
        public void SendAudience()
        {
            Send(new AudiencePayload() 
                { Content = selectedAudience }, 
                AreTypes(new DedicantType[2]
                {
                    DedicantType.Compatibility,
                    DedicantType.IdeaReviewer
                })
            );
        }
    }
}