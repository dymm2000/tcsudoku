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

            string inputFileName;
            try
            {
                inputFileName = args[0];
                if (!File.Exists(inputFileName))
                    throw new Exception(String.Format("file {0} not found.", inputFileName));
            }
            catch (Exception exp)
            {
                Console.WriteLine("sudoku.exe input_sudiku_tasks_file");
                Console.WriteLine();
                Console.WriteLine("Program failed with message: {0}", exp.Message);
                return;
            }
            

            Thread[] executerThreads = new Thread[executerCoresCount];            
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
            DateTime startThreadTime = DateTime.Now;
            int portionIndex = (byte) threadIndex;
            int portionSize = (inputData.Length/executerCoresCount) + 1;
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

            TimeSpan threadTime = DateTime.Now - startThreadTime;
            Debug.WriteLine(String.Format("thread #{0} time: {1}", portionIndex, threadTime));
        }
        static SudokuTable CreateSudokuTable(string inputLine)
        {
            const char UNDEFINED_CHAR = '*';
            const byte UNDEFINED_NUMBER = 0;

            byte sudokuSize = (byte)Math.Sqrt(inputLine.Length);
            SudokuTable sudokuTable = new SudokuTable(sudokuSize);

            for (int digitIndex = 0; digitIndex < inputLine.Length; digitIndex++)
            {
                int rowIndex = digitIndex / sudokuSize;
                int colIndex = digitIndex % sudokuSize;
                char inputDigit = inputLine[digitIndex];
                if (inputDigit == UNDEFINED_CHAR)
                    sudokuTable.table[rowIndex, colIndex] = UNDEFINED_NUMBER;
                else
                    sudokuTable.table[rowIndex, colIndex] = (byte)(BitConverter.GetBytes(inputDigit)[0] - charZeroCode);
            }
            return sudokuTable;
        }
    }
}
