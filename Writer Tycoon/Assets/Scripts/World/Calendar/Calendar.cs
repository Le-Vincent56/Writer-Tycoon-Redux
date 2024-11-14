using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GhostWriter.Input;
using GhostWriter.Patterns.EventBus;
using GhostWriter.Patterns.ServiceLocator;
using GhostWriter.Timers;

namespace GhostWriter.World.Calendar
{
    public class Calendar : MonoBehaviour
    {
        [SerializeField] private CalendarData data;
        [SerializeField] private GameInputReader inputReader;
        [SerializeField] private bool canChangeSpeed;

        public Dictionary<int, string> Months = new()
        {
            { 1, "January" },
            { 2, "February" },
            { 3, "March" },
            { 4, "April" },
            { 5, "May" },
            { 6, "June" },
            { 7, "July" },
            { 8, "August" },
            { 9, "September" },
            { 10, "October" },
            { 11, "November" },
            { 12, "December" }
        };

        public Dictionary<int, int> MonthDays = new()
        {
            { 1, 31 },
            { 2, 28 },
            { 3, 31 },
            { 4, 30 },
            { 5, 31 },
            { 6, 30 },
            { 7, 31 },
            { 8, 31 },
            { 9, 30 },
            { 10, 31 },
            { 11, 30 },
            { 12, 31 }
        };

        private int currentHour;
        private int currentDay;
        private int currentMonth;
        private int currentYear;
        private int daysInMonth;
        private int daysUntilWeekPass;

        [SerializeField] private Text dateText;
        private VariableFrequencyTimer hourTimer;
        private VariableFrequencyTimer dayTimer;

        private EventBinding<ChangeCalendarPauseState> changeCalendarPauseStateEvent;

        public int Day { get => currentDay; }
        public int Month { get => currentMonth; }
        public int Year { get => currentYear; }

        private void Awake()
        {
            // Register to the global service locator
            ServiceLocator.ForSceneOf(this).Register(this);
        }

        private void OnEnable()
        {
            inputReader.DefaultSpeed += SetDefaultSpeed;
            inputReader.FasterSpeed += SetFasterSpeed;
            inputReader.FastestSpeed += SetFastestSpeed;
            inputReader.PauseCalendar += HandlePause;

            changeCalendarPauseStateEvent = new EventBinding<ChangeCalendarPauseState>(HandleCalendarPauseStateChange);
            EventBus<ChangeCalendarPauseState>.Register(changeCalendarPauseStateEvent);
        }

        private void OnDisable()
        {
            inputReader.DefaultSpeed -= SetDefaultSpeed;
            inputReader.FasterSpeed -= SetFasterSpeed;
            inputReader.FastestSpeed -= SetFastestSpeed;
            inputReader.PauseCalendar -= HandlePause;

            EventBus<ChangeCalendarPauseState>.Deregister(changeCalendarPauseStateEvent);
        }

        private void Start()
        {
            // Allow the player to change speeds
            canChangeSpeed = true;

            // Set the current date to January 01, 2024
            currentHour = 1;
            currentDay = 1;
            currentMonth = 1;
            currentYear = 2024;
            daysUntilWeekPass = 0;

            // Check for leap year
            CheckForLeapYear();

            // Create the hour timer
            hourTimer = new VariableFrequencyTimer(
                data.DayIncrementTime * 24
            );

            // Set the Tick event
            hourTimer.OnTick += PassHour;

            // Create the day timer
            dayTimer = new VariableFrequencyTimer(
                data.DayIncrementTime
            );

            // Set the Tick event
            dayTimer.OnTick += PassDay;

            // Start the timers
            hourTimer.Start();
            dayTimer.Start();

            // Set text
            SetDateText();
        }

        private void OnDestroy()
        {
            // Dispose of the hour timer
            hourTimer.Dispose();

            // Dispose of the day timer
            dayTimer.Dispose();
        }

        /// <summary>
        /// Pass an horu on the calendar
        /// </summary>
        private void PassHour()
        {
            // Increment the current hour
            currentHour++;

            // Reset on a 24-hour cycle
            if(currentHour > 24)
            {
                currentHour = 1;
            }

            // Raise the Pass Hour event
            EventBus<PassHour>.Raise(new PassHour());
        }

        /// <summary>
        /// Pass a day on the Calendar
        /// </summary>
        private void PassDay()
        {
            bool newMonth = false;

            // Increment the current day
            currentDay++;
            daysUntilWeekPass++;

            // Check if the current day surpasses the days in the month
            if(currentDay > daysInMonth)
            {
                // Increment the current month
                currentMonth++;

                // Reset the first day of the month
                currentDay = 1;

                newMonth = true;

                // Raise the Pass Month event
                EventBus<PassMonth>.Raise(new PassMonth());
            }

            // Check if it is a new month
            if(newMonth)
            {
                // Check if the current month surpasses the amount of months in a year
                if (currentMonth > 12)
                {
                    // Increment the current year
                    currentYear++;

                    // Reset to the first month of the year
                    currentMonth = 1;
                }

                // Check for a leap year
                CheckForLeapYear();
            }

            // Set the date text
            SetDateText();

            // Raise the Pass Day event
            EventBus<PassDay>.Raise(new PassDay());

            // Check if a week has passed
            if(daysUntilWeekPass >= 7)
            {
                // If so, reset the counter
                daysUntilWeekPass = 0;

                // Raise the Pass Week event
                EventBus<PassWeek>.Raise(new PassWeek());
            }
        }

