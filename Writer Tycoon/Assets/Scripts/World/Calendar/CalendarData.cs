using UnityEngine;

namespace WriterTycoon.World.Calendar
{
    [CreateAssetMenu(fileName = "CalendarData", menuName = "Data/Calendar Data")]
    public class CalendarData : ScriptableObject
    {
        public float DayIncrementTime;
        public float FasterScalar;
        public float FastestScalar;
    }
}