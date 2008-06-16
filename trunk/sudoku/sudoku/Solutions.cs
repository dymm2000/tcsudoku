using System.Threading;

namespace sudoku
{
    public class Solutions
    {
        const int NONE_SOLUTION = 255;
        readonly byte[] allSolutions;
        Thread thread;
        public Solutions(int solutionsCount)
        {
            allSolutions = new byte[solutionsCount];
            for (int i = 0; i < allSolutions.Length; i++)
            {
                allSolutions[i] = NONE_SOLUTION;
            }
        }

        public void StartThread()
        {
            thread = new Thread(ExecutePrinintg);
            thread.Start();
        }
        public void Wait()
        {
            thread.Join();
        }

        void ExecutePrinintg()
        {
            for (int i = 0; i < allSolutions.Length; i++)
            {
                while (allSolutions[i] == NONE_SOLUTION)
                {
                }
                PrintServices.PrintSolution(i, allSolutions[i]);
            }
        }
        public void AddSolution(int taskIndex, byte solutionsNumber)
        {
            allSolutions[taskIndex] = solutionsNumber;
        }
    }
}