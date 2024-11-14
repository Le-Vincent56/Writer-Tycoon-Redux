namespace GhostWriter.Entities
{
    public interface IWorker
    {
        /// <summary>
        /// Get the status of the Worker
        /// </summary>
        float GetStatus();
        public string Name { get; }
        public bool Working { get; set; }
        public int AssignedHash { get; set; }
    }
}