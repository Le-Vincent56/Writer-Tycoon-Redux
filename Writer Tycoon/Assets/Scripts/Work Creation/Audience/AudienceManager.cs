using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WriterTycoon.WorkCreation.Genres;
using WriterTycoon.WorkCreation.Mediation;
using WriterTycoon.WorkCreation.UI;

namespace WriterTycoon.WorkCreation.Audience
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

        public override string Name { get => "Audience Manager"; }
        public override DedicantType Type { get => DedicantType.Audience; }

        /// <summary>
        /// Select an Audience
        /// </summary>
        public void SelectAudience(AudienceButton audienceButton)
        {
            // Select the audience button
            audienceButton.Select();

            // Set the selected audience
            selectedAudience = audienceButton.Type;

            // Invoke the audience selected event
            OnAudienceSelected.Invoke(audienceButton);
        }

        /// <summary>
        /// Clear the selected Audience
        /// </summary>
        public void ClearAudience() => selectedAudience = AudienceType.None;

        /// <summary>
        /// Send the selected Audience to the Compatibility Manager
        /// </summary>
        public void SendAudienceToCompatibilityManager() => Send(new AudiencePayload() { Content = selectedAudience }, IsType(DedicantType.Compatibility));
    }
}