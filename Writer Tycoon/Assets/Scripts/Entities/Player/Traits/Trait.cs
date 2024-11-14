using System.Collections.Generic;

namespace GhostWriter.Entities.Player.Traits
{
    public class Trait
    {
        public string Name;
        public Dictionary<Attribute, float> Attributes;

        public Trait(string name, Dictionary<Attribute, float> attributes)
        {
            Name = name;
            Attributes = attributes;
        }
    }
}