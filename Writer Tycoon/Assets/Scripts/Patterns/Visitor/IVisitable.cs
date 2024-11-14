namespace GhostWriter.Patterns.Visitor
{
    public interface IVisitable
    {
        /// <summary>
        /// Accept a Visitor
        /// </summary>
        void Accept(IVisitor visitor);
    }
}
