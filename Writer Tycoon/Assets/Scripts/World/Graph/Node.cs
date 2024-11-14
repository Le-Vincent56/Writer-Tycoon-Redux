using System.Collections.Generic;
using UnityEngine;

namespace GhostWriter.World.Graph
{
    public class Node
    {
        public Vector2Int position;
        public List<Node> neighbors = new();

        public Node(Vector2Int position)
        {
            this.position = position;
        }

        /// <summary>
        /// Add a neighboring Node to the current Node
        /// </summary>
        public void AddNeighbor(Node neighbor)
        {
            // Exit case - if the neighbor is already recognized
            if (neighbors.Contains(neighbor)) return;

            // Add the neighboring node
            neighbors.Add(neighbor);
        }
    }

}
