using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Pathfinding
{
    public class PathfindingServiceTests
    {
        [Test]
        public void FindRoute_FromToAreTheSame_ReturnsOneStep()
        {
            // arrange 
            var service = new PathfindingService();
            
            var map = new[,]
            {
                { 0, 0 },
                { 0, 0 }
            };

            // act
            var route = service.FindRoute(new Vector2Int(0, 0), new Vector2Int(0, 0), new MapMock(map));

            // assert
            CollectionAssert.AreEquivalent(new[] {new Vector2Int(0, 0)}, route);
        }

        [Test]
        public void FindRoute_FromToHas1CellDistance_ReturnsTwoSteps()
        {
            // arrange 
            var service = new PathfindingService();
            
            var map = new[,]
            {
                { 0, 0 },
                { 0, 0 }
            };

            // act
            var route = service.FindRoute(new Vector2Int(0, 0), new Vector2Int(0, 1), new MapMock(map));

            // assert
            CollectionAssert.AreEquivalent(new[]
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(0, 1)
                },
                route);
        }

        [Test]
        public void FindRoute_FromToHaveSimilarX_ReturnsVerticalRoute()
        {
            // arrange 
            var service = new PathfindingService();

            var map = new[,]
            {
                { 0, 0 },
                { 0, 0 },
                { 0, 0 },
                { 0, 0 },
                { 0, 0 }
            };
            
            // act
            var route = service.FindRoute(new Vector2Int(0, 0), new Vector2Int(3, 0), new MapMock(map));

            // assert
            CollectionAssert.AreEquivalent(new[]
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(1, 0),
                    new Vector2Int(2, 0),
                    new Vector2Int(3, 0)
                },
                route);
        }

        [Test]
        public void FindRoute_FromToHaveSimilarY_ReturnsHorizontalRoute()
        {
            // arrange 
            var service = new PathfindingService();

            var map = new[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 }
            };
            
            // act
            var route = service.FindRoute(new Vector2Int(0, 0), new Vector2Int(0, 3), new MapMock(map));

            // assert
            CollectionAssert.AreEquivalent(new[]
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(0, 1),
                    new Vector2Int(0, 2),
                    new Vector2Int(0, 3)
                },
                route);
        }

        [Test]
        public void FindRoute_FromToHaveOppositeXY_ReturnsDiagonalWithStepsRoute()
        {
            // arrange 
            var service = new PathfindingService();

            var map = new[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
            };
            
            // act
            var route = service.FindRoute(new Vector2Int(0, 0), new Vector2Int(3, 3), new MapMock(map));

            // assert
            CollectionAssert.AreEquivalent(new[]
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(0, 1),
                    new Vector2Int(0, 2),
                    new Vector2Int(1, 2),
                    new Vector2Int(2, 2),
                    new Vector2Int(2, 3),
                    new Vector2Int(3, 3)
                },
                route);
        }

        [Test]
        public void FindRoute_ObstacleExactBetweenFromAndTo_ReturnsRouteSurroundsObstacle()
        {
            // arrange 
            var service = new PathfindingService();

            var map = new[,]
            {
                { 0, 0, 0, 0, 0, 0 },
                { 0, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 0, 0 },
                { 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0 },
            };
            
            // act
            var route = service.FindRoute(new Vector2Int(0, 0), new Vector2Int(4, 4), new MapMock(map));

            // assert
            CollectionAssert.AreEquivalent(new[]
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(0, 1),
                    new Vector2Int(0, 2),
                    new Vector2Int(0, 3),
                    new Vector2Int(0, 4),
                    new Vector2Int(1, 4),
                    new Vector2Int(2, 4),
                    new Vector2Int(3, 4),
                    new Vector2Int(4, 4),
                },
                route);
        }

        [Test]
        public void FindRoute_DeadEndBetweenFromAndTo_ReturnsRouteWithShortcutToDeadEndExit()
        {
            // arrange 
            var service = new PathfindingService();

            var map = new[,]
            {
                { 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 0, 0 },
                { 0, 0, 0, 1, 0, 0 },
                { 0, 1, 1, 1, 0, 0 },
                { 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0 },
            };
            
            // act
            var route = service.FindRoute(new Vector2Int(0, 0), new Vector2Int(4, 4), new MapMock(map));

            // assert
            CollectionAssert.AreEquivalent(new[]
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(0, 1),
                    new Vector2Int(0, 2),
                    new Vector2Int(0, 3),
                    new Vector2Int(0, 4),
                    new Vector2Int(1, 4),
                    new Vector2Int(2, 4),
                    new Vector2Int(3, 4),
                    new Vector2Int(4, 4),
                },
                route);
        }

        private class MapMock : IMap
        {
            public const int FreeCell = 0;
            public const int Obstacle = 1;
            
            private readonly int[,] _map;

            public MapMock(int[,] map)
            {
                _map = map;
            }

            public IEnumerable<Vector2Int> FindNeighbours(Vector2Int point) 
                => FindNeighboursInternal(point).Where(p => p != null).Select(p => p.Value);

            IEnumerable<Vector2Int?> FindNeighboursInternal(Vector2Int point)
            {
                //yield return Shift(point, -1, -1);
                yield return Shift(point, -1, 0);
                //yield return Shift(point, -1, 1);
                yield return Shift(point, 0, -1);
                //yield return Shift(point, 0, 0); Ignore Centre
                yield return Shift(point, 0, 1);
                //yield return Shift(point, 1, -1);
                yield return Shift(point, 1, 0);
                //yield return Shift(point, 1, 1);
            }

            private Vector2Int? Shift(Vector2Int point, int x, int y)
            {
                var index = point + new Vector2Int(x, y);
                if (GetCell(index.x, index.y) == FreeCell)
                    return index;
                return null;
            }
            
            private int? GetCell(int x, int y)
            {
                var maxX = _map.GetLength(0);
                var maxY = _map.GetLength(1);
                if (x >= maxX || y >= maxY || x < 0 || y < 0)
                    return null;
                return _map[x, y];
            }
        }
    }
}