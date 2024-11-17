using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private int[,] board = new int[3, 3];
        private bool isPlayerXTurn = true;
        private int playerSymbol = 1;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void ChooseX_Click(object sender, RoutedEventArgs e)
        {
            playerSymbol = 1;
            StartGame();
        }

        private void ChooseO_Click(object sender, RoutedEventArgs e)
        {
            playerSymbol = 2;
            isPlayerXTurn = false;
            StartGame();
        }

        private void StartGame()
        {
            StartScreen.Visibility = Visibility.Collapsed;
            GameBoard.Visibility = Visibility.Visible;

            if (!isPlayerXTurn)
            {
                ComputerMove();
            }
        }
        private void Cell_Click(object sender, MouseButtonEventArgs e)
        {
            if (!isPlayerXTurn) return;

            Image clickedCell = (Image)sender;
            int row = int.Parse(clickedCell.Name[4].ToString());
            int col = int.Parse(clickedCell.Name[5].ToString());

            if (board[row, col] == 0)
            {
                MakeMove(row, col, playerSymbol);
                if (CheckWin(playerSymbol))
                {
                    MessageBox.Show("You win!");
                    ResetBoard();
                    return;
                }

                isPlayerXTurn = false;
                TurnText.Text = "Turn: Computer";

                ComputerMove();
            }
        }

        private void ComputerMove()
        {
            Random random = new Random();
            int row, col;

            do
            {
                row = random.Next(3);
                col = random.Next(3);
            } while (board[row, col] != 0);

            MakeMove(row, col, playerSymbol == 1 ? 2 : 1);

            if (CheckWin(playerSymbol == 1 ? 2 : 1))
            {
                MessageBox.Show("Computer wins!");
                ResetBoard();
                return;
            }

            isPlayerXTurn = true;
            TurnText.Text = "Turn: Player";
        }

        private void MakeMove(int row, int col, int symbol)
        {
            board[row, col] = symbol;
            string symbolImage = symbol == 1 ? "tic-tac-toe_x.png" : "tic-tac-toe_o.png";

            Image cell = (Image)GameGrid.Children.Cast<UIElement>()
                .First(c => Grid.GetRow(c) == row && Grid.GetColumn(c) == col);

            cell.Source = new BitmapImage(new Uri($"Images/{symbolImage}", UriKind.Relative));
        }

        private bool CheckWin(int symbol)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((board[i, 0] == symbol && board[i, 1] == symbol && board[i, 2] == symbol) ||
                    (board[0, i] == symbol && board[1, i] == symbol && board[2, i] == symbol))
                    return true;
            }

            return (board[0, 0] == symbol && board[1, 1] == symbol && board[2, 2] == symbol) ||
                   (board[0, 2] == symbol && board[1, 1] == symbol && board[2, 0] == symbol);
        }

        private void ResetBoard()
        {
            board = new int[3, 3];
            foreach (UIElement child in GameGrid.Children)
            {
                if (child is Image image)
                {
                    image.Source = new BitmapImage(new Uri("Images/blank_image.png", UriKind.Relative));
                }
            }

            StartScreen.Visibility = Visibility.Visible;
            GameBoard.Visibility = Visibility.Collapsed;
            TurnText.Text = "Turn: Player X";
            isPlayerXTurn = true;
        }
    }

}