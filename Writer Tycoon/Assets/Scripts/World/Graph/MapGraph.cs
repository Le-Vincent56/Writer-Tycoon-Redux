using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using WriterTycoon.Patterns.ServiceLocator;

namespace WriterTycoon.World.Graph
{
    public class MapGraph : MonoBehaviour
    {
        private Tilemap tilemap;
        [SerializeField] private List<Node> nodes = new();

        private void Awake()
        {
            if(tilemap == null)
                tilemap = GetComponent<Tilemap>();

            // Generate the graph from the tilemap
            GenerateGraphFromTilemap();

            // Connect the nodes
            ConnectNodes();

            // Register the graph to the Service Locator
            ServiceLocator.ForSceneOf(this).Register(this);
        }

        /// <summary>
        /// Generate the graph Nodes from the Tilemap
        /// </summary>
        private void GenerateGraphFromTilemap()
        {
            // Get the bounds of the tilemap
            BoundsInt bounds = tilemap.cellBounds;

            // Iterate through the x-positions
            for(int x = bounds.xMin; x < bounds.xMax; x++)
            {
                // Iterate through the y-positions
                for(int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    // Get the tile position
                    Vector3Int tilePos = new(x, y);

                    // Get the tile on the tilemap at the tile position
                    TileBase tile = tilemap.GetTile(tilePos);

                    // Exit case - if there is no tile
                    if (tile == null) continue;

                    // Create a new Node from the tile position
                    Node newNode = new(new Vector2Int(x, y));

                    // Add it to the Nodes list
                    nodes.Add(newNode);
                }
            }
        }

        /// <summary>
        /// Connect neighboring nodes
        /// </summary>
        private void ConnectNodes()
        {
            // Iterate through each node
            foreach(Node node in nodes)
            {
                // Set a list of directions
                Vector2Int[] directions = new Vector2Int[]
                {
                    new(1, 0), // Right
                    new(-1, 0), // Left
                    new(0, 1), // Up
                    new(0, -1) // Down
                };

                foreach(Vector2Int direction in directions)
                {
                    // Get the neighboring position of the current node
                    Vector2Int neighborPos = node.position + direction;

                    // Find a node at the neighboring position
                    Node neighbor = nodes.Find(n => n.position == neighborPos);

                    // Exit case - if there is no neighbor found
                    if (neighbor == null) continue;

                    // Have each node add each other as a neighbor
                    node.AddNeighbor(neighbor);
                    neighbor.AddNeighbor(node);
                }
            }
        }

        public List<Node> PathfindAStar(Node startNode, Node endNode)
        {
            // Exit case - the graph does not contain the start node
            if (!nodes.Contains(startNode)) return null;

            // Exit case - the graph does not contain the end node
            if (!nodes.Contains(endNode)) return null;

            // Initialize the open and closed sets
            List<Node> openSet = new() { startNode };
            HashSet<Node> closedSet = new HashSet<Node>();

            Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
            Dictionary<Node, float> gScore = new Dictionary<Node, float>();
            Dictionary<Node, float> fScore = new Dictionary<Node, float>();

            gScore[startNode] = 0;
            fScore[startNode] = GetHeuristicCost(startNode, endNode);

            // Loop through the open set
            while(openSet.Count > 0)
            {
                // Set the current Node to the Node with the lowest fScore in the open set
                Node current = openSet.OrderBy(n => fScore[n]).First();

                // Exit case - if the current Node is the end Node
                if (current == endNode)
                {
                    return ReconstructPath(cameFrom, current);
                }

                // Remove the current Node from the open set 
                openSet.Remove(current);

                // Add the current Node to the closed set
                closedSet.Add(current);

                // Iterate over each neighbor
                foreach(Node neighbor in current.neighbors)
                {
                    // Exit case - if the closed set contains the neighbor
                    if (closedSet.Contains(neighbor)) continue;

                    // Get the distance between the current Node and the neighbor Node
                    float nodeDistance = Vector2Int.Distance(current.position, neighbor.position);

                    // Calculate the tentative G-score
                    float tentativeGScore = gScore[current] + nodeDistance;

                    // Check if the open set contains the neighbor
                    if (!openSet.Contains(neighbor))
                        // If not, add the neighbor to the open set
                        openSet.Add(neighbor);
                    // Otherwise, if the tenative G-score is higher or equal to the G-score 
                    // of the neighbor, continue
                    else if (tentativeGScore >= gScore[neighbor])
                        continue;

                    // Set values to remember
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + GetHeuristicCost(neighbor, endNode);
                }
            }

            // If the open set is emptied, no path was found
            return null;
        }

        /// <summary>
        /// Use Manhattan distance as the heuristic for grid-based pathfinding
        /// </summary>
        private float GetHeuristicCost(Node startNode, Node endNode)
        {
            return Mathf.Abs(startNode.position.x - endNode.position.x) + Mathf.Abs(startNode.position.y - endNode.position.y);
        }

        /// <summary>
        /// Reconstruct the calculated Node path
        /// </summary>
        private List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
        {
            // Create a container for the path, starting with the current Node
            List<Node> path = new() { current };

            // Navigate backwards from the current Node
            while(cameFrom.ContainsKey(current))
            {
                // Set the current Node to the Node it came from
                current = cameFrom[current];

                // Add the Node to the path
                path.Add(current);
            }

            // Reverse the path
            path.Reverse();

            return path;
        }

        /// <summary>
        /// Check if the graph contains a Node
        /// </summary>
        public bool ContainsNode(Node node) => nodes.Contains(node);

        /// <summary>
        /// Get a Node on the graph from a given world position
        /// </summary>
        public Node GetNodeFromWorldPosition(Vector3 worldPos)
        {
            // Convert the world position to the tilemap position
            Vector3Int tilePos = tilemap.WorldToCell(worldPos);

            return GetNodeAtCellPosition(tilePos.x, tilePos.y);
        }

        /// <summary>
        /// Get a world position from a given Node on the graph
        /// </summary>
        public Vector3 GetWorldPosFromNode(Node node)
        {
            // Convert the node position to a Vector3Int
            Vector3Int convertedPos = new(node.position.x, node.position.y, 0);

            // Convert the cell position to a world position (bottom-left of the tile)
            Vector3 tileBottomLeftWorldPos = tilemap.CellToWorld(convertedPos);

            // Get the tile size
            Vector3 tileSize = tilemap.cellSize;

            // Offset the bottom-left world position to the center of the tile
            Vector3 tileCenterWorldPos = tileBottomLeftWorldPos + (tileSize / 2f);

            return tileCenterWorldPos;
        }

        public Node GetNodeAtCellPosition(int x, int y)
        {
            Vector2Int convertedPos = new(x, y);

            // Find the node at the player's tile position
            return nodes.Find(n => n.position == convertedPos);
        }
    }
}
