using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Board
    {
        public enum Player { None, X, O }
        private Player[,] grid = new Player[3, 3];
        public Player CurrentPlayer { get; private set; } = Player.X;

        public bool Select(int row, int column)
        {
            if (grid[row, column] != Player.None) return false;

            grid[row, column] = CurrentPlayer;
            CurrentPlayer = (CurrentPlayer == Player.X) ? Player.O : Player.X;
            return true;
        }

        public Player CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (grid[i, 0] != Player.None && grid[i, 0] == grid[i, 1] && grid[i, 1] == grid[i, 2]) return grid[i, 0];
                if (grid[0, i] != Player.None && grid[0, i] == grid[1, i] && grid[1, i] == grid[2, i]) return grid[0, i];
            }

            if (grid[0, 0] != Player.None && grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2]) return grid[0, 0];
            if (grid[0, 2] != Player.None && grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0]) return grid[0, 2];

            return Player.None;
        }

        public void ResetBoard()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    grid[i, j] = Player.None;

            CurrentPlayer = Player.X;
        }
    }
}

