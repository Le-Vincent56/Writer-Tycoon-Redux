using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WriterTycoon.Patterns.Command
{
    public interface ICommand
    {
        void Execute();
    }
}