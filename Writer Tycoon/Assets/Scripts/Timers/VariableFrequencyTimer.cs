using UnityEngine;

namespace GhostWriter.Timers
{
    public class VariableFrequencyTimer : FrequencyTimer
    {
        private float timeThresholdDef;
        private float timeThresholdFaster;
        private float timeThresholdFastest;
        private int scalar;
        private int mode;

        public VariableFrequencyTimer(float ticksPerSecond) : base(ticksPerSecond) 
        {
            mode = 1;
        }

        protected override void CalculateTimeThreshold(float ticksPerSecond)
        {
            // Set the ticks per second
            TicksPerSecond = ticksPerSecond;

            // Calculate the time threshold
            timeThresholdDef = 1f / TicksPerSecond;
            timeThresholdFaster = timeThresholdDef / 2f;
            timeThresholdFastest = timeThresholdDef / 5f;

            // Set the default time threshold
            timeThreshold = timeThresholdDef;
        }

        /// <summary>
        /// Set the default speed for the FrequencyTimer
        /// </summary>
        public void SetDefaultSpeed()
        {
            timeThreshold = timeThresholdDef;
            scalar = 1;
            mode = 1;
        }

        /// <summary>
        /// Set the faster speed for the FrequencyTimer
        /// </summary>
        public void SetFasterSpeed()
        {
            timeThreshold = timeThresholdFaster;
            scalar = 2;
            mode = 2;
        }

        /// <summary>
        /// Set the fastest speed for the FrequencyTimer
        /// </summary>
        public void SetFastestSpeed()
        {
            timeThreshold = timeThresholdFastest;
            scalar = 5;
            mode = 3;
        }

        /// <summary>
        /// Get the current scalar of the Timer
        /// </summary>
        public int GetScalar() => scalar;

        /// <summary>
        /// Get the current mode of the Timer
        /// </summary>
        public int GetMode() => mode;
    }
}