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
}