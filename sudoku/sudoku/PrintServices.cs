using System;

namespace sudoku
{
    public class PrintServices
    {
        public static void PrintSolution(int taskId, byte solutionsNumber)
        {
            switch (solutionsNumber)
            {
                case SudokuSolution.NO_SOLUTION:
                    Console.WriteLine("Puzzle # {0,5:} has NO solution", taskId);
                    break;
                case SudokuSolution.UNIQUE_SOLUTION:
                    Console.WriteLine("Puzzle # {0,5:} has a UNIQUE solution", taskId);
                    break;
                case SudokuSolution.MULTIPLE_SOLUTION:
                    Console.WriteLine("Puzzle # {0,5:} has a MULTIPLE solutions", taskId);
                    break;
                default:
                    Console.WriteLine("Puzzle # {0,5:} has a MULTIPLE solutions", taskId);
                    break;
            }
        }
    }
}