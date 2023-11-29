﻿using System.Collections.Generic;
using System.Linq;
using System;

public class ConnectFour
{
    public static void Main()
    {
        List<string> testList = new List<string>()
        {
            "E_Yellow", "A_Red", "C_Yellow", "C_Red", "G_Yellow", "B_Red", "D_Yellow",
            "B_Red", "A_Yellow", "E_Red", "A_Yellow", "G_Red", "G_Yellow", "D_Red"
        };

        // Test
        var t = WhoIsWinner(testList);
        // ...should return "Red"
    }

    const int ROWS = 6, COLUMNS = 7;

    public static string WhoIsWinner(List<string> piecesPositionList)
    {
        char[,] field = new char[ROWS, COLUMNS];
        int[] elementPositions = new int[COLUMNS];
        Array.Fill(elementPositions, ROWS);

        foreach (string element in piecesPositionList)
        {
            int position = element[0] - 65;
            field[--elementPositions[position], position] = element[2];

            if (CheckForWinner((0, ROWS), (0, COLUMNS - 3), (0, 1), field) != "Draw")
                return CheckForWinner((0, ROWS), (0, COLUMNS - 3), (0, 1), field);

            if (CheckForWinner((0, ROWS - 3), (0, COLUMNS), (1, 0), field) != "Draw")
                return CheckForWinner((0, ROWS - 3), (0, COLUMNS), (1, 0), field);

            if (CheckForWinner((0, ROWS - 3), (0, COLUMNS - 3), (1, 1), field) != "Draw")
                return CheckForWinner((0, ROWS - 3), (0, COLUMNS - 3), (1, 1), field);

            if (CheckForWinner((0, ROWS - 3), (3, COLUMNS), (1, -1), field) != "Draw")
                return CheckForWinner((0, ROWS - 3), (3, COLUMNS), (1, -1), field);
        }

        return "Draw";
    }

    public static string CheckForWinner((int, int) iStartEnd, (int, int) jStartEnd, (int, int) steps, char[,] field)
    {
        for (int i = iStartEnd.Item1; i < iStartEnd.Item2; i++)
        {
            for (int j = jStartEnd.Item1; j < jStartEnd.Item2; j++)
            {
                char[] chips = new char[4];

                for (int k = 0; k < 4; k++)
                    chips[k] = field[i + steps.Item1 * k, j + steps.Item2 * k];

                if (AreTheseFourConnected(chips))
                    return chips[0] == 'R' ? "Red" : "Yellow";
            }
        }

        return "Draw";
    }

    public static bool AreTheseFourConnected(params char[] four)
    {
        if (four.Contains('\0'))
            return false;

        return four.All(c => c == four[0]);
    }
}