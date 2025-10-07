using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipEngine
{
    public class Ship
    {
        public List<Tuple<int, int>> Positions { get; private set; }
        public int Size => Positions.Count;
        public bool IsSunk => Positions.All(pos => IsTileSunk(pos));

        private HashSet<Tuple<int, int>> hitPositions;

        public Ship(List<Tuple<int, int>> positions)
        {
            Positions = positions;
            hitPositions = new HashSet<Tuple<int, int>>();
        }

        public void MarkTileAsHit(Tuple<int, int> position)
        {
            if (Positions.Contains(position))
            {
                hitPositions.Add(position);
            }
        }

        private bool IsTileSunk(Tuple<int, int> position)
        {
            return hitPositions.Contains(position);
        }

        public bool IsAllTilesSunk()
        {
            return IsSunk;
        }

        public IEnumerable<Tuple<int, int>> GetNeighbors()
        {
            var neighbors = new List<Tuple<int, int>>();

            foreach (var position in Positions)
            {
                int row = position.Item1;
                int col = position.Item2;

                var possibleNeighbors = new List<Tuple<int, int>>
            {
                Tuple.Create(row - 1, col),
                Tuple.Create(row + 1, col),
                Tuple.Create(row, col - 1),
                Tuple.Create(row, col + 1),
                Tuple.Create(row - 1, col - 1),
                Tuple.Create(row - 1, col + 1),
                Tuple.Create(row + 1, col - 1),
                Tuple.Create(row + 1, col + 1)
            };

                foreach (var neighbor in possibleNeighbors)
                {
                    if (IsWithinBounds(neighbor) && !Positions.Contains(neighbor))
                    {
                        neighbors.Add(neighbor);
                    }
                }
            }

            return neighbors.Distinct();
        }

        private bool IsWithinBounds(Tuple<int, int> position)
        {
            return position.Item1 >= 0 && position.Item1 < 10 && position.Item2 >= 0 && position.Item2 < 10;
        }

    }
}