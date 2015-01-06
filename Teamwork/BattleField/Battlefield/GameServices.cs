namespace BattleField
{
    using System;
    using System.Collections.Generic;

    public static class GameServices
    {
        private static readonly Random RandomNumberGenerator = new Random();
        private const double LowerBoundMines = 0.15;
        private const double UpperBoundMines = 0.3;

        #region Generation Algorithm
        /// <summary>
        /// Returns the number of mines that need to be set on the field
        /// </summary>
        /// <param name="size">This sets the field's X and Y lengths</param>
        /// <returns>int</returns>
        private static int DetermineMineCount(int size)
        {
            double fields = (double)size * size;
            int lowBound = (int)(LowerBoundMines * fields);
            int upperBound = (int)(UpperBoundMines * fields);

            return RandomNumberGenerator.Next(lowBound, upperBound);
        }

        /// <summary>
        /// Generates a 2D char array to be used as a game field.
        /// The Generated array is a two-dimensional(square) array.
        /// </summary>
        /// <param name="size">The size of the array</param>
        /// <returns>char[,]</returns>
        public static char[,] GenerateField(int size)
        {
            char[,] field = new char[size, size];
            int minesCount = DetermineMineCount(size);
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    field[row, col] = '-';
                }
            }

            for (int i = 0; i < minesCount; i++)
            {
                int mineType = RandomNumberGenerator.Next('1', '6');
                int mineX = RandomNumberGenerator.Next(0, size);
                int mineY = RandomNumberGenerator.Next(0, size);
                field[mineX, mineY] = Convert.ToChar(mineType);
            }

            return field;
        }
        #endregion

        #region Logic Checks
        /// <summary>
        /// Checks a field to see whether the designated X Y coordinates exist.
        /// </summary>
        /// <param name="field">2D char array</param>
        /// <param name="row">X coordinate</param>
        /// <param name="col">Y coordinate</param>
        /// <returns>bool</returns>
        private static bool CheckField(char[,] field, int row, int col)
        {
            if (row < 0 ||
                col < 0 ||
                row >= field.GetLength(0) ||
                col >= field.GetLength(1))
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Checks whether a move is valid or not.
        /// </summary>
        /// <param name="field">2D char array.</param>
        /// <param name="row">X cooridnate</param>
        /// <param name="col">Y coordinate</param>
        /// <returns>bool</returns>
        public static bool IsValidMove(char[,] field, int row, int col)
        {
            if (!CheckField(field, row, col))
            {
                return false;
            }

            if (field[row, col] == 'X' ||
                field[row, col] == '-')
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks whether there is anything interactable left on the field
        /// </summary>
        /// <param name="field">2D char array.</param>
        /// <returns>bool</returns>
        public static bool ContainsMines(char[,] field)
        {
            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    if (field[row, col] != '-' &&
                        field[row, col] != 'X')
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        #endregion

        #region Explosion Actions
        /// <summary>
        /// Explodes the bomb at a given position.
        /// </summary>
        /// <param name="field">The game field</param>
        /// <param name="minePosition">The position of the selected mine.</param>
        public static void Explode(char[,] field, Position2D minePosition)
        {
            char mineType = field[minePosition.X, minePosition.Y];
            switch (mineType)
            {
                case '1': 
                    ExplodeOne(field, minePosition);
                    break;
                case '2':
                    ExplodeTwo(field, minePosition);
                    break;
                case '3':
                    ExplodeThree(field, minePosition);
                    break;
                case '4':
                    ExplodeFour(field, minePosition);
                    break;
                case '5':
                    ExplodeFive(field, minePosition);
                    break;
            }
        }

        private static void ExplodeOne(char[,] field, Position2D minePosition)
        {
            if (CheckField(field, minePosition.X, minePosition.Y))
            {
                field[minePosition.X, minePosition.Y] = 'X';
            }

            Position2D upperRightCorner = new Position2D(minePosition.X - 1, minePosition.Y - 1);
            if (CheckField(field, upperRightCorner.X, upperRightCorner.Y))
            {
                field[upperRightCorner.X, upperRightCorner.Y] = 'X';
            }

            Position2D upperLeftCorner = new Position2D(minePosition.X - 1, minePosition.Y + 1);
            if (CheckField(field, upperLeftCorner.X, upperLeftCorner.Y))
            {
                field[upperLeftCorner.X, upperLeftCorner.Y] = 'X';
            }

            Position2D downRightCorner = new Position2D(minePosition.X + 1, minePosition.Y - 1);
            if (CheckField(field, downRightCorner.X, downRightCorner.Y))
            {
                field[downRightCorner.X, downRightCorner.Y] = 'X';
            }

            Position2D downLeftCorner = new Position2D(minePosition.X + 1, minePosition.Y + 1);
            if (CheckField(field, downLeftCorner.X, downLeftCorner.Y))
            {
                field[downLeftCorner.X, downLeftCorner.Y] = 'X';
            }
        }

        private static void ExplodeTwo(char[,] field, Position2D minePosition)
        {
            int startRow = minePosition.X - 1;
            int endRow = minePosition.X + 1;
            for (int row = startRow; row <= endRow; row++)
            {
                int startCol = minePosition.Y - 1;
                int endCol = minePosition.Y + 1;
                for (int col = startCol; col <= endCol; col++)
                {
                    if(CheckField(field, row, col))
                    {
                        field[row, col] = 'X';
                    }
                }
            }
        }

        private static void ExplodeThree(char[,] field, Position2D minePosition)
        {
            ExplodeTwo(field, minePosition);
            Position2D up = new Position2D(minePosition.X - 2, minePosition.Y);
            if (CheckField(field, up.X, up.Y))
            {
                field[up.X, up.Y] = 'X';
            }

            Position2D down = new Position2D(minePosition.X + 2, minePosition.Y);
            if (CheckField(field, down.X, down.Y))
            {
                field[down.X, down.Y] = 'X';
            }

            Position2D left = new Position2D(minePosition.X, minePosition.Y - 2);
            if (CheckField(field, left.X, left.Y))
            {
                field[left.X, left.Y] = 'X';
            }

            Position2D right = new Position2D(minePosition.X, minePosition.Y + 2);
            if (CheckField(field, right.X, right.Y))
            {
                field[right.X, right.Y] = 'X';
            }
        }

        private static void ExplodeFour(char[,] field, Position2D minePosition)
        {
            int startRow = minePosition.X - 2;
            int endRow = minePosition.X + 2;
            for (int row = startRow; row <= endRow; row++)
            {
                int startCol = minePosition.Y - 2;
                int endCol = minePosition.Y + 2;
                for (int col = startCol; col <= endCol; col++)
                {
                    bool upRight = (row == minePosition.X - 2) && (col == minePosition.Y - 2);
                    bool upLeft = (row == minePosition.X - 2) && (col == minePosition.Y + 2);
                    bool downRight = (row == minePosition.X + 2) && (col == minePosition.Y - 2);
                    bool downLeft = (row == minePosition.X + 2) && (col == minePosition.Y + 2);

                    if (upRight) continue;
                    if (upLeft) continue;
                    if (downRight) continue;
                    if (downLeft) continue;
                    if (CheckField(field, row, col))
                    {
                        field[row, col] = 'X';
                    }
                }
            }
        }

        private static void ExplodeFive(char[,] field, Position2D minePosition)
        {
            int startRow = minePosition.X - 2;
            int endRow = minePosition.X + 2;
            for (int row = startRow; row <= endRow; row++)
            {
                int startCol = minePosition.Y - 2;
                int endCol = minePosition.Y + 2;
                for (int col = startCol; col <= endCol; col++)
                {
                    if (CheckField(field, row, col))
                    {
                        field[row, col] = 'X';
                    }
                }
            }
        }
        #endregion

        #region Extra actions
        /// <summary>
        /// Prints the field to the console.
        /// This is the method used to show player the gameboard.
        /// </summary>
        /// <param name="field">The field to show</param>
        public static void PrintResults(char[,] field)
        {
            Console.Write("   ");
            int size = field.GetLength(0);            
            for (int i = 0; i < size; i++)
            {
                Console.Write("{0} ", i);
            }

            Console.WriteLine();
            Console.Write("   ");            
            for (int i = 0; i < size * 2; i++)
            {
                Console.Write("-");
            }
            
            Console.WriteLine();
            for (int row = 0; row < size; row++)
            {
                Console.Write("{0} |", row);            
                for (int col = 0; col < size; col++)
                {
                    Console.Write("{0} ", field[row,col]);
                }
                
                Console.WriteLine();
            }
        }

        public static Position2D ExtractMineFromString(string inputString)
        {
            if (inputString == null ||
                inputString.Length < 3 ||
                !inputString.Contains(" "))
            {
                Console.WriteLine("Invalid index!");
                return null;
            }

            string[] splitted = inputString.Split(' ');
            int x = 0;
            int y = 0;
            if (!int.TryParse(splitted[0], out x))
            {
                Console.WriteLine("Invalid index!");
                return null;
            }

            if (!int.TryParse(splitted[1], out y))
            {
                Console.WriteLine("Invalid index!");
                return null;
            }

            return new Position2D(x, y);
        }
        #endregion
    }
}
