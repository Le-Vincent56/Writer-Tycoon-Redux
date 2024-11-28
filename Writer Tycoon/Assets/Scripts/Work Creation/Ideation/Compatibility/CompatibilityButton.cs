using GhostWriter.Patterns.EventBus;
using GhostWriter.World.GeneralUI;

namespace GhostWriter.WorkCreation.Ideation.Compatibility
{
    public class CompatibilityButton : CodeableButton
    {
        /// <summary>
        /// Handler for clicking the Compatibility button
        /// </summary>
        protected override void OnClick()
        {
            EventBus<CalculateCompatibility>.Raise(new CalculateCompatibility());
        }
    }
}