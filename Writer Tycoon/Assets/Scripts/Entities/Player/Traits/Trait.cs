using System.Collections.Generic;

namespace GhostWriter.Entities.Player.Traits
{
    public class Trait
    {
        public string Name;
        public string Description;
        public Dictionary<Attribute, float> Attributes;

        public Trait(string name, string description, Dictionary<Attribute, float> attributes)
        {
            Name = name;
            Description = description;
            Attributes = attributes;
        }
    }
}