using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace sudoku
{
    class Program
    {
        static readonly byte charZeroCode = BitConverter.GetBytes('0')[0];
        static readonly int executerCoresCount = Environment.ProcessorCount - 1;
        static string[] inputData;
        static Solutions solutions;
        static void Main(string[] args)
        {
            DateTime startFullTime = DateTime.Now;
            
            Thread[] executerThreads = new Thread[executerCoresCount];
            string inputFileName = args[0];
            inputData = File.ReadAllLines(inputFileName);
            solutions = new Solutions(inputData.Length);
            solutions.StartThread();

            for (byte threadIndex = 0; threadIndex < executerCoresCount; threadIndex++ )
            {
                executerThreads[threadIndex] = new Thread(Starter);
                executerThreads[threadIndex].Start(threadIndex);
            }
            foreach (Thread executer in executerThreads) executer.Join();
            solutions.Wait();
            TimeSpan fullTime = DateTime.Now - startFullTime;

            Debug.WriteLine("full time: " + fullTime);
        }
        static void Starter(object threadIndex)
        {
            int portionIndex = (byte) threadIndex;
            int portionSize = inputData.Length/executerCoresCount;
            int startIndex = portionIndex * portionSize;
            int finishIndex = Math.Min((portionIndex + 1) * portionSize, inputData.Length);

            for (int taskIndex = startIndex; taskIndex < finishIndex; taskIndex++)
            {
                string inputLine = inputData[taskIndex];
                SudokuTable sudokuTable = CreateSudokuTable(inputLine);

                SudokuSolution sudokuSolution = new SudokuSolution(taskIndex + 1, sudokuTable);
                ISolver solver = new SequentialSolver(sudokuSolution);
                solver.Execute();

                solutions.AddSolution(taskIndex, sudokuSolution.SolutionsNumber);
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
    }
}
