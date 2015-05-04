http://softwarecontests.intel.com/threadingchallenge/index.php

**Sudoku** is a logic puzzle that is solved by placing numbers into a grid such that the same number is not repeated within a row, a column, or a sub-block of the grid.  The most common grid size is 9x9.  Thus, each row, each column, and each of the nine 3x3 non-overlapping sub-blocks may contain one instance of the integers 1-9.  Besides the 9x9 grid, it is possible to use 16x16 and 6x6 grids.  There are also variations that do not use square sub-blocks.

**Problem:** Write a threaded application to decide if 6x6 Sudoku puzzles have a unique solution or not.  Correct solutions to this variation have the numbers 1-6 uniquely placed in each row and column.  Also, the non-overlapping 2x3 sub-blocks (2 rows, 3 columns) must contain a single instance of the 6 integers.
```
For example:
2 3 1 | 4 5 6
6 5 4 | 3 2 1
-------------
1 4 3 | 5 6 2
5 6 2 | 1 3 4
-------------
3 1 6 | 2 4 5
4 2 5 | 6 1 3
```

**Input file description:** The name of the input file is to be given to the application as it begins execution, nominally as a command line argument.  The file will contain some number of lines, each with 36 non-blank characters.  Each line will represent the start position of a possible 6x6 Sudoku puzzle layed out in row-major order.  Blanks in the inital puzzle will be represented by the asterisk (`*`) character. End of File will signify the end of the input.

**Output:** Output will be to stdout.  There should be some indication of whether or not each input puzzle contains a unique solution, has no solution, or has multiple correct solutions.
```
Input Example with three puzzles:
*314*******1**356**621**3*******561*
***4****41*2*4321**6534*5*16****6***
*****5*5*******5****5*******5*5*****
```
(The first line corresponds to the starting position of the solved puzzle above.)

**Output example:**
```
Puzzle #  1 has a UNIQUE solution
Puzzle #  2 has NO solution
Puzzle #  3 has MULTIPLE solutions
```

**Timing:** Wall-clock time (including I/O) will be used for judging.