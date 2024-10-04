namespace WriterTycoon.World.Economy
{
    public struct SalesData
    {
        public int WeekNumber;
        public int CopiesSold;
        public float Income;

        public SalesData(int weekNumber, int copiesSold, float income)
        {
            WeekNumber = weekNumber;
            CopiesSold = copiesSold;
            Income = income;
        }
    }
}