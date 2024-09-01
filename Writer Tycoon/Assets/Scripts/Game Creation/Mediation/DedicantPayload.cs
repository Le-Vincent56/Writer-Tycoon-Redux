using UnityEngine;
using WriterTycoon.GameCreation.Mediation;
using WriterTycoon.Patterns.Mediator;

namespace WriterTycoon.GameCreation.Mediator
{
    public class DedicantPayload : Payload<string>
    {
        public Dedicant Source { get; set; }
        public override string Content { get; set; }

        public override void Visit<T>(T visitable)
        {
            Debug.Log($"{Source.Name}: Received word that {(visitable as Dedicant).Name} has registered");
        }
    }
}
