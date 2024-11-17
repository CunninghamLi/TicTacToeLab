using System;

public enum Player
{
    None,
    X,
    O
}

public class Board
{
    private Player[,] grid = new Player[3, 3];
    public Player CurrentPlayer { get; set; } = Player.X;

    public Board()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                grid[i, j] = Player.None;
            }
        }
    }

    public bool Select(int row, int col)
    {
        if (grid[row, col] == Player.None)
        {
            grid[row, col] = CurrentPlayer;
            CurrentPlayer = (CurrentPlayer == Player.X) ? Player.O : Player.X;
            return true;
        }
        return false;
    }

    public Player CheckWin()
    {
        for (int i = 0; i < 3; i++)
        {
            if (grid[i, 0] == grid[i, 1] && grid[i, 1] == grid[i, 2] && grid[i, 0] != Player.None)
                return grid[i, 0];
            if (grid[0, i] == grid[1, i] && grid[1, i] == grid[2, i] && grid[0, i] != Player.None)
                return grid[0, i];
        }

        if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2] && grid[0, 0] != Player.None)
            return grid[0, 0];
        if (grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0] && grid[0, 2] != Player.None)
            return grid[0, 2];

        return Player.None;
    }
}

