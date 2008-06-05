using System;
using System.IO;

namespace sudoku
{
    public struct SudokuTable
    {
        public byte tableSize;
        public byte[,] table;
        public SudokuTable(byte tableSize)
        {
            this.tableSize = tableSize;
            table = new byte[tableSize,tableSize];
        }
        public SudokuTable(SudokuTable sudokuTable)
        {
            tableSize = sudokuTable.tableSize;
            table = (byte[,]) sudokuTable.table.Clone();
        }
    }
    public class SudokuSolution
    {
        public const int UNDEFINED_SOLUTION = -1;
        public const int NO_SOLUTION = 0;
        public const int UNIQUE_SOLUTION = 1;
        public const int MULTIPLE_SOLUTION = 2;

        readonly SudokuTable initialTable;
        int solutionsNumber;
        public int SolutionsNumber { get { return solutionsNumber; } set { solutionsNumber = value; }}
        public SudokuTable InitialTable { get { return initialTable; } }
        public SudokuSolution(SudokuTable initialTable)
        {
            this.initialTable = initialTable;
            solutionsNumber = UNDEFINED_SOLUTION;
        }
    }

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
                FindSolution(sudokuSolution);
                PrintSolution(sudokuSolution, taskIndex++);
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
                case SudokuSolution.UNDEFINED_SOLUTION:
                    Console.WriteLine("Puzzle # {0} has N/A solution", outputTaskIndex);
                    break;
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
        static void FindSolution(SudokuSolution sudokuSolution)
        {
            sudokuSolution.SolutionsNumber = 1;
            SudokuTable initialSudokuTable = new SudokuTable(sudokuSolution.InitialTable);
            SudokuTable workSudokuTable = new SudokuTable(initialSudokuTable);
            byte tableSize = initialSudokuTable.tableSize;

            for (byte colIndex = 0; colIndex < tableSize; colIndex++)
            {
                for (byte rowIndex = 0; rowIndex < tableSize; rowIndex++)
                {
                    if (initialSudokuTable.table[colIndex, rowIndex] != 0)
                    {
                        for (byte newDigit = 1; newDigit<= tableSize; newDigit++)
                        {
                            bool enabledDigit = CheckIfDigitEnabled(workSudokuTable, newDigit, colIndex, rowIndex);
                        }
                    }
                }
            }
        }

        static bool CheckIfDigitEnabled(SudokuTable table, byte digit, byte colIndex, byte rowIndex)
        {
            throw new NotImplementedException();
        }
    }
}
