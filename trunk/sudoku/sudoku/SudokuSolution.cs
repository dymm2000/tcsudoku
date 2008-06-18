namespace sudoku
{
    public class SudokuSolution
    {
        public const byte NO_SOLUTION = 0;
        public const byte UNIQUE_SOLUTION = 1;
        public const byte MULTIPLE_SOLUTION = 2;

        readonly int taskId;
        readonly SudokuTable initialTable;
        byte solutionsNumber;
        public int TaskId { get { return taskId; } }
        public byte SolutionsNumber { get { return solutionsNumber; } set { solutionsNumber = value; }}
        public bool IsMultiSolutions { get { return solutionsNumber >= 2; } }
        public SudokuTable InitialTable { get { return initialTable; } }

        public SudokuSolution(int taskId, SudokuTable initialTable)
        {
            this.taskId = taskId;
            this.initialTable = initialTable;
            solutionsNumber = NO_SOLUTION;
        }
        public void AddSolution(SudokuTable solutionTable)
        {
            solutionsNumber++;
        }
    }
}