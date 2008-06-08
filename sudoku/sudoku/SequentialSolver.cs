namespace sudoku
{
    public class SequentialSolver : ISolver
    {
        readonly SudokuSolution sudokuSolution;
        public SequentialSolver(SudokuSolution sudokuSolution)
        {
            this.sudokuSolution = sudokuSolution;
        }
        #region ISolver
        public void Execute()
        {
            FindSolution(new SudokuTable(sudokuSolution.InitialTable));
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
                                SudokuTable newSudokuTable = new SudokuTable(currentTable);
                                newSudokuTable.table[rowIndex, colIndex] = newDigit;
                                FindSolution(newSudokuTable);
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