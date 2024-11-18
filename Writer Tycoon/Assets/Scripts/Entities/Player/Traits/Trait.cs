using System.Collections.Generic;

namespace GhostWriter.Entities.Player.Traits
{
    public class Trait
    {
        public string Name;
        public string Flavor;
        public string[] Effects;
        public Dictionary<Attribute, float> Attributes;

        public Trait(string name, string description, string[] effects, Dictionary<Attribute, float> attributes)
        {
            Name = name;
            Flavor = description;
            Effects = effects;
            Attributes = attributes;
        }
    }
}