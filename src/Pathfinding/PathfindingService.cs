using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using UnityEngine;

namespace Pathfinding
{
    public class PathfindingService
    {
        [CanBeNull]
        public IReadOnlyList<Vector2Int> FindRoute(Vector2Int from, Vector2Int to, IMap map)
        {
            var closed = new Dictionary<Vector2Int, RouteToNode>();
            closed[from] = new RouteToNode(from, 0);
            var open = new PriorityQueue<Vector2Int>();
            open.Enqueue(from, int.MaxValue);

            while (open.Count > 0)
            {
                var current = open.Dequeue();
                if (current == to)
                    return ReconstructPath(from, to, closed);

                foreach (var neighbour in map.FindNeighbours(current))
                {
                    var newCost = closed[current].CostSoFar + 1;
                    if (!closed.TryGetValue(neighbour, out var route) || newCost < route.CostSoFar)
                    {
                        closed[neighbour] = new RouteToNode(current, newCost);
                        var priority = newCost + Heuristic(neighbour, to);
                        open.Enqueue(neighbour, priority);
                    }
                }
            }

            return null;
        }

        protected virtual int Heuristic(Vector2Int from, Vector2Int to)
        {
            var delta = from - to;
            var magnitude = Mathf.RoundToInt(delta.magnitude);
            return magnitude;
        }

        private IReadOnlyList<Vector2Int> ReconstructPath(Vector2Int from, Vector2Int to,
            Dictionary<Vector2Int, RouteToNode> closed)
        {
            var current = to;
            var path = new List<Vector2Int>
            {
                current
            };

            while (current != from)
            {
                current = closed[current].Previous;
                path.Add(current);
            }
            
            path.Reverse();
            return path;
        }

        
        private struct RouteToNode
        {
            public Vector2Int Previous { get; }
            public int CostSoFar { get; }

            public RouteToNode(Vector2Int previous, int costSoFar)
            {
                Previous = previous;
                CostSoFar = costSoFar;
            }
        }
    }
}