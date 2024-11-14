using UnityEngine;

namespace GhostWriter.World.Calendar
{
    [CreateAssetMenu(fileName = "CalendarData", menuName = "Data/Calendar Data")]
    public class CalendarData : ScriptableObject
    {
        public float DayIncrementTime;
        public float FasterScalar;
        public float FastestScalar;
    }
}