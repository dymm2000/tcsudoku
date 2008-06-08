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
            
            for (int taskIndex = 1; taskIndex < inputData.Length; taskIndex++)
            {
                string inputLine = inputData[taskIndex];
                SudokuTable sudokuTable = CreateSudokuTable(inputLine);

                SudokuSolution sudokuSolution = new SudokuSolution(taskIndex, sudokuTable);
                ISolver solver = new SequentialSolver(sudokuSolution);
                solver.Execute();
                PrintSolution(sudokuSolution);
            }

//            foreach (string inputLine in inputData)
//            {
//                SudokuTable sudokuTable = CreateSudokuTable(inputLine);
//
//                SudokuSolution sudokuSolution = new SudokuSolution(sudokuTable);
//                ISolver solver = new LogWrapperSolver(new MultiThreadedSolver(sudokuSolution));
//                solver.Execute();
//                PrintSolution(sudokuSolution, taskIndex);
//            }
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
        static void PrintSolution(SudokuSolution sudokuSolution)
        {
            switch (sudokuSolution.SolutionsNumber)
            {
                case SudokuSolution.NO_SOLUTION:
                    Console.WriteLine("Puzzle # {0,5:} has NO solution", sudokuSolution.TaskId);
                    break;
                case SudokuSolution.UNIQUE_SOLUTION:
                    Console.WriteLine("Puzzle # {0,5:} has a UNIQUE solution", sudokuSolution.TaskId);
                    break;
                case SudokuSolution.MULTIPLE_SOLUTION:
                    Console.WriteLine("Puzzle # {0,5:} has a MULTIPLE solutions", sudokuSolution.TaskId);
                    break;
                default:
                    Console.WriteLine("Puzzle # {0,5:} has a MULTIPLE solutions", sudokuSolution.TaskId);
                    break;
            }
        }
    }
}
