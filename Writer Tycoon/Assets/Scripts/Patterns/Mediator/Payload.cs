using UnityEngine;
using GhostWriter.Patterns.Visitor;

namespace GhostWriter.Patterns.Mediator
{
    public abstract class Payload<TData> : IVisitor
    {
        public abstract TData Content { get; set; }
        public abstract void Visit<T>(T visitable) where T : Component, IVisitable;
    }
}