        /// <summary>
        /// Set the date text
        /// </summary>
        private void SetDateText()
        {
            // Format the string depending on how many digits the current day has
            // This formats single digit days to have a zero in front ("01" instead of "1")
            string currentDayString = (currentDay >= 0 && currentDay <= 9) 
                ? $"0{currentDay}" 
                : $"{currentDay}";

            // Display text
            dateText.text = $"{Months[currentMonth]} {currentDayString}, {currentYear}";
        }

        /// <summary>
        /// Set the amount of days in the month according to if it is a Leap Year or not
        /// </summary>
        private void CheckForLeapYear()
        {
            // Check if it's February and a leap year
            if (currentMonth == 2 && currentYear % 4 == 0)
                // If so, set the amount of days to 29
                daysInMonth = 29;
            else
                // Otherwise, look up in the dictionary for the amount of days
                daysInMonth = MonthDays[currentMonth];
        }

        /// <summary>
        /// Set the default calendar speed
        /// </summary>
        private void SetDefaultSpeed()
        {
            // Exit case - if can't change speed
            if (!canChangeSpeed) return;

            // Set the default speeds for the timers
            hourTimer.SetDefaultSpeed();
            dayTimer.SetDefaultSpeed();

            // Invoke the event for changing the calendar speed
            EventBus<ChangeCalendarSpeed>.Raise(new ChangeCalendarSpeed()
            {
                TimeScale = dayTimer.GetScalar()
            });

            if (!dayTimer.IsRunning)
            {
                hourTimer.Resume();
                dayTimer.Resume();
            }
        }

        /// <summary>
        /// Set the faster calendar speed
        /// </summary>
        private void SetFasterSpeed()
        {
            // Exit case - if can't change speed
            if (!canChangeSpeed) return;

            // Set the faster speeds for the timers
            hourTimer.SetFasterSpeed();
            dayTimer.SetFasterSpeed();

            // Invoke the event for changing the calendar speed
            EventBus<ChangeCalendarSpeed>.Raise(new ChangeCalendarSpeed()
            {
                TimeScale = dayTimer.GetScalar()
            });

            if (!dayTimer.IsRunning)
            {
                hourTimer.Resume();
                dayTimer.Resume();
            }
        }

        /// <summary>
        /// Set the fastest calendar speed
        /// </summary>
        private void SetFastestSpeed()
        {
            // Exit case - if can't change speed
            if (!canChangeSpeed) return;

            // Set the fastest speed for the timers
            hourTimer.SetFastestSpeed();
            dayTimer.SetFastestSpeed();

            // Invoke the event for changing the calendar speed
            EventBus<ChangeCalendarSpeed>.Raise(new ChangeCalendarSpeed()
            {
                TimeScale = dayTimer.GetScalar()
            });

            // If the Timers are not running, resume them
            if (!dayTimer.IsRunning)
            {
                hourTimer.Resume();
                dayTimer.Resume();
            }
        }

        /// <summary>
        /// Pause the calendar
        /// </summary>
        private void HandlePause()
        {
            // Exit case - if the speed cannot be changed
            if (!canChangeSpeed) return;

            // Check if the Timer is running
            if (dayTimer.IsRunning)
                // If so, pause the game
                Pause();
            else
                // Otherwise, unpause the game
                Unpause();
        }

        /// <summary>
        /// Pause the game
        /// </summary>
        private void Pause() 
        {
            // If so, pause the Timers
            hourTimer.Pause();
            dayTimer.Pause();

            // TODO: Update UI

            // Invoke event
            EventBus<CalendarPauseStateChanged>.Raise(new CalendarPauseStateChanged
            {
                Paused = true
            });
        }

        /// <summary>
        /// Unpause the game
        /// </summary>
        private void Unpause()
        {
            // If not, resume the Timer
            hourTimer.Resume();
            dayTimer.Resume();

            switch (dayTimer.GetMode())
            {
                case 1:
                    // TODO: Update UI (Default)
                    break;

                case 2:
                    // TODO: Update UI (Faster)
                    break;

                case 3:
                    // TODO: Update UI (Fastest)
                    break;
            }

            // Invoke event
            EventBus<CalendarPauseStateChanged>.Raise(new CalendarPauseStateChanged
            {
                Paused = false
            });
        }

        /// <summary>
        /// Callback for handling Calendar Pause State changes
        /// </summary>
        private void HandleCalendarPauseStateChange(ChangeCalendarPauseState eventData)
        {
            if (eventData.Paused)
            {
                // Exit case - if already paused
                if (!dayTimer.IsRunning) return;

                // Pause the game
                Pause();
            }
            else
            {
                // Exit case - if already unpaused
                if (dayTimer.IsRunning) return;

                // Unpause the game
                Unpause();
            }

            canChangeSpeed = eventData.AllowSpeedChanges;
        }
    }
}