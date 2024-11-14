using UnityEngine;

namespace GhostWriter.Patterns.Visitor
{
    public interface IVisitor
    {
        /// <summary>
        /// Visit a Visitable component
        /// </summary>
        void Visit<T>(T visitable) where T : Component, IVisitable;
    }
}