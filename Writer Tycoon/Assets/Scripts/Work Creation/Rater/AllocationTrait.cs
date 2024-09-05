using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WriterTycoon.WorkCreation.Rater
{
    public enum BookTrait
    {
        Character,
        Plot,
        Setting
    }

    [System.Serializable]
    public class AllocationTrait
    {
        public BookTrait DedicatedTrait;
        public float Value;
    }
}