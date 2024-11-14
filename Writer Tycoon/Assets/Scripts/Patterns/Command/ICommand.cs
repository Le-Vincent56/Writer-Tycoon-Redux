using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GhostWriter.Patterns.Command
{
    public interface ICommand
    {
        void Execute();
    }
}