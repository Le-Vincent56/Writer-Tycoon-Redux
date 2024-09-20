using System.Collections.Generic;
using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Development.PointGeneration
{
    public class PointGenerator : Dedicant
    {
        private GenreFocusTargets genreFocusTargets;
        private Dictionary<PointCategory, int> allocatedPoints;

        public override string Name => "Point Generator";
        public override DedicantType Type => DedicantType.PointGenerator;

        private void Awake()
        {
            // Initialize variables
            genreFocusTargets = new();
            allocatedPoints = new();
        }

        /// <summary>
        /// Set the allocated points Dictionary
        /// </summary>
        public void SetAllocatedPoints(Dictionary<PointCategory, int> allocatedPoints)
        {
            this.allocatedPoints = allocatedPoints;
        }
    }
}