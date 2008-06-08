using System.Threading;

namespace sudoku
{
    public class MultiThreadedSolver: ISolver
    {
        int actualThreadsCount;
        readonly SudokuSolution sudokuSolution;
        public MultiThreadedSolver(SudokuSolution sudokuSolution)
        {
            this.sudokuSolution = sudokuSolution;
        }
        #region ISolver
        public void Execute()
        {
            actualThreadsCount = 0;

            FindSolution(new SudokuTable(sudokuSolution.InitialTable));

            while (actualThreadsCount != 0) {}
        }
        #endregion
        void FindSolution(SudokuTable currentTable)
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
                                if (sudokuSolution.IsMultiSolutions)
                                    return;

                                SudokuTable newSudokuTable = new SudokuTable(currentTable);
                                newSudokuTable.table[rowIndex, colIndex] = newDigit;

                                Interlocked.Increment(ref actualThreadsCount);
                                Thread t = new Thread(SubExecuter);
                                t.Start(newSudokuTable);
                            }
                        }
                        return;
                    }
                }
            }
            sudokuSolution.AddSolution(currentTable);
        }

        void SubExecuter(object data)
        {
            try
            {
                SudokuTable currentSudokuTable = (SudokuTable)data;
                FindSolution(currentSudokuTable);
            }
            finally 
            {
                Interlocked.Decrement(ref actualThreadsCount);    
            }            
        }
    }
}