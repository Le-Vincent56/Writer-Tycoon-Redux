namespace WriterTycoon.Timers
{
    public class VariableFrequencyTimer : FrequencyTimer
    {
        private float timeThresholdDef;
        private float timeThresholdFaster;
        private float timeThresholdFastest;

        public VariableFrequencyTimer(float ticksPerSecond, float fasterScalar, float fastestScalar) : base(ticksPerSecond) { }

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
        public void SetDefaultSpeed() => timeThreshold = timeThresholdDef;

        /// <summary>
        /// Set the faster speed for the FrequencyTimer
        /// </summary>
        public void SetFasterSpeed() => timeThreshold = timeThresholdFaster;

        /// <summary>
        /// Set the fastest speed for the FrequencyTimer
        /// </summary>
        public void SetFastestSpeed() => timeThreshold = timeThresholdFastest;
    }
}