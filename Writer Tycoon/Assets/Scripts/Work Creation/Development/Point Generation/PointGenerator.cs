using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WriterTycoon.WorkCreation.Development.PointGeneration
{
    public class PointGenerator : MonoBehaviour
    {
        GenreFocusTargets genreFocusTargets;

        private void Awake()
        {
            // Initialize the Genre Focus Targets
            genreFocusTargets = new();
        }
    }
}