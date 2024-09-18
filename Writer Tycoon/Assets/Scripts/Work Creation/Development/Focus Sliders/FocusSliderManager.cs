using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Development.Tracker;

public class FocusSliderManager : MonoBehaviour
{
    [SerializeField] private DevelopmentPhase currentPhase;

    private EventBinding<SetSliderPhaseState> setSliderPhaseStateEvent;

    private void OnEnable()
    {
        setSliderPhaseStateEvent = new EventBinding<SetSliderPhaseState>(SetSliderPhase);
        EventBus<SetSliderPhaseState>.Register(setSliderPhaseStateEvent);
    }

    private void OnDisable()
    {
        EventBus<SetSliderPhaseState>.Deregister(setSliderPhaseStateEvent);
    }

    /// <summary>
    /// Callback function to set the slider phase
    /// </summary>
    private void SetSliderPhase(SetSliderPhaseState eventData)
    {
        currentPhase = eventData.Phase;
    }
}
