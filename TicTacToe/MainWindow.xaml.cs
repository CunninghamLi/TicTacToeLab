using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private Board gameBoard = new Board();

        public MainWindow()
        {
            InitializeComponent();
            UpdateTurnLabel();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image clickedImage = sender as Image;
            if (clickedImage == null) return;

            string[] position = clickedImage.Tag.ToString().Split(',');
            int row = int.Parse(position[0]);
            int column = int.Parse(position[1]);

            if (gameBoard.Select(row, column))
            {
                clickedImage.Source = new BitmapImage(new Uri(gameBoard.CurrentPlayer == Board.Player.O ? "images/tic-tac-toe_x.png" : "images/tic-tac-toe_o.png", UriKind.Relative));

                var winner = gameBoard.CheckWin();
                if (winner != Board.Player.None)
                {
                    MessageBox.Show($"{winner} wins!");
                    ResetGame();
                }
                else
                {
                    UpdateTurnLabel();
                }
            }
        }

        private void UpdateTurnLabel()
        {
            TurnLabel.Text = $"Turn: Player {(gameBoard.CurrentPlayer == Board.Player.X ? "X" : "O")}";
        }

        private void ResetGame()
        {
            gameBoard.ResetBoard();
            foreach (UIElement element in MainGrid.Children)
            {
                if (element is Image img)
                {
                    img.Source = new BitmapImage(new Uri("images/tic-tac-toe_1181538.png", UriKind.Relative));
                }
            }
            UpdateTurnLabel();
        }
    }
}