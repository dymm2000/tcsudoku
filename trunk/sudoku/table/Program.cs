using System;

namespace table
{
    class Program
    {
        const int size = 5;
        static void Main(string[] args)
        {
            char[,] table = new char[5,5]
                                {
                                    {' ', 'a', 'm', ' ', ' '},
                                    {' ', 'm', 'a', 'm', 'a'},
                                    {'m', 'a', 'm', ' ', ' '},
                                    {' ', 'm', 'a', 'm', ' '},
                                    {' ', 'a', ' ', 'a', ' '}
                                };
            int counter = 0;
            Calculate(table, "mama", ref counter);
            Console.WriteLine("counter: " + counter);
        }

        static void Calculate(char[,] table, string keyLine, ref int counter)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    start(i, j, table, keyLine, ref counter);
                }
            }
        }

        static void start(int row, int col, char[,] table, string keyLine, ref int counter)
        {
            if (row < 0 || col < 0)
                return;
            if (row >= size || col >= size)
                return;

            if (table[row, col] != keyLine[0])
                return;

            if (keyLine.Length == 1)
            {
                counter++;
                return;
            }

            keyLine = keyLine.Substring(1);
            char[,] newTable = (char[,]) table.Clone();
            newTable[row, col] = ' ';
            
            start(row - 1, col, newTable, keyLine, ref counter);            
            start(row, col - 1, newTable, keyLine, ref counter);            
            start(row, col + 1, newTable, keyLine, ref counter);            
            start(row + 1, col, newTable, keyLine, ref counter);            
        }
    }
}
