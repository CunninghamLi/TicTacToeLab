using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private Board board = new Board();
        private Player playerSymbol;
        private Player computerSymbol;
        private Player currentPlayer;
        private int gamesPlayed = 0;
        private int gamesWon = 0;
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            ShowMainMenu();
        }

        private void ShowMainMenu()
        {
            MainMenuGrid.Visibility = Visibility.Visible;
            GameGrid.Visibility = Visibility.Collapsed;
        }

        private void StartGame(Player player)
        {
            playerSymbol = player;
            computerSymbol = player == Player.X ? Player.O : Player.X;
            currentPlayer = Player.X;
            MainMenuGrid.Visibility = Visibility.Collapsed;
            GameGrid.Visibility = Visibility.Visible;
            ResetBoard();

            if (playerSymbol == Player.O)
            {
                ComputerMove();
            }
        }

        private void XButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame(Player.X);
        }

        private void OButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame(Player.O);
        }

        private void Cell_Click(object sender, MouseButtonEventArgs e)
        {
            if (currentPlayer != playerSymbol) return;

            var cell = sender as System.Windows.Controls.Image;
            int row = Grid.GetRow(cell);
            int col = Grid.GetColumn(cell);

            if (board.Select(row, col, currentPlayer))
            {
                UpdateCellImage(cell, currentPlayer);
                if (board.CheckWin() == currentPlayer)
                {
                    MessageBox.Show($"{currentPlayer} wins!");
                    gamesWon++;
                    gamesPlayed++;
                    ResetBoard();
                }
                else
                {
                    currentPlayer = computerSymbol;
                    TurnLabel.Content = $"Turn: Player {currentPlayer}";
                    ComputerMove();
                }
            }
        }

        private void ComputerMove()
        {
            bool moveMade = false;
            while (!moveMade)
            {
                int row = random.Next(0, 3);
                int col = random.Next(0, 3);
                if (board.Select(row, col, computerSymbol))
                {
                    var cell = GetCellAt(row, col);
                    UpdateCellImage(cell, computerSymbol);
                    moveMade = true;
                    if (board.CheckWin() == computerSymbol)
                    {
                        MessageBox.Show("Computer wins!");
                        gamesPlayed++;
                        ResetBoard();
                    }
                    else
                    {
                        currentPlayer = playerSymbol;
                        TurnLabel.Content = $"Turn: Player {currentPlayer}";
                    }
                }
            }
        }

        private void UpdateCellImage(System.Windows.Controls.Image cell, Player player)
        {
            cell.Source = new BitmapImage(new Uri($"Images/tic-tac-toe_{player.ToString().ToLower()}.png", UriKind.Relative));
        }

        private System.Windows.Controls.Image GetCellAt(int row, int col)
        {
            foreach (var child in GameGrid.Children)
            {
                if (child is System.Windows.Controls.Image img && Grid.GetRow(img) == row && Grid.GetColumn(img) == col)
                {
                    return img;
                }
            }
            return null;
        }

        private void ResetBoard()
        {
            board.Reset();
            currentPlayer = Player.X;
            TurnLabel.Content = $"Turn: Player {currentPlayer}";
            GamesPlayedLabel.Content = $"Games Played: {gamesPlayed}";
            GamesWonLabel.Content = $"Games Won: {gamesWon}";
            WinRatioLabel.Content = $"Win Ratio: {(gamesPlayed > 0 ? (gamesWon * 100 / gamesPlayed) : 0)}%";

            foreach (var child in GameGrid.Children)
            {
                if (child is System.Windows.Controls.Image img && img.Name.StartsWith("Cell"))
                {
                    img.Source = new BitmapImage(new Uri("Images/blank_image.png", UriKind.Relative));
                }
            }

            if (playerSymbol == Player.O)
            {
                ComputerMove();
            }
        }
    }
}