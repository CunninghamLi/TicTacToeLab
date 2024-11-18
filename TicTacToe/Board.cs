using System;

namespace TicTacToe
{
    public enum Player { None, X, O }

    public class Board
    {
        private Player[,] grid = new Player[3, 3];

        public bool Select(int row, int col, Player player)
        {
            if (grid[row, col] == Player.None)
            {
                grid[row, col] = player;
                return true;
            }
            return false;
        }

        public Player GetPlayerAt(int row, int col)
        {
            return grid[row, col];
        }

        public Player CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (grid[i, 0] != Player.None && grid[i, 0] == grid[i, 1] && grid[i, 1] == grid[i, 2])
                    return grid[i, 0];
                if (grid[0, i] != Player.None && grid[0, i] == grid[1, i] && grid[1, i] == grid[2, i])
                    return grid[0, i];
            }

            if (grid[0, 0] != Player.None && grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2])
                return grid[0, 0];
            if (grid[0, 2] != Player.None && grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0])
                return grid[0, 2];

            return Player.None;
        }

        public void Reset()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    grid[i, j] = Player.None;
        }
    }
}