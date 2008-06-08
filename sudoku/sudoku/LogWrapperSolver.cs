using System;

namespace sudoku
{
    public class LogWrapperSolver: ISolver
    {
        readonly ISolver actualSolver;
        public LogWrapperSolver(ISolver actualSolver)
        {
            this.actualSolver = actualSolver;
        }
        #region ISolver
        public void Execute()
        {
            DateTime startTime = DateTime.Now;
            actualSolver.Execute();
            DateTime finishTime = DateTime.Now;
            TimeSpan timeSpan = finishTime - startTime;
            Console.WriteLine("time: " + timeSpan);
        }
        #endregion
    }
}