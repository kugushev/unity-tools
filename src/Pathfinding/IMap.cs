using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public interface IMap
    {
        IEnumerable<Vector2Int> FindNeighbours(Vector2Int point);
    }
}