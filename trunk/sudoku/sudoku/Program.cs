using System;
using System.Collections.Generic;
using System.IO;

namespace sudoku
{
    public struct SudokuTable
    {
        public byte tableSize;
        public byte sub_block_col_size;
        public byte sub_block_row_size;
        public byte[,] table;
        public SudokuTable(byte tableSize)
        {
            this.tableSize = tableSize;
            sub_block_col_size = (byte)(tableSize / 2);
            sub_block_row_size = (byte)(tableSize / sub_block_col_size);
            table = new byte[tableSize, tableSize];
        }
        public SudokuTable(SudokuTable sudokuTable)
        {
            tableSize = sudokuTable.tableSize;
            table = (byte[,]) sudokuTable.table.Clone();
            sub_block_col_size = sudokuTable.sub_block_col_size;
            sub_block_row_size = sudokuTable.sub_block_row_size;
        }
        public bool CheckIfDigitEnabled(byte newDigit, byte rowIndex, byte colIndex)
        {
            return
                ifDigitEnabledInRow(newDigit, rowIndex) &&
                ifDigitEnabledInCol(newDigit, colIndex) &&                 
                ifDigitEnabledInSubBlock(newDigit, rowIndex, colIndex);
        }
        bool ifDigitEnabledInRow(byte newDigit, byte rowIndex)
        {
            for (byte index = 0; index < tableSize; index++)
            {
                if (table[rowIndex, index] == newDigit)
                    return false;
            }
            return true;
        }
        bool ifDigitEnabledInCol(byte newDigit, byte colIndex)
        {
            for (byte index = 0; index < tableSize; index++)
            {
                if (table[index, colIndex] == newDigit)
                    return false;                
            }
            return true;
        }
        bool ifDigitEnabledInSubBlock(byte newDigit, byte rowIndex, byte colIndex)
        {
            byte block_row_index = (byte) (rowIndex/sub_block_row_size);
            byte block_col_index = (byte) (colIndex/sub_block_col_size);

            for (byte rowI = (byte) (block_row_index * sub_block_row_size); rowI < (block_row_index + 1) * sub_block_row_size; rowI++)
            {
                for (byte colI = (byte) (block_col_index * sub_block_col_size); colI < (block_col_index + 1) * sub_block_col_size; colI++)
                {
                    if (table[rowI, colI] == newDigit)
                        return false;
                }
            }
            return true;
        }
    }
    public class SudokuSolution
    {
        public const int NO_SOLUTION = 0;
        public const int UNIQUE_SOLUTION = 1;
        public const int MULTIPLE_SOLUTION = 2;

        readonly SudokuTable initialTable;
        int solutionsNumber;
        readonly List<SudokuTable> solutionTables = new List<SudokuTable>();
        public int SolutionsNumber { get { return solutionsNumber; } set { solutionsNumber = value; }}
        public bool IsMultiSolutions { get { return solutionsNumber >= 2; } }
        public SudokuTable InitialTable { get { return initialTable; } }

        public SudokuSolution(SudokuTable initialTable)
        {
            this.initialTable = initialTable;
            solutionsNumber = NO_SOLUTION;
        }
        public void AddSolution(SudokuTable solutionTable)
        {
            solutionsNumber++;
            solutionTables.Add(solutionTable);
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
                FindSolution(sudokuSolution, new SudokuTable(sudokuSolution.InitialTable));
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
        static void FindSolution(SudokuSolution sudokuSolution, SudokuTable currentTable)
        {
            if (sudokuSolution.IsMultiSolutions)
                return;

            byte tableSize = currentTable.tableSize;

            for (byte rowIndex = 0; rowIndex < tableSize; rowIndex++)
            {
                for (byte colIndex = 0; colIndex < tableSize; colIndex++)
                {
                    bool emptyCell = currentTable.table[rowIndex, colIndex] == 0;
                    if (emptyCell)
                    {
                        for (byte newDigit = 1; newDigit <= tableSize; newDigit++)
                        {
                            bool enabledDigit = currentTable.CheckIfDigitEnabled(newDigit, rowIndex, colIndex);
                            if (enabledDigit)
                            {
                                SudokuTable newSudokuTable = new SudokuTable(currentTable);
                                newSudokuTable.table[rowIndex, colIndex] = newDigit;
                                FindSolution(sudokuSolution, newSudokuTable);
                                if (sudokuSolution.IsMultiSolutions)
                                    return;
                            }
                        }
                        return;
                    }
                }
            }
            sudokuSolution.AddSolution(currentTable);
        }
    }
}
