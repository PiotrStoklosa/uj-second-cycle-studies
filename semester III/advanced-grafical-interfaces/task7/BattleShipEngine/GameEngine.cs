using System.Collections.Generic;
using System;
using System.Linq;

namespace BattleshipEngine
{
    public class GameEngine
    {
        private Dictionary<Tuple<int, int>, TileState> playerTileState;
        private Dictionary<Tuple<int, int>, TileState> computerTileState;

        private List<Ship> playerShips;
        private List<Ship> computerShips;

        private bool playerTurn = true;

        private void InitializePlayerAndComputerTileStates()
        {
            playerTileState = new Dictionary<Tuple<int, int>, TileState>();
            computerTileState = new Dictionary<Tuple<int, int>, TileState>();

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    var position = Tuple.Create(row, col);
                    playerTileState[position] = TileState.Empty;
                    computerTileState[position] = TileState.Empty;
                }
            }
        }
        private void initialize()
        {
            InitializePlayerAndComputerTileStates();
            playerShips = new List<Ship>();
            computerShips = new List<Ship>();
        }

        public void start(List<Tuple<int, int>> playerShipTiles)
        {
            if (!IsValidShipPlacement(playerShipTiles))
            {
                return;
            }
            initialize();
            computerShips = ShipGenerator.GenerateComputerShips();
            playerShips = CreateShips(playerShipTiles);
        }

        private List<Ship> CreateShips(List<Tuple<int, int>> selectedTiles)
        {
            var ships = new List<Ship>();
            var visited = new HashSet<Tuple<int, int>>();

            foreach (var tile in selectedTiles)
            {
                if (visited.Contains(tile))
                    continue;

                var shipPositions = GetShip(tile, selectedTiles);
                ships.Add(new Ship(shipPositions));
                foreach (var pos in shipPositions)
                {
                    visited.Add(pos);
                }
            }

            return ships;
        }

        public static bool IsValidShipPlacement(List<Tuple<int, int>> selectedTiles)
        {
            if (selectedTiles.Count != 20)
            {
                return false;
            }

            var ships = new List<List<Tuple<int, int>>>();
            var visited = new HashSet<Tuple<int, int>>();
            foreach (var tile in selectedTiles)
            {
                if (visited.Contains(tile))
                {
                    continue;
                }

                var ship = GetShip(tile, selectedTiles);
                if (ship.Count < 1)
                {
                    return false;
                }

                ships.Add(ship);
                foreach (var part in ship)
                {
                    visited.Add(part);
                }
            }

            var shipSizes = ships.Select(s => s.Count).ToList();
            shipSizes.Sort();
            if (!shipSizes.SequenceEqual(new List<int> { 1, 1, 1, 1, 2, 2, 2, 3, 3, 4 }))
            {
                return false;
            }

            foreach (var ship in ships)
            {
                foreach (var part in ship)
                {
                    if (HasAdjacentShip(part, ship, selectedTiles))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static List<Tuple<int, int>> GetShip(Tuple<int, int> startTile, List<Tuple<int, int>> selectedTiles)
        {
            var ship = new List<Tuple<int, int>> { startTile };
            var queue = new Queue<Tuple<int, int>>();
            queue.Enqueue(startTile);

            while (queue.Count > 0)
            {
                var tile = queue.Dequeue();
                var neighbors = GetNeighbors(tile);
                foreach (var neighbor in neighbors)
                {
                    if (selectedTiles.Contains(neighbor) && !ship.Contains(neighbor))
                    {
                        ship.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return ship;
        }

        private static List<Tuple<int, int>> GetNeighbors(Tuple<int, int> tile)
        {
            var neighbors = new List<Tuple<int, int>>
            {
                Tuple.Create(tile.Item1 - 1, tile.Item2),
                Tuple.Create(tile.Item1 + 1, tile.Item2),
                Tuple.Create(tile.Item1, tile.Item2 - 1),
                Tuple.Create(tile.Item1, tile.Item2 + 1),
                Tuple.Create(tile.Item1 - 1, tile.Item2 - 1),
                Tuple.Create(tile.Item1 - 1, tile.Item2 + 1),
                Tuple.Create(tile.Item1 + 1, tile.Item2 - 1),
                Tuple.Create(tile.Item1 + 1, tile.Item2 + 1)
            };

            return neighbors.Where(t => t.Item1 >= 0 && t.Item1 < 10 && t.Item2 >= 0 && t.Item2 < 10).ToList();
        }

        private static bool HasAdjacentShip(Tuple<int, int> tile, List<Tuple<int, int>> ship, List<Tuple<int, int>> selectedTiles)
        {
            var neighbors = GetNeighbors(tile);

            foreach (var neighbor in neighbors)
            {
                if (!ship.Contains(neighbor) && selectedTiles.Contains(neighbor))
                {
                    return true;
                }
            }

            return false;
        }

        public PlayerResponse PlayerTurn(Tuple<int, int> shot)
        {
            if (computerTileState[shot] != TileState.Empty)
            {
                return PlayerResponse.Visited;
            }

            bool isHit = false;
            Ship hittedShip = null;

            foreach (var ship in computerShips)
            {
                if (ship.Positions.Contains(shot))
                {
                    ship.MarkTileAsHit(shot);
                    hittedShip = ship;
                    isHit = true;
                    break;
                }
            }

            if (isHit)
            {
                computerTileState[shot] = TileState.Hit;

                if (hittedShip.IsAllTilesSunk())
                {
                    MarkSurroundingFieldsAsMissed(hittedShip);

                    if (computerShips.All(ship => ship.IsAllTilesSunk()))
                    {
                        return PlayerResponse.Won;
                    }
                    else
                    {
                        return PlayerResponse.HitSunk;
                    }
                }
                else
                {
                    return PlayerResponse.Hit;
                }
            }
            else
            {
                computerTileState[shot] = TileState.Miss;
                playerTurn = false;
                return PlayerResponse.Miss;
            }
        }
        public PlayerResponse ComputerTurn()
        {
            Random rand = new Random();

            Tuple<int, int> target;
            do
            {
                int row = rand.Next(10);
                int col = rand.Next(10);
                target = Tuple.Create(row, col);
            } while (playerTileState[target] != TileState.Empty);

            bool hitShip = false;
            Ship hittedShip = null;

            foreach (var ship in playerShips)
            {
                if (ship.Positions.Contains(target))
                {
                    ship.MarkTileAsHit(target);
                    hittedShip = ship;
                    hitShip = true;
                    break;
                }
            }

            if (hitShip)
            {
                playerTileState[target] = TileState.Hit;

                if (hittedShip.IsAllTilesSunk())
                {
                    MarkSurroundingFieldsAsMissed(hittedShip);
                    return PlayerResponse.HitSunk;
                }

                if (playerShips.All(ship => ship.IsAllTilesSunk()))
                {
                    return PlayerResponse.Won;
                }
                return PlayerResponse.Hit;
            }
            else
            {
                playerTileState[target] = TileState.Miss;
                playerTurn = true;
                return PlayerResponse.Miss;
            }
        }

        private void MarkSurroundingFieldsAsMissed(Ship ship)
        {
            var tileStates = playerTurn ? computerTileState : playerTileState;

            var surroundingTiles = ship.GetNeighbors();

            foreach (var neighbor in surroundingTiles)
            {
                if (tileStates.ContainsKey(neighbor) && tileStates[neighbor] == TileState.Empty)
                {
                    tileStates[neighbor] = TileState.Miss;
                }
            }
        }
    }


}