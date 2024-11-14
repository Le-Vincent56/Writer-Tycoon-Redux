namespace GhostWriter.WorkCreation.Rater
{
    public interface IMasterable
    {
        int MasteryLevel { get; set; }
        void IncreaseMastery();
    }
}