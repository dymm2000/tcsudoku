using System;
using System.Diagnostics;
using System.IO;

namespace sudoku
{
    class Program
    {
        static readonly byte charZeroCode = BitConverter.GetBytes('0')[0];
        static void Main(string[] args)
        {
            TimeSpan fullTime;
            TimeSpan processingTime = new TimeSpan();
            TimeSpan printingTime = new TimeSpan();

            DateTime startFullTime = DateTime.Now;

            string inputFileName = args[0];
            DateTime startLoadingData = DateTime.Now;
            string[] inputData = File.ReadAllLines(inputFileName);
            TimeSpan loadingTime = DateTime.Now - startLoadingData;
            
            for (int taskIndex = 0; taskIndex < inputData.Length; taskIndex++)
            {
                DateTime startProcessingTime = DateTime.Now;
                string inputLine = inputData[taskIndex];
                SudokuTable sudokuTable = CreateSudokuTable(inputLine);

                SudokuSolution sudokuSolution = new SudokuSolution(taskIndex + 1, sudokuTable);
                ISolver solver = new SequentialSolver(sudokuSolution);
                solver.Execute();
                processingTime += (DateTime.Now - startProcessingTime);

                DateTime startPrintingTime = DateTime.Now;
                PrintSolution(sudokuSolution);
                printingTime += DateTime.Now - startPrintingTime;
            }
            fullTime = DateTime.Now - startFullTime;

            if (fullTime.Ticks > 0)
            {
                Debug.WriteLine(String.Format("loading data time: {0}; {1:00.00}%", loadingTime,
                                              ((double) loadingTime.Ticks/(double) fullTime.Ticks)*100));
                Debug.WriteLine(String.Format("processing time: {0}; {1:00.00}%", processingTime,
                                              ((double) processingTime.Ticks/(double) fullTime.Ticks)*100));
                Debug.WriteLine(String.Format("printing time: {0}; {1:00.00}%", printingTime,
                                              ((double) printingTime.Ticks/(double) fullTime.Ticks)*100));
            }
            Debug.WriteLine("full time: " + fullTime);
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
