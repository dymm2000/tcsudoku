using System.Diagnostics;
using System.Threading;

namespace sudoku
{
    public class Solutions
    {
        readonly static object solutionsObject = new object();
        readonly byte[] allSolutions;
        Thread thread;
        public Solutions(int solutionsCount)
        {
            allSolutions = new byte[solutionsCount];
            for (int i = 0; i < allSolutions.Length; i++)
            {
                allSolutions[i] = 255;
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
                if (allSolutions[i] != 255)
                {
                    PrintServices.PrintSolution(i, allSolutions[i]);
                }
                else
                {
                    lock (solutionsObject)
                        Monitor.Wait(solutionsObject);
                }
            }
        }

        public void AddSolution(int taskIndex, byte solutionsNumber)
        {
            //Trace.WriteLine("taskIndex: " + taskIndex);
            allSolutions[taskIndex] = solutionsNumber;
            lock (solutionsObject)
                Monitor.Pulse(solutionsObject);
        }
    }
}