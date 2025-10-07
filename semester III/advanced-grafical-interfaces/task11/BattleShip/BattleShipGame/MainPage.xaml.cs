using BattleshipEngine;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BattleshipGame
{
    public sealed partial class MainPage : Page
    {
        private const int BoardSize = 10;
        private List<Tuple<int, int>> playerSelectedTiles = new List<Tuple<int, int>>();
        private GameEngine gameEngine = new GameEngine();

        public MainPage()
        {
            this.InitializeComponent();
            InitializeBoards();
        }

        private void InitializeBoards()
        {
            CreateBoard(OpponentBoard, isInteractive: false);

            CreateBoard(PlayerBoard, isInteractive: true);
        }

        private void CreateBoard(Grid board, bool isInteractive)
        {
            board.Children.Clear();
            board.RowDefinitions.Clear();
            board.ColumnDefinitions.Clear();

            for (int i = 0; i < BoardSize; i++)
            {
                board.RowDefinitions.Add(new RowDefinition());
                board.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    var tile = new Rectangle
                    {
                        Fill = new SolidColorBrush(isInteractive ? Windows.UI.Colors.LightBlue : Windows.UI.Colors.DarkGray),
                        Stroke = new SolidColorBrush(Windows.UI.Colors.Black),
                        StrokeThickness = 1,
                        Tag = Tuple.Create(row, col),
                        IsHitTestVisible = isInteractive
                    };
                    if (isInteractive)
                    {
                        tile.Tapped += OnTileTapped;
                    }
                    else
                    {
                        tile.Tapped += OnOpponentTileTapped;
                    }

                    Grid.SetRow(tile, row);
                    Grid.SetColumn(tile, col);
                    board.Children.Add(tile);
                }
            }
        }



        private void OnTileTapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is Rectangle tile && tile.Tag is Tuple<int, int> position)
            {
                if (!playerSelectedTiles.Contains(position))
                {
                    tile.Fill = new SolidColorBrush(Windows.UI.Colors.Blue);
                    playerSelectedTiles.Add(position);
                }
                else
                {
                    tile.Fill = new SolidColorBrush(Windows.UI.Colors.LightBlue);
                    playerSelectedTiles.Remove(position);
                }
            }
        }
        private void OnConfirmShipsClicked(object sender, RoutedEventArgs e)
        {
            if (GameEngine.IsValidShipPlacement(playerSelectedTiles))
            {
                ConfirmShipsButton.Visibility = Visibility.Collapsed;
                TurnText.Visibility = Visibility.Visible;
                TurnText.Text = "Twoja tura";
                PlayerBoard.IsHitTestVisible = false;
                OpponentBoard.IsHitTestVisible = true;
                EnableOpponentTiles();
                gameEngine.start(playerSelectedTiles);
            }
            else
            {
                var dialog = new ContentDialog
                {
                    Title = "Błąd",
                    Content = "Nieprawidłowe rozmieszczenie statków!",
                    CloseButtonText = "OK"
                };
                _ = dialog.ShowAsync();
            }
        }

        private async void OnOpponentTileTapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is Rectangle tile && tile.Tag is Tuple<int, int> position)
            {
                tile.IsHitTestVisible = false;
                var result = gameEngine.PlayerTurn(position);
                if (result == PlayerResponse.Won)
                {
                    tile.Fill = new SolidColorBrush(Windows.UI.Colors.Red);

                    DisableAllTiles();
                    var dialog = new ContentDialog
                    {
                        Title = "Gratulacje!",
                        Content = "Wygrałeś!"
                    };
                    _ = dialog.ShowAsync();
                }
                else if (result == PlayerResponse.Miss)
                {
                    TurnText.Text = "Tura przeciwnika";
                    TurnText.Visibility = Visibility.Visible;
                    tile.Fill = new SolidColorBrush(Windows.UI.Colors.White);
                    PlayerResponse computerResponse = PlayerResponse.Hit;
                    Tuple<int, int> computerShot;
                    DisableAllTiles();

                    while (computerResponse != PlayerResponse.Miss)
                    {
                        await Task.Delay(1000);
                        (computerResponse, computerShot) = gameEngine.ComputerTurn();
                        if (computerResponse == PlayerResponse.Won)
                        {
                            foreach (var child in PlayerBoard.Children)
                            {
                                if (child is Rectangle tile2 && tile2.Tag is Tuple<int, int> position2)
                                {
                                    if (position2.Equals(computerShot))
                                    {
                                        tile2.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
                                        break;
                                    }
                                }
                            }
                            var dialog = new ContentDialog
                            {
                                Title = "Przegrałeś!",
                                Content = "Komputer wygrał!"
                            };
                            _ = dialog.ShowAsync();
                            DisableAllTiles();
                            return;
                        }
                        else if (computerResponse == PlayerResponse.Hit || computerResponse == PlayerResponse.HitSunk)
                        {
                            foreach (var child in PlayerBoard.Children)
                            {
                                if (child is Rectangle tile2 && tile2.Tag is Tuple<int, int> position2)
                                {
                                    if (position2.Equals(computerShot))
                                    {
                                        tile2.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
                                        break;
                                    }
                                }
                            }

                        }
                        else if (computerResponse == PlayerResponse.Miss)
                        {
                            foreach (var child in PlayerBoard.Children)
                            {
                                if (child is Rectangle tile2 && tile2.Tag is Tuple<int, int> position2)
                                {
                                    if (position2.Equals(computerShot))
                                    {
                                        tile2.Fill = new SolidColorBrush(Windows.UI.Colors.White);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    EnableOpponentTiles();
                }
                else if (result == PlayerResponse.Hit)
                {
                    tile.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
                }
                else if (result == PlayerResponse.HitSunk)
                {
                    tile.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
                    MarkSurroundingTilesAsWhite(position);
                }
                TurnText.Text = "Twoja tura!";
                TurnText.Visibility = Visibility.Visible;
            }
        }

        private void MarkSurroundingTilesAsWhite(Tuple<int, int> sunkTilePosition)
        {
            var sunkShipTiles = gameEngine.GetSurroundingTilesForShip(sunkTilePosition);

            var surroundingTiles = new HashSet<Tuple<int, int>>();

            foreach (var tilePosition in sunkShipTiles)
            {
                var tile = GetTileAtPosition(tilePosition.Item1, tilePosition.Item2);
                if (tile != null)
                {
                    var currentColor = (tile.Fill as SolidColorBrush)?.Color;

                    if (currentColor != Windows.UI.Colors.Red)
                    {
                        tile.Fill = new SolidColorBrush(Windows.UI.Colors.White);
                    }
                }
            }
        }

        private Rectangle GetTileAtPosition(int row, int col)
        {
            foreach (var child in OpponentBoard.Children)
            {
                if (child is Rectangle tile && tile.Tag is Tuple<int, int> position && position.Item1 == row && position.Item2 == col)
                {
                    return tile;
                }
            }
            return null;
        }

        private void DisableAllTiles()
        {
            foreach (var child in PlayerBoard.Children)
            {
                if (child is Rectangle tile)
                {
                    tile.IsHitTestVisible = false;
                }
            }

            foreach (var child in OpponentBoard.Children)
            {
                if (child is Rectangle tile)
                {
                    tile.IsHitTestVisible = false;
                }
            }
        }
        private void EnableOpponentTiles()
        {
            foreach (var child in OpponentBoard.Children)
            {
                if (child is Rectangle tile)
                {
                    var fillColor = (tile.Fill as SolidColorBrush)?.Color;
                    if (fillColor != Windows.UI.Colors.White && fillColor != Windows.UI.Colors.Red)
                    {
                        tile.IsHitTestVisible = true;
                    }
                }
            }
        }

    }
}
