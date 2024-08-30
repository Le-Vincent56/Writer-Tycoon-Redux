using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Input;
using WriterTycoon.Timers;

namespace WriterTycoon.World.Calendar
{
    public class Calendar : MonoBehaviour
    {
        [SerializeField] private CalendarData data;
        [SerializeField] private PlayerInputReader inputReader;

        private int currentDay = 0;

        [SerializeField] private Text dayText;
        [SerializeField] private Text currentTimeText;
        [SerializeField] private Text currentSpeedText;
        VariableFrequencyTimer dayTimer;

        private void OnEnable()
        {
            inputReader.DefaultSpeed += SetDefaultSpeed;
            inputReader.FasterSpeed += SetDoubleSpeed;
            inputReader.FastestSpeed += SetQuintupleSpeed;
        }

        private void OnDisable()
        {
            inputReader.DefaultSpeed -= SetDefaultSpeed;
            inputReader.FasterSpeed -= SetDoubleSpeed;
            inputReader.FastestSpeed -= SetQuintupleSpeed;
        }

        private void Start()
        {
            currentDay = 0;

            // Create the day timer
            dayTimer = new VariableFrequencyTimer(
                data.DayIncrementTime,
                data.FasterScalar,
                data.FastestScalar
            );

            // Set the Tick event
            dayTimer.OnTick += PassDay;

            // Start the day timer
            dayTimer.Start();

            dayText.text = $"Day: {currentDay}";
            currentTimeText.text = $"Current Time: {dayTimer.CurrentTime}";
            currentSpeedText.text = $"Current Speed: Default";
        }

        private void Update()
        {
            currentTimeText.text = $"Current Time: {dayTimer.CurrentTime}";
        }

        private void OnDestroy()
        {
            // Dispose of the day timer
            dayTimer.Dispose();
        }

        private void PassDay()
        {
            currentDay++;
            dayText.text = $"Day: {currentDay}";
        }

        private void SetDefaultSpeed()
        {
            // Set the default speed for the timer
            dayTimer.SetDefaultSpeed();

            currentSpeedText.text = $"Current Speed: Default";
        }

        private void SetDoubleSpeed()
        {
            // Set the default speed for the timer
            dayTimer.SetFasterSpeed();

            currentSpeedText.text = $"Current Speed: Faster";
        }

        private void SetQuintupleSpeed()
        {
            // Set the default speed for the timer
            dayTimer.SetFastestSpeed();

            currentSpeedText.text = $"Current Speed: Fastest";
        }
    }
}