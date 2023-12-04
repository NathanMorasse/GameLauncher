using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HourGlassUnlimited.Games.Sudoku.Models;

namespace HourGlassUnlimited.Games.Sudoku.Tools
{
    public static class BoardEncoder
    {
        public static Board DecodeBoard(string sudokuString)
        {
            if (sudokuString.Length != 81) throw new ArgumentException("Longueur de string de sudoku invalide");

            Board board = new Board();

            int index = 0;
            for (int i = 0; i < 9; i++)
            {
                ObservableCollection<Cell> row = new ObservableCollection<Cell>();
                for (int j = 0; j < 9; j++)
                {
                    Cell cell = new Cell();
                    char currentChar = sudokuString[index];

                    if (currentChar >= '1' && currentChar <= '9')
                    {
                        cell.Value = (Int64)Char.GetNumericValue(currentChar);
                    }
                    else if (currentChar == '.')
                    {
                        cell.Value = 0;
                    }
                    else
                    {
                        throw new ArgumentException($"Charactère invalide '{currentChar}' dans la string de sudoku");
                    }

                    row.Add(cell);
                    index++;
                }
                board.Grid.Add(row);
            }

            return board;
        }

        public static string EncodeBoard(Board board)
        {
            if (board == null) throw new ArgumentNullException(nameof(board));
            if (board.Grid.Count != 9 || board.Grid.Any(row => row.Count != 9))
                throw new ArgumentException("Dimensions de grille invalide");

            StringBuilder result = new StringBuilder();

            foreach (var row in board.Grid)
            {
                foreach (var cell in row)
                {
                    if (cell.Value >= 1 && cell.Value <= 9)
                    {
                        result.Append(cell.Value);
                    }
                    else if (cell.Value == 0)
                    {
                        result.Append('.');
                    }
                    else
                    {
                        throw new ArgumentException($"valeur de cellule invalide {cell.Value} dans la grille");
                    }
                }
            }

            return result.ToString();
        }
    }
}
