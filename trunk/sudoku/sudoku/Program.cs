using System;
using System.IO;

namespace sudoku
{
    class Program
    {
        static readonly byte charZeroCode = BitConverter.GetBytes('0')[0];
        static void Main(string[] args)
        {
            string inputFileName = args[0];
            string[] inputData = File.ReadAllLines(inputFileName);
            int taskIndex = 1;
            foreach (string inputLine in inputData)
            {
                SudokuTable sudokuTable = CreateSudokuTable(inputLine);

                SudokuSolution sudokuSolution = new SudokuSolution(sudokuTable);
                ISolver solver = new LogWrapperSolver(new SequentialSolver(sudokuSolution));
                solver.Execute();
                PrintSolution(sudokuSolution, taskIndex);
                taskIndex++;
            }

            foreach (string inputLine in inputData)
            {
                SudokuTable sudokuTable = CreateSudokuTable(inputLine);

                SudokuSolution sudokuSolution = new SudokuSolution(sudokuTable);
                ISolver solver = new LogWrapperSolver(new MultiThreadedSolver(sudokuSolution));
                solver.Execute();
                PrintSolution(sudokuSolution, taskIndex);
            }
        }
        static SudokuTable CreateSudokuTable(string inputLine)
        {
            byte sudokuSize = (byte)Math.Sqrt(inputLine.Length);
            SudokuTable sudokuTable = new SudokuTable(sudokuSize);

            for (int digitIndex = 0; digitIndex < inputLine.Length; digitIndex++)
            {
                int rowIndex = digitIndex / sudokuSize;
                int colIndex = digitIndex % sudokuSize;
                char inputDigit = inputLine[digitIndex];
                if (inputDigit == '*')
                    sudokuTable.table[rowIndex, colIndex] = 0;
                else
                    sudokuTable.table[rowIndex, colIndex] = (byte)(BitConverter.GetBytes(inputDigit)[0] - charZeroCode);
            }
            return sudokuTable;
        }
        static void PrintSolution(SudokuSolution sudokuSolution, int taskIndex)
        {
            string outputTaskIndex = taskIndex.ToString().PadLeft(2);
            switch (sudokuSolution.SolutionsNumber)
            {
                case SudokuSolution.NO_SOLUTION:
                    Console.WriteLine("Puzzle # {0} has NO solution", outputTaskIndex);
                    break;
                case SudokuSolution.UNIQUE_SOLUTION:
                    Console.WriteLine("Puzzle # {0} has a UNIQUE solution", outputTaskIndex);
                    break;
                case SudokuSolution.MULTIPLE_SOLUTION:
                    Console.WriteLine("Puzzle # {0} has a MULTIPLE solutions", outputTaskIndex);
                    break;
                default:
                    Console.WriteLine("Puzzle # {0} has a MULTIPLE solutions", outputTaskIndex);
                    break;
            }
        }
    }
}
