using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipEngine
{
    public static class ShipGenerator
    {
        public static List<Ship> GenerateComputerShips()
        {
            Random rand = new Random();
            List<int> shipSizes = new List<int> { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };
            var computerShips = new List<Ship>();
            var occupiedTiles = new HashSet<Tuple<int, int>>();

            foreach (var size in shipSizes)
            {
                bool placed = false;
                while (!placed)
                {
                    rand = new Random();
                    int row = rand.Next(10);
                    int col = rand.Next(10);
                    bool isHorizontal = rand.Next(2) == 0;

                    var ship = TryGenerateShip(Tuple.Create(row, col), size, isHorizontal, occupiedTiles);
                    if (ship != null)
                    {
                        placed = true;
                        computerShips.Add(ship);
                        foreach (var pos in ship.GetNeighbors())
                        {
                            occupiedTiles.Add(pos);
                        }
                        foreach (var pos in ship.Positions)
                        {
                            occupiedTiles.Add(pos);
                        }
                    }
                }
            }

            return computerShips;
        }

        public static Ship TryGenerateShip(Tuple<int, int> startPosition, int size, bool isHorizontal, HashSet<Tuple<int, int>> occupiedTiles)
        {
            var ship = new List<Tuple<int, int>>();

            for (int i = 0; i < size; i++)
            {
                int row = startPosition.Item1 + (isHorizontal ? 0 : i);
                int col = startPosition.Item2 + (isHorizontal ? i : 0);

                if (row < 0 || row >= 10 || col < 0 || col >= 10 || occupiedTiles.Contains(Tuple.Create(row, col)))
                {
                    return null;
                }

                ship.Add(Tuple.Create(row, col));
            }
            return new Ship(ship);
        }
    }
}