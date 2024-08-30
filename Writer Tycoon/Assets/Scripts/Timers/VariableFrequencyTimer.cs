namespace WriterTycoon.Timers
{
    public class VariableFrequencyTimer : FrequencyTimer
    {
        private float timeThresholdDef;
        private float timeThresholdFaster;
        private float timeThresholdFastest;
        private int mode;

        public VariableFrequencyTimer(float ticksPerSecond, float fasterScalar, float fastestScalar) : base(ticksPerSecond) 
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
            mode = 1;
        }

        /// <summary>
        /// Set the faster speed for the FrequencyTimer
        /// </summary>
        public void SetFasterSpeed()
        {
            timeThreshold = timeThresholdFaster;
            mode = 2;
        }

        /// <summary>
        /// Set the fastest speed for the FrequencyTimer
        /// </summary>
        public void SetFastestSpeed()
        {
            timeThreshold = timeThresholdFastest;
            mode = 3;
        }

        public int GetMode() => mode;
    }
}