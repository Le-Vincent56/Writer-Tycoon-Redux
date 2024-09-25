namespace WriterTycoon.Entities
{
    public interface IWorker
    {
        public string Name { get; }
        public bool Working { get; set; }
        public int AssignedHash { get; set; }
    }
}